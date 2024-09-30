using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Base;

public class UiTitle : MonoBehaviour
{

    private Transform title;
    private Transform titleButtonParent;
    private Transform victory;
    private Transform defeat;

    private Transform titleImage;
    [SerializeField] private GameObject gameUi;

    private Transform easyClearCheck;
    private Transform normalClearCheck;
    private Transform hardClearCheck;

    private Button easyBtn;
    private Button normalBtn;
    private Button hardBtn;

    private Button victoryBtn;
    private Button defeatBtn;


    void Start()
    {
        gameUi.SetActive(false);
        titleImage = transform.GetChild(0).GetComponent<Transform>();
        title = transform.GetChild(1).GetComponent<Transform>();
        titleButtonParent = transform.GetChild(2).GetComponent<Transform>();
        victory = transform.GetChild(3).GetComponent<Transform>();
        defeat = transform.GetChild(4).GetComponent<Transform>();

        titleImage.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        titleButtonParent.gameObject.SetActive(true);
        victory.gameObject.SetActive(false);
        defeat.gameObject.SetActive(false);


        easyBtn = titleButtonParent.GetChild(0).GetComponent<Button>();
        normalBtn = titleButtonParent.GetChild(1).GetComponent<Button>();
        hardBtn = titleButtonParent.GetChild(2).GetComponent<Button>();

        easyClearCheck = easyBtn.transform.GetChild(1).GetComponent<Transform>();
        normalClearCheck = normalBtn.transform.GetChild(1).GetComponent<Transform>();
        hardClearCheck = hardBtn.transform.GetChild(1).GetComponent<Transform>();


        victoryBtn = victory.GetComponentInChildren<Button>();

        defeatBtn = defeat.GetComponentInChildren<Button>();

        easyBtn.onClick.AddListener(easyButton);
        normalBtn.onClick.AddListener(normalButton);
        hardBtn.onClick.AddListener(hardButton);

        victoryBtn.onClick.AddListener(victoyReturnMain);
        defeatBtn.onClick.AddListener(defeatReturnButton);
    }

    private void easyButton()
    {
        GameManager.Instance.SetAiLevel = 1;
        GameManager.Instance.SetGameStart = true;
        GameManager.Instance.SetCost = 0;
        titleImage.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        titleButtonParent.gameObject.SetActive(false);
        gameUi.SetActive(true);
    }

    private void normalButton()
    {
        GameManager.Instance.SetAiLevel = 2;
        GameManager.Instance.SetGameStart = true;
        GameManager.Instance.SetCost = 0;
        titleImage.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        titleButtonParent.gameObject.SetActive(false);
        gameUi.SetActive(true);
    }

    private void hardButton()
    {
        GameManager.Instance.SetAiLevel = 3;
        GameManager.Instance.SetCost = 0;
        GameManager.Instance.SetGameStart = true;
        titleImage.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        titleButtonParent.gameObject.SetActive(false);
        gameUi.SetActive(true);
    }

    private void victoyReturnMain()
    {

        if (GameManager.Instance.GetAiLevel == 1)
        {
            PlayerPrefs.SetInt("Clear1", 1);
        }
        else if (GameManager.Instance.GetAiLevel == 2)
        {
            PlayerPrefs.SetInt("Clear2", 1);
        }
        else if (GameManager.Instance.GetAiLevel == 3)
        {
            PlayerPrefs.SetInt("Clear3", 1);
        }

        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
        victory.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
    }


    private void defeatReturnButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
        defeat.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
        }

        if (PlayerPrefs.GetInt("Clear1") == 1)
        {
            if (easyClearCheck.gameObject.activeSelf == true)
            {
                return;
            }
            easyClearCheck.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Clear2") == 1)
        {
            if (normalClearCheck.gameObject.activeSelf == true)
            {
                return;
            }
            normalClearCheck.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Clear3") == 1)
        {
            if (hardClearCheck.gameObject.activeSelf == true)
            {
                return;
            }
            hardClearCheck.gameObject.SetActive(true);
        }
    }


}
