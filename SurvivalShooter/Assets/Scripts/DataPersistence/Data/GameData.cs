using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int points;
    public List<Gun> guns;

    public bool dashPurchased;
    public bool dashUpgraded;
    public bool shieldPurchased;
    public bool ShieldUpgraded;

    public GameData()
    {
        points = 0;
        dashPurchased = false;
        dashUpgraded = false;
        shieldPurchased = false;
        ShieldUpgraded = false;

    }



}
