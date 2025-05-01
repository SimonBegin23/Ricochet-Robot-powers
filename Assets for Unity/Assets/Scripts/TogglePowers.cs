using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePowers : MonoBehaviour
{
    private SettingPowersScript singleton = SettingPowersScript.instance;
    public Toggle toggle;
   
    public List<Toggle> toggles = new List<Toggle>(5);


    public void ToggleButtons()
    {
        if (toggle.isOn == true)
        {
            foreach (var toggle in toggles)
            {
                toggle.isOn = true;
            }
        }
        else
        {
            foreach (var toggle in toggles)
            {
                toggle.isOn = false;
            }
        }
    }


    public void SetBluePower()
    {
        singleton.SetbluePower();
    }
    public bool GetBluePower()
    {
        return singleton.bluePowerIsOn;
    }
    public void SetGreenPower()
    {
        singleton.SetgreenPower();
    }
    public bool GetGreenPower()
    {
        return singleton.greenPowerIsOn;
    }
    public void SetRedPower()
    {
        singleton.SetredPower();
    }
    public bool GetRedPower()
    {
        return singleton.redPowerIsOn;
    }
    public void SetYellowPower()
    {
        singleton.SetyellowPower();
    }
    public bool GetYellowPower()
    {
        return singleton.yellowPowerIsOn;
    }
    public void SetGreyPower()
    {
        singleton.SetgrayPower();
    }
    public bool GetGrayPower()
    {
        return singleton.grayPowerIsOn;
    }
}
