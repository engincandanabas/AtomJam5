using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public List<Room> _rooms;
    public Room _currentRoom;

    private void Awake()
    {
        instance = this;
        _rooms = new List<Room>();
        GameObject[] _roomTag = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < _roomTag.Length; i++)
        {
            _rooms.Add(_roomTag[i].GetComponent<Room>());
        }
    }
}
