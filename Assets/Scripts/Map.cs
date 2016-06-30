using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{

    public GameObject tile;
    public float tileSize;
    public int xTiles;
    public int yTiles;
    public GameObject[,] Grid = new GameObject[0, 0];

    void Start()
    {
        float X = 0;
        float Y = 0;
        Grid = new GameObject[xTiles, yTiles];
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
    }
    public void Clear()
    {
        for (int y = 0; y < yTiles; y++)
        {
            for (int x = 0; x < xTiles; x++)
            {
                if (!(x == 0 || y == 0 || x == xTiles - 1 || y == yTiles - 1))
                    if(Grid[x, y].GetComponent<Tile>().State==Tile.States.Used)
                        Grid[x, y].GetComponent<Tile>().State = Tile.States.Free;
            }
        }
    }
    private GameObject ShootRandom()
    {
        GameObject tile = null;
        for (int y = 1; y < yTiles-1; y++)
        {
            for (int x = 1; x < xTiles-1; x++)
            {
                if(Grid[x, y].GetComponent<Tile>().State==Tile.States.Free)
                    tile = Grid[x, y];
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
                t.GetComponent<Tile>().State = Tile.States.Broken;
            }
            foreach (GameObject t in t1)
            {
                t.GetComponent<Tile>().State = Tile.States.Free;
            }
        }
        else
        {
            foreach (GameObject t in t1)
            {
                t.GetComponent<Tile>().State = Tile.States.Broken;
            }
            foreach (GameObject t in t2)
            {
                t.GetComponent<Tile>().State = Tile.States.Free;
            }
        }
    }
    public List<GameObject> Fill(GameObject t)
    {
        List<GameObject> tiles = new List<GameObject>();
        tiles.Add(t);
        t.GetComponent<Tile>().State = Tile.States.Pending;
        if ((int)(t.transform.position.x / tileSize) != xTiles-2 && Grid[(int)(t.transform.position.x / tileSize)+1, (int)(t.transform.position.y / tileSize)].GetComponent<Tile>().State == Tile.States.Free)
            tiles.AddRange(Fill(Grid[(int)(t.transform.position.x / tileSize)+1, (int)(t.transform.position.y / tileSize)]));
        if ((int)(t.transform.position.x / tileSize)!=1 && Grid[(int)(t.transform.position.x / tileSize) - 1, (int)(t.transform.position.y / tileSize)].GetComponent<Tile>().State == Tile.States.Free)
            tiles.AddRange(Fill(Grid[(int)(t.transform.position.x / tileSize) - 1, (int)(t.transform.position.y / tileSize)]));
        if ((int)(t.transform.position.y / tileSize) != yTiles - 2 && Grid[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)+1].GetComponent<Tile>().State == Tile.States.Free)
            tiles.AddRange(Fill(Grid[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)+1]));
        if ((int)(t.transform.position.y / tileSize) != 1 && Grid[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)-1].GetComponent<Tile>().State == Tile.States.Free)
            tiles.AddRange(Fill(Grid[(int)(t.transform.position.x / tileSize), (int)(t.transform.position.y / tileSize)-1]));
        return tiles;
    }
    void Update()
    {

    }
}
