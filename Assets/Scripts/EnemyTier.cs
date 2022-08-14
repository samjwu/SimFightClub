using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Tier")]
public class EnemyTier : ScriptableObject
{
    public int tier;
    public List<EnemyData> enemyList;
}
