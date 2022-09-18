using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;
public class EnemyManager : Enemy
{
    [Header("Classes")]
    [NaughtyAttributes.HorizontalLine(height:2,color:EColor.Orange)]
    public Type[] _irklar;
    public Type[] _siniflar;
    public Type[] _cinsiyetler;
    public Type[] _silahlar;

    [Header("Sprites")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public SpriteRenderer[] _cuceSprites;
    public SpriteRenderer[] _hobbitSprites;
    public SpriteRenderer[] _elfSprites;
    public SpriteRenderer[] _insanSprites;
    public SpriteRenderer[] _siniflarSprites;
    public SpriteRenderer[] _silahlarSprites;

    public Vector3 _tempPos;

    [Header("Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Red)]

    public GameObject floatingPoint;
    public string silahName;

    [Header("Colors")]
    [NaughtyAttributes.HorizontalLine(height:2,color:EColor.Black)]
    public Color healColor;
    public Color missColor;
    public Color attackColor;

    public HeroManager _target;

    public RectTransform damageRect, missRect;

    public int _maxEnemyHeatlh;
    // baslangicta iksiri var
    public bool hasPotion = true;
    //can degisdiğinde yapilacak diğer islemlerinde yapildigi property
    public int Can
    {
        get { return this.can; }
        set
        {
            if (this.can < value)
            {
                // iyilesme
                Setup(-2, "Heal");
                GameObject healPopup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
                healPopup.GetComponent<TextMeshPro>().color = healColor;
                PopupManager.instance.popupList.Add(healPopup);
                this.can = value;
            }
            else
            {
                // damage yedi
                this.can = value;
                // slider 
                // detect who am i
                EnemySpawnManager enemySpawnManager = EnemySpawnManager.Instance;
                for(int i=0;i<enemySpawnManager._enemyList.Count;i++)
                {
                    if(enemySpawnManager._enemyList[i] == this)
                    {
                        enemySpawnManager._enemyHealthSlider[i].value = (Can * 100) / _maxEnemyHeatlh;
                        break;
                    }
                }
                // pop up cikacak
                setPopup();
            }
        }
    }

    void Start()
    {
        SetupEnemy();
        // baslangicta saldiri
        _maxEnemyHeatlh = this.can;
    }

    public void Setup(int damageAmount, string situation)
    {

        if (damageAmount == -1)
        {
            floatingPoint.GetComponent<TextMeshPro>().SetText(situation);
        }
        else if (damageAmount == -2)
        {
            floatingPoint.GetComponent<TextMeshPro>().SetText(situation);
        }
        else
        {
            floatingPoint.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
        }
    }
    void SetupEnemy()
    {
        int cinsiyetRandom = Random.Range(0, _cinsiyetler.Length);
        int sinifRandom = Random.Range(0, _siniflar.Length);
        int silahRandom= Random.Range(0, _silahlar.Length);
        //
        var _irk = _irklar[Random.Range(0, _irklar.Length)];
        var _sinif = _siniflar[sinifRandom];
        var _cinsiyet = _cinsiyetler[cinsiyetRandom];
        var _silah = _silahlar[silahRandom];

        this.yakin_etki += _irk.yakin_etki + _sinif.yakin_etki + _cinsiyet.yakin_etki + _silah.yakin_etki;
        this.uzak_etki += _irk.uzak_etki + _sinif.uzak_etki + _cinsiyet.uzak_etki + _silah.uzak_etki;
        this.iyilestirme += _irk.iyilestirme + _sinif.iyilestirme + _cinsiyet.iyilestirme + _silah.iyilestirme;
        this.ganimet += _irk.ganimet + _sinif.ganimet + _cinsiyet.ganimet + _silah.ganimet;
        this.defans += _irk.defans + _sinif.defans + _cinsiyet.defans + _silah.defans;
        this.kacinma += _irk.kacinma + _sinif.kacinma + _cinsiyet.kacinma + _silah.kacinma;
        this.can += _irk.can + _sinif.can + _cinsiyet.can + _silah.can;

        _maxEnemyHeatlh = can;
        silahName = _silah.name;

        Debug.Log(this.gameObject.name + " INFO \n Irk:" + _irk.name + "\nSınıf:" + _sinif.name + "\nCinsiyet:" + _cinsiyet.name + "\nSilah:" + _silah.name + "");
        // 1-e 2-k 3-g 4-a

        //find index
        int _currentEnemeyIndex = -1;
        EnemySpawnManager enemySpawnManager = EnemySpawnManager.Instance;
        for(int i=0;i< enemySpawnManager._enemyList.Count;i++)
        {
            if(enemySpawnManager._enemyList[i] == this)
            {
                _currentEnemeyIndex = i;
                break;
            }
        }
        Debug.Log("Current Enemy : " + _currentEnemeyIndex);

        switch(_irk.name)
        {
            case "Human":
                if(_currentEnemeyIndex==0)
                {
                    enemySpawnManager._insanSpritesFirstEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if(_currentEnemeyIndex==1)
                {
                    enemySpawnManager._insanSpritesSecondEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if(_currentEnemeyIndex==2)
                {
                    enemySpawnManager._insanSpritesThirdEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if(_currentEnemeyIndex==3)
                {
                    enemySpawnManager._insanSpritesFourthEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                DisableSprite(_insanSprites,cinsiyetRandom);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_cuceSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Elf":
                if (_currentEnemeyIndex == 0)
                {
                    enemySpawnManager._elfSpritesFirstEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 1)
                {
                    enemySpawnManager._elfSpritesSecondEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 2)
                {
                    enemySpawnManager._elfSpritesThirdEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 3)
                {
                    enemySpawnManager._elfSpritesFourthEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                DisableSprite(_elfSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_cuceSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Dwarf":
                if (_currentEnemeyIndex == 0)
                {
                    enemySpawnManager._cuceSpritesFirstEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 1)
                {
                    enemySpawnManager._cuceSpritesSecondEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 2)
                {
                    enemySpawnManager._cuceSpritesThirdEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 3)
                {
                    enemySpawnManager._cuceSpritesFourthEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                DisableSprite(_cuceSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Halfling":
                if (_currentEnemeyIndex == 0)
                {
                    enemySpawnManager._hobbitSpritesFirstEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 1)
                {
                    enemySpawnManager._hobbitSpritesSecondEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 2)
                {
                    enemySpawnManager._hobbitSpritesThirdEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                else if (_currentEnemeyIndex == 3)
                {
                    enemySpawnManager._hobbitSpritesFourthEnemy[cinsiyetRandom].gameObject.SetActive(true);
                }
                DisableSprite(_hobbitSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_cuceSprites, -1);
                break;
        }
        DisableSprite(_siniflarSprites, sinifRandom);
        DisableSprite(_silahlarSprites,silahRandom);
        if (_currentEnemeyIndex == 0)
        {
            enemySpawnManager._silahlarSpritesFirstEnemy[silahRandom].gameObject.SetActive(true);
            enemySpawnManager._siniflarSpritesFirstEnemy[sinifRandom].gameObject.SetActive(true);
        }
        else if (_currentEnemeyIndex == 1)
        {
            enemySpawnManager._silahlarSpritesSecondEnemy[silahRandom].gameObject.SetActive(true);
            enemySpawnManager._siniflarSpritesSecondEnemy[sinifRandom].gameObject.SetActive(true);
        }
        else if (_currentEnemeyIndex == 2)
        {
            enemySpawnManager._silahlarSpritesThirdEnemy[silahRandom].gameObject.SetActive(true);
            enemySpawnManager._siniflarSpritesThirdEnemy[sinifRandom].gameObject.SetActive(true);
        }
        else if (_currentEnemeyIndex == 3)
        {
            enemySpawnManager._silahlarSpritesFourthEnemy[silahRandom].gameObject.SetActive(true);
            enemySpawnManager._siniflarSpritesFourthEnemy[sinifRandom].gameObject.SetActive(true);
        }
        UpdateUISpriteWeapon(this.gameObject, _currentEnemeyIndex);
    }
    private void UpdateUISpriteWeapon(GameObject _enemy,int i)
    {
        switch (_enemy.GetComponent<EnemyManager>().silahName)
        {
            case "Bow-Arrow":
                EnemySpawnManager.Instance._enemyItems[i].GetComponent<Image>().sprite = EnemySpawnManager.Instance._enemyWeapons[0];
                EnemySpawnManager.Instance._enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().uzak_etki.ToString();
                break;
            case "Sword":
                EnemySpawnManager.Instance._enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().yakin_etki.ToString();
                EnemySpawnManager.Instance._enemyItems[i].GetComponent<Image>().sprite = EnemySpawnManager.Instance._enemyWeapons[1];
                break;
            case "Shield":
                EnemySpawnManager.Instance._enemyAttackTexts[i].text = _enemy.GetComponent<EnemyManager>().yakin_etki.ToString();
                EnemySpawnManager.Instance._enemyItems[i].GetComponent<Image>().sprite = EnemySpawnManager.Instance._enemyWeapons[2];
                break;
        }
    }

    public void Attack()
    {
        // yakın saldırı mı uzak saldırımı belirle
        int etki = yakin_etki;

        if (silahName == "Bow-Arrow")
            etki = uzak_etki;
        else if (silahName == "Sword" || silahName == "Shield")
            etki = yakin_etki;
        if (etki - _target.kacinma > 0)
        {
            if (Random.Range(0, etki) > _target.kacinma) // ıska mı değil mi 
            {     // target a hasar ver 
                Setup(etki, "a");
                _target.Can -= (etki - _target.defans);
                Debug.Log(_target.name + " canı " + _target.Can.ToString());
                if (_target.Can <= 0)
                {
                    _target.transform.parent.transform.parent.GetComponent<Room>().heroManagers.Remove(_target);
                    Destroy(_target.gameObject);
                }
            }
            else
            {
                Setup(-1, "Miss");
                GameObject healPopup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
                healPopup.GetComponent<TextMeshPro>().color = missColor;
                PopupManager.instance.popupList.Add(healPopup);
                Debug.Log(_target.name + " kaçındı");
            }

        }
        else
        {
            if (Random.Range(0, 10) == 5)   // Rastgele bir sayı
            {
                // target a hasar ver 
                Setup(etki, "a");
                _target.Can -= (etki - _target.defans);
                Debug.Log(_target.name + " canı " + _target.Can.ToString());
                if (_target.Can <= 0)
                {
                    _target.transform.parent.transform.parent.GetComponent<Room>().heroManagers.Remove(_target);
                    Destroy(_target.gameObject);
                }
            }
        }
    }

    private void setPopup()
    {
        GameObject healPopup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
        healPopup.GetComponent<TextMeshPro>().color = attackColor;
        PopupManager.instance.popupList.Add(healPopup);
    }
    private void DisableSprite(SpriteRenderer[] _array,int _index)
    {
        for(int i=0; i<_array.Length;i++)
        {
            if(i == _index)
            {
                _array[i].gameObject.SetActive(true);
            }
            else
            {
                _array[i].gameObject.SetActive(false);
            }
        }
    }
}
