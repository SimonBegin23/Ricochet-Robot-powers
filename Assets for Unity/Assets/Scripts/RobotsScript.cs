using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Linq;

public class RobotsScript : MonoBehaviour
{
    [SerializeField] private GameObject Blue;         //Telepuerte
    [SerializeField] private GameObject Green;        //4K_projector
    [SerializeField] private GameObject Red;          //The_graber
    [SerializeField] private GameObject Yellow;       //Doormamu
    [SerializeField] private GameObject Gray;         //Lemillion
    [SerializeField] private GameObject GreenHologram;//4K_hologram

    [SerializeField] private Tilemap map;

    [SerializeField] private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    [SerializeField] private Text text;
    [SerializeField] private Tile[] ricochetTiles;

    private SettingPowersScript singleton;

    private bool blueButtonPressed = false;
    private bool greenButtonPressed = false;
    private bool redButtonPressed = false;
    private bool redButton1Pressed = false;
    private bool yellowButtonPressed = false;

    private bool isGrapled = false;
    public bool hitRicochet = false;
    public GameObject hitRicochetRobot = null;
    private string directionAfterRicochet = "null";
    public List<GameObject> IsGrapledList = new List<GameObject>();
    private List<float> distances = new List<float>();
    private float distWall = 101;
    private Vector3 basePos = Vector3.zero;
    public Vector3 nextPosition = new Vector3(0, 0, 0);

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (Tile tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
        singleton = SettingPowersScript.instance;
        for (int i = 0; i < 7; i++)
        {
            distances.Add(100);
        }
    }



    void OnMouseUp()
    {
        basePos = transform.position;
        nextPosition = new Vector3(0, 0, 0);
        string direction = "None";
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = "Left";
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = "Right";
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = "Up";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = "Down";
        }

