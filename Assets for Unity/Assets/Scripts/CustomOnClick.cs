using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomOnClick : MonoBehaviour
{
    public Button button;
    public TimerScript script;
    
    void Start()
    {
        button.onClick.AddListener(button_onClick);

    }

    void button_onClick()
    {
        script.TimerOn = true;
    }
}
