using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGodScript : MonoBehaviour
{
    // List of records of pastselves
    public List<PastSelfRecord> records;
    // Reference to current player GameObject
    GameObject player;
    // Record for current player
    public PastSelfRecord playerRecord;
    // List of all the currently active past selves
    public List<GameObject> activeSelves;
    // Reference to godEnvironment script on gOD
    NewGodScriptEnvironment godEnvironment;

    // Reference to the past self prehab
    public GameObject pastSelfPrehab;
    // Goes up by one every "timeBetweenRecords" number of frames
    public int godTime = 0;
    // Number of frames before "godTime" goes up by one
    public int timeBetweenRecords = 3;

    public bool rewinded=false;

    private void Start()
    {
        records = new List<PastSelfRecord>();
        player = GameObject.Find("Character");
        playerRecord = new PastSelfRecord(0);
        activeSelves = new List<GameObject>();
        godEnvironment = GameObject.Find("gOD").GetComponent<NewGodScriptEnvironment>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % timeBetweenRecords == 0) // so we don't have to record every single frame
        {
            playerRecord.Add(player.transform.position); // add the player's lastest movement to their record

            // The foreach-loop creates, moves and destroys all the pastselves
            // All the index stuff (indexDecrease and record.index) is basically just a very clever way to find the right pastself from the "activeSelves" list
            int indexDecrease = 0;
            foreach (PastSelfRecord record in records) // for each recorded pastself record
            {
                if (godTime == record.startTime) // if this record indicates the pastself starts now
                {
                    record.index = activeSelves.Count;
                    activeSelves.Add(Instantiate(pastSelfPrehab)); // add a new pastself prehab to activeSelves
                    record.Use(godTime, activeSelves[record.index - indexDecrease]); // the new pastself is displayed in its starting location
                }

                if (record.isActive(godTime)) // if this record indicates the pastself is existing now
                {
                    record.Use(godTime, activeSelves[record.index - indexDecrease]); // the pastself is displayed in a new location
                    record.UseEffects(godTime, activeSelves[record.index - indexDecrease]); // Adds any relevent effects (eg invisibility)
                }

                if (godTime == record.endTime) // if this record indicates the pastself disapears (ends) now
                {
                    record.Use(godTime, activeSelves[record.index - indexDecrease]); // the pastself is displayed in a new location for the last time
                    Destroy(activeSelves[record.index - indexDecrease]); // Remove the instance of the prehap from existance
                    activeSelves.Remove(activeSelves[record.index - indexDecrease]); // the pastself prehab is removed from activeSelves
                    indexDecrease++;
                }

                record.index -= indexDecrease;
            }

            godTime++;
        }
    }

    public void Rewind(int restartTime)
    {
        rewinded = true;
        playerRecord.endTime = godTime - 1; // set the endtime of the player's record
        godTime = restartTime; // resets the godTime 
        records.Add(playerRecord); // adds the player's record to the list of records
        playerRecord = new PastSelfRecord(godTime); // player's record is blanked
        // Current activeSelves are destroyed
        foreach (GameObject self in activeSelves)
        {
            Destroy(self);
        }
        // new activeSelves are set (alongside the corresponding record indexes)
        List<GameObject> newActiveSelf = new List<GameObject>();
        foreach (PastSelfRecord record in records)
        {
            if (record.isActive(godTime))
            {
                record.index = newActiveSelf.Count;
                newActiveSelf.Add(Instantiate(pastSelfPrehab));
            }
        }
        activeSelves = newActiveSelf;
    }
}
