using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Image characterIcon;
    public Text characterName;
    public Text dialogueText;

    private Queue<DialogueLine> lines;
    public bool isDialogueActive = false;
    public int lettersPerSecond = 10;

    //public Animator animator;

    private void Awake()
    {
        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialogueBox?.SetActive(true);
        //animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        if (currentLine.character != null)
        {
            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;
        }

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    public IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueText.text = "";

        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public IEnumerator TypeSentence(Text dialogueText, string line)
    {
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void DisplaySentence(Text dialogueText, string line)
    {
        dialogueText.text = line;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox?.SetActive(false);
        //animator.Play("hide");
    }
}
