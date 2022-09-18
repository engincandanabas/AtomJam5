using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameStartScene3 : MonoBehaviour
{
    public GameObject LeftHand1;
    public GameObject LeftHand2;
    public GameObject RightHand1;
    public GameObject RightHand2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scene3());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Scene3()
    {
        yield return new WaitForSeconds(1f);
        LeftHand1.transform.DOMove(new Vector3(-6.25f, -2.47f, 9.32f), 1).OnComplete(() =>
        {

            LeftHand1.gameObject.SetActive(false);
            LeftHand2.gameObject.SetActive(true);

        });
        LeftHand1.transform.DORotate(new Vector3(0, 0, 6.63f), 1);
        yield return new WaitForSeconds(3f);
        RightHand1.gameObject.SetActive(false);
        RightHand2.gameObject.SetActive(true);

    }
}
