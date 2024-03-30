using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyUnitController : MonoBehaviour
{
    [SerializeField] private CharacterSObject characterSO;

    private UnitController characterController;

    public CharacterSObject CharacterBaseData
    {
        get { return characterSO; }
        set
        {
            characterSO = value;
        }
    }

    private void Awake()
    {
        if (characterSO == null)
        {
            Debug.LogError("Variable [characterSO] must to be set.");

            return;
        }

        characterController = GetComponentInChildren<UnitController>();
    }

    private void Start()
    {
        //characterController.CharacterSO = characterSO;
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
