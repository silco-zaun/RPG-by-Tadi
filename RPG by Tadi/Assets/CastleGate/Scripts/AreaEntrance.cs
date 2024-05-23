using Tadi.Datas.Scene;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public SceneList targetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Managers.Ins.Scn.LoadScene(targetScene);
        }
    }
}
