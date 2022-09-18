using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState{menu,game,finish}
    public GameState gameState;

    public GameObject _bottomHeroPanel;
    public Vector3 _enabledBottomHeroPos,_disabledBottomHeroPos;

    


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void StartGame()
    {
        for(int i=0;i<EnemySpawnManager.Instance._enemyList.Count;i++)
        {
            EnemySpawnManager.Instance._enemyList[i].GetComponent<AIPathFinder>().SetEnemyTargetFirstSetup();
        }
        gameState = GameState.game;
        _bottomHeroPanel.transform.DOLocalMove(_disabledBottomHeroPos, .5f);
    }


}
