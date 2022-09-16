using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroesPlaceManager : MonoBehaviour
{
    [Header("UI Attributes")]
    [SerializeField] private HorizontalLayoutGroup _heroesPlaceLayout;
    [SerializeField] private GameObject[] _heroesBoxes; // 0-mouse 1-dog 2-goblin 3-ogre 4-ork 5-gnoll 
    [SerializeField] private TextMeshProUGUI[] _heroesTexts; // 0-mouse 1-dog 2-goblin 3-ogre 4-ork 5-gnoll 


    [SerializeField] private int[] _heroesCounts; // 0-mouse 1-dog 2-goblin 3-ogre 4-ork 5-gnoll 
    
    private GameObject _preview;

    void Awake() {
        InitializeVariables();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(GameManager.instance.gameState==GameManager.GameState.menu)
        {
            // eger oyun baslamamissa
            if(_preview!=null)
            {
                // Olusturulan heronun mouse'u takip etmesi

                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                _preview.transform.position = new Vector3(mousePos.x,mousePos.y,0);

                // Odaya yerlestirme islemi

                RaycastHit2D hit = Physics2D.Raycast(_preview.transform.position, _preview.transform.forward, 10f);
                Debug.DrawLine(_preview.transform.position, hit.point);
                if (Input.GetMouseButtonDown(0))
                {
                    if(hit.collider!=null)
                    {
                        if(hit.collider.CompareTag("Room"))
                        {
                            // odaya yerlestirme islemi gercekleþti
                            Debug.Log("Odaya Isteginde Bulunuldu");
                            // atama yapilan odanin scriptinde degisiklik yapilma yeri
                            var _room=hit.transform.gameObject.GetComponent<Room>();
                            if(_room!=null)
                            {
                                if (_room.CurrentCapacity < _room.MaxCapacity)
                                {
                                    // odanin yerlestirme kapasitesi dolmamis
                                    // yerlestirme yapilabilir

                                    // parent objenin cocugu olarak ata
                                    _preview.transform.parent = hit.transform.GetChild(0);
                                    // parent objenin listesine heroyu ata
                                    _room.heroManagers.Add(_preview.GetComponent<HeroManager>());
                                    // current kapasitiyi artýr
                                    _room.CurrentCapacity++;
                                    // isimiz bitti
                                    _preview = null;
                                }
                                else
                                {
                                    // kapasite dolmus
                                    Debug.Log("Oda Kapasitesi Dolu");
                                }
                            } 
                        }
                    }
                }

                // Iptal etme islemi
                if(Input.GetMouseButtonDown(1))
                {
                    // iptal olacak
                    CancelSpawnObjectType(_preview);
                    Destroy(_preview);
                    _preview = null;
                }
            }
        }
    }
    private void InitializeVariables()
    {
        // test assaign 
        for (int i = 0; i < 6; i++)
        {
            _heroesCounts[i] = Random.Range(0, 4);
        }
        CheckCount();
    }
    private void CheckCount()
    {
        // aktif herolara gore ui guncellemeleri
        int _activeBoxs = 0;
        for (int i = 0; i < 6; i++)
        {
            
            _heroesTexts[i].text = "x" + _heroesCounts[i].ToString();
            if (_heroesCounts[i] <= 0)
                _heroesBoxes[i].SetActive(false);
            else
            {
                _activeBoxs++;
                _heroesBoxes[i].SetActive(true);
            }

        }
        // aktif hero sayisine gore layoutun guncellenmesi
        switch (_activeBoxs)
        {
            case 2:
                _heroesPlaceLayout.spacing = -1450;
                break;
            case 3:
                _heroesPlaceLayout.spacing = -1150;
                break;
            case 4:
                _heroesPlaceLayout.spacing = -850;
                break;
            case 5:
                _heroesPlaceLayout.spacing = -650;
                break;
            case 6:
                _heroesPlaceLayout.spacing = -450;
                break;
        }
    }
    public void SpawnHero(GameObject heroPrefab)
    {
        if(_preview==null)
        {
            _preview = Instantiate(heroPrefab);
            SpawnObjectType(_preview);
        }
    }
    void SpawnObjectType(GameObject gameObject)
    {
        switch (gameObject.GetComponent<HeroManager>().type)
        {
            case HeroManager.HeroTYpe.mouse:
                _heroesCounts[0]--;
                break;
            case HeroManager.HeroTYpe.dog:
                _heroesCounts[1]--;
                break;
            case HeroManager.HeroTYpe.goblin:
                _heroesCounts[2]--;
                break;
            case HeroManager.HeroTYpe.ogre:
                _heroesCounts[3]--;
                break;
            case HeroManager.HeroTYpe.ork:
                _heroesCounts[4]--;
                break;
            case HeroManager.HeroTYpe.gnoll:
                _heroesCounts[5]--;
                break;

        }
        // spawn isleminden sonra gereken guncellemenin yapilmasi
        CheckCount();
    }
    void CancelSpawnObjectType(GameObject gameObject)
    {
            switch (gameObject.GetComponent<HeroManager>().type)
            {
                case HeroManager.HeroTYpe.mouse:
                    _heroesCounts[0]++;
                    break;
                case HeroManager.HeroTYpe.dog:
                    _heroesCounts[1]++;
                    break;
                case HeroManager.HeroTYpe.goblin:
                    _heroesCounts[2]++;
                    break;
                case HeroManager.HeroTYpe.ogre:
                    _heroesCounts[3]++;
                    break;
                case HeroManager.HeroTYpe.ork:
                    _heroesCounts[4]++;
                    break;
                case HeroManager.HeroTYpe.gnoll:
                    _heroesCounts[5]++;
                    break;
            }
        // spawn isleminden sonra gereken guncellemenin yapilmasi
        CheckCount();
    }

}
