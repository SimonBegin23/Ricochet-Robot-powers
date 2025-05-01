using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingPowersScript : MonoBehaviour
{
    public bool bluePowerIsOn;
    public bool greenPowerIsOn;
    public bool redPowerIsOn;
    public bool yellowPowerIsOn;
    public bool grayPowerIsOn;

    public static SettingPowersScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetbluePower()
    {
        bluePowerIsOn = !bluePowerIsOn;
    }
    public void SetgreenPower()
    {
        greenPowerIsOn = !greenPowerIsOn;
    }
    public void SetredPower()
    {
        redPowerIsOn = !redPowerIsOn;
    }
    public void SetyellowPower()
    {
        yellowPowerIsOn = !yellowPowerIsOn;
    }
    public void SetgrayPower()
    {
        grayPowerIsOn = !grayPowerIsOn;
    }
}
