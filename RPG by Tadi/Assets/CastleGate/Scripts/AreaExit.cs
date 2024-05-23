using Tadi.Datas.Scene;
using UnityEngine;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private SceneList prevScene;

    private void Start()
    {
        Managers.Ins.Scn.InitScene(prevScene, transform.position);
    }
}
