using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameStartScene2 : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera startCam;
    public GameObject Goblin1;
    public GameObject Goblin2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene2());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Scene2()
    {
        yield return new WaitForSeconds(1f);
        startCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        Goblin1.gameObject.SetActive(false);
        Goblin2.gameObject.SetActive(true);
    }
}
