using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class central : MonoBehaviour {
    public GameObject medi;
     public GameObject med1, current_med, temp;
     public int turnCount;
    //This will be based on each scene;
     public int level;
     public GameObject virus_gameObject, virus1_gameObject, virus2_gameObject;
    //List of virus
     private List<GameObject> virus = new List<GameObject>();
     private List<GameObject> virus1 = new List<GameObject>();
     private List<GameObject> boss = new List<GameObject>();
     private List<Vector3> gridPositions = new List<Vector3>();
     public int virus1_count, virus2_count, virus3_count;
     public int speed;
     public GameObject UI1, UI2, UI3, UI4, UI5;
     public GameObject TimeUI;
     public int time;
     public int turn_down;
     private int score;
     public int color;
     private int multiply;
     private bool did_score;
     public int game_mode;
     private GameObject store_place;
    //public GameObject med2;
	// Use this for initialization
	void Start () {
        InitialiseList();
        virus.Clear();
        virus1.Clear();
        //Generate Board
        Generate();
        did_score = false;
        GetSignal();
        turnCount = 0;
        multiply = 1;
        score = 0;
        InitialiseGameMode();
        Time.timeScale = 0;
        UI2.SetActive(true);
	}

    void InitialiseGameMode()
    {
        if (game_mode == 1)
        {
            StartCountDown();
            //Deal with UI
        }
        if(game_mode == 2)
        {
            //Deal with UI
        }
        if(game_mode == 3)
        {
            //Deal with UI
        }
    }

    public void AddVirus(GameObject other)
    {
        virus.Add(other);
    }

    public void RemoveVirus(GameObject other)
    {
        virus.Remove(other);
        CheckWin();
    }


    public void AddVirus1(GameObject other)
    {
        virus1.Add(other);
    }

    public void AddBoss(GameObject other)
    {
        boss.Add(other);
    }

    public void RemoveBoss(GameObject other)
    {
        boss.Remove(other);
    }

    public void RemoveVirus1(GameObject other)
    {
        virus1.Remove(other);
    }

    void CheckWin()
    {
        if(game_mode == 3)
        {
            if(boss.Count == 0)
            {
                Winning();
            }
        }
        if(virus.Count == 0)
        {
            Winning();
        }
    }
    void Update()
    {
        //If player click escape, enable the canvas, pause time
        Text text = TimeUI.GetComponent<Text>();
        
        text.text = "Time: " + time;
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            UI1.SetActive(true);
            UI2.SetActive(true);
            UI3.SetActive(true);
            UI4.SetActive(true);
        }
        if (time == 0)
        {
            Losing();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Store();
        }
    }
    void Winning()
    {
        Time.timeScale = 0;
        UI1.SetActive(true);
        //UI2.SetActive(true);
        UI3.SetActive(true);
        UI4.SetActive(true);
        UI5.SetActive(true);
    }

    public void Losing()
    {
        Time.timeScale = 0;
        UI1.SetActive(true);
       // UI2.SetActive(true);
        UI3.SetActive(true);
        UI4.SetActive(true);
    }

    public void Continue()
    {
        //if player click continue, continue the game and set inactive the UI

        Time.timeScale = 1;
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        UI4.SetActive(false);
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

        
    }

    public void StartCountDown()
    {
       
            InvokeRepeating("decreaseTimeRemaining", 1.0f, 1.0f);
     

    }

   

    void decreaseTimeRemaining()
    {
        time--;
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
            

            for (int i = 0; i < virus1_count; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                //GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(virus_gameObject, randomPosition, Quaternion.identity);
            }

            for (int i = 0; i < virus2_count; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                //GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(virus1_gameObject, randomPosition, Quaternion.identity);
            }

            for (int i = 0; i < virus3_count; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                //GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(virus2_gameObject, randomPosition, Quaternion.identity);
            }

            //Set speed

            //Set timer
        
    }


    //call when a medicine touch down, immediately send the listed medicine signal to be ready
    void GetSignal()
    {
        NewTurn();
        //Send signal to the current med1
        med1.SendMessage("Ready");
        current_med = med1;
        //Create a new gameobject at the show place and disable control
        med1 = (GameObject)Instantiate(medi, new Vector3(30.5f, 30, 0), Quaternion.identity);
        medicine_blitz med_sc = med1.GetComponent<medicine_blitz>();
        med_sc.control = false;
    }

    void Store()
    {
        //First, check if store space is empty
        if(store_place == null)
        {
            Debug.Log(store_place);
            //If empty, store the current medicine to right place, and turn off ready and control
            current_med.SendMessage("Store");
            store_place = current_med;
            //store_place = current_med;
            med1.SendMessage("Ready");
            current_med = med1;
            //Create a new gameobject at the show place and disable control
            med1 = (GameObject)Instantiate(medi, new Vector3(30.5f, 30, 0), Quaternion.identity);
            medicine_blitz med_sc = med1.GetComponent<medicine_blitz>();
            med_sc.control = false;
        }
        else
        {
            current_med.SendMessage("Store");
            temp = store_place;
            store_place = current_med;
            current_med = temp;
            current_med.SendMessage("Ready");
        }
        


        //Do GetSignal without making new turn()


        //Else, switch place between the current and store
    }
	// Update is called once per frame
	

  public void Scoring(int num)
  {
      Debug.Log("Score");
      score = score+ num * multiply;
      multiply++;
      did_score = true;
  }

  

    void NewTurn()
    {
        turnCount++;
        Debug.Log(turnCount);
        if(game_mode == 2)
        {
            if(turnCount >= turn_down)
            {
                Losing();
            }
        }
        int i = 0;
        for(i = 0; i < virus1.Count; i++)
        {
            virus1[i].SendMessage("TurnPlus");
        }
        if(did_score)
        {
            did_score = false;
        }
        else
        {
            if (multiply > 1)
                multiply--;
        }
    }
}
