using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    // Use this for initialization
    SpriteRenderer sprite;
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
	}
	public enum States {Free,Used,Pending,Broken,Wall};

    public States State { get; set; }
    // Update is called once per frame
    void Update()
    {
        if (State == States.Wall)
        {
            sprite.color = Color.blue;
        }
        else if(State==States.Broken)
        {
            sprite.color = Color.black;
        }
        else if(State==States.Used)
        {
            sprite.color = Color.yellow;
        }
        else if(State==States.Pending)
        {
            sprite.color = Color.magenta;
        }
        else
        {
            sprite.color = Color.green;
        }
    }
}
