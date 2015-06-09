﻿using UnityEngine;
using System.Collections;


//This is the base class for all kind of tiles in game
//Every other class will inherit from this

public class individual : MonoBehaviour {

	// Use this for initialization
    public GameObject right, left, up, down;
    public int color;
    public int type;
    //0 normal, from the medicine
    //1 normal, not from medicine, but do not come down
    //2 virus
    public int hp;
    public GameObject link;
    public bool is_link;
    private SpriteRenderer spriteRenderer; 
    public Sprite[] color_sprite = new Sprite[5];

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //First, use random to randomize the color. The maximum color will be based on GameManager
        Init_color();
       

        //init
    }


    void Init_color()
    {
        color = Random.Range(0, 4);
        if (color == 0)
        {
            transform.tag = "Yellow";
            spriteRenderer.sprite = color_sprite[color];
        }
        if(color == 1)
        {
            transform.tag = "Blue";
            spriteRenderer.sprite = color_sprite[color];
        }
        if (color == 2)
        {
            transform.tag = "Red";
            spriteRenderer.sprite = color_sprite[color];
        }
        if (color == 3)
        {
            transform.tag = "Green";
            spriteRenderer.sprite = color_sprite[color];
        }
    }
  
	// Update is called once per frame
	void Update () {
        if (type == 0 && is_link == false)
        {
            if (down == null)
            {
                transform.Translate(0, -3 * Time.deltaTime, 0);
            }
            else if (!is_link)
            {
                if (Mathf.Abs(transform.position.y - (int)transform.position.y) > 0.8)
                    transform.position = new Vector3(transform.position.x, (int)transform.position.y + 1, transform.position.z);
                if (Mathf.Abs((int)transform.position.y - transform.position.y) < 0.2)
                    transform.position = new Vector3(transform.position.x, (int)transform.position.y, transform.position.z);
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
            down.SendMessage("Damaged_Loss", 1);
            up.SendMessage("Damaged_Loss", 1);
            
        }
        if(horizontal == true)
        {
            right.SendMessage("Damaged_Loss", 1);
            left.SendMessage("Damaged_Loss", 1);
        }
        if(vertical || horizontal)
            Damaged_Loss(1);

    }


    void Damaged_Loss(int loss)
    {
        Debug.Log("Call" + loss + transform.name);
        Destroy(gameObject);
    }


    IEnumerator Delay()
    {
       
        yield return new WaitForSeconds(0.5f);
        Check(0);
    }


    void OnDestroy()
    {
        if (transform.root != transform)
            transform.parent.gameObject.SendMessage("Break");
        if (up != null)
            up.transform.gameObject.SendMessage("Down_exit",gameObject);
    }


    void Break_link()
    {
        transform.parent = null;
        link = null;
        is_link = false;
    }

    void Down(GameObject other)
    {
        down = other;
        StartCoroutine("Delay");
        if (is_link)
        {
            if (down != link)
            {
                transform.parent.SendMessage("Down", true);
            }
        }
    }

    void Down_exit(GameObject other)
    {
        if (down == other)
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
        up = other;
        if (is_link)
        {
            if (up != link)
            {
                transform.parent.SendMessage("Up", true);
            }
        }
    }

    void Up_exit(GameObject other)
    {
        if (up == other)
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
        right = other;
        if (is_link)
        {
            if (right != link)
            {
                transform.parent.SendMessage("Right", true);
            }
        }
    }

    void Right_exit(GameObject other)
    {
        if (right == other)
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
       left = other;
        if (is_link)
        {
            if (left != link)
            {
                transform.parent.SendMessage("Left", true);
            }
        }
    }

    void Left_exit(GameObject other)
    {
        if(left == other)
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
