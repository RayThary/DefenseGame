using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuy : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    public GameObject GetUnit {  get { return unit; } }
    [SerializeField] private float buyCost;
    public float GetBuyCost { get { return buyCost; } }

    [SerializeField] private float unitBuyCoolTime;
    [SerializeField] private bool isBase = false;
    private Image fillImage;
    private TextMeshProUGUI costText;
    private Button btn;

    void Start()
    {
        fillImage = transform.Find("Fill").GetComponent<Image>();
        costText = transform.Find("text").GetComponent<TextMeshProUGUI>();
        btn = GetComponent<Button>();
        costText.text = buyCost.ToString();
        if (isBase)
        {
            btn.onClick.AddListener(buyBaseUpgrade);
        }
        else
        {
            btn.onClick.AddListener(buyUnit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBase && buyCost == 100)
        {
            costText.text = "Max";
            btn.enabled = false;
            return;
        }

        if (btn.enabled == false)
        {
            fillImage.fillAmount -= Time.deltaTime / unitBuyCoolTime;
            if (fillImage.fillAmount <= 0)
            {
                btn.enabled = true;
            }
        }

    }

    private void buyUnit()
    {
        if (GameManager.Instance.GetCost >= buyCost)
        {

            GameManager.Instance.MinusCost(buyCost);
            GameObject buyUnit = Instantiate(unit, GameManager.Instance.GetUnitParent);
            buyUnit.GetComponent<Unit>().SetUnitCamp(Unit.eUnitCamp.Blue);
            Transform spawnTrs = GameManager.Instance.GetBlueSpawnTrs;
            buyUnit.transform.position = spawnTrs.position;
            
            SoundManager.instance.SFXCreate(SoundManager.Clips.Buy, transform);
            fillImage.fillAmount = 1;
            btn.enabled = false;
        }
    }

    private void buyBaseUpgrade()
    {
        if (GameManager.Instance.GetCost >= buyCost)
        {
            GameManager.Instance.MinusCost(buyCost);
            GameManager.Instance.CostSpeedUpgrade();
            SoundManager.instance.SFXCreate(SoundManager.Clips.Upgrade, transform);
            buyCost += 20;
            costText.text = buyCost.ToString();
            fillImage.fillAmount = 1;
            btn.enabled = false;
        }
    }
}
