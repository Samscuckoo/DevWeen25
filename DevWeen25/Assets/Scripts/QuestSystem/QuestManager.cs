using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
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

    private void ClaimRewards(Quest quest){
        // Grant experience, items, etc. to the player
        Debug.Log($"Quest {quest.info.id} finished! Rewards granted.");
    }
    

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        //if (currentPlayerLevel < quest.info.levelRequirement)
        // meetsRequirements = false;

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if(GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
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
            idToQuestMap[questInfo.id] = new Quest(questInfo);
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

}
