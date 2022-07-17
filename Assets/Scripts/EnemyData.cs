using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Texture enemyPicture;

    public double hp;
    public double damage;
    public double dodgeChance;
    public double attackSpeed;
    public double defense;
    public double criticalStrikeDamage;
}
