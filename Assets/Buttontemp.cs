using UnityEngine;
using System.Collections;

public class Buttontemp : MonoBehaviour {
    public int level;
    public string hit;
    public int value;
    public int type;
    public GameObject central_obj;
    public central central_scr;
    public Sprite sprite;
    private Sprite cur;
	// Use this for initialization
	void Start () {
        central_obj = GameObject.Find("Central");
        central_scr = central_obj.GetComponent<central>();
        cur = gameObject.GetComponent<SpriteRenderer>().sprite;
	}

    void OnMouseDown()
    {
        if (type != 10)
        {
            Invoke(hit, 0.1f);
        }
        else if(type == 10)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            if(value == 0)
            {
                central_scr.left_st = true;
            }
            if(value == 1)
            {
                central_scr.right_st = true;
            }
            if(value == 2)
            {
                central_scr.down_st = true;
            }
            if(value == 3)
            {
                central_scr.turn_st = true;
            }
               
        }
    }

	// Update is called once per frame
	void Update () {

	}

    public void GoTo()
    {

        central_scr.GoToLevel(value);
    }

    public void NextLevel()
    {
        central_scr.NextLevel();
    }

    public void OnMouseEnter()
    {
        if(type!= 10)
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnMouseExit()
    {
        if (type != 10)
        gameObject.GetComponent<SpriteRenderer>().sprite = cur;
    }


    public void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = cur;
        if(type == 10 && value == 2)
        {
            central_scr.down_st = false;
        }
    }

    public void Play()
    {
        central_scr.GoToChoose();
    }
}
