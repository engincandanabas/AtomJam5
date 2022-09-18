using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
public class MainMenuRandomEnemy : MonoBehaviour
{
    [Header("Enemy Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Blue)]
    public GameObject parent;
    public Image[] _cuceSprites;
    public Image[] _hobbitSprites;
    public Image[] _elfSprites;
    public Image[] _insanSprites;
    public Image[] _siniflarSprites;
    public Image[] _silahlarSprites;

    [Header("Classes")]
    [NaughtyAttributes.HorizontalLine(height: 2, color: EColor.Orange)]
    public Type[] _irklar;
    public Type[] _siniflar;
    public Type[] _cinsiyetler;
    public Type[] _silahlar;
    void Start()
    {
        SetupEnemy();
    }
    void SetupEnemy()
    {
        int cinsiyetRandom = Random.Range(0, _cinsiyetler.Length);
        int sinifRandom = Random.Range(0, _siniflar.Length);
        int silahRandom = Random.Range(0, _silahlar.Length);
        //
        var _irk = _irklar[Random.Range(0, _irklar.Length)];
        var _sinif = _siniflar[sinifRandom];
        var _cinsiyet = _cinsiyetler[cinsiyetRandom];
        var _silah = _silahlar[silahRandom];
        switch (_irk.name)
        {
            case "Human":
                DisableSprite(_insanSprites, cinsiyetRandom);
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
        DisableSprite(_silahlarSprites, silahRandom);
        parent.SetActive(true);
    }
    private void DisableSprite(Image[] _array, int _index)
    {
        for (int i = 0; i < _array.Length; i++)
        {
            if (i == _index)
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
