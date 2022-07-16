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

    void Start()
    {
        _currentEnemy = enemyList[Random.Range(0, enemyList.Count)];
        _enemyName.text = _currentEnemy.enemyName;
        _enemyImage.texture = _currentEnemy.enemyPicture;
    }
}
