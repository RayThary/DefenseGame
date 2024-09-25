using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    private Button optionButton;

    private Slider masterSlider;
    private Slider backGroundSlider;
    private Slider SFXSlider;

    void Start()
    {
        optionButton = GetComponent<Button>();

        masterSlider = optionMenu.transform.GetChild(0).GetComponentInChildren<Slider>();
        backGroundSlider = optionMenu.transform.GetChild(1).GetComponentInChildren<Slider>();
        SFXSlider = optionMenu.transform.GetChild(2).GetComponentInChildren<Slider>();

        optionMenu.SetActive(false);

        optionButton.onClick.AddListener(optionMenuBtn);

        SoundManager.instance.SetMasterSound(masterSlider);
        SoundManager.instance.SetBGMSound(backGroundSlider);
        SoundManager.instance.SetSFXSound(SFXSlider);
    }

    private void optionMenuBtn()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            optionMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
