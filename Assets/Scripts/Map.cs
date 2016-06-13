using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{

    public GameObject tile;
    
    public int xTiles;
    public int yTiles;
    float X = 0;
    float Y = 0;

    void Start()
    {
        for (int y = 0; y < yTiles; y++)
        {
            for (int x = 0; x < xTiles; x++)
            {
                    Instantiate(tile, new Vector3(X, Y, 0), Quaternion.identity);
                X += tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            }
            Y += tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            X = 0;
        }
    }

    void Update()
    {

    }
}
