using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinder : MonoBehaviour
{
    private List<Room> rooms = new List<Room>();

    [SerializeField] private float speed;
    [SerializeField] private float radius;

    private Transform target;
    void Start()
    {
        rooms = PathManager.instance._rooms;
        SetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                SetTarget();
            }
        }
    }
    
    void SetTarget()
    {
        var _target = target;
        target = null;
        //check current target fight or pass
        if(_target != null && rooms.Count > 0)
        {
            if(_target.transform.parent.GetComponent<Room>().heroManagers.Count>0)
            {
                this.transform.parent = _target.transform.parent.GetComponent<Room>()._enemyLayoutGroup.transform;
                //saldiri baslayacak
                Debug.Log("Bu odada savas baslayacak");
            }
            else
            {
                // dusman yok yeni odaya git
                // first target
                target = rooms[rooms.Count - 1]._enemyLayoutGroup.transform;
                rooms.Remove(rooms[rooms.Count - 1]);
            }
        }
        else
        {
            // first target
            target=rooms[rooms.Count-1]._enemyLayoutGroup.transform;
            rooms.Remove(rooms[rooms.Count-1]);
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
