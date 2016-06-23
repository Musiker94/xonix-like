using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    public float tileSize = 1;
    private Map Map = null;
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
        if (move != new Vector3(0, 0, 0) && (int)transform.position.x < Map.xTiles - 1 && (int)transform.position.x > 0 && (int)transform.position.y > 0 && (int)transform.position.y < Map.yTiles - 1)
        {
            var tile = Map.Grid[(int)transform.position.x, (int)transform.position.y];
            if ((Vector2)this.transform.position * tileSize == (Vector2)tile.transform.position)
            {
                Tile t = tile.GetComponent<Tile>();
                if (t.Free && !t.Broken)
                    t.Fill();
                else if (!t.Free && !t.Broken)
                    Map.Block((int)tile.transform.position.x,(int)tile.transform.position.y);
                else
                {
                    Map.Clear();
                    this.transform.position = new Vector3(1, 1, -1);
                }
            }
            this.transform.position += move;
        }

    }
}
