using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    [SerializeField] string dialogueKnotName = "Intro1";
    [SerializeField] string completionDialogueKnot = "Outro1";
    [SerializeField] string gatekeeperQuestId = "intro_quest";
    
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // initialize all events
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        dialogueEvents = new DialogueEvents();
    }

    private void Start()
    {
        dialogueEvents.EnterDialogue(dialogueKnotName);
    }

    // private void OnEnable()
    // {
    //     questEvents.onFinishQuest += OnQuestFinished;
    // }

    // private void OnDisable()
    // {
    //     questEvents.onFinishQuest -= OnQuestFinished;
    // }

    private void OnQuestFinished(string questId)
    {
        if (questId == gatekeeperQuestId)
        {
            Debug.Log("Gatekeeper quest finished! Playing completion dialogue.");
            dialogueEvents.EnterDialogue(completionDialogueKnot);
        }
        else if (questId == completionDialogueKnot)
        {
            
        }
    }
}