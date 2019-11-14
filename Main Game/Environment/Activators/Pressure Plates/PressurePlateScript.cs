using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public int colourID;
    StateStorage childLock;
    private bool state;
    public Sprite PlateUp;
    public Sprite PlateDown;


    // Update is called once per frame
    void Update()
    {
        state = this.GetComponent<StateStorage>().state;
        if (state)
        {
            this.GetComponent<SpriteRenderer>().sprite = PlateDown;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = PlateUp;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Feet")
        {
            this.GetComponent<StateStorage>().UpdateState();

            foreach (Transform child in transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    childLock = child.GetComponent<StateStorage>();
                    childLock.UpdateState();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Feet")
        {
            //this.GetComponent<StateStorage>().state = !this.GetComponent<StateStorage>().state;
            this.GetComponent<StateStorage>().UpdateState();

            foreach (Transform child in transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    childLock = child.GetComponent<StateStorage>();
                    childLock.UpdateState();
                }
            }
        }
    }
}
