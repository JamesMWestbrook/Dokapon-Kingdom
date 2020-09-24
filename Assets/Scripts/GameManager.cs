using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera camera;
    public List<Player> Players;
    public int PlayerTurn;
    public int GameTurn;
    public GameStates GameState;
    public enum GameStates
    {
        Battle,
        Movement,
        Cutscene

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartTurn(Players[0]);
    }

    void Update()
    {
        switch (GameState)
        {

            case GameStates.Battle:
                break;
            case GameStates.Movement:
                string input = "";
                Player player = Players[PlayerTurn];

                if (Input.GetKeyDown("up"))
                {
                    input = "up";
                }
                else if (Input.GetKeyDown("down"))
                {
                    input = "down";
                }
                else if (Input.GetKeyDown("left"))
                {
                    input = "left";
                }
                else if (Input.GetKeyDown("right"))
                {
                    input = "right";
                }

                if (input != "") MoveTile(input, player);

                break;
            case GameStates.Cutscene:
                break;
        }
    }
    public bool IsTurn(Player player)
    {
        return Players.IndexOf(player) == PlayerTurn;
    }
    public void StartTurn(Player player)
    {
        //every player in a node, assign them a spot
        SetNodeSpot();

        GameState = GameStates.Movement;
        //Roll dice I guess?
        //start movement
    }

    public void SetNodeSpot()
    {
        List<TileBase> activeNodes = new List<TileBase>();
        foreach (Player player in Players)
        {
            if (!activeNodes.Contains(player.CurrentTile))
            {
                activeNodes.Add(player.CurrentTile);
            }
        }
        foreach (TileBase tile in activeNodes) tile.OrganizePlayers();

    }

    public bool CanMove = true;
    public void MoveTile(string input, Player player)
    {
        if (!CanMove) return;
        TileBase tile = player.CurrentTile;
        int NewY = 0;
        switch (input)
        {
            case "up":
                NewY = 180;
                if (player.CurrentTile.UpTile != null)
                {
                    player.transform.localEulerAngles = new Vector3(0, NewY, 0);
                    tile = player.CurrentTile.UpTile;
                }
                break;
            case "down":
                if (player.CurrentTile.DownTile != null)
                {
                    player.transform.localEulerAngles = new Vector3(0, NewY, 0);
                    tile = player.CurrentTile.DownTile;
                    //0
                }
                break;
            case "left":
                NewY = 90;
                if (player.CurrentTile.LeftTile != null)
                {
                    player.transform.localEulerAngles = new Vector3(0, NewY, 0);
                    tile = player.CurrentTile.LeftTile;
                    //90
                }
                break;
            case "right":
                NewY = 270;
                if (player.CurrentTile.RightTile != null)
                {
                    player.transform.localEulerAngles = new Vector3(0, NewY, 0);
                    tile = player.CurrentTile.RightTile;
                }
                break;
        }

        player.CurrentTile.Players.Remove(player);
        player.CurrentTile.OrganizePlayers();
        tile.Players.Insert(0, player);
        tile.OrganizePlayers();

        Transform NewTileTrans = tile.PlayerSpots[0].transform;
        LeanTween.move(player.gameObject, tile.PlayerSpots[0].position, 0.1f);
        LeanTween.move(camera.gameObject, new Vector3(NewTileTrans.position.x, camera.transform.position.y, NewTileTrans.position.z + 5f), 0.1f);
        player.CurrentTile = tile;
    }
}
