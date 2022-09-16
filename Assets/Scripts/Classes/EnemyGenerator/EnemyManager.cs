using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : Enemy
{
    public Type[] _irklar;
    public Type[] _siniflar;
    public Type[] _cinsiyetler;
    public Type[] _silahlar;

    public GameObject floatingPoint;
    string silahName;

    [SerializeField] private HeroManager _target;

    private int mevcutCan;
    

    void Awake()
    {
        SetupEnemy();
        // baslangicta saldiri
        StartCoroutine(Attack());
    }
    void Start()
    {
        mevcutCan = this.can;

    }
    void Update()
    {

        if (can == mevcutCan)
        {

        }
        else
        {
            GameObject popup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
           
            mevcutCan = this.can;
            Destroy(popup, 20f);
        }
    }

    public void Setup(int damageAmount,string situation)
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
        var _irk=_irklar[Random.Range(0,_irklar.Length)];
        var _sinif=_siniflar[Random.Range(0,_siniflar.Length)];
        var _cinsiyet=_cinsiyetler[Random.Range(0,_cinsiyetler.Length)];
        var _silah=_silahlar[Random.Range(0,_silahlar.Length)];

        this.yakin_etki+=_irk.yakin_etki+_sinif.yakin_etki+_cinsiyet.yakin_etki+_silah.yakin_etki;
        this.uzak_etki+=_irk.uzak_etki+_sinif.uzak_etki+_cinsiyet.uzak_etki+_silah.uzak_etki;
        this.iyilestirme+=_irk.iyilestirme+_sinif.iyilestirme+_cinsiyet.iyilestirme+_silah.iyilestirme;
        this.ganimet+=_irk.ganimet+_sinif.ganimet+_cinsiyet.ganimet+_silah.ganimet;
        this.defans+=_irk.defans+_sinif.defans+_cinsiyet.defans+_silah.defans;
        this.kacinma+=_irk.kacinma+_sinif.kacinma+_cinsiyet.kacinma+_silah.kacinma;
        this.can+=_irk.can+_sinif.can+_cinsiyet.can+_silah.can;

        silahName = _silah.name;

        Debug.Log(this.gameObject.name+" INFO \n Irk:"+_irk.name+"\nSınıf:"+_sinif.name+"\nCinsiyet:"+_cinsiyet.name+"\nSilah:"+_silah.name+"");
    }
    public IEnumerator Attack()
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
                Setup(etki,"a");
                _target.can -= (etki - _target.defans);
                Debug.Log(_target.name + " canı " + _target.can.ToString());
                if (_target.can <= 0)
                    Destroy(_target.gameObject);
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
                _target.can -= (etki - _target.defans);
                Debug.Log(_target.name + " canı " + _target.can.ToString());
                if (_target.can <= 0)
                    Destroy(_target.gameObject);
            }
        }


        // atak bitti 
        yield return new WaitForSeconds(1);
        StartCoroutine(_target.Attack());

    }
}
