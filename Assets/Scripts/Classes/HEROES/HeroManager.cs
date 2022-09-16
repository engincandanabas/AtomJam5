using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public enum HeroTYpe { mouse,dog,goblin,ogre,ork,gnoll}
    public HeroTYpe type;

    public GameObject floatingPoint;

    public Type _heroScriptable;
    public int yakin_etki;
    public int uzak_etki;
    public int iyilestirme;
    public int ganimet;
    public int defans;
    public int kacinma;
    public int can;

    private int mevcutCan;

    // target bulundugu odadan random cekilcek
    [SerializeField] private EnemyManager _target;
    public void Awake()
    {
        InitializeVariables();
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
    void InitializeVariables()
    {
        // baslangic degerlerinin scriptable objelerden cekilmesi
        yakin_etki = _heroScriptable.yakin_etki;
        uzak_etki = _heroScriptable.uzak_etki;
        iyilestirme = _heroScriptable.iyilestirme;
        ganimet = _heroScriptable.ganimet;
        kacinma = _heroScriptable.kacinma;
        defans = _heroScriptable.defans;
        can = _heroScriptable.can;
    }

    private void Update()
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


    public IEnumerator Attack()
    {
        Debug.Log(_heroScriptable.name);

        if (this.yakin_etki - _target.kacinma > 0)
        {
            if (Random.Range(0, yakin_etki) > _target.kacinma) // ýska mý deðil mi 
            {     // target a hasar ver 
                Setup(yakin_etki,"a");
                _target.can -= (yakin_etki - _target.defans);
                
                
                Debug.Log(_target.name + " caný " + _target.can.ToString());
                if (_target.can <= 0)
                    Destroy(_target.gameObject);
            }
            else
            {
                Setup(-1, "miss");
                Instantiate(floatingPoint, _target.transform.position, Quaternion.identity);
                Debug.Log(_target.name + " kaçýndý");
            }

        }
        else
        {
            if (Random.Range(0, 10) == 5)   // Rastgele bir sayý
            {
                // target a hasar ver
                Setup(yakin_etki,"a");
                _target.can -= (yakin_etki - _target.defans);
                Debug.Log(_target.name + " caný " + _target.can.ToString());
                if (_target.can <= 0)
                    Destroy(_target.gameObject);
            }
        }

        // atak bitti 
        yield return new WaitForSeconds(1);
        StartCoroutine(_target.Attack());

    }
}
