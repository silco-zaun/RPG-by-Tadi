using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Tadi.Datas.Unit;
using Tadi.Datas.Scene;
using Tadi.Interface.unit;

public class DialogManager : MonoBehaviour
{
    private GameObject dialogBox;
    private Text nameText;
    private Text dialogText;

    private Queue<DialogLine> lines;
    private int lettersPerSecond = 10;

    public System.Action OnTalkFinish;
    public System.Action OnStartScene;

    public GameObject DialogBox { get; set; }
    public Text NameText { get; set; }
    public Text DialogText { get; set; }

    private void Awake()
    {
        lines = new Queue<DialogLine>();
    }

    public void Init()
    {
        dialogBox = DialogSystemController.Ins.DialogBox;
        nameText = DialogSystemController.Ins.NameText;
        dialogText = DialogSystemController.Ins.DialogText;
    }

    public void StartDialog(int dialogIndex, System.Action OnTalkFinish)
    {
        Dialog dialog = DialogData.Dialogs[(Managers.Ins.Scn.CurScene, dialogIndex)];

        if (dialog == null)
            return;

        StartDialog(dialog, OnTalkFinish);
    }

    public void StartDialog(UnitType unitType, System.Action OnTalkFinish)
    {
        Dialog dialog = DialogData.UnitDialogs[(Managers.Ins.Scn.CurScene, unitType)];

        if (dialog == null)
            return;

        StartDialog(dialog, OnTalkFinish);
    }

    public void StartDialog(Dialog dialog, System.Action OnTalkFinish)
    {
        this.OnTalkFinish = OnTalkFinish;
        dialogBox.SetActive(true);

        lines.Clear();

        foreach (DialogLine dialogLine in dialog.DialogLines)
        {
            lines.Enqueue(dialogLine);
        }

        DisplayNextDialogLine();
    }

    public bool DisplayNextDialogLine()
    {
        if (lines.Count == 0)
        {
            EndDialog();
            
            return true;
        }

        DialogLine currentLine = lines.Dequeue();
        nameText.text = currentLine.Name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));

        return false;
    }

    public IEnumerator TypeSentence(DialogLine dialogLine)
    {
        dialogText.text = "";

        foreach (char letter in dialogLine.Line.ToCharArray())
        {
            dialogText.text += letter;
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

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        OnTalkFinish?.Invoke();
    }
}
