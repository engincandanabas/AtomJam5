using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinder : MonoBehaviour
{
    private List<Room> rooms = new List<Room>();

    [SerializeField] private float speed;
    [SerializeField] private float radius;


    private Transform target;
    int _roomIndex=0;
    public void SetEnemyTargetFirstSetup()
    {
        rooms = PathManager.instance._rooms;
        _roomIndex = rooms.Count - 1;
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null && GameManager.instance.gameState==GameManager.GameState.game)
        {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                SetTarget();
            }
        }
    }
    
    public void SetTarget()
    {
        var _target = target;
        target = null;
        //check current target fight or pass
        if(_target != null && rooms.Count > 0)
        {
            if (_roomIndex == -1)
            { 
                Debug.LogWarning("SAVAS BÝTTÝ");
                GameManager.instance.gameState = GameManager.GameState.finish;
                GameManager.instance.levelFailedUI.SetActive(true);
                return;

            }
            if (_roomIndex==rooms.Count-2 && !GameManager.instance._isTargetSelected)
            {
                GameManager.instance._followTarget = this.gameObject;
                GameManager.instance._firstRoomTriggered = true;
                GameManager.instance._isTargetSelected = true;
            }
            if(_target.transform.parent.GetComponent<Room>().heroManagers.Count>0)
            {
                this.transform.parent = _target.transform.parent.GetComponent<Room>()._enemyLayoutGroup.transform;
                //saldiri baslayacak
                Debug.Log("Bu odada savas baslayacak");
                _target.transform.parent.GetComponent<Room>().enemyManagers.Add(this.transform.GetComponent<EnemyManager>());
            }
            else
            {
                // dusman yok yeni odaya git
                target = rooms[_roomIndex]._roomTarget.transform;
                _roomIndex--;
            }
        }
        else
        {
            // first target
            target=rooms[_roomIndex]._roomTarget.transform;
            _roomIndex--;
        }
    }
    Room GetRoom(Vector2 center, float radius)
    {
        Room room=null;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Room")
            {
                //do something
                room=hitColliders[i].GetComponent<Room>();
            }
            i++;
        }
        return room;
    }
    void OnDrawGizmos()
    {
        if(target != null)
        {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target.position, radius);
        }
    }
}
