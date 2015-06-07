using UnityEngine;
using System.Collections;

public class central : MonoBehaviour {
    public GameObject medi;
     public GameObject med1;
    //public GameObject med2;
	// Use this for initialization
	void Start () {
        med1 = (GameObject)Instantiate(medi, new Vector3( 40.5f, 40, 0), Quaternion.identity);
        //public medicine med2;
        //    med2 = Instantiate(medi, new Vector3(335.5f, 40, 0), Quaternion.identity)as medicine;
        //GameObject med2 = GameObject.Instantiate(medi, new Vector3(35.5f, 40, 0), Quaternion.identity)as GameObject;
        medicine med_sc = med1.GetComponent<medicine>();
        Debug.Log(med_sc.control);
        med_sc.control = false;
        
        
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
