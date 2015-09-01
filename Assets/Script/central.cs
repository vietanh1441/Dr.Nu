using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;


public class central : MonoBehaviour {
    string[] lines;
    public GameObject medi;
    public GameObject holder;
    public Holder holder_scr;
     public GameObject med1, current_med, temp;
     public int turnCount;
    //This will be based on each scene;
     public int level;
     public GameObject virus_gameObject, virus1_gameObject, virus2_gameObject;
    //List of virus
     private List<GameObject> virus = new List<GameObject>();
     private List<GameObject> virus1 = new List<GameObject>();
     public List<GameObject> medicine_lst = new List<GameObject>();
     private List<GameObject> boss = new List<GameObject>();
     private List<Vector3> gridPositions = new List<Vector3>();
     public int virus1_count, virus2_count, virus3_count;
     public int speed;
     private GameObject UI1, UI2, UI3, UI4, UI5, UI6, UI7;
     private GameObject TimeUI, TurnUI, ScoreUI, EndScore,Objective;
     public int time;
     public int turn_down;
     private int score;
     public int color;
     private int multiply;
     private bool did_score;
     public int game_mode;
     public int downspeed;
     private GameObject store_place;
     private bool ingame;
     public bool right_st, left_st, down_st, turn_st;
    //public GameObject med2;
	// Use this for initialization
	void Start () {
        right_st = false;
        left_st = false;
        down_st = false;
        turn_st = false;

        ingame = false;
        UI1 = GameObject.Find("Pause_BG");
        UI2 = GameObject.Find("Continue");
        UI3 = GameObject.Find("Restart");
        UI4 = GameObject.Find("MainMenu");
        UI5 = GameObject.Find("NextLevel");
        UI6 = GameObject.Find("Start");
        UI7 = GameObject.Find("Options");
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        UI4.SetActive(false);
        UI5.SetActive(false);
        UI6.SetActive(false);
        UI7.SetActive(false);
        TimeUI = GameObject.Find("TimerUI");
        TurnUI = GameObject.Find("TurnUI");
        ScoreUI = GameObject.Find("ScoreUI");
        EndScore = GameObject.Find("EndScore");
        Objective = GameObject.Find("Objective");
        TimeUI.SetActive(false);
        TurnUI.SetActive(false);
        ScoreUI.SetActive(false);
        EndScore.SetActive(false);
        Objective.SetActive(false);
        Time.timeScale = 1;
        holder_scr = holder.GetComponent<Holder>();
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
        if(game_mode == 10) //endless mode
        {

        }
        if(game_mode == 11)
        {
            StartCountDown();
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

    bool CheckWin()
    {
        if(game_mode == 3)
        {
            if(boss.Count == 0)
            {
                Winning();
                return true;
            }
        }
        if(virus.Count == 0)
        {
            Winning();
            return true;
        }
        return false;
    }
    void Update()
    {
        if(UI6.activeSelf == true)
        {
            if(Input.anyKey)
            {
                ingame = true;
                UI6.SetActive(false);
                Time.timeScale = 1;
                Objective.SetActive(false);
            }
        }
        //If player click escape, enable the canvas, pause time
        Text timetext = TimeUI.GetComponent<Text>();
        Text turntext = TurnUI.GetComponent<Text>();
        Text scoretext = ScoreUI.GetComponent<Text>();
        timetext.text = "Time: " + time;
        turntext.text = "Turn: " + turnCount;
        scoretext.text = "Score: " + score*100;
        if (ingame)
        {
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Store();
            }
        }
        //right_st = false;
        //left_st = false;
       // down_st = false;
        //turn_st = false;
    }

    void EndlessGenerate()
    {
        if (this.enabled)
        {
            virus1_count++;
            if(virus1_count%3 == 0)
            {
                virus2_count++;
            }
            if(virus2_count%3 == 0)
            {
                virus3_count++;
            }
           // Debug.Log("Generate");
            InitialiseList();
            Generate();
        }    
            
    }

    void Winning()
    {
        if (game_mode == 10)
        {
            Invoke("EndlessGenerate", 0.5f);
            
        }
        else if ( game_mode == 11)
        {
            Invoke("EndlessGenerate", 0.5f);
            time = time + 10;
        }
        else
        {
            Time.timeScale = 0;
            //UI1.SetActive(true);
            //UI2.SetActive(true);
            UI3.SetActive(true);
            UI4.SetActive(true);
            UI5.SetActive(true);
            CalculateScore();
        }
    }

    void CalculateScore()
    {
        EndScore.SetActive(true);
        int endScore = 0;
        if(game_mode == 0)
        {
            endScore = score * 100;
        }
        if(game_mode == 1)
        {
            endScore = score * 100;
            endScore = endScore + time * 100;
        }
        EndScore.GetComponent<Text>().text = "Final Score: " + endScore;
    }
    public void Losing()
    {
        //CancelInvoke("decreaseTimeRemaining");
        //countDownMode = false;
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
            for (int y = 24; y <33; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }

        
    }

    public void Clear()
    {
        for(int i = 0; i < virus.Count; i++)
        {
            Destroy(virus[i]);
        }
        for(int i = 0; i < virus1.Count; i++)
        {
            if(virus1[i] != null)
            {
                Destroy(virus1[i]);
            }
        }
        for(int i = 0; i < medicine_lst.Count; i++)
        {
            medicine_lst[i].SendMessage("Damaged_Loss", 1);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        InitNewLevel();

        /*
        Clear();
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        UI4.SetActive(false);
        UI5.SetActive(false);
        //Restart counter
        turnCount = 0;

        med1 = Instantiate(medi, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        medicine_blitz med_sc = med1.GetComponent<medicine_blitz>();
        med_sc.control = false;
        Generate();
        GetSignal();
        InitialiseGameMode();

        UI6.SetActive(true);
        Time.timeScale = 0;
         * */
    }

    public void StartCountDown()
    {
        CancelInvoke("decreaseTimeRemaining");
        
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
        int randomIndex = UnityEngine.Random.Range(0, gridPositions.Count);

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
        med1 = (GameObject)Instantiate(medi, new Vector3(28.5f, 30, 0), Quaternion.identity);
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
      score = score+ num * multiply;
      multiply++;
      did_score = true;
  }

  

    void NewTurn()
    {
        turnCount++;

        if(game_mode == 2)
        {
            if(turnCount > turn_down)
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

    public void GoToMainMenu()
    {
        CancelInvoke("decreaseTimeRemaining");
        ingame = false;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(-10 , -10, -10);
        
        TimeUI.SetActive(false);
        TurnUI.SetActive(false);
        ScoreUI.SetActive(false);
        EndScore.SetActive(false);
        Objective.SetActive(false);
        Debug.Log(UI3);
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        UI4.SetActive(false);
        Time.timeScale = 1;
        UI5.SetActive(false);
        
    }

    public void GoToLevel(int value)
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(42, 31, -10);

        level = value;
        InitNewLevel();
    }

    public void GoToChoose()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(-10, 20, -10);

    }

    public void NextLevel()
    {
        if (level < 31)
        {
            level++;
            Debug.Log(level);
            InitNewLevel();
        }
        else
        {
            Debug.Log("You Break the game");
            InitNewLevel();
        }
    }

    public void Read()
    {
        if (level == 0)
        {

        }
        else
        {
            //lines = System.IO.File.ReadAllLines("Assets/Textfile.txt");
            string[] ssize;
            ssize = holder_scr.str[level - 1].Split(null);
            if (Convert.ToInt16(ssize[0]) == 1)
            {
                game_mode = 2;
                time = 99;
                turn_down = Convert.ToInt16(ssize[1]);
                color = Convert.ToInt16(ssize[2]);
                downspeed = Convert.ToInt16(ssize[3]);
                virus1_count = Convert.ToInt16(ssize[4]);
                virus2_count = Convert.ToInt16(ssize[5]);
                virus3_count = Convert.ToInt16(ssize[6]);
            }
            else if (Convert.ToInt16(ssize[0]) == 5)
            {
                game_mode = 1;
                time = Convert.ToInt16(ssize[1]);
                turn_down = 999;
                color = Convert.ToInt16(ssize[2]);
                virus1_count = Convert.ToInt16(ssize[3]);
                virus2_count = Convert.ToInt16(ssize[4]);
                virus3_count = Convert.ToInt16(ssize[5]);
            }
            else if (Convert.ToInt16(ssize[0]) == 10)
            {
                game_mode = 10;
                time = -1;
                turn_down = 99999999;
                color = 4;
                virus1_count = 3;
                virus2_count = 0;
                virus3_count = 0;
            }
            else if (Convert.ToInt16(ssize[0]) == 11)
            {
                game_mode = 11;
                time = 60;
                turn_down = 99999999;
                color = 4;
                virus1_count = 3;
                virus2_count = 0;
                virus3_count = 0;
            }
        }
    }


    void InitNewLevel()
    {
        Read();
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        UI4.SetActive(false);
        UI5.SetActive(false);
        Clear();
        med1 = Instantiate(medi, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        medicine_blitz med_sc = med1.GetComponent<medicine_blitz>();
        med_sc.control = false;
        EndScore.SetActive(false);
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
        TimeUI.SetActive(true);
        TurnUI.SetActive(true);
        ScoreUI.SetActive(true);
        EndScore.SetActive(true);
        Objective.SetActive(true);
        UI6.SetActive(true);
        
    }
}
