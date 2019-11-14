using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMachine : MonoBehaviour
{

    NewGodScript god;
    NewGodScriptEnvironment godEnvironment;
    public bool onMachine;
    private bool backToStart=true;
    public int RewindUnit;

    // Start is called before the first frame update
    void Start()
    {
        onMachine = false;
        god = GameObject.Find("gOD").GetComponent<NewGodScript>();
        godEnvironment = GameObject.Find("gOD").GetComponent<NewGodScriptEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onMachine == true) {
            if (backToStart)
            {
                Rewind();
            }
            else
            {
                Rewind(god.godTime - RewindUnit);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            onMachine = true;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        onMachine = false;
    }

    private void Rewind(int restartTime = 0)
    {
        god.Rewind(restartTime);
        godEnvironment.Rewind(restartTime);
    }
}
