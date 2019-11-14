using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public int colourID;
    StateStorage childLock;
    private bool touching;
    public Sprite leverTurnLeft;
    public Sprite leverTurnRight;

    // Start is called before the first frame update
    void Start()
    {
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touching)
        {
            foreach (Transform child in transform)
            {
                //Checking if the child object is not the minimap object by comparison of layer
                //May need tweaking if more children are added to the lever
                if (child.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    childLock = child.GetComponent<StateStorage>();
                    childLock.UpdateState();
                }
            }
            this.GetComponent<StateStorage>().UpdateState();
        }

        //Must be called every second to be updated by time travel
        if (this.GetComponent<StateStorage>().state)
        {
            this.GetComponent<SpriteRenderer>().sprite = leverTurnLeft;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = leverTurnRight;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (
           (col.gameObject.name == "TopCol") |
           (col.gameObject.name == "BottomCol") |
           (col.gameObject.name == "LeftCol") |
           (col.gameObject.name == "RightCol")
           )
        {
            touching = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        touching = false;
    }
}
