using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleDataManager;

public class Formation : MonoBehaviour
{
    private BattleUnitParty battleUnitParty;
    private int formationIndex;
    private int hlineIndex;
    private int vlineIndex;

    public BattleUnitParty Party { get { return battleUnitParty; } set { battleUnitParty = value; } }
    public int Index { get {  return formationIndex; } set {  formationIndex = value; } }
    public int HlineIndex { get { return hlineIndex; } set {  hlineIndex = value; } }
    public int VlineIndex { get { return vlineIndex; } set {  vlineIndex = value; } }
}
