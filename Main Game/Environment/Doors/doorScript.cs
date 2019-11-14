using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    private StateStorage stateStorage;
    private bool touching;
    public Sprite doorOpen;
    public Sprite doorClosed;
    CharacterControl cctrl;

    // Start is called before the first frame update
    void Start()
    {
        stateStorage = this.GetComponent<StateStorage>();
        touching = false;
        //this.gameObject.tag = "gimmick";
        cctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touching == true)
        {
            stateStorage.UpdateState();
            if (stateStorage.state) {
                cctrl.up8 = false;
                cctrl.down8 = false;
                cctrl.right8 = false;
                cctrl.left8 = false;
            }

        }
        //Must be done every second to account for changes with time travel
        if (stateStorage.state)
        {
            this.GetComponent<SpriteRenderer>().sprite = doorOpen;
            this.gameObject.tag = "gimmickNonWall";

        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = doorClosed;
            this.gameObject.tag = "gimmick";

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
