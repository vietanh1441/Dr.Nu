using UnityEngine;
using System.Collections;

public class medicine : MonoBehaviour {
    public bool up, down, right, left;
    public Transform med1, med2;
    public bool control;
    public int state;
	// Use this for initialization
	void Start () {
        //First control is false, put it in display***
        
        control = true;

        //4 state of spining of the medicine
        state = 0;
        med1.localPosition = new Vector3(-0.5f, 0, 0);
        med2.localPosition = new Vector3(0.5f, 0, 0);
	}
	
    void Ready()
    {
        //When central signal is ready, send it to the top***
        //Also, enable control***
    }

	// Update is called once per frame
	void Update () {

        if (down == false && control)
        {
            transform.Translate(0, -1 * Time.deltaTime, 0);

            if (Input.GetKeyDown(KeyCode.A) && !left)
            {
                transform.Translate(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && !right)
            {
                transform.Translate(1, 0, 0);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Move(0);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                Move(1);
            }
        }
        else
        {
            //Function to send signal to ready the next one, disable control***
            control = false;
        }
    }

    //direnction:   0 = Q = left
    //              1 = E = right
    void Move(int dir)
    {

        //In state that 2 med are next to each other where med1 = -0.5, med2 = 0.5
        if(state == 0)
        {
            Debug.Log("In state 0" + dir);  
            if(dir == 0 && !up)
            {
                //The right object move to top
                state = 1;
                med2.localPosition = new Vector3(med1.localPosition.x,1,0);
                
            }
            if(dir == 1 && !up)
            {
                //left object move to top
                state = 3;
                med1.localPosition = new Vector3(med2.localPosition.x, 1, 0);
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
                if (!right)
                {
                    med1.localPosition = new Vector3(med1.localPosition.x + 1, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x, 0, 0);
                }
                else
                {
                    med1.localPosition = new Vector3(med1.localPosition.x,0,0);
                    med2.localPosition = new Vector3(med2.localPosition.x - 1, 0, 0);
                }

            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 0;
                if (!left)
                {
                    med1.localPosition = new Vector3(med1.localPosition.x-1, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x, 0, 0);
                }
                else
                {
                    med1.localPosition = new Vector3(med1.localPosition.x, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x+1 , 0, 0);
                }
            }
        }


        //In state that med2 is on left and med1 is on right
        else if (state == 2)
        {
            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 3;

                med1.localPosition = new Vector3(med2.localPosition.x, 1, 0);

            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 1;
                med2.localPosition = new Vector3(med1.localPosition.x, 1, 0);
            }
        }

        //In state that med 1 is on top of med 2 
        else if (state == 3)
        {
            if (dir == 0 && !up)
            {
                //The right object move to top
                state = 0;
                if (!right)
                {
                    med1.localPosition = new Vector3(med1.localPosition.x, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x + 1, 0, 0);
                }
                else
                {
                    med1.localPosition = new Vector3(med1.localPosition.x -1, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x , 0, 0);
                }

            }
            if (dir == 1 && !up)
            {
                //left object move to top
                state = 2;
                if (!left)
                {
                    med1.localPosition = new Vector3(med1.localPosition.x, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x-1, 0, 0);
                }
                else
                {
                    med1.localPosition = new Vector3(med1.localPosition.x+1, 0, 0);
                    med2.localPosition = new Vector3(med2.localPosition.x , 0, 0);
                }
            }
        }

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
}
