using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileBase : MonoBehaviour
{
    public List<Player> Players;
    public List<Transform> PlayerSpots;

    public TileBase UpTile;
    public TileBase DownTile;
    public TileBase LeftTile;
    public TileBase RightTile;

    //    public List<Players> PlayersOnNode;
    public void Start()
    {

    }

    void OnDrawGizmos()
    { /*
        if (ConnectedNodes != null)
        {
            foreach (TileBase tile in ConnectedNodes)
            {
               if(tile) Gizmos.DrawLine(transform.position, tile.transform.position);
            }
        } */
    }



    public void OrganizePlayers(){
        for(int i = 0; i < Players.Count; i++){
            Players[i].transform.position = PlayerSpots[i].position;
        }
    }
}
