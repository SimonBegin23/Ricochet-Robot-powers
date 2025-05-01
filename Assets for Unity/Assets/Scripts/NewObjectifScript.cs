using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class NewObjectifScript : MonoBehaviour
{
    [SerializeField] private GameObject[] objectifs;
    [SerializeField] private GameObject boardHider;
    [SerializeField] private GameObject GameDoneText;
    [SerializeField] private GameObject endRoundButton;
    [SerializeField] private GameObject endRoundText;
    [SerializeField] private GameObject ResetTryButton;
    void Start()
    {
        reshuffle(objectifs);
        objectifs[objectifs.Length - 1].SetActive(true);
        
    }
    void reshuffle(GameObject[] objs)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < objs.Length; t++)
        {
            GameObject tmp = objs[t];
            int r = Random.Range(t, objs.Length);
            objs[t] = objs[r];
            objs[r] = tmp;
        }
    }

    public void NewObjectif()
    {
        if(objectifs.Length > 1)
        {
            objectifs[objectifs.Length - 1].SetActive(false);
            GameObject[] tmp = new GameObject[objectifs.Length - 1];
            for (int i = 0; i < objectifs.Length - 1; i++)
                tmp[i] = objectifs[i];
            objectifs = tmp;
            objectifs[objectifs.Length - 1].SetActive(true);
        }
        else
        {
            GameObject[] tmp = new GameObject[objectifs.Length - 1];
            for (int i = 0; i < objectifs.Length - 1; i++)
                tmp[i] = objectifs[i];
            objectifs = tmp;
            boardHider.SetActive(true);
            GameDoneText.SetActive(true);
        }
        
    }

    public void endDisappear()
    {
        if (objectifs.Length == 0)
        {
            endRoundButton.SetActive(false);
            endRoundText.SetActive(false);
            ResetTryButton.SetActive(false);
        }
        
    }

}
