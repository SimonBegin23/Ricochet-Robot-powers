using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTogglePowersScript : MonoBehaviour
{
    private SettingPowersScript singleton;
    public Toggle toggleAll;
    public Toggle toggleBlue;
    public Toggle toggleGreen;
    public Toggle toggleRed;
    public Toggle toggleYellow;
    public Toggle toggleGray;

    public void Awake()
    {
        singleton = SettingPowersScript.instance;
    }
    public void Start()
    {
        SetToggles();
    }
    private void SetToggles()
    {
        if (toggleBlue.isOn != singleton.bluePowerIsOn)
        {
            toggleBlue.isOn = singleton.bluePowerIsOn;
            singleton.bluePowerIsOn = !singleton.bluePowerIsOn;
        }
        if(toggleGreen.isOn != singleton.greenPowerIsOn)
        {
            toggleGreen.isOn = singleton.greenPowerIsOn;
            singleton.greenPowerIsOn = !singleton.greenPowerIsOn;
        }
        if(toggleRed.isOn != singleton.redPowerIsOn)
        {
            toggleRed.isOn = singleton.redPowerIsOn;
            singleton.redPowerIsOn = !singleton.redPowerIsOn;
        }
        if(toggleYellow.isOn != singleton.yellowPowerIsOn)
        {
            toggleYellow.isOn = singleton.yellowPowerIsOn;
            singleton.yellowPowerIsOn = !singleton.yellowPowerIsOn;
        }
        if(toggleGray.isOn != singleton.grayPowerIsOn)
        {
            toggleGray.isOn = singleton.grayPowerIsOn;
            singleton.grayPowerIsOn = !singleton.grayPowerIsOn;
        }
    }
}
