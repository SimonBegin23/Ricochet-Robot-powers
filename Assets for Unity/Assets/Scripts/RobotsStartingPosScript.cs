using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsStartingPosScript : MonoBehaviour
{

    [SerializeField] private GameObject Bleu;
    [SerializeField] private GameObject Vert;
    [SerializeField] private GameObject Rouge;
    [SerializeField] private GameObject Jaune;
    [SerializeField] private GameObject Gris;

    Vector3[] robotStartPos = new Vector3[5];

    public void setStartPos()
    {
        robotStartPos[0] = Bleu.transform.position;
        robotStartPos[1] = Vert.transform.position;
        robotStartPos[2] = Rouge.transform.position;
        robotStartPos[3] = Jaune.transform.position;
        robotStartPos[4] = Gris.transform.position;
    }

    public void resetToStartPos()
    {
        Bleu.transform.position = robotStartPos[0];
        Vert.transform.position = robotStartPos[1];
        Rouge.transform.position = robotStartPos[2];
        Jaune.transform.position = robotStartPos[3];
        Gris.transform.position = robotStartPos[4];
    }




}
