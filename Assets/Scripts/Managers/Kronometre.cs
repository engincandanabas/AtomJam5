using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Kronometre : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(KronometreyiBaslat());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    IEnumerator KronometreyiBaslat()
    {
        int sayac = 0;
        while(true)
        {
            yield return new WaitForSeconds(1);
            sayac++;
            if(sayac==19)
            {
                break;
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
