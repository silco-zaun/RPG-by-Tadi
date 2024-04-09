
using Tadi.Datas.Unit;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour
{
    private UnitController unitController;

    private void Awake()
    {
        unitController = GetComponentInChildren<UnitController>();
    }

    private void Start()
    {
        unitController.Init(UnitType.Knight, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            // Start Bat
        }
        */
    }
}
