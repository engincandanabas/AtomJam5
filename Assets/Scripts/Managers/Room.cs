using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Room : MonoBehaviour
{
    [SerializeField] private int _maxCapacity=4;
    [SerializeField] private int _currentCapacity=0;
    public VerticalLayoutGroup _heroLayoutGroup;
    public VerticalLayoutGroup _enemyLayoutGroup;

    [SerializeField] private int _currentPassCount=0;
    public int MaxCapacity { get { return _maxCapacity; }}
    public int CurrentCapacity { get { return _currentCapacity; }set { _currentCapacity = value; } }
    public int CurrentPassCount { get { return _currentPassCount; }set { _currentPassCount = value; } }

    public GameObject _roomTarget;
    public List<HeroManager> heroManagers = new List<HeroManager>();
    public List<EnemyManager> enemyManagers = new List<EnemyManager>();

    public bool _enemyTurn = false;

    private void Awake()
    {
        StartCoroutine(WarControl());
    }
    IEnumerator WarControl()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if(heroManagers.Count > 0 && enemyManagers.Count>0)
            {
                StartCoroutine(SetAttack());
                break;
            }
        }
    }
    public IEnumerator SetAttack()
    {
        while (true)
        {
            if (_enemyTurn)
            {
               if(enemyManagers.Count != 0)
                {
                    // enemy turn
                    // hangi enemy saldiracak
                    var currentEnemy = enemyManagers[Random.Range(0, enemyManagers.Count)];
                    if (currentEnemy != null)
                    {
                        var targetHero = heroManagers[Random.Range(0, heroManagers.Count)];
                        currentEnemy._target = targetHero;
                        if (currentEnemy.Can < currentEnemy._maxEnemyHeatlh / 5 && currentEnemy.hasPotion)
                        {
                            // iyilesitrme

                            currentEnemy.Can = currentEnemy.Can + 25;
                            Debug.Log(currentEnemy.name + "iyile�tirme yapt� " + 25);
                            Debug.Log(currentEnemy.name + "mevcut can " + currentEnemy.Can);

                            currentEnemy.hasPotion = false;
                            // can+=
                        }
                        else
                        {
                            Debug.Log("sald�r�ya �ncesi");
                            currentEnemy.Attack();
                            Debug.Log("sald�r�ya girdi");
                            // attack
                        }
                    }
                }
            }
            else
            {
                if(heroManagers.Count!=0)
                {
                    // hero turn
                    // hangi hero saldiracak
                    var currentHero = heroManagers[Random.Range(0, heroManagers.Count)];
                    if (currentHero != null)
                    {
                        var targetEnemy = enemyManagers[Random.Range(0, enemyManagers.Count)];
                        if (targetEnemy != null)
                        {
                            currentHero._target = targetEnemy;
                            currentHero.Attack();
                        }
                    }
                }
                
            }
            yield return new WaitForSeconds(0.5f);
            _enemyTurn = !_enemyTurn;
            if(enemyManagers.Count==0)
            {
                break;
            }
            else if(heroManagers.Count==0)
            {
                break;
            }
        }
        // ikisinden biri sifir olmadan buraya gelmez
        if(heroManagers.Count!=0)
        {
            // savas bitti hero kazandi
            GameManager.instance.gameState = GameManager.GameState.finish;
            GameManager.instance.levelPassUI.SetActive(true);
        }
        if(enemyManagers.Count!=0)
        {
            Debug.Log("Enemy Kazandi.");
            // savas bitti enemy kazandi
            StartCoroutine(AfterEnemyVictory());

        }
    }
    IEnumerator AfterEnemyVictory()
    {
        for (int i = 0; i < enemyManagers.Count; i++)
        {
            enemyManagers[i]._target = null;
            //
            enemyManagers[i]._tempPos = enemyManagers[i].transform.position;
            // layouttan cikar
            enemyManagers[i].transform.parent = null;
            // listeden cikar
            enemyManagers[i].transform.position = enemyManagers[i]._tempPos;
            enemyManagers[i].transform.DOMove(_enemyLayoutGroup.transform.position + new Vector3(i * 0.45f, 0, 0),1f);
            // set parent
            enemyManagers[i].GetComponent<AIPathFinder>().SetTarget();
        }
        heroManagers.Clear();
        // diger odaya yonel
        yield return null;
    }


}
