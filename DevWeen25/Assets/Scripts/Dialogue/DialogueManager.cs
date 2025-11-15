using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] Animator portraitAnimator;

    private Story story;
    private int currentChoiceIndex = -1;
    public bool dialoguePlaying = false;
    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string REWARD_SHELLY_TAG = "rewardShelly";

    private const string REWARD_VIKTOR_TAG = "rewardViktor";

    private const string REWARD_DECAYLA_TAG = "rewardDecayla";

    private const string REWARD_ANKHESARA_TAG = "rewardAnkhesara";


    private void Awake()
    {
        story = new Story(inkJSON.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
        inkDialogueVariables = new InkDialogueVariables(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }   

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.inputEvents.onInteractPressed += InteractPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable += UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange; }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.instance.inputEvents.onInteractPressed -= InteractPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkDialogueVariable -= UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable(
            quest.info.id + "State",
            new StringValue(quest.state.ToString()));
    }

    private void UpdateInkDialogueVariable(string variableName, Ink.Runtime.Object value)
    {
        inkDialogueVariables.UpdateVariableState(variableName, value);
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
        this.currentChoiceIndex = choiceIndex;
    }

    private void InteractPressed(InputEventContext inputEventContext)
    {
        if (!inputEventContext.Equals(InputEventContext.DIALOGUE))
        {
            return;
        }

        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying)
        {
            Debug.LogWarning("Dialogue is already playing!");
            return;
        }
        dialoguePlaying = true;

        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        GameEventsManager.instance.playerEvents.DisablePlayerMovement();

        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("No knot name provided for dialogue.");
        }

        inkDialogueVariables.SyncVariablesAndStartListening(story);

        displayNameText.text = "???";
        portraitAnimator.Play("Default");


        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string text = story.Continue();

            while (IsLineBlank(text) && story.canContinue)
            {
                text = story.Continue();
            }

            if (IsLineBlank(text) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(text, story.currentChoices);
                HandleTags(story.currentTags);
           
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> tags)
    {

        // Debug.Log($"Handling {tags.Count} tags.");
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning($"Invalid tag format: {tag}");
                continue;
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case REWARD_SHELLY_TAG:
                    GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Shelly, ParseTagInt(tagValue));
                    break;
                case REWARD_VIKTOR_TAG:
                    GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Viktor, ParseTagInt(tagValue));
                    break;
                case REWARD_DECAYLA_TAG:
                    GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Decalya, ParseTagInt(tagValue));
                    break;
                case REWARD_ANKHESARA_TAG:
                    GameEventsManager.instance.playerEvents.PlayerPrestigePointsChange(CharacterType.Ankhesara, ParseTagInt(tagValue));
                    break;
                default:
                    Debug.LogWarning($"Unhandled tag key: {tagKey}");
                    break;
            }
        }
    }
    
    private int ParseTagInt(string tagValue, int defaultValue = 0)
    {
        if (int.TryParse(tagValue, out int result))
            return result;

        Debug.LogWarning($"Failed to parse tag numeric value '{tagValue}'. Using default {defaultValue}.");
        return defaultValue;
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;

        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        GameEventsManager.instance.playerEvents.EnablePlayerMovement();

        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.DEFAULT);

        inkDialogueVariables.StopListening(story);

        story.ResetState();
    }

    private bool IsLineBlank(string line)
    {
        return line.Trim().Equals("") || line.Trim().Equals("\n");
    }
    
}
