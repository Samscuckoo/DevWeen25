using UnityEngine;

public class GameEventsManager : MonoBehaviour
{

    public static GameEventsManager instance { get; private set; }

    public InputEvents inputEvents;
    public MiscEvents miscEvents;
    public PlayerEvents playerEvents;
    public DialogueEvents dialogueEvents;
    public QuestEvents questEvents;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        inputEvents = new InputEvents();
        miscEvents = new MiscEvents();
        playerEvents = new PlayerEvents();
        dialogueEvents = new DialogueEvents();
        questEvents = new QuestEvents();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
