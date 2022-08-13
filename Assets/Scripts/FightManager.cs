using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public List<EnemyData> enemyList;

    [SerializeField]
    TextMeshProUGUI _playerHp;
    [SerializeField]
    TextMeshProUGUI _enemyName;
    [SerializeField]
    RawImage _enemyImage;
    [SerializeField]
    TextMeshProUGUI _enemyHp;
    [SerializeField]
    ContinueButton _continueButton;
    [SerializeField]
    TextMeshProUGUI _explanation;

    EnemyData _currentEnemy;

    double _currentPlayerHp;
    double _currentEnemyHp;
    int _turnNumber;

    void Start()
    {
        _currentEnemy = enemyList[Random.Range(0, enemyList.Count)];
        _enemyName.text = _currentEnemy.enemyName;
        _enemyImage.texture = _currentEnemy.enemyPicture;

        _continueButton.gameObject.SetActive(false);

        _currentPlayerHp = PlayerStatistics.hp;
        _currentEnemyHp = _currentEnemy.hp;
        _turnNumber = 0;

        InvokeRepeating(nameof(CalculateFightTurn), 1.0f, 1.0f);
    }

    void CalculateFightTurn()
    {
        if (_currentEnemyHp <= 0)
        {
            _explanation.text = "You won the fight!";
            _continueButton.gameObject.SetActive(true);
            CancelInvoke();
            return;
        }

        if (_currentPlayerHp <= 0)
        {
            _explanation.text = "You lost the fight!";
            _continueButton.gameObject.SetActive(true);
            CancelInvoke();
            return;
        }

        double playerHitChance = 100 - _currentEnemy.dodgeChance;
        double enemyHitChance = 100 - PlayerStatistics.dodgeChance;

        bool isPlayerAccurate = playerHitChance >= Random.Range(0, 101);
        bool isEnemyAccurate = enemyHitChance >= Random.Range(0, 101);

        bool isPlayerHitCritical = 0.1 >= Random.Range(0, 101);
        bool isEnemyHitCritical = 0.1 >= Random.Range(0, 101);

        double playerDamageDealt = PlayerStatistics.damage * (isPlayerHitCritical ? PlayerStatistics.criticalStrikeDamage : 1) - _currentEnemy.defense;
        double enemyDamageDealt = _currentEnemy.damage * (isEnemyHitCritical ? _currentEnemy.criticalStrikeDamage / 100 : 1) - PlayerStatistics.defense;

        _turnNumber++;
        string turnExplanation = $"Turn {_turnNumber}";
        string playerExplanation;
        string enemyExplanation;

        if (isPlayerAccurate)
        {
            _currentEnemyHp = Mathf.Max(0, (float)(_currentEnemyHp - playerDamageDealt));
            _enemyHp.text = $"HP: {_currentEnemyHp}";
            playerExplanation = $"Player dealt {playerDamageDealt} damage to {_enemyName.text}!";
        }
        else
        {
            playerExplanation = "Player's attack missed!";
        }

        if (isEnemyAccurate)
        {
            _currentPlayerHp = Mathf.Max(0, (float)(_currentPlayerHp - enemyDamageDealt));
            _playerHp.text = $"HP: {_currentPlayerHp}";
            enemyExplanation = $"{_enemyName.text} dealt {enemyDamageDealt} damage to Player!";
        }
        else
        {
            enemyExplanation = $"{_enemyName.text}'s attack missed!";
        }

        _explanation.text = turnExplanation + '\n' + playerExplanation + '\n' + enemyExplanation;
    }
}
