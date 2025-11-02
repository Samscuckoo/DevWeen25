using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FalarComPeixeStep : QuestStep
{
    private bool hasSpokenToFish = false;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += HandleTalk;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= HandleTalk;
    }

    private void HandleTalk(string npcName)
    {
        if (npcName == "Peixe" && !hasSpokenToFish)
        {
            hasSpokenToFish = true;
            FinishQuestStep();
        }
        UpdateState();
    }

    private void UpdateState()
    {
        string state = hasSpokenToFish ? "TalkedToFish" : "NotTalkedToFish";
        string status = hasSpokenToFish ? "Completed" : "InProgress";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        if (state == "TalkedToFish")
        {
            hasSpokenToFish = true;
        }
        else
        {
            hasSpokenToFish = false;
        }
        UpdateState();
    }
}
