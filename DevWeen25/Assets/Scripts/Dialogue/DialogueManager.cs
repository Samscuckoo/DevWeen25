using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextMeshProUGUI displayNameText;
    private Story story;
    private int currentChoiceIndex = -1;
    private bool dialoguePlaying = false;
    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";


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
      
        Debug.Log($"Handling {tags.Count} tags.");
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
                    Debug.Log($"Portrait changed to: {tagValue}");
                    break;
                default:
                    Debug.LogWarning($"Unhandled tag key: {tagKey}");
                    break;
            }
        }
    }

    private void UpdateChoiceSprites(List<Choice> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            Sprite choiceSprite = GetSpriteForChoice(choices[i]); // Obtém a sprite correspondente
            GameEventsManager.instance.dialogueEvents.PortraitChanged(choiceSprite); // Atualiza a imagem do retrato
        }
    }

    private Sprite GetSpriteForChoice(Choice choice)
    {
        // Aqui você deve implementar a lógica para retornar a sprite correta
        // Isso pode ser um dicionário ou uma estrutura que mapeia escolhas para sprites
        return null; // Substitua isso pela lógica real
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
