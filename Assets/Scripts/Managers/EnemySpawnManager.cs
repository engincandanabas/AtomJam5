using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Enemy Prefab")]
    [NaughtyAttributes.HorizontalLine(height: 1, color: EColor.White)]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("UI Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Indigo)]
    [SerializeField] private GameObject[] _enemySprites;
    [SerializeField] private Slider[] _enemyHealthSlider;
    [SerializeField] private GameObject[] _enemyItems;
    [SerializeField] private Sprite[] _enemyWeapons;

    [Header("Text Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Black)]
    [SerializeField] private TextMeshProUGUI[] _enemyNameTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyAttackTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyDefenseTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyChanceTexts;

    [Header("Level Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Green)]
    [SerializeField] GameObject _enemySpawnPoint;

    [Header("Enemy List")]
    [SerializeField] private List<EnemyManager> _enemyList=new List<EnemyManager>();
    private void Awake()
    {
        Instance = this;
        StartCoroutine(CreateEnemyGroup());
    }
    public IEnumerator CreateEnemyGroup()
    {
        for(int i=0;i<4;i++)
        {
            GameObject _enemy=Instantiate(_enemyPrefab,_enemySpawnPoint.transform.position,Quaternion.identity);
            //_enemySprites[i].GetComponent<Image>().sprite=
            switch(_enemy.GetComponent<EnemyManager>().silahName)
            {
                case "Bow-Arrow":
                    _enemyItems[i].GetComponent<Image>().sprite = _enemyWeapons[0];
                    _enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().uzak_etki.ToString();
                    break;
                case "Sword":
                    _enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().yakin_etki.ToString();
                    _enemyItems[i].GetComponent<Image>().sprite = _enemyWeapons[1];
                    break;
                case "Shield":
                    _enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().yakin_etki.ToString();
                    _enemyItems[i].GetComponent<Image>().sprite = _enemyWeapons[2];
                    break;
            }
            _enemyDefenseTexts[i].text= _enemy.GetComponent<EnemyManager>().defans.ToString();
            _enemyChanceTexts[i].text = _enemy.GetComponent<EnemyManager>().kacinma.ToString();

            _enemyList.Add(_enemy.GetComponent<EnemyManager>());
            yield return new WaitForSeconds(0.05f);
        }

    }

    
}
