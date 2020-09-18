using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    public List<Player> Players;
    public int CurrentTurn;
    public GameStates GameState;
    public enum GameStates
    {
        Battle,
        Movement,
        Cutscene

    }
    void Start()
    {
        StartTurn(Players[0]);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameState)
        {

            case GameStates.Battle:
                break;
            case GameStates.Movement:
                string input = "";
                Player player = Players[CurrentTurn];

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

               if(input != "") MoveTile(input, player);

                break;
            case GameStates.Cutscene:
                break;
        }
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
        foreach(TileBase tile in activeNodes) tile.OrganizePlayers();

    }

    public void MoveTile(string input, Player player)
    {
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
        tile.Players.Insert(0,player);
        tile.OrganizePlayers();

        player.transform.position = tile.PlayerSpots[0].position;
        player.CurrentTile = tile;
        


            //camera.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y, player.CurrentTile.transform.position.z +  5f);

    }
}
