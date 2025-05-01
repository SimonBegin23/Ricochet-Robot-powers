using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetTimer : MonoBehaviour
{
    public Button button;
    public TimerScript script;

    void Start()
    {
        button.onClick.AddListener(button_onClick);
    }

    void button_onClick()
    {
        script.TimeLeft = 60;
        script.TimerOn = false;
        script.TimerCountText.text = "Time's Up!";
    }
}
