using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{

    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.questEvents.onInteractPressed += InteractPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.questEvents.onInteractPressed -= InteractPressed;
    }

    private void InteractPressed()
    {
        if (!playerIsNear)
        {
            return;
        }
        GameEventsManager.instance.questEvents.StartQuest(questId);
        GameEventsManager.instance.questEvents.AdvanceQuest(questId);
        GameEventsManager.instance.questEvents.FinishQuest(questId);

    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

}
