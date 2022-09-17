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
        damageRect.DOScale(Vector2.zero, 4f);
        damageRect.DOMove(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z),4f);
        
    }
}
