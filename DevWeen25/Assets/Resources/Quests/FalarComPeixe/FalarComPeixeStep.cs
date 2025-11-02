using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FalarComPeixeStep : QuestStep
{
    private bool hasSpokenToFish = false;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.OnEnterDialogue += HandleTalk;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.OnEnterDialogue -= HandleTalk;
    }

    private void HandleTalk(string npcName)
    {
        if (npcName == "Peixe" && !hasSpokenToFish)
        {
            hasSpokenToFish = true;
            FinishQuestStep();
        }
    }
}
