using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private GameObject unitCostParent;

    [SerializeField] private float aiCost = 0;
    private float aiMaxCost = 100;

    //이걸 쉬움1 보통2 어려움3 으로해도될듯?
    private float costUpSpeed = 2;
    private List<float> lBuyCost = new List<float>();
    private List<GameObject> lBuyUnit = new List<GameObject>();

    private bool unitCheck = false;
    private int randomUnit;

    [SerializeField]private float costUpCycleTime = 120;
    private int costUpCheck = 0;
    private float timer = 0;

    public bool test = false;
    public int unitas = 1;
    void Start()
    {
        int buyCount = unitCostParent.transform.childCount;
        for (int i = 1; i < buyCount; i++)
        {
            lBuyCost.Add(unitCostParent.transform.GetChild(i).GetComponent<UnitBuy>().GetBuyCost);
            lBuyUnit.Add(unitCostParent.transform.GetChild(i).GetComponent<UnitBuy>().GetUnit);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject unit = Instantiate(lBuyUnit[unitas], GameManager.Instance.GetUnitParent);
                unit.transform.position = GameManager.Instance.GetRedSpawnTrs.position;
                unit.GetComponent<Unit>().SetUnitCamp(Unit.eUnitCamp.Red);
                
            }
            return;
        }
        cost();
        unitBuy();
    }

    private void cost()
    {
        timer += Time.deltaTime;
        if (aiCost <= aiMaxCost)
        {
            aiCost += Time.deltaTime * costUpSpeed;
        }

        if (timer >= costUpCycleTime)
        {
            if (costUpCheck == 3)
            {
                return;
            }
            costUpSpeed++;
            costUpCheck++;
            costUpCycleTime += 120;
            timer = 0;
        }

    }
    private void unitBuy()
    {
        if (unitCheck == false)
        {
            randomUnit = Random.Range(0, lBuyCost.Count);
            unitCheck = true;
        }
        else
        {
            if (aiCost >= lBuyCost[randomUnit])
            {
                GameObject unit = Instantiate(lBuyUnit[randomUnit], GameManager.Instance.GetUnitParent);
                unit.transform.position = GameManager.Instance.GetRedSpawnTrs.position;
                unit.GetComponent<Unit>().SetUnitCamp(Unit.eUnitCamp.Red);
                aiCost -= lBuyCost[randomUnit];
                unitCheck = false;
            }
        }
    }
}
