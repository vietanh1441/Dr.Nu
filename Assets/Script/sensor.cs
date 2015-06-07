using UnityEngine;
using System.Collections;

public class sensor : MonoBehaviour {

    public int type;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (type == 0)       //down
            transform.parent.SendMessage("Down", other.gameObject);
        else if (type == 1) //up
            transform.parent.SendMessage("Up", other.gameObject);
        else if (type == 2) //right
            transform.parent.SendMessage("Right", other.gameObject);
        else if (type == 3) //left
            transform.parent.SendMessage("Left", other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (type == 0)       //down
            transform.parent.SendMessage("Down_exit", other.gameObject);
        else if (type == 1) //up
            transform.parent.SendMessage("Up_exit", other.gameObject);
        else if (type == 2) //right
            transform.parent.SendMessage("Right_exit", other.gameObject);
        else if (type == 3) //left
            transform.parent.SendMessage("Left_exit", other.gameObject);
    }
}
