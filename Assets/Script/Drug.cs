﻿using UnityEngine;
using System.Collections;

public class Drug : MonoBehaviour {
    public GameObject right, left, up, down;
    public int color;
    public GameObject central_obj;
    public central central_scr;
    public GameObject link;
    public bool is_link;
    private SpriteRenderer spriteRenderer;
    public Sprite[] color_sprite = new Sprite[5];

	// Use this for initialization
	void Start () {
        central_obj = GameObject.FindGameObjectWithTag("Central");
        central_scr = central_obj.GetComponent<central>();
        central_scr.medicine_lst.Add(gameObject);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = color_sprite[0];   
        //First, use random to randomize the color. The maximum color will be based on GameManager
        Init_color();
	}

    void Init_color()
    {
        int color_num = central_scr.color;
        color = Random.Range(0, color_num);
        if (color == 0)
        {
            transform.tag = "Yellow";
            spriteRenderer.color = Color.yellow;
        }
        if (color == 1)
        {
            transform.tag = "Blue";
            spriteRenderer.color = Color.blue;
        }
        if (color == 2)
        {
            transform.tag = "Red";
            spriteRenderer.color = Color.red;
        }
        if (color == 3)
        {
            transform.tag = "Green";
            spriteRenderer.color = Color.green;
        }
        if(color== 4)
        {
            transform.tag = "Gray";
            spriteRenderer.color = Color.gray;
        }

    }

    void Update()
    {
        
            CheckDown();
            if (is_link == false)
            {


                if (down == null)
                {
                    transform.Translate(0, -0.1f, 0);
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

    void GoLeft()
    {
        CheckAll();
        if(left != null && left.tag == transform.tag)
        {
                left.SendMessage("GoLeft");
            
        }

        else
        {
            GoRight(0);
        }
    }

    void GoRight(int num)
    {
        CheckAll();
        if(right != null && right.tag == transform.tag)
        {
            
                right.SendMessage("GoRight", num + 1);
        }
        else
        {
            if(num > 1)
            {
                central_obj.SendMessage("Scoring", num);
                DoMatchVert(num);
            }
        }
    }

    void DoMatchVert(int num)
    {
        if (num != 0)
        {
            left.SendMessage("DoMatchVert", num - 1);
        }
        
        Damaged_Loss(1);
    }

    void GoUp()
    {
        CheckAll();
        if (up != null && up.tag == transform.tag)
        {
            up.SendMessage("GoUp");

        }

        else
        {
            GoDown(0);
        }
    }

    void GoDown(int num)
    {
        CheckAll();
        if (down != null && down.tag == transform.tag)
        {

            down.SendMessage("GoDown", num + 1);
        }
        else
        {
            if (num > 1)
            {
                central_obj.SendMessage("Scoring", num);
                DoMatchHor(num);
            }
        }
    }

    void DoMatchHor(int num)
    {
        if (num != 0)
        {
            up.SendMessage("DoMatchHor", num - 1);
        }
        Damaged_Loss(1);
    }

    void Check()
    {
        //Check if its position is bigger than deadzone, if it does, then send central game over
        if(transform.position.y > 35)
        {
            central_scr.Losing();
        }

        CheckAll();
        GoLeft();
        GoUp();
       /* bool vertical = false;
        bool horizontal = false;
        CheckAll();
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
                    up.SendMessage("Check", 1);
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
            if (up != null)
            {
                if (up.tag == transform.tag)
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
        if (horizontal == true)
        {
            right.SendMessage("Damaged_Loss", 1);
            left.SendMessage("Damaged_Loss", 1);
        }
        if (vertical || horizontal)
            Damaged_Loss(1);
         */

    }
    
     void Damaged_Loss(int loss)
     {
         if (transform.root != transform)
         {
             if (transform.root != null)
                 transform.parent.gameObject.SendMessage("Break");
         }
         Destroy(gameObject);
     }

     IEnumerator Delay()
     {

         yield return new WaitForSeconds(0.5f);
         Check();
     }

    void CheckAll()
     {
         CheckDown();
         CheckLeft();
         CheckRight();
         CheckUp();
     }

    void CheckDown()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x , transform.position.y - 0.5f), -Vector2.up, 0.1f);
        if (up1.transform != null)
        {
            if(up1.transform.gameObject != down)
            {
                down = up1.transform.gameObject;
                if(!is_link)
                StartCoroutine("Delay");
                else if(is_link)
                {
                    if (down != link)
                    {
                        link.SendMessage("More_Delay");
                        StartCoroutine("Delay");
                    }
                }
            }
            
        }
        else
        {
            down = null;
        }
    }

    void More_Delay()
    {
        StartCoroutine("Delay");
    }

    void CheckRight()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x +0.5f, transform.position.y ), Vector2.right, 0.4f);
        if (up1.transform != null)
        {
            if(up1.transform.gameObject != right)
           right = up1.transform.gameObject;
        }
        else
        {
            right = null;
        }
    }

    void CheckLeft()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), -Vector2.right, 0.4f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != left)
            left = up1.transform.gameObject;
        }
        else
        {
            left = null;
        }
    }

    void CheckUp()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x , transform.position.y + 0.5f), Vector2.up, 0.4f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != up)
            up = up1.transform.gameObject;
        }
        else
        {
            up = null;
        }
    }


    void State(int state)
    {

        spriteRenderer.sprite = color_sprite[state];

    }


    void OnDestroy()
    {

        central_scr.medicine_lst.Remove(gameObject);

    }


    void Break_link()
    {
        transform.parent = null;
        link = null;
        is_link = false;
        spriteRenderer.sprite = color_sprite[4];
    }

  

}
