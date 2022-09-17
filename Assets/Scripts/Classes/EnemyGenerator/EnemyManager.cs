using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;

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


    [Header("Attributes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Red)]

    public GameObject floatingPoint;
    public string silahName;

    public HeroManager _target;

    public RectTransform damageRect, missRect;

    private int _maxEnemyHeatlh;
    // baslangicta iksiri var
    public bool hasPotion = true;
    public int Can
    {
        get { return this.can; }
        set
        {
            if (this.can < value)
            {
                // iyilesme
                this.can = value;
            }
            else
            {
                // damage yedi
                this.can = value;
                // pop up cikacak
                setPopup();
            }
        }
    }



    void Awake()
    {
        SetupEnemy();
        // baslangicta saldiri
    }

    public void Setup(int damageAmount, string situation)
    {

        if (damageAmount == -1)
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

        switch(_irk.name)
        {
            case "Human":
                DisableSprite(_insanSprites,cinsiyetRandom);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_cuceSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Elf":
                DisableSprite(_elfSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_cuceSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Dwarf":
                DisableSprite(_cuceSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_hobbitSprites, -1);
                break;
            case "Halfling":
                DisableSprite(_hobbitSprites, cinsiyetRandom);
                DisableSprite(_insanSprites, -1);
                DisableSprite(_elfSprites, -1);
                DisableSprite(_cuceSprites, -1);
                break;
        }
        DisableSprite(_siniflarSprites, sinifRandom);
        DisableSprite(_silahlarSprites,silahRandom);
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
                Instantiate(floatingPoint, _target.transform.position, Quaternion.identity);
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
        GameObject popup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
        Destroy(popup, 0.75f);
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
