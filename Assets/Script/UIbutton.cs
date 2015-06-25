using UnityEngine;
using System.Collections;

public class UIbutton : MonoBehaviour {

    
    


    //Determine if this button is available for clicking
    public bool available;

    //Id of the button that is asign and used by the central;
    //0: Adventure mode
    //8: Blitz mode
    //1: Profile
    //2: Options
    //3: Quit
    //4: Stage Select
    //5: Pause
    //6: Restart
    //7: Back to main menu
    public int id;

	// Use this for initialization
	void Start () {
        //How to determine if a button is available?
        //Have a central unit that can't be erased hold every id for button
       
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    //When mouse press down the button, do stuff
    void OnMouseDown()
    {
        if(available == true)
        {
            switch(id)
            {

            }
        }
    }
}
