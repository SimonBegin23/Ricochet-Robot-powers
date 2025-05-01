using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ZeroMoveCountScript : MonoBehaviour
{
    [SerializeField] private Text text;
    public void zeroCount()
    {
        text.text = "0";
    }
}
