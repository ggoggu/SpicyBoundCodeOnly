using UnityEngine;

public struct StatData
{
    public int maxHP;
    public int maxAD;
}

[System.Serializable]
public struct GameData
{
    public StatData playerStat;
    public bool[] clearStatge;
}