        nextPosition = NextPos(transform.position, direction);
        if (IsGrapledList.Count == 0 && !this.name.Contains("4K_hologram"))
        {
            Red.GetComponent<RobotsScript>().IsGrapledList.Add(Red);
            Blue.GetComponent<RobotsScript>().IsGrapledList.Add(Red);
            Green.GetComponent<RobotsScript>().IsGrapledList.Add(Red);
            Yellow.GetComponent<RobotsScript>().IsGrapledList.Add(Red);
            Gray.GetComponent<RobotsScript>().IsGrapledList.Add(Red);
        }
        if (isGrapled)
        {
            if (singleton.redPowerIsOn == true && redButtonPressed)
            {
                Red.GetComponent<RobotsScript>().RedButtonUnpress();
                Blue.GetComponent<RobotsScript>().RedButtonUnpress();
                Green.GetComponent<RobotsScript>().RedButtonUnpress();
                Yellow.GetComponent<RobotsScript>().RedButtonUnpress();
                Gray.GetComponent<RobotsScript>().RedButtonUnpress();
            }
            if (singleton.bluePowerIsOn == true && Blue.GetComponent<RobotsScript>().GetIsGrapled() && blueButtonPressed)
            {
                List<Vector3> nextPoss = new List<Vector3>();
                foreach (GameObject go in IsGrapledList)
                {
                    go.GetComponent<RobotsScript>().nextPosition = NextPos(go.transform.position, direction);
                    Vector3 nextPosition1 = NextPos(go.GetComponent<RobotsScript>().nextPosition, direction);
                    Vector3 nextPosition2 = NextPos(nextPosition1, direction);
                    Vector3 nextPosition3 = NextPos(nextPosition2, direction);
                    nextPoss.Add(nextPosition3);
                }
                bool isPossible = true;
                foreach (Vector3 nextPos in nextPoss)
                {
                    if (IsRobot(nextPos) || IsCenter(nextPos) || IsBorder(nextPos, direction))
                    {
                        isPossible = false;
                        break;
                    }
                }
                if (isPossible)
                {
                    foreach (GameObject go in IsGrapledList)
                    {
                        go.GetComponent<RobotsScript>().nextPosition = NextPos(go.transform.position, direction);
                        Vector3 nextPosition1 = NextPos(go.GetComponent<RobotsScript>().nextPosition, direction);
                        Vector3 nextPosition2 = NextPos(nextPosition1, direction);
                        Vector3 nextPosition3 = NextPos(nextPosition2, direction);
                        go.transform.position = nextPosition3;
                    }
                }

                blueButtonPressed = false;
            }
            else if (singleton.redPowerIsOn == true && redButton1Pressed)
            {
                SetIsUngrapled();
                switch (this.name)
                {
                    case "4K_projector":
                        Red.GetComponent<RobotsScript>().IsGrapledList.Remove(Green);
                        Blue.GetComponent<RobotsScript>().IsGrapledList.Remove(Green);
                        Green.GetComponent<RobotsScript>().IsGrapledList.Remove(Green);
                        Yellow.GetComponent<RobotsScript>().IsGrapledList.Remove(Green);
                        Gray.GetComponent<RobotsScript>().IsGrapledList.Remove(Green);
                        break;
                    case "Telepuerte":
                        Red.GetComponent<RobotsScript>().IsGrapledList.Remove(Blue);
                        Blue.GetComponent<RobotsScript>().IsGrapledList.Remove(Blue);
                        Green.GetComponent<RobotsScript>().IsGrapledList.Remove(Blue);
                        Yellow.GetComponent<RobotsScript>().IsGrapledList.Remove(Blue);
                        Gray.GetComponent<RobotsScript>().IsGrapledList.Remove(Blue);
                        break;
                    case "Doormamu":
                        Red.GetComponent<RobotsScript>().IsGrapledList.Remove(Yellow);
                        Blue.GetComponent<RobotsScript>().IsGrapledList.Remove(Yellow);
                        Green.GetComponent<RobotsScript>().IsGrapledList.Remove(Yellow);
                        Yellow.GetComponent<RobotsScript>().IsGrapledList.Remove(Yellow);
                        Gray.GetComponent<RobotsScript>().IsGrapledList.Remove(Yellow);
                        break;
                    case "Lemillion":
                        Red.GetComponent<RobotsScript>().IsGrapledList.Remove(Gray);
                        Blue.GetComponent<RobotsScript>().IsGrapledList.Remove(Gray);
                        Green.GetComponent<RobotsScript>().IsGrapledList.Remove(Gray);
                        Yellow.GetComponent<RobotsScript>().IsGrapledList.Remove(Gray);
                        Gray.GetComponent<RobotsScript>().IsGrapledList.Remove(Gray);
                        break;
                }
                if (IsGrapledList.Count == 1)
                {
                    Red.GetComponent<RobotsScript>().SetIsUngrapled();
                }
                Red.GetComponent<RobotsScript>().RedButton1Unpress();
                Blue.GetComponent<RobotsScript>().RedButton1Unpress();
                Green.GetComponent<RobotsScript>().RedButton1Unpress();
                Yellow.GetComponent<RobotsScript>().RedButton1Unpress();
                Gray.GetComponent<RobotsScript>().RedButton1Unpress();
            }
            else if (!direction.Contains("None"))
            {
                do
                {
                    foreach (GameObject go in IsGrapledList)
                    {
                        go.GetComponent<RobotsScript>().hitRicochet = false;
                    }
                    moveAllRobotsGrapled(direction);
                    if (hitRicochet)
                    {
                        direction = directionAfterRicochet;
                    }
                } while (hitRicochet);
            }

        }
        else
        {
            if (singleton.redPowerIsOn == true && redButton1Pressed)
            {
                Red.GetComponent<RobotsScript>().RedButton1Unpress();
                Blue.GetComponent<RobotsScript>().RedButton1Unpress();
                Green.GetComponent<RobotsScript>().RedButton1Unpress();
                Yellow.GetComponent<RobotsScript>().RedButton1Unpress();
                Gray.GetComponent<RobotsScript>().RedButton1Unpress();
            }
            if (singleton.greenPowerIsOn == true && this.name.Contains("4K_projector") && !greenButtonPressed)
            {
                GreenHologram.SetActive(false);
                SetGreenHoloPos();
            }
            if (singleton.bluePowerIsOn == true && this.name.Contains("Telepuerte") && blueButtonPressed)
            {

                nextPosition = NextPos(transform.position, direction);
                Vector3 nextPosition1 = NextPos(nextPosition, direction);
                Vector3 nextPosition2 = NextPos(nextPosition1, direction);
                Vector3 nextPosition3 = NextPos(nextPosition2, direction);
                if (!(IsRobot(nextPosition3) || IsCenter(nextPosition3) || IsBorder(nextPosition3, direction)))
                {
                    transform.position = nextPosition3;
                }
                blueButtonPressed = false;
            }
            else if (singleton.greenPowerIsOn == true && this.name.Contains("4K_projector") && greenButtonPressed)
            {
                if (!IsBorder(NextPos(Green.transform.position, "Up"), "Up") && !IsRobot(NextPos(Green.transform.position, "Up")))
                {
                    GreenHologram.transform.position = NextPos(Green.transform.position, "Up");
                }
                else if (!IsBorder(NextPos(Green.transform.position, "Down"), "Down") && !IsRobot(NextPos(Green.transform.position, "Down")))
                {
                    GreenHologram.transform.position = NextPos(Green.transform.position, "Down");
                }
                else
                {
                    if (IsRobot(NextPos(Green.transform.position, "Left")))
                        GreenHologram.transform.position = NextPos(Green.transform.position, "Right");
                    else
                        GreenHologram.transform.position = NextPos(Green.transform.position, "Left");
                }

                GreenHologram.SetActive(true);
                greenButtonPressed = false;
            }
            else if (this.name.Contains("4K_hologram"))
            {
                transform.position = nextPosition;
                nextPosition = NextPos(transform.position, direction);
            }
            else if (singleton.redPowerIsOn == true && redButtonPressed)
            {
                GameObject go = new GameObject();
                switch (this.name)
                {
                    case "4K_projector":
                        go = Green;
                        break;
                    case "Telepuerte":
                        go = Blue;
                        break;
                    case "Doormamu":
                        go = Yellow;
                        break;
                    case "Lemillion":
                        go = Gray;
                        break;
                }

                if (IsNextToRed(go) && IsNotGrapledRobot(go))
                {
                    SetIsGrapled();
                    if (!Red.GetComponent<RobotsScript>().GetIsGrapled())
                    {
                        Red.GetComponent<RobotsScript>().SetIsGrapled();
                    }
                    switch (this.name)
                    {
                        case "4K_projector":
                            Red.GetComponent<RobotsScript>().IsGrapledList.Add(Green);
                            Blue.GetComponent<RobotsScript>().IsGrapledList.Add(Green);
                            Green.GetComponent<RobotsScript>().IsGrapledList.Add(Green);
                            Yellow.GetComponent<RobotsScript>().IsGrapledList.Add(Green);
                            Gray.GetComponent<RobotsScript>().IsGrapledList.Add(Green);
                            break;
                        case "Telepuerte":
                            Red.GetComponent<RobotsScript>().IsGrapledList.Add(Blue);
                            Blue.GetComponent<RobotsScript>().IsGrapledList.Add(Blue);
                            Green.GetComponent<RobotsScript>().IsGrapledList.Add(Blue);
                            Yellow.GetComponent<RobotsScript>().IsGrapledList.Add(Blue);
                            Gray.GetComponent<RobotsScript>().IsGrapledList.Add(Blue);
                            break;
                        case "Doormamu":
                            Red.GetComponent<RobotsScript>().IsGrapledList.Add(Yellow);
                            Blue.GetComponent<RobotsScript>().IsGrapledList.Add(Yellow);
                            Green.GetComponent<RobotsScript>().IsGrapledList.Add(Yellow);
                            Yellow.GetComponent<RobotsScript>().IsGrapledList.Add(Yellow);
                            Gray.GetComponent<RobotsScript>().IsGrapledList.Add(Yellow);
                            break;
                        case "Lemillion":
                            Red.GetComponent<RobotsScript>().IsGrapledList.Add(Gray);
                            Blue.GetComponent<RobotsScript>().IsGrapledList.Add(Gray);
                            Green.GetComponent<RobotsScript>().IsGrapledList.Add(Gray);
                            Yellow.GetComponent<RobotsScript>().IsGrapledList.Add(Gray);
                            Gray.GetComponent<RobotsScript>().IsGrapledList.Add(Gray);
                            break;
                    }
                }
                else { print("no"); }
                Red.GetComponent<RobotsScript>().RedButtonUnpress();
                Blue.GetComponent<RobotsScript>().RedButtonUnpress();
                Green.GetComponent<RobotsScript>().RedButtonUnpress();
                Yellow.GetComponent<RobotsScript>().RedButtonUnpress();
                Gray.GetComponent<RobotsScript>().RedButtonUnpress();
            }
            else if (singleton.yellowPowerIsOn == true && this.name.Contains("Doormamu") && yellowButtonPressed)
            {
                moveRobotYellow(nextPosition, direction);
            }
            else if (singleton.grayPowerIsOn == true && this.name.Contains("Lemillion"))
            {
                moveRobotGray(nextPosition, direction);
            }
            else { moveRobot(nextPosition, direction); }
        }


