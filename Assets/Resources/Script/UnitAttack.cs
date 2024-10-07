using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public enum eAttackType
    {
        Melee,
        Arrow,
        Magic,
    }
    [SerializeField] private eAttackType attackType;
    [SerializeField] private float moveAttackSpeed = 1;
    private float damage;
    private bool unitCampCheck;
    private bool unitType;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (unitCampCheck)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Blue"))
            {
                collision.GetComponent<Unit>().SetHitUnit(damage);

                if (attackType == eAttackType.Arrow)
                {
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("BlueBase"))
            {
                collision.GetComponent<Base>().BaseHit(damage);

                if (attackType == eAttackType.Arrow)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Red"))
            {
                collision.GetComponent<Unit>().SetHitUnit(damage);

                if (attackType == eAttackType.Arrow)
                {
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("RedBase"))
            {
                collision.GetComponent<Base>().BaseHit(damage);

                if (attackType == eAttackType.Arrow)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    void Start()
    {
        if (attackType == eAttackType.Melee)
        {
            damage = GetComponentInParent<Unit>().GetUnitDamage;
            unitCampCheck = GetComponentInParent<Unit>().GetUnitCampCheck();
        }
        else if (attackType == eAttackType.Arrow)
        {
            if (unitCampCheck)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 1);
            }
            Destroy(gameObject, 1.5f);
        }
    }

    private void Update()
    {
        if (attackType == eAttackType.Arrow)
        {
            if (unitCampCheck)
            {
                transform.position += Vector3.left * moveAttackSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * moveAttackSpeed * Time.deltaTime;
            }
        }
    }

    public void SetUnitCampCheck(Unit _unit)
    {
        damage = _unit.GetUnitDamage;
        unitCampCheck = _unit.GetUnitCampCheck();
    }
}
