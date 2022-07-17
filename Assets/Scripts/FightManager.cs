using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public List<EnemyData> enemyList;

    [SerializeField]
    TextMeshProUGUI _enemyName;
    [SerializeField]
    RawImage _enemyImage;
    [SerializeField]
    TextMeshProUGUI _explanation;

    EnemyData _currentEnemy;

    double _currentPlayerHp;
    double _currentEnemyHp;

    void Start()
    {
        _currentEnemy = enemyList[Random.Range(0, enemyList.Count)];
        _enemyName.text = _currentEnemy.enemyName;
        _enemyImage.texture = _currentEnemy.enemyPicture;

        _currentPlayerHp = PlayerStatistics.hp;
        _currentEnemyHp = _currentEnemy.hp;
    }

    void Update()
    {
        double playerHitChance = 100 - _currentEnemy.dodgeChance;
        double enemyHitChance = 100 - PlayerStatistics.dodgeChance;

        bool isPlayerAccurate = playerHitChance >= Random.Range(0, 101);
        bool isEnemyAccurate = enemyHitChance >= Random.Range(0, 101);

        bool isPlayerHitCritical = 0.1 >= Random.Range(0, 101);
        bool isEnemyHitCritical = 0.1 >= Random.Range(0, 101);

        double playerDamageDealt = PlayerStatistics.damage * (isPlayerHitCritical ? PlayerStatistics.criticalStrikeDamage : 1) - _currentEnemy.defense;
        double enemyDamageDealt = _currentEnemy.damage * (isEnemyHitCritical ? _currentEnemy.criticalStrikeDamage / 100 : 1) - PlayerStatistics.defense;

        string playerExplanation;
        string enemyExplanation;
        if (isPlayerAccurate)
        {
            _currentEnemyHp -= playerDamageDealt;
        }
        else
        {

        }

        if (isEnemyAccurate)
        {
            _currentPlayerHp -= enemyDamageDealt;
        }
        else
        {

        }
    }
}
