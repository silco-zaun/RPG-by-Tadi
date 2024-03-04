using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleSetting : MonoBehaviour
{
    [SerializeField] private GameObject battleUnitPrefab;
    [SerializeField] private List<GameObject> LeftPartyFormation;
    [SerializeField] private List<GameObject> rightPartyFormation;

    private BattleUI battleUI;
    private PlayerUnit player;
    private Vector3 battleLocation;

    private List<GameObject> battleUnits = new List<GameObject>();
    private List<GameObject> leftPartyBattleUnits = new List<GameObject>();
    private List<GameObject> rightPartyBattleUnits = new List<GameObject>();
    private List<string> playerUnitNames = new List<string>();

    [SerializeField] private List<CharacterBaseData> leftPartyCharacters;
    [SerializeField] private List<CharacterBaseData> rightPartyCharacters;

    public List<GameObject> BattleUnits { get { return battleUnits; } }
    public List<GameObject> LeftPartyBattleUnits { get { return leftPartyBattleUnits; } }
    public List<GameObject> RightPartyBattleUnits { get { return rightPartyBattleUnits; } }

    private void Awake()
    {
        battleUI = GetComponent<BattleUI>();
        player = FindObjectOfType<PlayerUnit>();
    }

    private void Update()
    {
        HandleBattleLocation();
    }

    public void SetBattleUnit()
    {
        leftPartyBattleUnits.Clear();
        rightPartyBattleUnits.Clear();
        playerUnitNames.Clear();

        for (int i = 0; i < leftPartyCharacters.Count; i++)
        {
            if (leftPartyCharacters[i] == null)
                continue;

            Transform parent = LeftPartyFormation[i].transform;
            leftPartyBattleUnits.Add(Instantiate(battleUnitPrefab, parent));

            Unit unit = leftPartyBattleUnits[i].GetComponent<Unit>();
            BattleUnit battleUnit = leftPartyBattleUnits[i].GetComponent<BattleUnit>();

            unit.CharacterBaseData = leftPartyCharacters[i];
            unit.SetCharacterData(1);
            battleUnit.IsPlayerParty =  true;
            DataManager.CharacterTypeKor type = (DataManager.CharacterTypeKor)battleUnit.Character.Type;
            playerUnitNames.Add(type.ToString());
        }

        battleUI.SetBattleUnitMenu(playerUnitNames);

        for (int i = 0; i < rightPartyCharacters.Count; i++)
        {
            if (rightPartyCharacters[i] == null)
                continue;

            Transform parent = rightPartyFormation[i].transform;

            rightPartyBattleUnits.Add(Instantiate(battleUnitPrefab, parent));
            rightPartyBattleUnits[i].GetComponent<Unit>().CharacterBaseData = rightPartyCharacters[i];
            rightPartyBattleUnits[i].GetComponent<Unit>().SetCharacterData(1);
            rightPartyBattleUnits[i].GetComponent<BattleUnit>().IsPlayerParty = false;
            rightPartyBattleUnits[i].transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        battleUnits = leftPartyBattleUnits.Concat(rightPartyBattleUnits).OrderBy(u => u.GetComponent<BattleUnit>().Character.Speed).ToList();
    }

    private void HandleBattleLocation()
    {
        if (player != null)
        {
            battleLocation.Set(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = battleLocation;
        }
    }

}