        if (!(this.name.Contains("4K_hologram") || basePos == transform.position))
            IncrementCount();

    }

    bool IsRobot(Vector3 nextPosition)
    {
        if (nextPosition == Blue.transform.position || nextPosition == Green.transform.position ||
                nextPosition == Red.transform.position || nextPosition == Yellow.transform.position ||
                nextPosition == Gray.transform.position || nextPosition == GreenHologram.transform.position)
            return true;
        else
            return false;
    }
    bool IsNotGrapledRobot(Vector3 nextPosition)
    {
        List<GameObject> allRobots = new List<GameObject>();
        allRobots.Add(Blue);
        allRobots.Add(Green);
        allRobots.Add(Red);
        allRobots.Add(Yellow);
        allRobots.Add(Gray);
        allRobots.Add(GreenHologram);
        for (int i = 0; i < IsGrapledList.Count; i++)
        {
            allRobots.Remove(IsGrapledList[i]);
        }
        foreach (GameObject robot in allRobots)
        {
            if (nextPosition == robot.transform.position)
                return true;
        }
        return false;
    }
    bool IsNotGrapledRobot(GameObject robot)
    {
        List<GameObject> notGrapledRobots = new List<GameObject>();
        notGrapledRobots.Add(Blue);
        notGrapledRobots.Add(Green);
        notGrapledRobots.Add(Red);
        notGrapledRobots.Add(Yellow);
        notGrapledRobots.Add(Gray);
        notGrapledRobots.Add(GreenHologram);
        for (int i = 0; i < IsGrapledList.Count; i++)
        {
            notGrapledRobots.Remove(IsGrapledList[i]);
        }
        foreach (GameObject nGRobot in notGrapledRobots)
        {
            if (nGRobot.name.Contains(robot.name))
                return true;
        }
        return false;
    }

    bool IsBorder(Vector3 nextPosition, string direction)
    {
        switch (direction)
        {
            case "Left":
                return (decimal)Mathf.Round(nextPosition.x * 10) < -78;
            case "Right":
                return (decimal)Mathf.Round(nextPosition.x * 10) > 12;
            case "Up":
                return (decimal)Mathf.Round(nextPosition.y * 10) > 45;
            case "Down":
                return (decimal)Mathf.Round(nextPosition.y * 10) < -45;
            default: return false;
        }
    }
    bool IsCenter(Vector3 nextPosition)
    {
        return ((decimal)Mathf.Round(nextPosition.y * 10) == 3 || ((decimal)Mathf.Round(nextPosition.y * 10)) == -3) &&
                    ((decimal)Mathf.Round(nextPosition.x * 10)) == -30 || ((decimal)Mathf.Round(nextPosition.y * 10) == 3 || ((decimal)Mathf.Round(nextPosition.y * 10)) == -3) &&
                ((decimal)Mathf.Round(nextPosition.x * 10)) == -36 || ((decimal)Mathf.Round(nextPosition.x * 10) == -36 || ((decimal)Mathf.Round(nextPosition.x * 10)) == -30) &&
                ((decimal)Mathf.Round(nextPosition.y * 10)) == -03 || ((decimal)Mathf.Round(nextPosition.x * 10) == -36 || ((decimal)Mathf.Round(nextPosition.x * 10)) == -30) &&
                ((decimal)Mathf.Round(nextPosition.y * 10)) == 03;
    }
    bool IsRicochet(Vector3 nextPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(nextPosition);

        TileBase nextTile = map.GetTile(gridPosition);

        for (int i = 0; i < ricochetTiles.Length; i++)
        {
            if (ricochetTiles[i] == nextTile)
            {
                return true;
            }

        }
        return false;
    }
    bool IsSameColorRicochet(TileBase nextTile)
    {
        if (nextTile.name.Contains("Bleu") && name.Equals("Telepuerte"))
        {
            return true;
        }
        if (nextTile.name.Contains("Vert") && name.Equals("4K_projector"))
        {
            return true;
        }
        if (nextTile.name.Contains("Rouge") && name.Equals("The_graber"))
        {
            return true;
        }
        if (nextTile.name.Contains("Jaune") && name.Equals("Doormamu"))
        {
            return true;
        }

        return false;
    }
    bool CanExitCurrent(Vector3 position, string direction)
    {
        switch (direction)
        {
            case "Left":
                Vector3Int gridPosition = map.WorldToCell(position);

                TileBase currTile = map.GetTile(gridPosition);

                for (int i = 0; i < dataFromTiles[currTile].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile].exitsDirections[i].Equals(direction))
                        return true;
                }
                return false;
            case "Right":
                Vector3Int gridPosition1 = map.WorldToCell(position);

                TileBase currTile1 = map.GetTile(gridPosition1);

                for (int i = 0; i < dataFromTiles[currTile1].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile1].exitsDirections[i].Equals(direction))
                        return true;
                }
                return false;
            case "Up":
                Vector3Int gridPosition2 = map.WorldToCell(position);

                TileBase currTile2 = map.GetTile(gridPosition2);

                for (int i = 0; i < dataFromTiles[currTile2].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile2].exitsDirections[i].Equals(direction))
                        return true;
                }
                return false;
            case "Down":
                Vector3Int gridPosition3 = map.WorldToCell(position);

                TileBase currTile3 = map.GetTile(gridPosition3);

                for (int i = 0; i < dataFromTiles[currTile3].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile3].exitsDirections[i].Equals(direction))
                        return true;
                }
                return false;
            default: return false;
        }
    }
    bool CanEnterNext(Vector3 nextPosition, string direction)
    {
        switch (direction)
        {
            case "Left":
                Vector3Int gridPosition = map.WorldToCell(nextPosition);

                TileBase nextTile = map.GetTile(gridPosition);

                for (int i = 0; i < dataFromTiles[nextTile].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[nextTile].exitsDirections[i].Equals("Right"))
                        return true;
                }
                return false;
            case "Right":
                Vector3Int gridPosition1 = map.WorldToCell(nextPosition);

                TileBase currTile1 = map.GetTile(gridPosition1);

                for (int i = 0; i < dataFromTiles[currTile1].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile1].exitsDirections[i].Equals("Left"))
                        return true;
                }
                return false;
            case "Up":
                Vector3Int gridPosition2 = map.WorldToCell(nextPosition);

                TileBase currTile2 = map.GetTile(gridPosition2);

                for (int i = 0; i < dataFromTiles[currTile2].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile2].exitsDirections[i].Equals("Down"))
                        return true;
                }
                return false;
            case "Down":
                Vector3Int gridPosition3 = map.WorldToCell(nextPosition);

                TileBase currTile3 = map.GetTile(gridPosition3);

                for (int i = 0; i < dataFromTiles[currTile3].exitsDirections.Length; i++)
                {
                    if (dataFromTiles[currTile3].exitsDirections[i].Equals("Up"))
                        return true;
                }
                return false;
            default: return false;
        }
    }

    void IncrementCount()
    {
        int count = int.Parse(text.text.Substring(0));
        count++;
        text.text = count.ToString();
    }

    Vector3 NextPos(Vector3 pos, string direction)
    {
        if (direction.Equals("Left"))
            return new Vector3(pos.x - 0.6f, pos.y, 0);
        if (direction.Equals("Right"))
            return new Vector3(pos.x + 0.6f, pos.y, 0);
        if (direction.Equals("Up"))
            return new Vector3(pos.x, pos.y + 0.6f, 0);
        if (direction.Equals("Down"))
            return new Vector3(pos.x, pos.y - 0.6f, 0);
        else return pos;
    }

    public void BlueButtonPress() { blueButtonPressed = true; }
    public void GreenButtonPress() { greenButtonPressed = true; }
    public void RedButtonPress() { redButtonPressed = true; }
    public void RedButtonUnpress() { redButtonPressed = false; }
    public void RedButton1Press() { redButton1Pressed = true; }
    public void RedButton1Unpress() { redButton1Pressed = false; }
    public void YellowButtonPress() { yellowButtonPressed = true; }
    public void SetGreenHoloPos() { GreenHologram.transform.position = new Vector3(-8.8f, 4.5f, 0); }

    public void SetIsGrapled() { isGrapled = true; }
    public void SetIsUngrapled() { isGrapled = false; }
    public void SetIsUngrapledAll()
    {
        IsGrapledList.Clear();
        SetIsUngrapled();
    }
    public bool GetIsGrapled() { return isGrapled; }

    private bool IsNextToRed(GameObject robot)
    {
        if (Mathf.Abs(robot.transform.position.y - Red.transform.position.y) < 0.7f && Mathf.Round(10 * robot.transform.position.x) - Mathf.Round(10 * Red.transform.position.x) == 0 ||
            Mathf.Abs(robot.transform.position.x - Red.transform.position.x) < 0.7f && Mathf.Round(10 * robot.transform.position.y) - Mathf.Round(10 * Red.transform.position.y) == 0)
            return true;
        else
            return false;
    }

    private void moveRobot(Vector3 nextPosition, string direction)
    {
        while (!(IsRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction) ||
            !CanExitCurrent(transform.position, direction) || !CanEnterNext(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[0];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[1];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[2];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[3];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
            }

        }
    }
    private void moveRobotGray(Vector3 nextPosition, string direction)
    {
        while (!(IsRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[0];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[1];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[2];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                            direction = dataFromTiles[nextTile].exitsDirections[3];
                    }
                    transform.position = nextPosition;
                    nextPosition = NextPos(transform.position, direction);
                    break;
            }

        }
    }
    private void moveRobotYellow(Vector3 nextPosition, string direction)
    {
        while (!(IsRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            if (yellowButtonPressed && !CanExitCurrent(transform.position, direction) || !CanEnterNext(nextPosition, direction))
            {

                transform.position = nextPosition;
                nextPosition = NextPos(transform.position, direction);
                yellowButtonPressed = false;
                break;
            }
            else
            {
                switch (direction)
                {
                    case "Left":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                                direction = dataFromTiles[nextTile].exitsDirections[0];
                        }
                        transform.position = nextPosition;
                        nextPosition = NextPos(transform.position, direction);
                        break;
                    case "Right":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                                direction = dataFromTiles[nextTile].exitsDirections[1];
                        }
                        transform.position = nextPosition;
                        nextPosition = NextPos(transform.position, direction);
                        break;
                    case "Up":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                                direction = dataFromTiles[nextTile].exitsDirections[2];
                        }
                        transform.position = nextPosition;
                        nextPosition = NextPos(transform.position, direction);
                        break;
                    case "Down":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                                direction = dataFromTiles[nextTile].exitsDirections[3];
                        }
                        transform.position = nextPosition;
                        nextPosition = NextPos(transform.position, direction);
                        break;
                }
            }


        }
        moveRobot(nextPosition, direction);
    }

    private float moveRobotGrapled(Vector3 nextPosition, string direction)
    {
        string originalDirection = direction;
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction) ||
            !CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                        {
                            direction = dataFromTiles[nextTile].exitsDirections[0];
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                                go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                        {
                            direction = dataFromTiles[nextTile].exitsDirections[1];
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                                go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                        {
                            direction = dataFromTiles[nextTile].exitsDirections[2];
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                                go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        if (!IsSameColorRicochet(nextTile))
                        {
                            direction = dataFromTiles[nextTile].exitsDirections[3];
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                                go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
            }
            if (hitRicochet) { break; }


        }
        if (!hitRicochet)
        {
            foreach (GameObject go in IsGrapledList)
            {
                go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
            }
        }
        Vector3 newPos = nextPosition1;
        direction = originalDirection;
        float distx = Mathf.Abs(newPos.x - basePos.x);
        float disty = Mathf.Abs(newPos.y - basePos.y);
        float totDist = distx + disty;
        return totDist;
    }
    private float moveRobotYellowGrapled(Vector3 nextPosition, string direction)
    {
        string originalDirection = direction;
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        bool yellowButtonState = yellowButtonPressed;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            if (yellowButtonPressed && (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction) || IsRicochet(nextPosition)))
            {
                switch (direction)
                {
                    case "Left":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[0];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        if (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction))
                        {
                            Vector3 newPos1 = nextPosition1;
                            float distx1 = Mathf.Abs(newPos1.x - basePos.x);
                            float disty1 = Mathf.Abs(newPos1.y - basePos.y);
                            distWall = distx1 + disty1;
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        if (!hitRicochet)
                        {
                            yellowButtonPressed = false;
                        }

                        break;
                    case "Right":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[1];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        if (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction))
                        {
                            Vector3 newPos1 = nextPosition1;
                            float distx1 = Mathf.Abs(newPos1.x - basePos.x);
                            float disty1 = Mathf.Abs(newPos1.y - basePos.y);
                            distWall = distx1 + disty1;
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        if (!hitRicochet)
                        {
                            yellowButtonPressed = false;
                        }
                        break;
                    case "Up":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[2];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        if (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction))
                        {
                            Vector3 newPos1 = nextPosition1;
                            float distx1 = Mathf.Abs(newPos1.x - basePos.x);
                            float disty1 = Mathf.Abs(newPos1.y - basePos.y);
                            distWall = distx1 + disty1;
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        if (!hitRicochet)
                        {
                            yellowButtonPressed = false;
                        }
                        break;
                    case "Down":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            direction = dataFromTiles[nextTile].exitsDirections[3];
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        if (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction))
                        {
                            Vector3 newPos1 = nextPosition1;
                            float distx1 = Mathf.Abs(newPos1.x - basePos.x);
                            float disty1 = Mathf.Abs(newPos1.y - basePos.y);
                            distWall = distx1 + disty1;
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        if (!hitRicochet)
                        {
                            yellowButtonPressed = false;
                        }
                        break;
                }
                
            }
            else if (CanExitCurrent(nextPosition1, direction) && CanEnterNext(nextPosition, direction))
            {
                switch (direction)
                {
                    case "Left":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[0];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Right":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[1];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Up":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[2];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Down":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            direction = dataFromTiles[nextTile].exitsDirections[3];
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                }
            }
            else
                break;
            if (hitRicochet) { break; }
            

        }
        float Dist2 = 0;

        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
        }
        yellowButtonPressed = yellowButtonState;
        Vector3 newPos = nextPosition1;
        direction = originalDirection;
        float distx = Mathf.Abs(newPos.x - basePos.x);
        float disty = Mathf.Abs(newPos.y - basePos.y);
        float Dist1 = distx + disty;

        float totDist = Dist1 + Dist2;
        return totDist;
    }
    private float moveRobotGrayGrapled(Vector3 nextPosition, string direction)
    {
        string originalDirection = direction;
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        direction = dataFromTiles[nextTile].exitsDirections[0];
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                            go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        direction = dataFromTiles[nextTile].exitsDirections[1];
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                            go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        direction = dataFromTiles[nextTile].exitsDirections[2];
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                            go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        direction = dataFromTiles[nextTile].exitsDirections[3];
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                            go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
            }
            if (hitRicochet) { break; }
        }
        if (!hitRicochet)
        {
            foreach (GameObject go in IsGrapledList)
            {
                go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
            }
        }
        Vector3 newPos = nextPosition1;
        direction = originalDirection;
        float distx = Mathf.Abs(newPos.x - basePos.x);
        float disty = Mathf.Abs(newPos.y - basePos.y);
        float totDist = distx + disty;
        return totDist;
    }

    private Vector3 moveRobotGrapledVector(Vector3 nextPosition, string direction)
    {
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction) ||
            !CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[0];
                        if (!IsSameColorRicochet(nextTile))
                        {
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[1];
                        if (!IsSameColorRicochet(nextTile))
                        {
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[2];
                        if (!IsSameColorRicochet(nextTile))
                        {
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[3];
                        if (!IsSameColorRicochet(nextTile))
                        {
                            foreach (GameObject go in IsGrapledList)
                            {
                                go.GetComponent<RobotsScript>().hitRicochet = true;
                            }
                        }

                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
            }
            if (hitRicochet) { break; }

        }
        if (!hitRicochet)
        {
            foreach (GameObject go in IsGrapledList)
            {
                go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
            }
        }
        Vector3 newPos = nextPosition1;
        Vector3 moveVector = newPos - basePos;
        return moveVector;
    }
    private Vector3 moveRobotYellowGrapledVector(Vector3 nextPosition, string direction)
    {
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            if (yellowButtonPressed && (!CanExitCurrent(nextPosition1, direction) || !CanEnterNext(nextPosition, direction) || IsRicochet(nextPosition)))
            {
                switch (direction)
                {
                    case "Left":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[0];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        break;

                    case "Right":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[1];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        break;
                    case "Up":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[2];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        break;
                    case "Down":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            
                            if (!IsSameColorRicochet(nextTile))
                            {
                                direction = dataFromTiles[nextTile].exitsDirections[3];
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = direction;
                                }
                            }
                        }
                        break;
                }
                nextPosition1 = nextPosition;
                nextPosition = NextPos(nextPosition1, direction);
                if (!hitRicochet)
                {
                    yellowButtonPressed = false;
                }
            }
            else if (CanExitCurrent(nextPosition1, direction) && CanEnterNext(nextPosition, direction))
            {
                switch (direction)
                {
                    case "Left":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[0];
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Right":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[1];
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Up":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[2];
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                    case "Down":
                        if (IsRicochet(nextPosition))
                        {
                            Vector3Int gridPosition = map.WorldToCell(nextPosition);

                            TileBase nextTile = map.GetTile(gridPosition);
                            if (!IsSameColorRicochet(nextTile))
                            {
                                foreach (GameObject go in IsGrapledList)
                                {
                                    go.GetComponent<RobotsScript>().hitRicochet = true;
                                    go.GetComponent<RobotsScript>().directionAfterRicochet = dataFromTiles[nextTile].exitsDirections[3];
                                }
                            }
                        }
                        nextPosition1 = nextPosition;
                        nextPosition = NextPos(nextPosition1, direction);
                        break;
                }
            }
            else
                break;
            if (hitRicochet) { break; }

        }
        if (!hitRicochet)
        {
            foreach (GameObject go in IsGrapledList)
            {
                go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
            }
        }
        Vector3 newPos = nextPosition1;

        Vector3 moveVector = newPos - basePos;
        return moveVector;
    }
    private Vector3 moveRobotGrayGrapledVector(Vector3 nextPosition, string direction)
    {
        Vector3 basePos = transform.position;
        Vector3 nextPosition1 = transform.position;
        bool hitRicochetState = hitRicochet;
        foreach (GameObject go in IsGrapledList)
        {
            go.GetComponent<RobotsScript>().hitRicochet = false;
        }
        while (!(IsNotGrapledRobot(nextPosition) || IsCenter(nextPosition) || IsBorder(nextPosition, direction)))
        {
            switch (direction)
            {
                case "Left":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Right":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Up":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
                case "Down":
                    if (IsRicochet(nextPosition))
                    {
                        Vector3Int gridPosition = map.WorldToCell(nextPosition);

                        TileBase nextTile = map.GetTile(gridPosition);
                        foreach (GameObject go in IsGrapledList)
                        {
                            go.GetComponent<RobotsScript>().hitRicochet = true;
                        }
                    }
                    nextPosition1 = nextPosition;
                    nextPosition = NextPos(nextPosition1, direction);
                    break;
            }
            if (hitRicochet) { break; }

        }
        if (!hitRicochet)
        {
            foreach (GameObject go in IsGrapledList)
            {
                go.GetComponent<RobotsScript>().hitRicochet = hitRicochetState;
            }
        }
        Vector3 newPos = nextPosition1;
        Vector3 moveVector = newPos - basePos;
        return moveVector;
    }
    private void moveAllRobotsGrapled(string direction)
    {
        foreach (GameObject go in IsGrapledList)
        {
            if (singleton.greenPowerIsOn == true && go.name.Contains("4K_projector") && !greenButtonPressed)
            {
                GreenHologram.SetActive(false);
                SetGreenHoloPos();
            }
            if (singleton.greenPowerIsOn == true && go.name.Contains("4K_projector") && greenButtonPressed)
            {

                if (!IsBorder(NextPos(Green.transform.position, "Up"), "Up") && !IsRobot(NextPos(Green.transform.position, "Up")))
                {
                    GreenHologram.transform.position = NextPos(Green.transform.position, "Up");
                }
                else if (!IsBorder(NextPos(Green.transform.position, "Down"), "Down") && !IsRobot(NextPos(Green.transform.position, "Down")))
                {
                    GreenHologram.transform.position = NextPos(Green.transform.position, "Down");
                }
                else
                {
                    if (IsRobot(NextPos(Green.transform.position, "Left")))
                        GreenHologram.transform.position = NextPos(Green.transform.position, "Right");
                    else
                        GreenHologram.transform.position = NextPos(Green.transform.position, "Left");
                }
                GreenHologram.SetActive(true);
                greenButtonPressed = false;
            }
            else if (singleton.yellowPowerIsOn == true && go.name.Contains("Doormamu") && yellowButtonPressed)
            {
                distances[5] = moveRobotYellowGrapled(NextPos(go.transform.position, direction), direction);
                distances[3] = 100;
            }
            else if (singleton.grayPowerIsOn == true && go.name.Contains("Lemillion"))
            {
                distances[6] = go.GetComponent<RobotsScript>().moveRobotGrayGrapled(NextPos(go.transform.position, direction), direction);
            }
            else
            {
                float dist = go.GetComponent<RobotsScript>().moveRobotGrapled(NextPos(go.transform.position, direction), direction);
                switch (go.name)
                {
                    case "Telepuerte":
                        distances[0] = dist;
                        break;
                    case "4K_projector":
                        distances[1] = dist;
                        break;
                    case "The_graber":
                        distances[2] = dist;
                        break;
                    case "Doormamu":
                        distances[3] = dist;
                        break;
                    case "Lemillion":
                        distances[4] = dist;
                        break;

                }
            }
            foreach (GameObject go1 in IsGrapledList)
            {
                go1.GetComponent<RobotsScript>().hitRicochet = false;
            }
        }
        int index = 0;
        float minValue = distances[0];
        for (int i = 1; i < distances.Count; i++)
        {
            if (distances[i] < minValue)
            {
                minValue = distances[i];
                index = i;
            }
        }
        for (int i = 0; i < distances.Count; i++)
        {
            distances[i] = 100;
        }
        Vector3 moveVectorGrapled = Vector3.zero;
        switch (index)
        {
            case 0:
                moveVectorGrapled = Blue.GetComponent<RobotsScript>().moveRobotGrapledVector(NextPos(Blue.transform.position, direction), direction); break;
            case 1:
                moveVectorGrapled = Green.GetComponent<RobotsScript>().moveRobotGrapledVector(NextPos(Green.transform.position, direction), direction); break;
            case 2:
                moveVectorGrapled = Red.GetComponent<RobotsScript>().moveRobotGrapledVector(NextPos(Red.transform.position, direction), direction); break;
            case 3:
                moveVectorGrapled = Yellow.GetComponent<RobotsScript>().moveRobotGrapledVector(NextPos(Yellow.transform.position, direction), direction); break;
            case 4:
                moveVectorGrapled = Gray.GetComponent<RobotsScript>().moveRobotGrapledVector(NextPos(Gray.transform.position, direction), direction); break;
            case 5:
                moveVectorGrapled = Yellow.GetComponent<RobotsScript>().moveRobotYellowGrapledVector(NextPos(Yellow.transform.position, direction), direction); break;
            case 6:
                moveVectorGrapled = Gray.GetComponent<RobotsScript>().moveRobotGrayGrapledVector(NextPos(Gray.transform.position, direction), direction); break;
        }
        if (distWall <= minValue)
        {
            yellowButtonPressed = false;
        }

        foreach (GameObject go in IsGrapledList)
        {
            go.transform.position = go.transform.position + moveVectorGrapled;
        }
    }
}