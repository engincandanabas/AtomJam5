using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using DG.Tweening;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Enemy List")]
    public List<EnemyManager> _enemyList = new List<EnemyManager>();

    [Header("Enemy Prefab")]
    [NaughtyAttributes.HorizontalLine(height: 1, color: EColor.White)]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _coinPrefab;
    public Transform _mainMenuCoinTransform;
 
    [Header("UI Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Indigo)]
    [SerializeField] private GameObject[] _enemySprites;
    public Slider[] _enemyHealthSlider;
    public GameObject[] _enemyItems;
    public Sprite[] _enemyWeapons;

    [Header("Text Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Black)]
    [SerializeField] private TextMeshProUGUI[] _enemyNameTexts;
    public TextMeshProUGUI[] _enemyAttackTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyDefenseTexts;
    [SerializeField] private TextMeshProUGUI[] _enemyChanceTexts;

    [Header("Level Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Green)]
    [SerializeField] GameObject _enemySpawnPoint;

    [Header("First Enemy Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public Image[] _cuceSpritesFirstEnemy;
    public Image[] _hobbitSpritesFirstEnemy;
    public Image[] _elfSpritesFirstEnemy;
    public Image[] _insanSpritesFirstEnemy;
    public Image[] _siniflarSpritesFirstEnemy;
    public Image[] _silahlarSpritesFirstEnemy;

    [Header("Second Enemy Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public Image[] _cuceSpritesSecondEnemy;
    public Image[] _hobbitSpritesSecondEnemy;
    public Image[] _elfSpritesSecondEnemy;
    public Image[] _insanSpritesSecondEnemy;
    public Image[] _siniflarSpritesSecondEnemy;
    public Image[] _silahlarSpritesSecondEnemy;

    [Header("Third Enemy Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public Image[] _cuceSpritesThirdEnemy;
    public Image[] _hobbitSpritesThirdEnemy;
    public Image[] _elfSpritesThirdEnemy;
    public Image[] _insanSpritesThirdEnemy;
    public Image[] _siniflarSpritesThirdEnemy;
    public Image[] _silahlarSpritesThirdEnemy;

    

    [Header("Fourth Enemy Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public Image[] _cuceSpritesFourthEnemy;
    public Image[] _hobbitSpritesFourthEnemy;
    public Image[] _elfSpritesFourthEnemy;
    public Image[] _insanSpritesFourthEnemy;
    public Image[] _siniflarSpritesFourthEnemy;
    public Image[] _silahlarSpritesFourthEnemy;

   
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
            _enemyDefenseTexts[i].text= _enemy.GetComponent<EnemyManager>().defans.ToString();
            _enemyChanceTexts[i].text = _enemy.GetComponent<EnemyManager>().kacinma.ToString();

            _enemyList.Add(_enemy.GetComponent<EnemyManager>());
            _enemyHealthSlider[i].value = 100;
            _enemy.transform.position += new Vector3(i * 0.45f, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }

    }
 
}
