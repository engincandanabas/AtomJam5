using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinder : MonoBehaviour
{
    [SerializeField]private List<Door> doors = new List<Door>();
    private List<Room> rooms = new List<Room>();

    [SerializeField] private float speed;
    [SerializeField] private float radius;

    private Vector3 target;
    void Start()
    {
        doors = PathManager.instance._doors;
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
        if(doors.Count>1)
        {
            var tempTarget = doors[0];
            for(int i= 1; i < doors.Count; i++)
            {
                if(Vector3.Distance(doors[i].transform.position,transform.position)<Vector3.Distance(tempTarget.transform.position,transform.position))
                {
                    // en yakin kapiyi bulma islemi
                    tempTarget = doors[i];
                }
            }
            // gecilme sayisini artir
            tempTarget.OpenCount++;
            // en kisa bulundu atama islemini yap
            target = tempTarget.transform.parent.position;
        }
    }
    void SetTarget()
    {
        Room _currentRoom= GetRoom(target, radius);
        target = Vector3.zero;
        // listeyi resetle
        doors = new List<Door>();

        // mevcut odanýn kapýlarýný bul
        for (int i=0;i<_currentRoom.transform.childCount;i++)
        {
            if(_currentRoom.transform.GetChild(i).CompareTag("Door"))
            {
                //mevcut odanin kapilarini listeye ata
                doors.Add(_currentRoom.transform.GetChild(i).GetComponent<Door>());
            }
        }
        // target belirle
        if(doors.Count>0)
        {
            var tempTarget=doors[0];
            // en az gecilme sayisina sahip olani bulma
            for(int i=0;i<doors.Count;i++)
            {
                if(doors[i].OpenCount< tempTarget.OpenCount)
                {
                    tempTarget = doors[i];
                }
            }
            // gecilme sayisini artir
            tempTarget.OpenCount++;
            // en az gecilme sayisina sahip kapi bulundu
            Room _selectedRoom=null;
            var rightRoom = GetRoom(tempTarget.transform.position + transform.right * 1.5f, radius);
            var leftRoom = GetRoom(tempTarget.transform.position + transform.right * -1.5f, radius);
            var upRoom = GetRoom(tempTarget.transform.position + transform.up * 1.5f, radius);
            var bottomRoom = GetRoom(tempTarget.transform.position + transform.up * -1.5f, radius);
            if (rightRoom != null && rightRoom != _currentRoom)
            {
                _selectedRoom= rightRoom;
                target = rightRoom.transform.position;
            }
            else if (leftRoom != null && leftRoom != _currentRoom)
            {
                _selectedRoom = leftRoom;
                target = leftRoom.transform.position;
            }
            else if (upRoom != null && upRoom != _currentRoom)
            {
                _selectedRoom = upRoom;
                target = upRoom.transform.position;
            }
            else if (bottomRoom != null && bottomRoom != _currentRoom)
            {
                _selectedRoom = bottomRoom;
                target = bottomRoom.transform.position;
            }
            if(target != null)
            {
                for(int i=0;i<_selectedRoom.transform.childCount;i++)
                {
                    if(_selectedRoom.transform.GetChild(i).tag=="Door"&& Vector3.Distance(target, _selectedRoom.transform.GetChild(i).transform.position)<0.3f)
                    {
                        Debug.Log("Çok yakýn bir kapý bulundu.");
                        _selectedRoom.transform.GetChild(i).GetComponent<Door>().OpenCount++;
                    }
                }
            }
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
        Gizmos.DrawSphere(target, radius);
        }
    }
}
