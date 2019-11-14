using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTimeRec
{

    //Time when the chase started
    public int startTime;

    //Time when the chase ended, i.e. when returned to its post
    public int endTime;

    public GameObject enemyObj;

    //Bool to know if it should be kept updating,m

    private bool keepRecording;

    public List<Vector2> positions = new List<Vector2>();
    public List<int> correspondingTimes = new List<int>();

    public GameObject player;

    public EnemyChaseTimeRec(GameObject obj)
    {
        this.enemyObj = obj;
    }

    public void updateR(int time)
    {
        if (enemyObj.GetComponent<AggroStr>().aggroed)
        {
            positions.Add(enemyObj.transform.position);
            correspondingTimes.Add(time);
        }
    }

    //TODO: Perhaps optimize this, since done in chunks
    //Does moving, but also does checking for paradox
    public void moveIfTime(int time) {
        for (int i=0;i<correspondingTimes.Count;i++) {
            if (correspondingTimes[i] == time) {
                if (((player.transform.position - enemyObj.transform.position).magnitude < 4))
                {
                    player.GetComponent<GameOver>().killed = true;
                }
                enemyObj.transform.position = positions[i];
                return;
            } 
        }
    }

    /*
    public bool checkParadoxWithObj(GameObject obj) {
        if (((obj.transform.position-enemyObj.transform.position).magnitude < 4)&&) {
            return true;
        }
        return false;
    }
    */



}
