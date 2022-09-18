using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public enum HeroTYpe { mouse, dog, goblin, ogre, ork, gnoll }
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

    
    public Color missColor;
    public Color attackColor;

    // target bulundugu odadan random cekilcek
    public EnemyManager _target;

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


    public void Attack()
    {
        Debug.Log(_heroScriptable.name);

        if (this.yakin_etki - _target.kacinma > 0)
        {
            if (Random.Range(0, yakin_etki) > _target.kacinma) // ýska mý deðil mi 
            {     // target a hasar ver 
                Setup(yakin_etki, "a");
                _target.Can -= (yakin_etki - _target.defans);


                Debug.Log(_target.name + " caný " + _target.Can.ToString());
                if (_target.Can <= 0)
                {
                    _target.transform.parent.transform.parent.GetComponent<Room>().enemyManagers.Remove(_target);
                    Destroy(_target.gameObject);
                }
            }
            else
            {
                Setup(-1, "miss");
                GameObject healPopup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
                healPopup.GetComponent<TextMeshPro>().color = missColor;
                PopupManager.instance.popupList.Add(healPopup);
                Debug.Log(_target.name + " kaçýndý");
            }

        }
        else
        {
            if (Random.Range(0, 10) == 5)   // Rastgele bir sayý
            {
                // target a hasar ver
                Setup(yakin_etki, "a");
                _target.Can -= (yakin_etki - _target.defans);
                Debug.Log(_target.name + " caný " + _target.Can.ToString());
                if (_target.Can <= 0)
                {
                    _target.transform.parent.transform.parent.GetComponent<Room>().enemyManagers.Remove(_target);
                    Destroy(_target.gameObject);
                }
            }
        }
    }

    private void setPopup()
    {
        GameObject healPopup = Instantiate(floatingPoint, transform.position, Quaternion.identity);
        healPopup.GetComponent<TextMeshPro>().color = attackColor;
    }

}
