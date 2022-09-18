using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject _canvasUI;

    private void OnMouseEnter()
    {
        _canvasUI.SetActive(true);
    }
    private void OnMouseExit()
    {
        _canvasUI.SetActive(false);
    }
}
