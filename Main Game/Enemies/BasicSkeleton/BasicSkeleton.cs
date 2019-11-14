using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkeleton : MonoBehaviour
{

    Pathfinding.AIDestinationSetter aiDest;
    Transform player;
    public Transform patrolTarget;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        aiDest = this.GetComponent<Pathfinding.AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position - this.transform.position).magnitude < range)
        {
            aiDest.target = player;
            this.GetComponent<AggroStr>().aggroed = true;
        } //If it is still returning to the patrol target
        else if ((patrolTarget.position - this.transform.position).magnitude > 1) {
            aiDest.target = patrolTarget;
            this.GetComponent<AggroStr>().aggroed = true;
        }
        else {
            aiDest.target = patrolTarget;
            this.GetComponent<AggroStr>().aggroed = false;
        }
        
    }


}
