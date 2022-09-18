using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCameraFollow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //GameManager.instance._firstRoomTriggered = true;
        }
    }
}
