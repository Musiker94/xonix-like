using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{

    public Transform tile;
    
    public int xTiles;
    public int yTiles;

    void Start()
    {
        for (int y = 0; y < yTiles; y++)
        {
            for (int x = 0; x < xTiles; x++)
            {
                float rnd = Random.value;
                if (rnd < 0.05)
                {
                    Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {

    }
}
