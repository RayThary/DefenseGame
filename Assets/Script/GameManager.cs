using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float nowCost = 0;
    public float GetCost { get { return nowCost; } }
    [SerializeField] private float maxCost = 100;
    private float costUpSpeed = 2;
    private Transform redSpawnTrs;
    public Transform GetRedSpawnTrs { get { return redSpawnTrs; } }
    private Transform blueSpawnTrs;
    public Transform GetBlueSpawnTrs { get { return blueSpawnTrs; } }
    private Transform unitParent;
    public Transform GetUnitParent { get { return unitParent; } }
    private Transform unitObjectParent;
    public Transform GetUnitObjectParent { get { return unitObjectParent; } }

    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] protected GameObject backGroundObj;
    private float bgTimer = 0;
    private int bgCreateTime = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    void Start()
    {
        redSpawnTrs = transform.GetChild(0);
        blueSpawnTrs = transform.GetChild(1);
        unitParent = transform.GetChild(2);
        unitObjectParent = transform.GetChild(3);
        bgCreateTime = Random.Range(10, 20);
    }

    void Update()
    {
        costAdd();
        gameBackGround();
    }

    private void costAdd()
    {
        if (nowCost <= maxCost)
        {
            nowCost += Time.deltaTime * costUpSpeed;
            int iCost = (int)nowCost;
            costText.text = "Cost:" + iCost + "/" + maxCost;
        }
    }

    private void gameBackGround()
    {
        bgTimer += Time.deltaTime;

        float _rY = Random.Range(2, 5.5f);
        if (bgTimer >= bgCreateTime)
        {
            GameObject obj = Instantiate(backGroundObj);
            obj.transform.position = new Vector3(-13, _rY, 0);
            bgCreateTime = Random.Range(10, 20);
            bgTimer = 0;
        }
    }

    public void MinusCost(float _cost)
    {
        nowCost -= _cost;
    }

    public void CostSpeedUpgrade()
    {
        costUpSpeed += 1f;
        maxCost += 10;
    }
}
