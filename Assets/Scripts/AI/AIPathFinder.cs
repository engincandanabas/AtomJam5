using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinder : MonoBehaviour
{
    private List<Room> rooms = new List<Room>();

    [SerializeField] private float speed;
    [SerializeField] private float radius;

    private Vector3 target;
    void Start()
    {
        rooms = PathManager.instance._rooms;
        SetFirstTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            Vector2 dir = target - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target) <= 0.1f)
            {
                SetTarget();
            }
        }
    }
    void SetFirstTarget()
    {
        
    }
    void SetTarget()
    {
        
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
        Gizmos.DrawSphere(target, radius);
        }
    }
}
