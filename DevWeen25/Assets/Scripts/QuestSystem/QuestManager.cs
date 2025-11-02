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
        GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange()

        // Mantém o debug
        Debug.Log($"Quest {quest.info.id} finished! Prestige rewards granted.");
    }



     private bool CheckRequirementsMet(Quest quest)
{
    bool meetsRequirements = true;

    // Verifica se o jogador tem pontos de prestígio suficientes para cada personagem
    if (PlayerPrestigeManager.instance.GetPrestigePoints(CharacterType.Shelly) < quest.info.shellyPoints)
        meetsRequirements = false;
    if (PlayerPrestigeManager.instance.GetPrestigePoints(CharacterType.Viktor) < quest.info.viktorPoints)
        meetsRequirements = false;
    if (PlayerPrestigeManager.instance.GetPrestigePoints(CharacterType.Ankhesara) < quest.info.ankhesaraPoints)
        meetsRequirements = false;
    if (PlayerPrestigeManager.instance.GetPrestigePoints(CharacterType.Decalya) < quest.info.decalyaPoints)
        meetsRequirements = false;

    // Verifica pré-requisitos de outras quests 
    foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
    {
        if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            meetsRequirements = false;
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
