using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = null;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DataManager();

            return instance;
        }
    }

    private DataManager() { }

    public enum CharacterType
    {
        Knight,
        Orc
    }

    public enum CharacterTypeKor
    {
        기사,
        오크
    }
}
