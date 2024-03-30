using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour
{
    [SerializeField] private CharacterSObject characterSO;

    private UnitController characterController;

    public CharacterSObject CharacterSO { get { return characterSO; } }

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
