using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    [SerializeField] private CharacterBaseData characterBaseData;

    private CharacterController character;

    public CharacterBaseData CharacterBaseData
    {
        get { return characterBaseData; }
        set
        {
            characterBaseData = value;
            character.BaseData = value;
        }
    }

    private void Awake()
    {
        character = GetComponentInChildren<CharacterController>();
    }

    private void Start()
    {
        //character.BaseData = characterBaseData;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            // Start Battle
        }
        */
    }

    // Temp method
    public void SetCharacterData(int level)
    {
        character.Level = level;
    }
}
