using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class AreaScript : MonoBehaviour
{
    public Sprite[] sprites;
    private GameObject createdArea;
    public GameObject IntantaneArea;
    public int AreaIndex = 0;
    public GameObject[] DoorList;
    bool onBlock = false;
    bool targetRoom;
    bool Active;
    int soundIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        Create2DRay();
    }


    // Update is called once per frame
    void Update()
    {
        if (Active)
        {

            Create2DRay();

        }

    }

    private void Create2DRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Room"))
            {
                onBlock = true;
            }

            if (hit.collider.CompareTag("TargetRoom"))
            {
                targetRoom = true;
                
            }
        }
    }

    public void AreaCreating()
    {

        if (!onBlock)
        {
            soundIndex = Random.Range(0, 3);
            SceneManager_.Instance.sources[soundIndex].Play();

            createdArea = Instantiate(IntantaneArea);

            createdArea.transform.parent = transform;
            createdArea.transform.localPosition = new Vector3(0, 0, 0);
            SceneManager_.Instance.particle.transform.position = createdArea.transform.position;
            SceneManager_.Instance.particle.Play();
            // createdArea.GetComponent<SpriteRenderer>().sprite = sprites[AreaIndex];
            Image[] img = createdArea.transform.GetChild(0).GetComponentsInChildren<Image>();
            foreach (Image item in img)
            {
                item.color = new Color(1, 1, 1, 0);
            }
            //createdArea.transform.localScale = new Vector3(0, 0, 0);
            createdArea.transform.parent = null;
            GetComponent<Image>().enabled = false;
            //createdArea.transform.GetChild(0).gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);



            //////////////////////////////////////////////
            if (targetRoom)
            {
                //Hedef Odaya Ulaþýldý ve Oda Yaratýldý
                Debug.Log("Karakterleri Yerleþtir");
                createdArea.transform.GetChild(0).gameObject.SetActive(false);
                SceneManager_.Instance.particle.transform.position = createdArea.transform.position;
                SceneManager_.Instance.particle.Play();
                targetRoom = false;
                //
                GameManager.instance._bottomHeroPanel.transform.DOLocalMove(GameManager.instance._enabledBottomHeroPos, 1f);
                
            }

            if (this.CompareTag("Up"))
            {
                DoorList[0].gameObject.transform.DOScale(new Vector2(.3f, .3f), .3f);
            }
            if (this.CompareTag("Left"))
            {
                DoorList[1].gameObject.transform.DOScale(new Vector2(.3f, .3f), .3f);
            }
            if (this.CompareTag("Down"))
            {
                DoorList[2].gameObject.transform.DOScale(new Vector2(.3f, .3f), .3f);
            }
            if (this.CompareTag("Right"))
            {
                DoorList[3].gameObject.transform.DOScale(new Vector2(.3f, .3f), .3f);
            }

            PathManager.instance._rooms.Add(createdArea.GetComponent<Room>());
        }
    }
    public void OpenArrow()
    {
        Active = true;

        if (onBlock)
        {

            GetComponent<Image>().color = new Color(1f, 0.1847454f, 0.1847454f, .5f);
            transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().color = new Color(0.4153922f, 0.8396226f, 0.3833748f, .5f);
            transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

    }
    public void CloseArrow()
    {
        Active = false;
        transform.GetChild(0).GetComponent<Image>().enabled = false;
        GetComponent<Image>().color = new Color(1, 1, 1, 0);

    }
    //public void ScrollChangeArea()
    //{
    //    if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
    //    {


    //        if (onArrow)
    //        {
    //            AreaIndex++;
    //            GetComponent<Image>().sprite = sprites[AreaIndex];

    //        }

    //    }
    //    if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
    //    {

    //        if (onArrow)
    //        {
    //            AreaIndex--;
    //            GetComponent<Image>().sprite = sprites[AreaIndex];

    //        }

    //    }

    //}

}
