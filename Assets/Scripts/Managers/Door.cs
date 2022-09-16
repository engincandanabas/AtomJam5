using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int _openCount;
    public int OpenCount { get { return _openCount; } set { _openCount = value; } }
}
