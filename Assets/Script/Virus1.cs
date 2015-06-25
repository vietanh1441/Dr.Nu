using UnityEngine;
using System.Collections;

public class Virus1 : MonoBehaviour
{
    public GameObject right, left, up, down;
    public int color;
    public int type;
    public GameObject central_obj;
    public central central_scr;
    public GameObject link;
    public bool is_link;
    private SpriteRenderer spriteRenderer;
    public int turnCount;
    public Sprite[] color_sprite = new Sprite[5];

    // Use this for initialization
    void Start()
    {
        central_obj = GameObject.FindGameObjectWithTag("Central");
        central_scr = central_obj.GetComponent<central>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (type == 0)
        {
            spriteRenderer.sprite = color_sprite[0];
        }
        central_scr.AddVirus(gameObject);
        if(type == 1)
        {
            central_scr.AddVirus1(gameObject);
        }
        //First, use random to randomize the color. The maximum color will be based on GameManager
        Init_color();
    }

    void Init_color()
    {
        color = Random.Range(0, 4);
        if (color == 0)
        {
            transform.tag = "Yellow";
            spriteRenderer.color = Color.yellow;
        }
        if (color == 1)
        {
            transform.tag = "Blue";
            spriteRenderer.color = Color.blue;
        }
        if (color == 2)
        {
            transform.tag = "Red";
            spriteRenderer.color = Color.red;
        }
        if (color == 3)
        {
            transform.tag = "Green";
            spriteRenderer.color = Color.green;
        }

    }

    void Update()
    {
        
    }

    void Check(int dir)
    {
        bool vertical = false;
        bool horizontal = false;
        CheckAll();
        if (dir == 0)
        {
            if (down != null)
            {
                if (down.tag == transform.tag)
                {
                    Debug.Log("Check");
                    down.SendMessage("Check", 2);
                    if (up != null)
                    {
                        if (up.tag == transform.tag)
                            vertical = true;
                    }
                }
            }
            if (up != null)
            {
                if (up.tag == transform.tag)
                {
                    up.SendMessage("Check", 1);
                }
            }
            if (right != null)
            {
                if (right.tag == transform.tag)
                {
                    right.SendMessage("Check", 3);
                    if (left != null)
                    {
                        if (left.tag == transform.tag)
                            horizontal = true;
                    }
                }
            }
            if (left != null)
            {
                if (left.tag == transform.tag)
                {
                    left.SendMessage("Check", 4);
                }
            }

        }

        //Check up
        if (dir == 1)
        {
            if (up != null)
            {
                if (up.tag == transform.tag)
                {
                    up.SendMessage("Check", 1);
                    vertical = true;
                }
            }
        }

        //Check down
        if (dir == 2)
        {
            if (down != null)
            {
                if (down.tag == transform.tag)
                {
                    down.SendMessage("Check", 2);
                    vertical = true;
                }
            }
        }

        //Check right
        if (dir == 3)
        {
            if (right != null)
            {
                if (right.tag == transform.tag)
                {
                    right.SendMessage("Check", 3);
                    horizontal = true;
                }
            }
        }

        //Check left
        if (dir == 4)
        {
            if (left != null)
            {
                if (left.tag == transform.tag)
                {
                    left.SendMessage("Check", 4);
                    horizontal = true;
                }
            }
        }

        if (vertical == true)
        {
            down.SendMessage("Damaged_Loss", 1);
            up.SendMessage("Damaged_Loss", 1);

        }
        if (horizontal == true)
        {
            right.SendMessage("Damaged_Loss", 1);
            left.SendMessage("Damaged_Loss", 1);
        }
        if (vertical || horizontal)
            Damaged_Loss(1);

    }

    void Damaged_Loss(int loss)
    {
        central_scr.RemoveVirus(gameObject);
        if(type == 1)
        {
            central_scr.RemoveVirus1(gameObject);
        }
        Destroy(gameObject);
    }

    IEnumerator Delay()
    {

        yield return new WaitForSeconds(0.5f);
        Check(0);
    }

    void CheckAll()
    {
        CheckDown();
        CheckLeft();
        CheckRight();
        CheckUp();
    }

    void CheckDown()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), -Vector2.up, 0.1f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != down)
            {
                down = up1.transform.gameObject;
                StartCoroutine("Delay");
            }

        }
        else
        {
            down = null;
        }
    }

    void CheckRight()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right, 0.4f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != right)
                right = up1.transform.gameObject;
        }
        else
        {
            right = null;
        }
    }

    void CheckLeft()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), -Vector2.right, 0.4f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != left)
                left = up1.transform.gameObject;
        }
        else
        {
            left = null;
        }
    }

    void CheckUp()
    {
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.up, 0.4f);
        if (up1.transform != null)
        {
            if (up1.transform.gameObject != up)
                up = up1.transform.gameObject;
        }
        else
        {
            up = null;
        }
    }


    void State(int state)
    {

        spriteRenderer.sprite = color_sprite[state];

    }

    void TurnPlus()
    {
        turnCount++;
        if(type == 1)
        if(turnCount % 3 == 0)
        {
            DoVirusStuff1();
        }
        //Here if virus type is different, do according stuff
    }

    void DoVirusStuff1()
    {
        //The virus randomly move right or left
        //First get random direction
        //then check for that direction, if doesn't work, check the other
        //if both doesn't work, don't move

        int move = 0;
        RaycastHit2D right = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right, 0.4f);
        RaycastHit2D left = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), -Vector2.right, 0.4f);
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            if (right.transform == null)
            {
                move = 1;
            }
            else if (left.transform == null)
            {
                move = -1;
            }
            else
            {
                move = 0;
            }
        }
        else
        {
            if (left.transform == null)
            {
                move = -1;
            }
            else if (right.transform == null)
            {
                move = 1;
            }
            else
            {
                move = 0;
            }
        }
        transform.Translate(move, 0, 0);
    }

    void OnDestroy()
    {
        if (type == 0)
        {
            central_scr.RemoveVirus(gameObject);
        }
        
    }


    void Break_link()
    {
        transform.parent = null;
        link = null;
        is_link = false;
        spriteRenderer.sprite = color_sprite[4];
    }
}
