using UnityEngine;
using System.Collections;

public class medicine : MonoBehaviour {
    public bool up, down, right, left;
    public Transform med1, med2;
    public bool control;
	// Use this for initialization
	void Start () {
        //First control is false, put it in display***
        
        control = true;
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
        }
        else
        {
            //Function to send signal to ready the next one, disable control***
            control = false;
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
}
