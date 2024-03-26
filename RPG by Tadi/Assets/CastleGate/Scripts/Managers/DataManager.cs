using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = null;
    private BattleDataManager battleDataManager = null;
    private CharacterDataManager characterDataManager = null;

    public static DataManager Ins
    {
        get
        {
            if (instance == null)
                instance = new DataManager();

            return instance;
        }
    }
    public BattleDataManager Bat
    {
        get
        {
            if (battleDataManager == null)
                battleDataManager = new BattleDataManager();

            return battleDataManager;
        }
    }
    public CharacterDataManager Cha
    {
        get
        {
            if (characterDataManager == null)
                characterDataManager = new CharacterDataManager();

            return characterDataManager;
        }
    }

    private DataManager() { }
}
