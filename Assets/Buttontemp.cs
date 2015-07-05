using UnityEngine;
using System.Collections;

public class Buttontemp : MonoBehaviour {
    public int level;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoTo()
    {
        
        Application.LoadLevel(level);
    }

}
