using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    public List<GameObject> popupList;


    public void Awake()
    {
        instance = this;
        StartCoroutine(ClearPopUp());
    }

    IEnumerator ClearPopUp()
    {
        while (true)
        {

            yield return new WaitForSeconds(2f);
            if (popupList.Count > 0)
            {
                GameObject popuplist = this.popupList[0].gameObject;
                Destroy(popuplist);
                this.popupList.RemoveAt(0);
            }
        }
    }

}
