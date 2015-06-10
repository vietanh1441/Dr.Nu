using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class central : MonoBehaviour {
    public GameObject medi;
     public GameObject med1;

    //This will be based on each scene;
     public int level;
     public GameObject virus_gameObject;
    //List of virus
     public List<GameObject> virus = new List<GameObject>();
     private List<Vector3> gridPositions = new List<Vector3>();
     private int virus1_count, virus2_count, virus3_count;
     private int speed;
     private int timer;
    //public GameObject med2;
	// Use this for initialization
	void Start () {
        InitialiseList();
        virus.Clear();

        //Generate Board
        Generate();

        GetSignal();
        
	}


    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 36; x < 47; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 24; y <34; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }

        if(level == 1)
        {
            virus1_count = 5;
            virus2_count = 0;
            virus3_count = 0;
            speed = 5;
            timer = 120;
        }
    }


    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    void Generate()
    {
        //First, Initialize a list of each board

      
            //Set number of virus for each type
            int objectCount = 5;

            for (int i = 0; i < objectCount; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                //GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(virus_gameObject, randomPosition, Quaternion.identity);
            }
            

            //Set speed

            //Set timer
        
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
