using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(transform.tag != "SideWall")
        transform.tag = "Wall";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
