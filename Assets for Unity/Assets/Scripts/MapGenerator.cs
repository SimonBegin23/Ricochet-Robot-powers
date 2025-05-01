using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;


public class Plateaux
{
    public Tile[] tiles;

    public Plateaux(Tile[] tiles)
    {
        this.tiles = tiles;
    }
}
public class Couleurs
{
    public string nom;
    public Couleurs(string nom)
    {
        this.nom = nom;
    }
}

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Couleurs[] randCoul = new Couleurs[4];
    
    [SerializeField] private Tile[] tilesRouge1;
    [SerializeField] private Tile[] tilesRouge2;
    [SerializeField] private Tile[] tilesRouge3;
    [SerializeField] private Tile[] tilesRouge4;
    [SerializeField] private Tile[] tilesRouge1_1;
    [SerializeField] private Tile[] tilesRouge2_1;
    [SerializeField] private Tile[] tilesRouge3_1;
    [SerializeField] private Tile[] tilesRouge4_1;
    [SerializeField] private Tile[] tilesRouge1_2;
    [SerializeField] private Tile[] tilesRouge2_2;
    [SerializeField] private Tile[] tilesRouge3_2;
    [SerializeField] private Tile[] tilesRouge4_2;
    [SerializeField] private Tile[] tilesRouge1_3;
    [SerializeField] private Tile[] tilesRouge2_3;
    [SerializeField] private Tile[] tilesRouge3_3;
    [SerializeField] private Tile[] tilesRouge4_3;
    [SerializeField] private Tile[] tilesJaune1;
    [SerializeField] private Tile[] tilesJaune2;
    [SerializeField] private Tile[] tilesJaune3;
    [SerializeField] private Tile[] tilesJaune4;
    [SerializeField] private Tile[] tilesJaune1_1;
    [SerializeField] private Tile[] tilesJaune2_1;
    [SerializeField] private Tile[] tilesJaune3_1;
    [SerializeField] private Tile[] tilesJaune4_1;
    [SerializeField] private Tile[] tilesJaune1_2;
    [SerializeField] private Tile[] tilesJaune2_2;
    [SerializeField] private Tile[] tilesJaune3_2;
    [SerializeField] private Tile[] tilesJaune4_2;
    [SerializeField] private Tile[] tilesJaune1_3;
    [SerializeField] private Tile[] tilesJaune2_3;
    [SerializeField] private Tile[] tilesJaune3_3;
    [SerializeField] private Tile[] tilesJaune4_3;
    [SerializeField] private Tile[] tilesVert1;
    [SerializeField] private Tile[] tilesVert2;
    [SerializeField] private Tile[] tilesVert3;
    [SerializeField] private Tile[] tilesVert4;
    [SerializeField] private Tile[] tilesVert1_1;
    [SerializeField] private Tile[] tilesVert2_1;
    [SerializeField] private Tile[] tilesVert3_1;
    [SerializeField] private Tile[] tilesVert4_1;
    [SerializeField] private Tile[] tilesVert1_2;
    [SerializeField] private Tile[] tilesVert2_2;
    [SerializeField] private Tile[] tilesVert3_2;
    [SerializeField] private Tile[] tilesVert4_2;
    [SerializeField] private Tile[] tilesVert1_3;
    [SerializeField] private Tile[] tilesVert2_3;
    [SerializeField] private Tile[] tilesVert3_3;
    [SerializeField] private Tile[] tilesVert4_3;
    [SerializeField] private Tile[] tilesBleu1;
    [SerializeField] private Tile[] tilesBleu2;
    [SerializeField] private Tile[] tilesBleu3;
    [SerializeField] private Tile[] tilesBleu4;
    [SerializeField] private Tile[] tilesBleu1_1;
    [SerializeField] private Tile[] tilesBleu2_1;
    [SerializeField] private Tile[] tilesBleu3_1;
    [SerializeField] private Tile[] tilesBleu4_1;
    [SerializeField] private Tile[] tilesBleu1_2;
    [SerializeField] private Tile[] tilesBleu2_2;
    [SerializeField] private Tile[] tilesBleu3_2;
    [SerializeField] private Tile[] tilesBleu4_2;
    [SerializeField] private Tile[] tilesBleu1_3;
    [SerializeField] private Tile[] tilesBleu2_3;
    [SerializeField] private Tile[] tilesBleu3_3;
    [SerializeField] private Tile[] tilesBleu4_3;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject[] robots;
    


    void Start()
    {
        randCoul[0] = new Couleurs("Bleu");
        randCoul[1] = new Couleurs("Rouge");
        randCoul[2] = new Couleurs("Vert");
        randCoul[3] = new Couleurs("Jaune");
        reshuffle(randCoul);

        Plateaux[] plateauxRouge1 = new Plateaux[4];
        plateauxRouge1[0] = new Plateaux(tilesRouge1);
        plateauxRouge1[1] = new Plateaux(tilesRouge1_1);
        plateauxRouge1[2] = new Plateaux(tilesRouge1_2);
        plateauxRouge1[3] = new Plateaux(tilesRouge1_3);
        Plateaux[] plateauxRouge2 = new Plateaux[4];
        plateauxRouge2[0] = new Plateaux(tilesRouge2);
        plateauxRouge2[1] = new Plateaux(tilesRouge2_1);
        plateauxRouge2[2] = new Plateaux(tilesRouge2_2);
        plateauxRouge2[3] = new Plateaux(tilesRouge2_3);
        Plateaux[] plateauxRouge3 = new Plateaux[4];
        plateauxRouge3[0] = new Plateaux(tilesRouge3);
        plateauxRouge3[1] = new Plateaux(tilesRouge3_1);
        plateauxRouge3[2] = new Plateaux(tilesRouge3_2);
        plateauxRouge3[3] = new Plateaux(tilesRouge3_3);
        Plateaux[] plateauxRouge4 = new Plateaux[4];
        plateauxRouge4[0] = new Plateaux(tilesRouge4);
        plateauxRouge4[1] = new Plateaux(tilesRouge4_1);
        plateauxRouge4[2] = new Plateaux(tilesRouge4_2);
        plateauxRouge4[3] = new Plateaux(tilesRouge4_3);

        Plateaux[] plateauxJaune1 = new Plateaux[4];
        plateauxJaune1[1] = new Plateaux(tilesJaune1);
        plateauxJaune1[2] = new Plateaux(tilesJaune1_1);
        plateauxJaune1[3] = new Plateaux(tilesJaune1_2);
        plateauxJaune1[0] = new Plateaux(tilesJaune1_3);
        Plateaux[] plateauxJaune2 = new Plateaux[4];
        plateauxJaune2[1] = new Plateaux(tilesJaune2);
        plateauxJaune2[2] = new Plateaux(tilesJaune2_1);
        plateauxJaune2[3] = new Plateaux(tilesJaune2_2);
        plateauxJaune2[0] = new Plateaux(tilesJaune2_3);
        Plateaux[] plateauxJaune3 = new Plateaux[4];
        plateauxJaune3[1] = new Plateaux(tilesJaune3);
        plateauxJaune3[2] = new Plateaux(tilesJaune3_1);
        plateauxJaune3[3] = new Plateaux(tilesJaune3_2);
        plateauxJaune3[0] = new Plateaux(tilesJaune3_3);
        Plateaux[] plateauxJaune4 = new Plateaux[4];
        plateauxJaune4[1] = new Plateaux(tilesJaune4);
        plateauxJaune4[2] = new Plateaux(tilesJaune4_1);
        plateauxJaune4[3] = new Plateaux(tilesJaune4_2);
        plateauxJaune4[0] = new Plateaux(tilesJaune4_3);

        Plateaux[] plateauxVert1 = new Plateaux[4];
        plateauxVert1[3] = new Plateaux(tilesVert1);
        plateauxVert1[0] = new Plateaux(tilesVert1_1);
        plateauxVert1[1] = new Plateaux(tilesVert1_2);
        plateauxVert1[2] = new Plateaux(tilesVert1_3);
        Plateaux[] plateauxVert2 = new Plateaux[4];
        plateauxVert2[3] = new Plateaux(tilesVert2);
        plateauxVert2[0] = new Plateaux(tilesVert2_1);
        plateauxVert2[1] = new Plateaux(tilesVert2_2);
        plateauxVert2[2] = new Plateaux(tilesVert2_3);
        Plateaux[] plateauxVert3 = new Plateaux[4];
        plateauxVert3[3] = new Plateaux(tilesVert3);
        plateauxVert3[0] = new Plateaux(tilesVert3_1);
        plateauxVert3[1] = new Plateaux(tilesVert3_2);
        plateauxVert3[2] = new Plateaux(tilesVert3_3);
        Plateaux[] plateauxVert4 = new Plateaux[4];
        plateauxVert4[3] = new Plateaux(tilesVert4);
        plateauxVert4[0] = new Plateaux(tilesVert4_1);
        plateauxVert4[1] = new Plateaux(tilesVert4_2);
        plateauxVert4[2] = new Plateaux(tilesVert4_3);

        Plateaux[] plateauxBleu1 = new Plateaux[4];
        plateauxBleu1[2] = new Plateaux(tilesBleu1);
        plateauxBleu1[3] = new Plateaux(tilesBleu1_1);
        plateauxBleu1[0] = new Plateaux(tilesBleu1_2);
        plateauxBleu1[1] = new Plateaux(tilesBleu1_3);
        Plateaux[] plateauxBleu2 = new Plateaux[4];
        plateauxBleu2[2] = new Plateaux(tilesBleu2);
        plateauxBleu2[3] = new Plateaux(tilesBleu2_1);
        plateauxBleu2[0] = new Plateaux(tilesBleu2_2);
        plateauxBleu2[1] = new Plateaux(tilesBleu2_3);
        Plateaux[] plateauxBleu3 = new Plateaux[4];
        plateauxBleu3[2] = new Plateaux(tilesBleu3);
        plateauxBleu3[3] = new Plateaux(tilesBleu3_1);
        plateauxBleu3[0] = new Plateaux(tilesBleu3_2);
        plateauxBleu3[1] = new Plateaux(tilesBleu3_3);
        Plateaux[] plateauxBleu4 = new Plateaux[4];
        plateauxBleu4[2] = new Plateaux(tilesBleu4);
        plateauxBleu4[3] = new Plateaux(tilesBleu4_1);
        plateauxBleu4[0] = new Plateaux(tilesBleu4_2);
        plateauxBleu4[1] = new Plateaux(tilesBleu4_3);



        Plateaux[] plateau = new Plateaux[16];
        switch (randCoul[0].nom)
        {
            case "Rouge":
                plateau[0] = plateauxRouge1[0];
                plateau[1] = plateauxRouge2[0];
                plateau[2] = plateauxRouge3[0];
                plateau[3] = plateauxRouge4[0];
                break;
            case "Jaune":
                plateau[0] = plateauxJaune1[0];
                plateau[1] = plateauxJaune2[0];
                plateau[2] = plateauxJaune3[0];
                plateau[3] = plateauxJaune4[0];
                break;
            case "Vert":
                plateau[0] = plateauxVert1[0];
                plateau[1] = plateauxVert2[0];
                plateau[2] = plateauxVert3[0];
                plateau[3] = plateauxVert4[0];
                break;
            case "Bleu":
                plateau[0] = plateauxBleu1[0];
                plateau[1] = plateauxBleu2[0];
                plateau[2] = plateauxBleu3[0];
                plateau[3] = plateauxBleu4[0];
                break;
        }
        switch (randCoul[1].nom)
        {
            case "Rouge":
                plateau[4] = plateauxRouge1[1];
                plateau[5] = plateauxRouge2[1];
                plateau[6] = plateauxRouge3[1];
                plateau[7] = plateauxRouge4[1];
                break;
            case "Jaune":
                plateau[4] = plateauxJaune1[1];
                plateau[5] = plateauxJaune2[1];
                plateau[6] = plateauxJaune3[1];
                plateau[7] = plateauxJaune4[1];
                break;
            case "Vert":
                plateau[4] = plateauxVert1[1];
                plateau[5] = plateauxVert2[1];
                plateau[6] = plateauxVert3[1];
                plateau[7] = plateauxVert4[1];
                break;
            case "Bleu":
                plateau[4] = plateauxBleu1[1];
                plateau[5] = plateauxBleu2[1];
                plateau[6] = plateauxBleu3[1];
                plateau[7] = plateauxBleu4[1];
                break;
        }
        switch (randCoul[2].nom)
        {
            case "Rouge":
                plateau[8] = plateauxRouge1[2];
                plateau[9] = plateauxRouge2[2];
                plateau[10] = plateauxRouge3[2];
                plateau[11] = plateauxRouge4[2];
                break;
            case "Jaune":
                plateau[8] = plateauxJaune1[2];
                plateau[9] = plateauxJaune2[2];
                plateau[10] = plateauxJaune3[2];
                plateau[11] = plateauxJaune4[2];
                break;
            case "Vert":
                plateau[8] = plateauxVert1[2];
                plateau[9] = plateauxVert2[2];
                plateau[10] = plateauxVert3[2];
                plateau[11] = plateauxVert4[2];
                break;
            case "Bleu":
                plateau[8] = plateauxBleu1[2];
                plateau[9] = plateauxBleu2[2];
                plateau[10] = plateauxBleu3[2];
                plateau[11] = plateauxBleu4[2];
                break;
        }
        switch (randCoul[3].nom)
        {
            case "Rouge":
                plateau[12] = plateauxRouge1[3];
                plateau[13] = plateauxRouge2[3];
                plateau[14] = plateauxRouge3[3];
                plateau[15] = plateauxRouge4[3];
                break;
            case "Jaune":
                plateau[12] = plateauxJaune1[3];
                plateau[13] = plateauxJaune2[3];
                plateau[14] = plateauxJaune3[3];
                plateau[15] = plateauxJaune4[3];
                break;
            case "Vert":
                plateau[12] = plateauxVert1[3];
                plateau[13] = plateauxVert2[3];
                plateau[14] = plateauxVert3[3];
                plateau[15] = plateauxVert4[3];
                break;
            case "Bleu":
                plateau[12] = plateauxBleu1[3];
                plateau[13] = plateauxBleu2[3];
                plateau[14] = plateauxBleu3[3];
                plateau[15] = plateauxBleu4[3];
                break;
        }

        int randIndice1 = Random.Range(0, 4);
        int randIndice2 = Random.Range(4, 8);
        int randIndice3 = Random.Range(8, 12);
        int randIndice4 = Random.Range(12, 16);
        

        Tile[] quart1 = plateau[randIndice1].tiles;
        Tile[] quart2 = plateau[randIndice2].tiles;
        Tile[] quart3 = plateau[randIndice3].tiles;
        Tile[] quart4 = plateau[randIndice4].tiles;

        Vector3Int v = new Vector3Int(-16, 7, 0);
        for (int i = 0; i < quart1.Length;)
        {
            for(int j = 0; j < 8 && i < quart1.Length; j++)
            {
                tilemap.SetTile(v, quart1[i]);
                v.x++;
                i++;
            }

            v.x -= 8;
            v.y--;
        }
        v = new Vector3Int(-8, 7, 0);
        for (int i = 0; i < quart2.Length;)
        {
            for (int j = 0; j < 8 && i < quart2.Length; j++)
            {
                if (!(v.x == -8 && v.y == 0))
                {
                    tilemap.SetTile(v, quart2[i]);
                    i++;
                }
                v.x++;
            }

            v.x -= 8;
            v.y--;
        }
        v = new Vector3Int(-8, -1, 0);
        for (int i = 0; i < quart3.Length;)
        {
            for (int j = 0; j < 8 && i < quart3.Length; j++)
            {
                if (!(v.x == -8 && v.y == -1))
                {
                    tilemap.SetTile(v, quart3[i]);
                    i++;
                }
                v.x++;
            }
            v.x -= 8;
            v.y--;
        }
        v = new Vector3Int(-16, -1, 0);
        for (int i = 0; i < quart4.Length;)
        {
            for (int j = 0; j < 8 && i < quart4.Length; j++)
            {
                if(!(v.x == -9 && v.y == -1))
                {
                    tilemap.SetTile(v, quart4[i]);
                    i++;
                }
                v.x++;
            }
            v.x -= 8;
            v.y--;
        }

        for (int i = 0; i < robots.Length; i++)
        {
            robots[i].transform.localPosition = new Vector3(Random.Range(0, 16) * 0.6f, Random.Range(-15,1) * 0.6f, 0);
            if (IsCenter(robots[i].transform.localPosition)||
                IsRobot(robots[i].transform.localPosition,i))
            {
                i--;
            }
        }


    }

    private bool IsRobot(Vector3 pos, int indice)
    {
        bool isRobot = false;
        for(int i = 0; i < indice; i++)
        {
            if (pos.Equals(robots[i].transform.localPosition))
            {
                return true;
            }
        }
        return isRobot;
        
    }
    private bool IsCenter(Vector3 pos)
    {
        return (pos.x == 7 * 0.6f || pos.x == 4.8f) && (pos.y == -7 * 0.6f || pos.y == -4.8f);
    }

    void reshuffle(Couleurs[] couleurs)
    {
        for (int t = 0; t < couleurs.Length; t++)
        {
            Couleurs tmp = couleurs[t];
            int r = Random.Range(t, couleurs.Length);
            couleurs[t] = couleurs[r];
            couleurs[r] = tmp;
        }
    }

}
