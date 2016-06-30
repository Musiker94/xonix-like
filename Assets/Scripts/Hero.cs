using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    public float tileSize = 1;
    private Map Map = null; GameObject prevtile = null;
    private int used = 0;
    // Use this for initialization
    void Start()
    {
        Map = GameObject.Find("Map").GetComponent<Map>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            move = new Vector3(-1, 0, 0) * tileSize;
        else if (Input.GetKey(KeyCode.RightArrow))
            move = new Vector3(1, 0, 0) * tileSize;
        else if (Input.GetKey(KeyCode.UpArrow))
            move = new Vector3(0, 1, 0) * tileSize;
        else if (Input.GetKey(KeyCode.DownArrow))
            move = new Vector3(0, -1, 0) * tileSize;
        if (move != new Vector3(0, 0, 0))
        {
            var tile = Map.Grid[(int)transform.position.x, (int)transform.position.y];
            var newpos = this.transform.position + move;
            var newtile = Map.Grid[(int)newpos.x, (int)newpos.y];
            if ((Vector2)this.transform.position * tileSize == (Vector2)tile.transform.position && tile != prevtile)
            {
                tile.GetComponent<Tile>().State = Tile.States.Used;
                prevtile = tile;
                used++;
            }
            if (newtile.GetComponent<Tile>().State == Tile.States.Free)
            {
                this.transform.position += move;
            }
            else if (used > 0 && (newtile.GetComponent<Tile>().State == Tile.States.Wall || newtile.GetComponent<Tile>().State == Tile.States.Broken))
            {
                Map.Ebosh();
                used = 0;
                Map.Clear();
            }
        }

    }
}
