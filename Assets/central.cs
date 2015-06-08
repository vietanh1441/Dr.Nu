using UnityEngine;
using System.Collections;

public class central : MonoBehaviour {
    public GameObject medi;
     public GameObject med1;
    //public GameObject med2;
	// Use this for initialization
	void Start () { 
        GetSignal();
        
	}
	
    //call when a medicine touch down, immediately send the listed medicine signal to be ready
    void GetSignal()
    {
        //Send signal to the current med1
        med1.SendMessage("Ready");

        //Create a new gameobject at the show place and disable control
        med1 = (GameObject)Instantiate(medi, new Vector3(30.5f, 30, 0), Quaternion.identity);
        medicine med_sc = med1.GetComponent<medicine>();
        med_sc.control = false;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
