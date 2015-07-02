using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //the current level that player is playing.
    //When user start new game, load it.
    public int currentLvl;

    public int savedLvl;


    void Awake()
    {
        currentLvl = Application.loadedLevel;
        DontDestroyOnLoad(transform.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }




    void OnLevelWasLoaded(int level)
    {
        currentLvl = Application.loadedLevel;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Called by Adventure mode, go to the current level
    void GoToCurrentLvl()
    {
        Application.LoadLevel(savedLvl);
    }

    void GoToBlitz()
    {
        Application.LoadLevel(1);
    }

    void GoToQuickPlay()
    {
        Application.LoadLevel(2);
    }

    void GoToLevel(int lvl)
    {
        Application.LoadLevel(lvl);
    }

    void GoToNextLevel()
    {
        Application.LoadLevel(currentLvl + 1);
        if(currentLvl + 1 > savedLvl)
        {
            savedLvl = currentLvl + 1;
        }
    }

    void ReplayLevlel()
    {
        Application.LoadLevel(currentLvl);
    }
}
