using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;
    private bool isDialoguePlaying;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!isDialoguePlaying) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void EndDialogue()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            EndDialogue();
        }
    }
}
