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
    }
}
