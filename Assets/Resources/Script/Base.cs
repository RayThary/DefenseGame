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
    [SerializeField] private Sprite baseDestroySprite;
    [SerializeField] private Transform baseDestroyTrs;

    private bool destroyCheck = false;
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
            if (destroyCheck)
            {
                return;
            }
            SpriteRenderer spr = GetComponent<SpriteRenderer>();
            spr.sprite = baseDestroySprite;
            baseDestroyTrs.gameObject.SetActive(true);
            SoundManager.instance.bgSoundPause(true);
            switch (baseDestroyTrs.name)
            {
                case "Defeat":
                    SoundManager.instance.SFXCreate(SoundManager.Clips.Defeat, 0.3f, 0, transform);
                    break;
                case "Victory":
                    SoundManager.instance.SFXCreate(SoundManager.Clips.Victory, 0.3f, 0, transform);
                    break;
            }
            destroyCheck = true;
            GameManager.Instance.SetTimeStopCheck = true;
            Time.timeScale = 0;
        }
    }

    public void BaseHit(float _damage)
    {
        baseHp -= _damage;
    }
}

