using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public List<Door> _doors;
    public List<Room> _rooms;
    public Room _currentRoom;

    private void Awake()
    {
        instance = this;

        _doors = new List<Door>();
        GameObject[] _doorTag = GameObject.FindGameObjectsWithTag("Door");
        for(int i=0;i< _doorTag.Length; i++)
        {
            _doors.Add(_doorTag[i].GetComponent<Door>());
            _doors[i].OpenCount = 0;
        }

        _rooms = new List<Room>();
        GameObject[] _roomTag = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < _roomTag.Length; i++)
        {
            _rooms.Add(_roomTag[i].GetComponent<Room>());
        }
    }
}
