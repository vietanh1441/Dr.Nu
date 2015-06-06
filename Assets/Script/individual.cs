using UnityEngine;
using System.Collections;


//This is the base class for all kind of tiles in game
//Every other class will inherit from this

public class individual : MonoBehaviour {

	// Use this for initialization
    public GameObject right, left, up, down;
    public int color;
    public int type;
    public int hp;
    public GameObject link;
    public bool is_link;

    void Start()
    {
        //init
    }

  
	// Update is called once per frame
	void Update () {
        if (type == 0 && is_link == false)
        {
            if (down == null)
            {
                transform.Translate(0, -1 * Time.deltaTime, 0);
            }
        }
	}

    void Down(GameObject other)
    {
        if(other != down)
        {
            down = other;
            if(is_link)
            {
                if(down != link)
                {
                    transform.parent.SendMessage("Down", true);
                }
            }
        }
        else
        {
            down = null;
            if (is_link)
            {
                if (down != link)
                {
                    transform.parent.SendMessage("Down", false);
                }
            }
        }
    }

    void Up(GameObject other)
    {
        if (other != up)
        {
            up = other;
            if (is_link)
            {
                if (up != link)
                {
                    transform.parent.SendMessage("Up", true);
                }
            }
        }
        else
        {
            up = null;
            if (is_link)
            {
                if (up != link)
                {
                    transform.parent.SendMessage("Up", false);
                }
            }
        }
    }

    void Right(GameObject other)
    {
        if (other != right)
        {
            right = other;
            if (is_link)
            {
                if (right != link)
                {
                    transform.parent.SendMessage("Right", true);
                }
            }
        }
        else
        {
            right = null;
            if (is_link)
            {
                if (right != link)
                {
                    transform.parent.SendMessage("Right", false);
                }
            }
        }
    }

    void Left(GameObject other)
    {
        if (other != left)
        {
            left = other;
            if (is_link)
            {
                if (left != link)
                {
                    transform.parent.SendMessage("Left", true);
                }
            }
        }
        else
        {
            left = null;
            if (is_link)
            {
                if (left != link)
                {
                    transform.parent.SendMessage("Left", false);
                }
            }
        }
    }
}
