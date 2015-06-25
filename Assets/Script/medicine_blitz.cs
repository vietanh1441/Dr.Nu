using UnityEngine;
using System.Collections;

public class medicine_blitz : MonoBehaviour {
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

    void Ready()
    {
        //When central signal is ready, send it to the top
        transform.position = new Vector3(40.5f, 40, 0);


        //Also, enable control
        StartCoroutine("Delay_control");
    }


    IEnumerator Delay_control()
    {
        yield return new WaitForEndOfFrame();
        control = true;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (ready == false)
        {
            //Do Nothing
        }
        else
        {
            if (down == false && control)
            {

                if (Input.GetKeyDown(KeyCode.A) && !left && transform.position.x > left_limit)
                {
                    transform.Translate(-1, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.D) && !right && transform.position.x < right_limit)
                {
                    transform.Translate(1, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Move(0);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Move(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    GoDown();
                }
                //if there is an object under, then slow down
                if (slow)
                {
                    speed = 1;
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    speed = 1;
                }
            }
            else
            {
                //Function to send signal to ready the next one, disable control
                if (control == true)
                    central.SendMessage("GetSignal");
                control = false;
            }
            if (!control)
            {
                if (down == false)
                {
                    transform.Translate(0, -0.1f, 0);
                }
                else
                {
                    if (Mathf.Abs(transform.position.y - (int)transform.position.y) > 0.8)
                        transform.position = new Vector3(transform.position.x, (int)transform.position.y + 1, transform.position.z);
                    if (Mathf.Abs((int)transform.position.y - transform.position.y) < 0.2)
                        transform.position = new Vector3(transform.position.x, (int)transform.position.y, transform.position.z);
                }
            }
            if (transform.position.x < left_limit)
                transform.position = new Vector3(left_limit + 0.5f, transform.position.y, transform.position.z);
            if (transform.position.x > right_limit)
                transform.position = new Vector3(right_limit - 0.5f, transform.position.y, transform.position.z);
        }

        CheckSide();

    }


    void GoDown()
    {
        float pos = 0;
        if (state == 0 || state == 2)
        {
            //Check if there is anything underneath
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), -Vector2.up, 40f);
            RaycastHit2D up2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), -Vector2.up, 40f);
            //if there is anything underneath, get the highest y
             if ((up1.transform != null) || (up2.transform != null))
             {
                 if(up1.transform.position.y >= up2.transform.position.y)
                 {
                     pos = up1.transform.position.y + 1;
                 }
                 else
                 {
                     pos = up2.transform.position.y + 1;
                 }
             }

             transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        }
        else if(state == 1 )
        {
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), -Vector2.up, 40f);
            pos = up1.transform.position.y + 1;
            transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        }
        else if (state == 3)
        {
            RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), -Vector2.up, 40f);
            pos = up1.transform.position.y + 1;
            transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        }
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

    //direnction:   0 = Q = left
    //              1 = E = right
    void Move(int dir)
    {

        //In state that 2 med are next to each other where med1 = -0.5, med2 = 0.5
        if (state == 0)
        {

            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 1;


            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 3;

            }
        }

        //In state that med2 is on top of med 1 and both are in (-0.5)
        //
        else if (state == 1)
        {
            if (dir == 0 && !right)
            {
                state = 2;


            }
            if (dir == 1 && !right)
            {
                state = 0;

            }
        }


        //In state that med2 is on left and med1 is on right
        else if (state == 2)
        {
            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 3;


            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 1;

            }
        }

        //In state that med 1 is on top of med 2 
        else if (state == 3)
        {
            if (dir == 0 && !left)
            {
                //The right object move to top
                state = 0;


            }
            if (dir == 1 && !left)
            {
                //left object move to top
                state = 2;

            }
        }

        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();

        if (state == 1)
        {
            med1.localPosition = new Vector3(-0.5f, 0, 0);
            med2.localPosition = new Vector3(-0.5f, 1, 0);
            left_limit = 36;
            right_limit = 47;
            box.offset = new Vector2(-0.5f, -1);
        }
        else if (state == 2)
        {
            med1.localPosition = new Vector3(0.5f, 0, 0);
            med2.localPosition = new Vector3(-0.5f, 0, 0);

            left_limit = 36;
            right_limit = 46;

            box.offset = new Vector2(0, -1);
        }
        else if (state == 3)
        {
            med1.localPosition = new Vector3(0.5f, 1, 0);
            med2.localPosition = new Vector3(0.5f, 0, 0);
            left_limit = 35;
            right_limit = 46;
            box.offset = new Vector2(0.5f, -1);
        }
        else if (state == 0)
        {
            med1.localPosition = new Vector3(-0.5f, 0, 0);
            med2.localPosition = new Vector3(0.5f, 0, 0);
            left_limit = 36;
            right_limit = 46;

            box.offset = new Vector2(0, -1);
        }
        med1.SendMessage("State", state);
        med2.SendMessage("State", state);
        control = false;
        StartCoroutine("Delay_control");
    }


    void Break()
    {
        //When one obj is destroyed, it will send its parent that it is destroyed
        //The medicine will then broadcast message to unlink to all its children if there are any children left

        if (med1 != null || med2 != null)
            BroadcastMessage("Break_link");

        // transform.DetachChildren();
        //It then destroyed itself
        Destroy(gameObject);
    }

   

}
