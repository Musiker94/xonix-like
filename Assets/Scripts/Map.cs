using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{

    public GameObject tile;
    public float tileSize;
    public int xTiles;
    public int yTiles;
    public GameObject[,] Grid = new GameObject[0,0];

    void Start()
    {
        float X = 0;
        float Y = 0;
        Grid = new GameObject[xTiles, yTiles];
        tileSize =tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int y = 0; y < yTiles; y++)
        {
            for (int x = 0; x < xTiles; x++)
            {
                Grid[x,y]=(GameObject)Instantiate(tile, new Vector3(X, Y, 0), Quaternion.identity);
                X += tileSize;
            }
            Y += tileSize;
            X = 0;
        }
    }
    public void Clear()
    {
        foreach(var t in Grid)
        {
            t.GetComponent<Tile>().MakeFree();
        }
    }
    public void Block(int x, int y)
    {
        if (Grid[x, y].GetComponent<Tile>().Free == true) Grid[x, y].GetComponent<Tile>().Break();
        if (Grid[x - 1,y].GetComponent<Tile>().Free==true && x>0) Block(x - 1, y);
        if (Grid[x + 1,y].GetComponent<Tile>().Free == true && x<xTiles) Block(x + 1, y);
        if (Grid[x,y - 1].GetComponent<Tile>().Free == true && y>0) Block(x, y - 1);
        if (Grid[x,y + 1].GetComponent<Tile>().Free == true && y>yTiles) Block(x, y + 1);
    }
    void Update()
    {

    }
}
