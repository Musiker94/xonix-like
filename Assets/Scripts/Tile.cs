using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public bool Broken = false;
    public bool Free = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Broken)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    else if(!Broken && Free)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    else if(!Free && !Broken)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
	}

    public void Break()
    {
        Broken = true;
        Free = false;
    }
    public void Fill()
    {
        Free = false;
    }
    public void MakeFree()
    {
        Free = true;
        Broken = false;
    }
}
