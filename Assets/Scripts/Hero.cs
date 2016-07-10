using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{
    public float tileSize = 1;
    public float p, p1, p2;
    private Map Map = null; GameObject prevtile = null;
    private int used = 0;
    // Use this for initialization
    void Start()
    {
        Map = GameObject.Find("Map").GetComponent<Map>();
    }

    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            move = new Vector3(-1, 0, 0);
        else if (Input.GetKey(KeyCode.RightArrow))
            move = new Vector3(1, 0, 0);
        else if (Input.GetKey(KeyCode.UpArrow))
            move = new Vector3(0, 1, 0);
        else if (Input.GetKey(KeyCode.DownArrow))
            move = new Vector3(0, -1, 0);
        p = (int)(transform.position.x);
        p1 = (transform.position.x);
        var tile = Map.Nodes[(int)(transform.position.x), (int)(transform.position.y)];
        var newpos = this.transform.position + move;
        if ((int)(newpos.x) < Map.xTiles - 1 && (int)(newpos.y) < Map.yTiles - 1 && (int)(newpos.x) >= 0 && (int)(newpos.y) >= 0)
        {
            var newtile = Map.Nodes[(int)(newpos.x), (int)(newpos.y)];
            if (newtile != null)
            {
                if (new Vector2((int)(transform.position.x), (int)(transform.position.y)) == new Vector2((int)(tile.transform.position.x), (int)(tile.transform.position.y)) && tile != prevtile)
                {
                    tile.GetComponent<Node>().State = Node.States.Used;
                    prevtile = tile;
                    used++;
                }
                if (newtile.GetComponent<Node>().State == Node.States.Free)
                {
                    this.transform.position = Vector3.MoveTowards(transform.position, transform.position + move, 1);
                }
                else if (used > 0 && (newtile.GetComponent<Node>().State == Node.States.Wall || newtile.GetComponent<Node>().State == Node.States.Broken))
                {
                    Map.Ebosh();
                    used = 0;
                    Map.Clear();
                }
            }

        }
    }
}
