using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void StartQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(questId, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(questId, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string questId)
    {
        Quest quest = GetQuestById(questId);
        ClaimRewards(quest);
        ChangeQuestState(questId, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        // Debug.Log($" {quest.info.prestigePointsShelly}");
        // Concede pontos de prestígio para cada personagem
        if (quest.info.prestigePointsShelly > 0)
            GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Shelly, quest.info.prestigePointsShelly);
        if (quest.info.prestigePointsViktor > 0)
            GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Viktor, quest.info.prestigePointsViktor);
        if (quest.info.prestigePointsAnkhesara > 0)
            GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Ankhesara, quest.info.prestigePointsAnkhesara);
        if (quest.info.prestiegePointsDecalya > 0)
            GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Decalya, quest.info.prestiegePointsDecalya);

        Debug.Log($"Quest {quest.info.id} finished! Prestige rewards granted.");
    }



     private Dictionary<CharacterType, int> prestigePoints = new Dictionary<CharacterType, int>();

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        // Verifica se o jogador tem pontos de prestígio suficientes para cada personagem
        foreach (var character in System.Enum.GetValues(typeof(CharacterType)))
        {
            CharacterType characterType = (CharacterType)character;
            int requiredPoints = 0;

            // Mapeia o tipo de personagem para os pontos requeridos
            switch (characterType)
            {
                case CharacterType.Shelly:
                    requiredPoints = quest.info.shellyPoints;
                    break;
                case CharacterType.Viktor:
                    requiredPoints = quest.info.viktorPoints;
                    break;
                case CharacterType.Ankhesara:
                    requiredPoints = quest.info.ankhesaraPoints;
                    break;
                case CharacterType.Decalya:
                    requiredPoints = quest.info.decalyaPoints;
                    break;
            }

            // Se há requisito de pontos e não temos pontos suficientes
            if (requiredPoints > 0 && (!prestigePoints.ContainsKey(characterType) || prestigePoints[characterType] < requiredPoints))
            {
                meetsRequirements = false;
                break;
            }
        }

        // Verifica pré-requisitos de outras quests 
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
                break;
            }
        }

        return meetsRequirements;
    }


    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string questId, QuestState newState)
    {
        Quest quest = GetQuestById(questId);
        quest.state = newState;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }


    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogError($"Duplicate quest ID found: {questInfo.id}");
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string questId)
    {
        Quest quest = questMap[questId];
        if (quest == null)
        {
            Debug.LogError($"Quest with ID {questId} not found.");
        }
        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);

        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving quest {quest.info.id}: {e.Message}");
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading quest {questInfo.id}: {e.Message}");
        }
        return quest;
    }
}
