using UnityEngine;
using System;




public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override, // 2
}


[Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}