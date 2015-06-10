using UnityEngine;
using System.Collections;

public class medicine : MonoBehaviour {
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
	void Awake () {
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
	void Update () {

        if (ready == false)
        {
            //Do Nothing
        }
        else
        {
            if (down == false && control)
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);

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
                    if (!slow)
                        speed = 10;
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
                    transform.Translate(0, -speed * Time.deltaTime, 0);
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
                transform.position = new Vector3(left_limit + 0.5f,transform.position.y, transform.position.z);
            if (transform.position.x > right_limit)
                transform.position = new Vector3(right_limit - 0.5f, transform.position.y, transform.position.z);
        }
    }

    //direnction:   0 = Q = left
    //              1 = E = right
    void Move(int dir)
    {

        //In state that 2 med are next to each other where med1 = -0.5, med2 = 0.5
        if(state == 0)
        {

            if(dir == 0 && !up)
            {
                //The right object move to top
                state = 1;
               
                
            }
            if(dir == 1 && !up)
            {
                //left object move to top
                state = 3;
               
            }
        }

        //In state that med2 is on top of med 1 and both are in (-0.5)
        //
        else if (state == 1)
        {
            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 2;
       

            }
            if (dir == 1 && !up)
            {
                //left object move to top
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
            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 0;
               

            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 2;
              
            }
        }

        if(state == 1)
        {
            med1.localPosition = new Vector3(-0.5f, 0, 0);
            med2.localPosition = new Vector3(-0.5f, 1, 0);
            left_limit = 36;
            right_limit = 47;
        }
        else if (state == 2)
        {
            med1.localPosition = new Vector3(0.5f, 0, 0);
            med2.localPosition = new Vector3(-0.5f, 0, 0);

            left_limit = 36;
            right_limit = 46;
        }
        else if (state == 3)
        {
            med1.localPosition = new Vector3(0.5f, 1, 0);
            med2.localPosition = new Vector3(0.5f, 0, 0);
            left_limit = 35;
            right_limit = 46;
        }
        else if (state == 0)
        {
            med1.localPosition = new Vector3(-0.5f, 0, 0);
            med2.localPosition = new Vector3(0.5f, 0, 0);
            left_limit = 36;
            right_limit = 46;
        }
        med1.SendMessage("State", state);
        med2.SendMessage("State", state);
    }


    //When one piece is destroyed, unlink and unparent the other piece***
    //And destroy self.
    void Unlink(GameObject other)
    {

    }
    void Up(bool sensor)
    {
        up = sensor;
    }

    void Down(bool sensor)
    {
        down = sensor;
    }

    void Right(bool sensor)
    {
        right = sensor;
    }

    void Left(bool sensor)
    {
        left = sensor;
    }

    void Break()
    {
        //When one obj is destroyed, it will send its parent that it is destroyed
        //The medicine will then broadcast message to unlink to all its children if there are any children left

        if(med1 != null || med2!= null)
        BroadcastMessage("Break_link");

       // transform.DetachChildren();
        Debug.Log("Breaking Link");
        //It then destroyed itself
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag != "SideWall")
        {
            slow = true;
            lookahead = other.gameObject;
        }
    }

   

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag != "SideWall")
        {
            if(other.gameObject == lookahead)
            {
                slow = false;
            }
        }
    }

}
