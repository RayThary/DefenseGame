using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float nowCost = 0;
    public float SetCost { set { nowCost = value; } }
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

    private int aiLevel = 1;
    public int SetAiLevel { set { aiLevel = value; } }
    public int GetAiLevel { get { return aiLevel; } }

    private bool GameStart = false;
    public bool SetGameStart { set { GameStart = value; } }
    public bool GetGameStart { get { return GameStart; } }

    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] protected GameObject backGroundObj;
    private float bgTimer = 0;
    private int bgCreateTime = 0;

    private bool timeStopCheck = false;
    public bool SetTimeStopCheck { set { timeStopCheck = value; } }
    public bool GetTimeStopCheck { get { return timeStopCheck; } }
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
        bgTimer = 99;
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
        }
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
            float dustSpeed = Random.Range(0.05f, 0.2f);
            obj.GetComponent<DustMove>().SetMoveSpeed(dustSpeed);
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
