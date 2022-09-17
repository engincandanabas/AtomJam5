using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenManager : MonoBehaviour
{

    public RectTransform damageRect;

    private void Awake()
    {
        damageTween();
    }



    public void damageTween()
    {
        
        damageRect.DOScale(Vector2.zero, 3f);
        damageRect.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 3f);
       

    }
}
