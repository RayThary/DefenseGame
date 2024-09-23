using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private float baseHp;
    private float maxBaseHp;
    public enum eBaseType
    {
        Red,
        Blue,
    }
    [SerializeField] private eBaseType baseType;

    [SerializeField] private Image hpBar;
    [SerializeField] private SpriteRenderer baseDestroySprite;
    [SerializeField] private Transform baseDestroyTrs;
    void Start()
    {
        maxBaseHp = baseHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = baseHp / maxBaseHp;
        if (baseHp <= 0)
        {
            
            SpriteRenderer spr = GetComponent<SpriteRenderer>();
            spr = baseDestroySprite;
            baseDestroyTrs.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

    }


    public void BaseHit(float _damage)
    {
        baseHp -= _damage;
    }
}
