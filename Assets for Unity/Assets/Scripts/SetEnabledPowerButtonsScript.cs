using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEnabledPowerButtonsScript : MonoBehaviour
{
    private SettingPowersScript singleton;

    private void Update()
    {
        singleton = SettingPowersScript.instance;
    }
    [SerializeField] private Button ButtonBlue;
    [SerializeField] private Button ButtonGreen;
    [SerializeField] private Button ButtonRed;
    [SerializeField] private Button ButtonRed1;
    [SerializeField] private Button ButtonYellow;
    public void SetInteractable()
    {
        ButtonBlue.interactable = false; 
        ButtonGreen.interactable = false; 
        ButtonRed.interactable = false;
        ButtonRed1.interactable = false;
        ButtonYellow.interactable = false;
        if (singleton.bluePowerIsOn) { ButtonBlue.interactable = true; }
        if (singleton.greenPowerIsOn) { ButtonGreen.interactable = true; }
        if (singleton.redPowerIsOn) { ButtonRed.interactable = true; }
        if (singleton.redPowerIsOn) { ButtonRed1.interactable = true; }
        if (singleton.yellowPowerIsOn) { ButtonYellow.interactable = true; }
    }

}
