using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{

    public GameObject tile;
    public GameObject node;
    public float tileSize;
    public int xTiles;
    public int yTiles;
    public GameObject[,] Grid = new GameObject[0, 0];
    public GameObject[,] Nodes = new GameObject[0, 0];

    void Start()
    {
        float X = 0;
        float Y = 0;
        Grid = new GameObject[xTiles, yTiles];
        Nodes = new GameObject[xTiles, yTiles];
        tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int y = 0; y < yTiles; y++)
        {
            for (int x = 0; x < xTiles; x++)
            {
                var obj = (GameObject)Instantiate(tile, new Vector3(X, Y, 0), Quaternion.identity);
                if (x == 0 || y == 0 || x == xTiles - 1 || y == yTiles - 1)
                    obj.GetComponent<Tile>().State = Tile.States.Wall;
                else
                    obj.GetComponent<Tile>().State = Tile.States.Free;
                Grid[x, y] = obj;
                X += tileSize;
            }
            Y += tileSize;
            X = 0;
        }
        X = 0.5f;Y = 0.5f;
        for (int y = 0; y < yTiles-1; y++)
        {
            for (int x = 0; x < xTiles-1; x++)
            {
                var obj = (GameObject)Instantiate(node, new Vector3(X, Y, -1), Quaternion.identity);
                if (x == 0 || y == 0 || x == xTiles - 2 || y == yTiles - 2)
                    obj.GetComponent<Node>().State = Node.States.Wall;
                else
                    obj.GetComponent<Node>().State =Node.States.Free;
                Nodes[x, y] = obj;
                X += tileSize;
            }
            Y += tileSize;
            X = 0.5f;
        }
    }
    public void Clear()
    {
        for (int y = 1; y < yTiles-2; y++)
        {
            for (int x = 1; x < xTiles-2; x++)
            {
              if(Nodes[x, y].GetComponent<Node>().State==Node.States.Used)
                 Nodes[x, y].GetComponent<Node>().State = Node.States.Free;
            }
        }
    }
    private GameObject ShootRandom()
    {
        GameObject tile = null;
        for (int y = 0; y < yTiles-1; y++)
        {
            for (int x = 0; x < xTiles-1; x++)
            {
                if(Nodes[x, y].GetComponent<Node>().State==Node.States.Free)
                    tile = Nodes[x, y];
            }
        }
        return tile;
    }
    public void Ebosh()
    {
        var t1 = new List<GameObject>();
        var t2 = new List<GameObject>();
        GameObject startTile = ShootRandom();
        if (startTile != null)
            t1=Fill(startTile);
        GameObject startTile2 = ShootRandom();
        if (startTile2 != null)
            t2=Fill(startTile2);
        if (t1.Count > t2.Count)
        {
            foreach (GameObject t in t2)
            {
                t.GetComponent<Node>().State = Node.States.Broken;
                Grid[(int)(t.transform.position.x + 0.5f),(int) (t.transform.position.y + 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x - 0.5f), (int)(t.transform.position.y + 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x + 0.5f), (int)(t.transform.position.y - 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x - 0.5f), (int)(t.transform.position.y -0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
            }
            foreach (GameObject t in t1)
            {
                t.GetComponent<Node>().State = Node.States.Free;
            }
        }
        else
        {
            foreach (GameObject t in t1)
            {
                t.GetComponent<Node>().State = Node.States.Broken;
                Grid[(int)(t.transform.position.x + 0.5f), (int)(t.transform.position.y + 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x - 0.5f), (int)(t.transform.position.y + 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x + 0.5f), (int)(t.transform.position.y - 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
                Grid[(int)(t.transform.position.x - 0.5f), (int)(t.transform.position.y - 0.5f)].GetComponent<Tile>().State = Tile.States.Broken;
            }
            foreach (GameObject t in t2)
            {
                t.GetComponent<Node>().State = Node.States.Free;
            }
        }
    }
    public List<GameObject> Fill(GameObject t)
    {
        List<GameObject> tiles = new List<GameObject>();
        tiles.Add(t);
        t.GetComponent<Node>().State = Node.States.Pending;
        if ((int)(t.transform.position.x / tileSize) != xTiles-1 && Nodes[(int)(t.transform.position.x / tileSize)+1, (int)(t.transform.position.y / tileSize)].GetComponent<Node>().State == Node.States.Free)
            tiles.AddRange(Fill(Nodes[(int)(t.transform.position.x / tileSize)+1, (int)(t.transform.position.y / tileSize)]));
        if ((int)(t.transform.position.x / tileSize)!=1 && Nodes[(int)(t.transform.position.x / tileSize) - 1, (int)(t.transform.position.y / tileSize)].GetComponent<Node>().State == Node.States.Free)
            tiles.AddRange(Fill(Nodes[(int)(t.transform.position.x / tileSize) - 1, (int)(t.transform.position.y / tileSize)]));
        if ((int)(t.transform.position.y / tileSize) != yTiles - 1 && Nodes[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)+1].GetComponent<Node>().State == Node.States.Free)
            tiles.AddRange(Fill(Nodes[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)+1]));
        if ((int)(t.transform.position.y / tileSize) != 1 && Nodes[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)-1].GetComponent<Node>().State == Node.States.Free)
            tiles.AddRange(Fill(Nodes[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)-1]));
        return tiles;
    }
    void Update()
    {

    }
}
