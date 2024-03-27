using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : MonoBehaviour
{
    [SerializeField] private CharacterSO characterBaseData;

    private CharacterController character;

    public CharacterSO CharacterBaseData
    {
        get { return characterBaseData; }
        set
        {
            characterBaseData = value;
            character.CharacterSO = value;
        }
    }

    private void Awake()
    {
        character = GetComponentInChildren<CharacterController>();
    }

    private void Start()
    {
        //characterController.CharacterSO = characterBaseData;
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
