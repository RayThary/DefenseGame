using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject endMenu;

    
    private Button optionButton;

    private Slider masterSlider;
    private Slider backGroundSlider;
    private Slider SFXSlider;

    private Button cancelButton;
    private Button checkButton;

    void Start()
    {
        
        optionButton = GetComponent<Button>();

        masterSlider = optionMenu.transform.GetChild(0).GetComponentInChildren<Slider>();
        backGroundSlider = optionMenu.transform.GetChild(1).GetComponentInChildren<Slider>();
        SFXSlider = optionMenu.transform.GetChild(2).GetComponentInChildren<Slider>();

        cancelButton = endMenu.transform.GetChild(0).GetComponent<Button>();
        checkButton = endMenu.transform.GetChild(1).GetComponent<Button>();

        optionMenu.SetActive(false);
        endMenu.SetActive(false);


        optionButton.onClick.AddListener(optionMenuBtn);

        SoundManager.instance.SetMasterSound(masterSlider);
        SoundManager.instance.SetBGMSound(backGroundSlider);
        SoundManager.instance.SetSFXSound(SFXSlider);

        cancelButton.onClick.AddListener(cancel);
        checkButton.onClick.AddListener(end);
    }

    private void optionMenuBtn()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);
            if (GameManager.Instance.GetTimeStopCheck == false)
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            optionMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void cancel()
    {
        endMenu.SetActive(false);
        if (GameManager.Instance.GetTimeStopCheck == false)
        {
            Time.timeScale = 1;
        }
    }

    private void end()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (endMenu.activeSelf)
            {
                endMenu.SetActive(false);
                if (GameManager.Instance.GetTimeStopCheck == false)
                {
                    Time.timeScale = 1;
                }
            }
            else
            {
                endMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
