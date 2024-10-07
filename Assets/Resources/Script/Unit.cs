using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Unit : MonoBehaviour
{
    public enum eUnitType
    {
        sword,
        magic,
        Cavalry,
        Arrow,
    }
    public enum eUnitCamp
    {
        Red,
        Blue,
    }

    [SerializeField] private eUnitCamp unitCamp;
    [SerializeField] private eUnitType unitType;
    public eUnitCamp SetUnitCamp(eUnitCamp _value)
    {
        return unitCamp = _value;
    }
    public eUnitCamp GetUnitCamp { get { return unitCamp; } }
    //유닛의 기본셋팅
    [SerializeField] private float unitHp;
    [SerializeField] private float unitSpeed;
    [SerializeField] private float unitDamage;
    public float GetUnitDamage { get { return unitDamage; } }

    //근접유닛인지?
    [SerializeField] private bool vicinityUnit = false;
    //원거리 유닛의공격 이펙트
    [SerializeField] private List<GameObject> rangedAttackObj = new List<GameObject>();
    [SerializeField] private GameObject rangedAttack;
    //공격 사거리
    [SerializeField] private float unitAttackDistance;
    private float enemyUnitDistance;

    private SpriteRenderer sprShadow;
    private Animator anim;

    private BoxCollider2D box2d;

    //근접의 공격범위
    private BoxCollider2D box2dAttack;
    private bool noMove = false;

    private bool soundCheck = false;
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        sprShadow = transform.Find("Shadow").GetComponentInChildren<SpriteRenderer>();
        if (vicinityUnit)
        {
            box2dAttack = transform.Find("Attack").GetComponent<BoxCollider2D>();
            box2dAttack.enabled = false;
        }
        else
        {
            if (unitType == eUnitType.magic)
            {
                if (unitCamp == eUnitCamp.Blue)
                {
                    rangedAttack = rangedAttackObj.Find(x => x.name == "MagicBlue");
                }
                else if (unitCamp == eUnitCamp.Red)
                {
                    rangedAttack = rangedAttackObj.Find(x => x.name == "MagicRed");
                }
            }
            else if (unitType == eUnitType.Arrow)
            {
                rangedAttack = rangedAttackObj.Find(x => x.name == "Arrow");
            }
        }

        anim = GetComponent<Animator>();


        Vector3 trsScale = transform.localScale;
        if (unitCamp == eUnitCamp.Red)
        {
            sprShadow.color = Color.red;
            gameObject.layer = 7;
        }
        else if (unitCamp == eUnitCamp.Blue)
        {
            sprShadow.color = Color.blue;
            trsScale.x = trsScale.x * -1;
            transform.localScale = trsScale;

            gameObject.layer = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        unitMove();
        unitAttack();
        unitDeath();
        unitSound();
    }

    private void unitMove()
    {

        if (noMove)
        {
            return;
        }
        if (unitCamp == eUnitCamp.Red)
        {
            transform.position += Vector3.left * Time.deltaTime * unitSpeed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * unitSpeed;
        }

    }

    private void unitAttack()
    {

        float rayDistance;
        if (unitCamp == eUnitCamp.Red)
        {
            if (vicinityUnit)
            {
                rayDistance = 1;
            }
            else
            {
                rayDistance = unitAttackDistance;

            }
            RaycastHit2D unitCheck = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, LayerMask.GetMask("Blue"));
            RaycastHit2D baseCheck = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, LayerMask.GetMask("BlueBase"));

            if (unitCheck.collider != null || baseCheck.collider != null)
            {
                if (baseCheck.collider != null && unitCheck.collider != null)
                {
                    enemyUnitDistance = unitCheck.distance;
                }
                else if (unitCheck.collider == null)
                {
                    enemyUnitDistance = baseCheck.distance + 1.5f;
                }
                else
                {
                    enemyUnitDistance = unitCheck.distance;
                }

                anim.SetBool("Attack", true);
                noMove = true;

            }
            else
            {
                anim.SetBool("Attack", false);
                noMove = false;
            }

        }
        else if (unitCamp == eUnitCamp.Blue)
        {
            if (vicinityUnit)
            {
                rayDistance = 1;
            }
            else
            {
                rayDistance = unitAttackDistance;

            }

            RaycastHit2D unitCheck = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, LayerMask.GetMask("Red"));
            RaycastHit2D baseCheck = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, LayerMask.GetMask("RedBase"));
            if (unitCheck.collider != null || baseCheck.collider != null)
            {
                if (baseCheck.collider != null && unitCheck.collider != null)
                {
                    enemyUnitDistance = unitCheck.distance;
                }
                else if (unitCheck.collider == null)
                {
                    enemyUnitDistance = baseCheck.distance + 1.5f;
                }
                else
                {
                    enemyUnitDistance = unitCheck.distance;
                }

                anim.SetBool("Attack", true);
                noMove = true;
            }
            else
            {
                anim.SetBool("Attack", false);
                noMove = false;
            }
        }

    }

    private void unitSound()
    {
        if (unitType == eUnitType.Arrow)
        {
            if (soundCheck)
            {
                SoundManager.instance.SFXCreate(SoundManager.Clips.Atk_Range, 1, 0, GameManager.Instance.GetUnitObjectParent);
                soundCheck = false;
            }
        }
        else if (unitType == eUnitType.sword)
        {
            if (soundCheck)
            {
                SoundManager.instance.SFXCreate(SoundManager.Clips.Atk_Sword, 1, 0, GameManager.Instance.GetUnitObjectParent);
                soundCheck = false;
            }
        }
        else if (unitType == eUnitType.Cavalry)
        {
            if (soundCheck)
            {
                SoundManager.instance.SFXCreate(SoundManager.Clips.Atk_Cavalry, 1, 0, GameManager.Instance.GetUnitObjectParent);
                soundCheck = false;
            }
        }
        else if (unitType == eUnitType.magic)
        {
            if (soundCheck)
            {
                SoundManager.instance.SFXCreate(SoundManager.Clips.Atk_Magic, 1, 0, GameManager.Instance.GetUnitObjectParent);
                soundCheck = false;
            }
        }
    }

    private void unitDeath()
    {
        if (unitHp <= 0)
        {
            box2d.enabled = false;
            if (vicinityUnit)
            {
                if (box2dAttack != null)
                {
                    box2dAttack.enabled = false;
                }
            }
            noMove = true;
            anim.SetTrigger("Death");
        }
    }

    public void SetHitUnit(float _damage)
    {
        unitHp -= _damage;
    }

    /// <summary>
    /// 진영체크
    /// </summary>
    /// <returns>true면 레드 false 면 블루</returns>
    public bool GetUnitCampCheck()
    {
        if (unitCamp == eUnitCamp.Red)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //애니메이션용
    private void AttackBoxOn()
    {
        box2dAttack.enabled = true;
        soundCheck = true;
    }

    private void AttackBoxOff()
    {
        box2dAttack.enabled = false;
    }

    private void RangedAttack()
    {
        Vector3 targetVec = transform.position;

        if (unitType == eUnitType.magic)
        {

            if (unitCamp == eUnitCamp.Red)
            {
                targetVec.x -= enemyUnitDistance;
            }
            else
            {
                targetVec.x += enemyUnitDistance;

            }
        }
        else if (unitType == eUnitType.Arrow)
        {
            if (unitCamp == eUnitCamp.Red)
            {
                targetVec.x -= 0.3f;

            }
            else
            {
                targetVec.x += 0.3f;

            }
            targetVec.y += 0.3f;
        }
        GameObject att = Instantiate(rangedAttack, GameManager.Instance.GetUnitObjectParent);
        att.transform.position = targetVec;
        att.GetComponent<UnitAttack>().SetUnitCampCheck(this);
        soundCheck = true;

    }

    private void unitDeathDestroy()
    {
        Destroy(gameObject);
    }
}
