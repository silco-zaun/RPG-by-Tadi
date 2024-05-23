using Cinemachine;
using Tadi.Datas.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldCameraController : Singleton<FieldCameraController>
{
    private CinemachineConfiner2D confiner;
    private PolygonCollider2D bounding;
    private Vector2[] range;

    private new void Awake()
    {
        base.Awake();

        confiner = GetComponentInChildren<CinemachineConfiner2D>();
        bounding = GetComponentInChildren<PolygonCollider2D>();
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
        float x = 0f, y = 0f;

        // Check if the loaded scene is the scene you want to perform an action in
        if (scene.buildIndex == (int)SceneList.Village)
        {
            x = 30f; y = 20f;
        }
        else if (scene.buildIndex == (int)SceneList.EsternDionis)
        {
            x = 40f; y = 28f;
        }
        else if (scene.buildIndex == (int)SceneList.EsternDionisDungeon)
        {
            x = 22f; y = 32f;
        }

        range = new Vector2[]
        {
            new Vector2(-x, y), new Vector2(x, y),
            new Vector2(x, -y), new Vector2(-x, -y)
        };

        bounding.points = range;

        //confiner.m_BoundingShape2D = bounding;
        confiner.InvalidateCache();
    }
}
