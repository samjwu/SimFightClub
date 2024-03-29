using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public List<EnemyTier> enemyTiers;

    [SerializeField]
    string _gameOverScene;

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
    double _playerDoubleHitCounter = 100;

    void Start()
    {
        List<EnemyData> enemies = enemyTiers[PlayerStatistics.tier].enemyList;
        _currentEnemy = enemies[Random.Range(0, enemies.Count)];
        _enemyName.text = _currentEnemy.enemyName;
        _enemyImage.texture = _currentEnemy.enemyPicture;

        _continueButton.gameObject.SetActive(false);

        _currentPlayerHp = PlayerStatistics.hp;
        _playerHp.text = $"HP: {_currentPlayerHp}";

        _currentEnemyHp = _currentEnemy.hp;
        _enemyHp.text = $"HP: {_currentEnemyHp}";

        _turnNumber = 0;

        InvokeRepeating(nameof(CalculateFightTurn), 1.0f, 2.0f);
    }

    void CalculateFightTurn()
    {
        if (_currentEnemyHp <= 0)
        {
            _explanation.text = "You won the fight!\n You have gained 5 additional stat points for the next fight.";
            PlayerStatistics.statPoints = 5;

            PlayerStatistics.tier += 1;
            if (PlayerStatistics.tier >= 3)
            {
                _explanation.text += " You win!";
                _continueButton.nextSceneName = _gameOverScene;
            }

            _continueButton.gameObject.SetActive(true);
            CancelInvoke();
            return;
        }

        if (_currentPlayerHp <= 0)
        {
            _explanation.text = "You lost the fight! You lose!\nTry again. You have gained one additional stat point on your new start.";
            PlayerStatistics.lossCount += 1;

            _continueButton.nextSceneName = _gameOverScene;

            _continueButton.gameObject.SetActive(true);
            CancelInvoke();
            return;
        }

        _turnNumber++;

        string playerTurn = CalculateFighterTurn(_currentEnemy.dodgeChance,
                PlayerStatistics.criticalStrikeChance, PlayerStatistics.damage, PlayerStatistics.criticalStrikeDamage,
                _currentEnemy.defense, ref _currentEnemyHp, _enemyHp, "Player", _enemyName.text);
        string playerTurn2 = "";
        _playerDoubleHitCounter += 100 - PlayerStatistics.attackSpeed;
        if (_playerDoubleHitCounter <= 0)
        {
            _playerDoubleHitCounter += 100;
            playerTurn2 = "Second attack! " + CalculateFighterTurn(_currentEnemy.dodgeChance,
                PlayerStatistics.criticalStrikeChance, PlayerStatistics.damage, PlayerStatistics.criticalStrikeDamage,
                _currentEnemy.defense, ref _currentEnemyHp, _enemyHp, "Player", _enemyName.text);
        }

        string enemyTurn = CalculateFighterTurn(PlayerStatistics.dodgeChance,
                0.1, _currentEnemy.damage, _currentEnemy.criticalStrikeDamage,
                PlayerStatistics.defense, ref _currentPlayerHp, _playerHp, _enemyName.text, "Player");

        _explanation.text = $"Turn {_turnNumber}" + '\n' + playerTurn + '\n' + enemyTurn + '\n' + playerTurn2;
    }

    string CalculateFighterTurn(double opposingDodgeChance, double criticalStrikeChance, double damage, double criticalStrikeDamage,
        double opposingDefense, ref double opposingHp, TextMeshProUGUI opposingText, string fighterName, string opposingName)
    {
        double hitChance = 100 - opposingDodgeChance;
        bool isHitAccurate = hitChance >= Random.Range(0, 101);
        bool isHitCritical = criticalStrikeChance >= Random.Range(0, 101);
        double damageDealt = Mathf.Max((float)(damage * (isHitCritical ? criticalStrikeDamage / 100 : 1) - opposingDefense), 1f);

        string explanation;
        if (isHitAccurate)
        {
            opposingHp = Mathf.Max(0, (float)(opposingHp - damageDealt));
            opposingText.text = $"HP: {opposingHp}";
            explanation = (isHitCritical ? "Critical strike! " : "") + $"{fighterName} dealt {damageDealt} damage to {opposingName}!";
        }
        else
        {
            explanation = $"{fighterName}'s attack missed!";
        }
        return explanation;
    }
}
