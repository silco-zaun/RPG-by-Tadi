using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public enum BattleUnitBehavior
    {
        Attack,
        Defence,
        SKill,
        Item,
        Party,
        Formation,
        Escape
    }

    private Character character;
    private bool isPlayerParty;
    private BattleUnitBehavior behavior;

    public Character Character { get { return character; } }
    public bool IsPlayerParty { get { return isPlayerParty; } set { isPlayerParty = value; } }
    public BattleUnitBehavior Behavior { get { return behavior; } set { behavior = value; } }

    private void Awake()
    {
        character = GetComponentInChildren<Character>();
    }

    public void AttackTarget(BattleUnit targetUnit)
    {
        targetUnit.character.TakeDamage(character.Level, character.Attack);
    }
}
