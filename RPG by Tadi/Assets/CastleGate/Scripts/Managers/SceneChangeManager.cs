using Tadi.Datas.Scene;
using Tadi.Interface.unit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public SceneList PrevScene { get; private set; } = SceneList.None;
    public SceneList CurScene { get; private set; } = SceneList.None;

    private void Awake()
    {
        Debug.Log("test");
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event when this script is enabled
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event when this script is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurScene = (SceneList)scene.buildIndex;

        Managers.Ins.Stat.Init();

        if (CurScene == SceneList.EsternDionisDungeon)
        {
            PlayerUnitController.Ins.Interact(IInteractable.InteractState.ShowDialog);
            Managers.Ins.Dlg.StartDialog(0, null);
        }
    }

    public void LoadScene(SceneList targetScene)
    {
        PrevScene = (SceneList)SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((int)targetScene, LoadSceneMode.Single);
    }

    public void InitScene(SceneList prevScene, Vector3 position)
    {
        if (prevScene == PrevScene)
        {
            PlayerUnitController.Ins.transform.position = position;
        }
    }
}
