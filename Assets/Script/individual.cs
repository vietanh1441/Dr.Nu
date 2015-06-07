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
        if (color == 0)
            transform.tag = "Yellow";
        if (color == 10)
            transform.tag = "Wall";
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

    //Check to see if match 3
    //param: direction:
    // 0 = all direction
    // 1 = up
    // 2 = down
    // 3 = right
    // 4 = left
    void Check(int dir)
    {
        Debug.Log(dir);
        bool vertical = false;
        bool horizontal = false;
        if (dir == 0)
        {
            if (down != null)
            {
                if (down.tag == transform.tag)
                {
                   
                    down.SendMessage("Check", 2);
                    if (up != null)
                    {
                        if (up.tag == transform.tag)
                            vertical = true;
                    }
                }
            }
            if (up != null)
            {
                if (up.tag == transform.tag)
                {
                    up.SendMessage("Check",1);
                }
            }
            if (right != null)
            {
                if (right.tag == transform.tag)
                {
                    right.SendMessage("Check", 3);
                    if (left != null)
                    {
                        if (left.tag == transform.tag)
                            horizontal = true;
                    }
                }
            }
            if (left != null)
            {
                if (left.tag == transform.tag)
                {
                    left.SendMessage("Check", 4);
                }
            }
           
        }

        //Check up
        if (dir == 1)
        {
            if(up != null)
            {
                if(up.tag == transform.tag)
                {
                    up.SendMessage("Check", 1);
                    vertical = true;
                }
            }
        }

        //Check down
        if (dir == 2)
        {
            if (down != null)
            {
                if (down.tag == transform.tag)
                {
                    down.SendMessage("Check", 2);
                    vertical = true;
                }
            }
        }

        //Check right
        if (dir == 3)
        {
            if (right != null)
            {
                if (right.tag == transform.tag)
                {
                    right.SendMessage("Check", 3);
                    horizontal = true;
                }
            }
        }

        //Check left
        if (dir == 4)
        {
            if (left != null)
            {
                if (left.tag == transform.tag)
                {
                    left.SendMessage("Check", 4);
                    horizontal = true;
                }
            }
        }

        if (vertical == true)
        {
            Destroy(down);
            Destroy(up);
            
        }
        if(horizontal == true)
        {
            Destroy(right);
            Destroy(left);
        }
        if(vertical || horizontal)
            Destroy(gameObject);

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Check(0);
    }


    void OnDestroy()
    {
        Debug.Log("Destroy");
        if(transform.parent != null)
            transform.parent.gameObject.SendMessage("Break");
    }


    void Break_link()
    {
        transform.parent = null;
        link = null;
        is_link = false;
    }

    void Down(GameObject other)
    {
        
        if(other != down)
        {
            down = other;
            Debug.Log("In Down");
            //Run check***
            StartCoroutine("Delay");

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
