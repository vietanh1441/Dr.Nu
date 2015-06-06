using UnityEngine;
using System.Collections;

public class medicine : MonoBehaviour {
    public bool up, down, right, left;
    public Transform med1, med2;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
        if (down == false)
            {
                transform.Translate(0, -1 * Time.deltaTime, 0);
            }
        if(Input.GetKeyDown(KeyCode.A) && !left)
        {
            transform.Translate(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D) && !right)
        {
            transform.Translate(1, 0, 0);
        }
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
