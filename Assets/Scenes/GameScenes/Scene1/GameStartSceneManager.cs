using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class GameStartSceneManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera startCam;
    public GameObject Dwarf;
    public GameObject Paper;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene1());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Scene1()
    {
        yield return new WaitForSeconds(1f);
        startCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Dwarf.transform.DOMoveY(-5.5f, .6f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Dwarf.transform.DOMoveY(-7f, .6f).SetEase(Ease.Linear);
        });
        yield return new WaitForSeconds(1.5f);
        Debug.Log("xxxx");
        Dwarf.transform.DOMoveY(-5.5f, 1).OnComplete(() =>
        {
            Dwarf.transform.DOMoveY(-7f, 1);
        });
        yield return new WaitForSeconds(1.5f);

        Paper.transform.DOMoveY(-7f, 1);
    }
}
