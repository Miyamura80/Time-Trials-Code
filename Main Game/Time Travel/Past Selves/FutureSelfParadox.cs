using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSelfParadox : MonoBehaviour
{
    private Transform player;
    private GameOver gameOver;
    public float radius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameOver = GameObject.FindGameObjectWithTag("Player").GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange(player.position, this.transform.position))
        {
            print("Killed by self");
            gameOver.killed = true;
        }
    }

    bool InRange(Vector2 thingOne, Vector2 thingTwo)
    {
        return (thingOne - thingTwo).magnitude <= radius;
    }
}
