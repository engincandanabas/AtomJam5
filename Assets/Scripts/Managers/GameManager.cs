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
    public bool _firstRoomTriggered=false;

    public GameObject _followTarget;
    public bool _isTargetSelected = false;

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
    private void Update()
    {
        if(_firstRoomTriggered)
        {
            if(_followTarget != null)
            {
                if (EnemySpawnManager.Instance._enemyList.Count > 0 && _followTarget.transform.parent == null)
                {
                    Debug.Log("Tetiklendi");
                    Camera.main.transform.position = _followTarget.transform.position + new Vector3(0, 0, -10f);
                }
            }
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
        Camera.main.DOOrthoSize(2,1f);
        Camera.main.transform.DOMove(PathManager.instance._rooms[PathManager.instance._rooms.Count-1].transform.position+new Vector3(0,0,-10f),1f);
    }


}
