using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private Text text;
    public void IncrementScore()
    {
        int score = int.Parse(text.text);
        score++;
        text.text = score.ToString();
    }

}
