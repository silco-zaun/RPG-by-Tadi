using UnityEngine;
using UnityEngine.UI;

public class DialogSystemController : Singleton<DialogSystemController>
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;

    public GameObject DialogBox { get { return dialogBox; } }
    public Text NameText { get { return nameText; } }
    public Text DialogText { get { return dialogText; } }

    private void Start()
    {
        //InitUI();
    }

    public void InitUI()
    {
        Managers.Ins.Dlg.DialogBox = dialogBox;
        Managers.Ins.Dlg.NameText = nameText;
        Managers.Ins.Dlg.DialogText = dialogText;
    }
}
