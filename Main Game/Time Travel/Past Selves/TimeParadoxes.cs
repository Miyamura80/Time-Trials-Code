using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeParadoxes : MonoBehaviour
{
    private GameOver gameOver;
    private NewGodScript god;
    private NewGodScriptEnvironment godEnvironment;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = GameObject.FindGameObjectWithTag("Player").GetComponent<GameOver>();
        god = GameObject.FindGameObjectWithTag("god").GetComponent<NewGodScript>();
        godEnvironment = GameObject.FindGameObjectWithTag("god").GetComponent<NewGodScriptEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> gimms = godEnvironment.updates.gimms;

        for (int i = 0; i < gimms.Count; i++)
        {
            bool wrongState = godEnvironment.updates.StateAtTime(god.godTime, gimms[i]) != godEnvironment.oldUpdates.StateAtTime(god.godTime, gimms[i]);
            bool inRange = InRange(this.transform.position, gimms[i].transform.position);

            if (wrongState && inRange)
            {
                gameOver.killed = true;
            }
        }
    }

    bool InRange(Vector2 thingOne, Vector2 thingTwo)
    {
        return (thingOne - thingTwo).magnitude <= radius;
    }
}
