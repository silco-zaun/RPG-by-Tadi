using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitController : MonoBehaviour
{
    [SerializeField] private GameObject selector;

    private BattleUnitMovement battleUnitMovement;
    private CharacterController character;
    private bool isPlayerParty;
    private BattleDataManager.BattleUnitBehavior behavior;
    List<BattleUnitController> targetUnitControllers = new List<BattleUnitController>();

    public CharacterController Character { get { return character; } }
    public bool IsPlayerParty
    {
        get { return isPlayerParty; }
        set
        {
            isPlayerParty = value;
            bool isFacingLeft = !isPlayerParty;
            battleUnitMovement.RotateCharacter(isFacingLeft);
        }
    }
    public BattleDataManager.BattleUnitBehavior Behavior { get { return behavior; } set { behavior = value; } }
    public bool IsDefeated { get { return character.CurHP < 0; } }

    private void Awake()
    {
        battleUnitMovement = GetComponent<BattleUnitMovement>();
        character = GetComponentInChildren<CharacterController>();
    }

    public void DoBehavior(System.Action OnBehaviorComplete)
    {
        if (behavior == BattleDataManager.BattleUnitBehavior.Attack)
        {
            AttackTarget(OnBehaviorComplete);
        }
    }

    public void AttackTarget(System.Action OnBehaviorComplete)
    {
        foreach (BattleUnitController target in targetUnitControllers)
        {
            battleUnitMovement.Attack(target.transform.position,
                () =>
                {
                    target.character.TakeDamage(character.Level, character.Attack);
                },
                OnBehaviorComplete);
        }
    }

    public void SetSelector(bool activating)
    {
        selector.SetActive(activating);
    }

    public void AddTarget(BattleUnitController targetUnitController)
    {
        targetUnitControllers.Add(targetUnitController);
    }

    public void ClearTarget()
    {
        targetUnitControllers.Clear();
    }

}
