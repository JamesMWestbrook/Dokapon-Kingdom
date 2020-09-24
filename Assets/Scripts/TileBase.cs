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



    public void OrganizePlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (i < 1)
            {
                Players[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                if (!GameManager.instance.IsTurn(Players[i])) { Players[i].transform.position = PlayerSpots[i].position; }
            }
            else
            {
                Players[i].transform.position = PlayerSpots[i].position;
                Players[i].transform.localScale = new Vector3(0.10f, 0.10f, 0.10f);
            }
        }
    }
}
