using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGodScript : MonoBehaviour
{
    public List<EnemyChaseTimeRec> aggroRecord;
    Transform playerTrs;
    private NewGodScript gscript;
    private int timeBetweenRecords;
    public float range = 4;
    private int godTimeForEnemy;

    // Start is called before the first frame update
    void Start()
    {
        aggroRecord = new List<EnemyChaseTimeRec>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            aggroRecord.Add(new EnemyChaseTimeRec(enemy));
        }
        godTimeForEnemy = 0;
        playerTrs = GameObject.FindWithTag("Player").transform;
        gscript = GameObject.Find("gOD").GetComponent<NewGodScript>();
        timeBetweenRecords = gscript.timeBetweenRecords;
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % timeBetweenRecords == 0) // so we don't have to record every single frame
        {
            
            godTimeForEnemy = gscript.godTime;


            for (int i = 0; i < aggroRecord.Count; i++)
            {
                if (gscript.rewinded)
                {
                    aggroRecord[i].moveIfTime(godTimeForEnemy);
                    print("Round 2 bby");
                }
                else
                {
                    aggroRecord[i].updateR(godTimeForEnemy);
                }
            }
        }


        
    }

}
