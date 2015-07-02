using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
public bool up, down, right, left;
    public Transform med1, med2;
    public bool control;
    public bool ready;
    public int state;
    public float speed = 3;
    public GameObject central;
    bool slow;
    private int left_limit, right_limit;
    private GameObject lookahead;
    // Use this for initialization
    void Awake()
    {
        slow = false;
        ready = false;
        left_limit = 36;
        right_limit = 46;
        //First control is false, put it in display***
        central = GameObject.FindGameObjectWithTag("Central");
        control = false;
        //4 state of spining of the medicine
        state = 0;
        med1.localPosition = new Vector3(-0.5f, 0, 0);
        med2.localPosition = new Vector3(0.5f, 0, 0);
    }

   



    // Update is called once per frame
    void Update()
    {

    }


   
    void CheckSide()
    {
        if (state == 0 || state == 2)
        {
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.5f), Vector2.up, 0.4f);
            RaycastHit2D up2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Vector2.up, 0.4f);
            if ((up1.transform != null) || (up2.transform != null))
            {
                up = true;

            }
            else
            {
                up = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x - 1f, transform.position.y), -Vector2.right, 0.4f);
            if (up1.transform != null)
            {
                left = true;
            }
            else
            {
                left = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x + 1f, transform.position.y), Vector2.right, 0.4f);
            if (up1.transform != null)
            {
                right = true;
            }
            else
            {
                right = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), -Vector2.up, 0.1f);
            up2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), -Vector2.up, 0.1f);
            if ((up1.transform != null) || (up2.transform != null))
            {
                down = true;
            }
            else
            {
                down = false;
            }
        }

        if (state == 1)
        {
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + 1.5f), Vector2.up, 0.4f);
            if (up1.transform != null)
            {
                up = true;
            }
            else
            {
                up = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), -Vector2.right, 0.4f);
            RaycastHit2D up2 = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y + 1), -Vector2.right, 0.4f);
            RaycastHit2D up3 = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y + 0.5f), -Vector2.right, 0.4f);
            if ((up1.transform != null) || (up2.transform != null) || (up3.transform != null))
            {
                left = true;
            }
            else
            {
                left = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.4f);
            up2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), Vector2.right, 0.4f);
            up3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 0.4f);
            if ((up1.transform != null) || (up2.transform != null) || (up3.transform != null))
            {
                right = true;
            }
            else
            {
                right = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), -Vector2.up, 0.1f);
            if (up1.transform != null)
            {
                down = true;
            }
            else
            {
                down = false;
            }
        }

        if (state == 3)
        {
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + 1.5f), Vector2.up, 0.4f);
            if (up1.transform != null)
            {
                up = true;
            }
            else
            {
                up = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.right, 0.4f);
            RaycastHit2D up2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), -Vector2.right, 0.4f);
            RaycastHit2D up3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), -Vector2.right, 0.4f);
            if ((up1.transform != null) || (up2.transform != null) || (up3.transform != null))
            {
                left = true;
            }
            else
            {
                left = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), Vector2.right, 0.4f);
            up2 = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y + 1), Vector2.right, 0.4f);
            up3 = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y + 0.5f), Vector2.right, 0.4f);
            if ((up1.transform != null) || (up2.transform != null) || (up3.transform != null))
            {
                right = true;
            }
            else
            {
                right = false;
            }
            up1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), -Vector2.up, 0.1f);
            if (up1.transform != null)
            {
                down = true;
            }
            else
            {
                down = false;
            }
        }
    }

  

}

