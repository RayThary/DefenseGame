using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private GameObject unitCostParent;

    private float aiCost = 0;
    private float aiMaxCost = 100;

    private bool levelCheck = false;
    //이걸 쉬움1 보통2 어려움3 으로해도될듯?
    private float costUpSpeed = 2;
    private List<float> lBuyCost = new List<float>();
    private List<GameObject> lBuyUnit = new List<GameObject>();

    private int maxBuyUnit = 0;

    private bool unitCheck = false;
    private int randomUnit;

    private float costUpCycleTime = 30;

    private int costUpCheck = 0;
    private float timer = 0;
  

    void Start()
    {
        int buyCount = unitCostParent.transform.childCount;
        for (int i = 1; i < buyCount; i++)
        {
            lBuyCost.Add(unitCostParent.transform.GetChild(i).GetComponent<UnitBuy>().GetBuyCost);
            lBuyUnit.Add(unitCostParent.transform.GetChild(i).GetComponent<UnitBuy>().GetUnit);
        }
        maxBuyUnit = 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameStart == true && levelCheck == false)
        {
            int _ailevel = GameManager.Instance.GetAiLevel;
            switch (_ailevel)
            {
                case 1:
                    costUpSpeed = 2.5f;
                    break;
                case 2:
                    costUpSpeed = 3f;

                    break;
                case 3:
                    costUpSpeed = 3.5f;
                    break;
            }
            levelCheck = true;
        }
        else if (GameManager.Instance.GetGameStart == true)
        {
            cost();
            unitBuy();
        }
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
                if (maxBuyUnit < lBuyUnit.Count)
                {
                    maxBuyUnit++;
                }
                return;
            }
            costUpSpeed++;
            costUpCheck++;
            costUpCycleTime += 30;
            timer = 0;
        }

    }
    private void unitBuy()
    {
        if (unitCheck == false)
        {
            randomUnit = Random.Range(0, maxBuyUnit);
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
