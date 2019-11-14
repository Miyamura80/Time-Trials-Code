using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Fix stuff with constant time rewind
//TODO: update states of the environment when initially rewinding

public class godScript : MonoBehaviour
{

    public int godTime;
    //Can be winded back -> absolute environment time
    public int godUnit;
    //Cannot be winded back, relative counter for the current player
    private int moveAddCounter;
    private int UnitRef;
    private Follow mini;

    public int generation;
    public int frequencyKinda=3;
    public GameObject pastSelfPrehab;

    //Time Machine stuffs
    public List<GameObject> pastObjRecord = new List<GameObject>();
    public List<List<List<float>>> godRecord = new List<List<List<float>>>();
    Transform trs;

    godScriptEnvironment gsEnv;

    // Start is called before the first frame update
    void Start()
    {
        godTime = 0;
        trs = GameObject.FindGameObjectWithTag("Player").transform;
        generation = 0;
        godUnit = 0;
        moveAddCounter = 0;
        godRecord.Add(new List<List<float>>());
        gsEnv = GameObject.FindGameObjectWithTag("god").GetComponent<godScriptEnvironment>();
        mini = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<Follow>();
    }


    void FixedUpdate() {

        if (godTime % frequencyKinda == 0)
        {
            //Adding the current movements of the current player
            try
            {
                godRecord[generation].Add(new List<float>());
                godRecord[generation][moveAddCounter].Add(godUnit);
                godRecord[generation][moveAddCounter].Add(trs.position.x);
                godRecord[generation][moveAddCounter].Add(trs.position.y);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Debug.Log("section 1");
            }

                

            //If time travelled at least once, move all past selves this way
            //Also involves environmental updates
            if (generation > 0)
            {
                for (int gen = 0; gen < generation; gen++)
                {
                    try
                    {
                        //PastSelf update
                        if (godUnit < godRecord[gen].Count && (pastObjRecord[gen]) != null)
                        {
                            UnitRef = GetIndexForGodUnit(godRecord[gen]);
                            pastObjRecord[gen].transform.position = new Vector2(godRecord[gen][UnitRef][1], godRecord[gen][UnitRef][2]);
                        }
                        else if (godUnit == godRecord[gen].Count)
                        {
                            Destroy(pastObjRecord[gen]);
                            mini.followee = GameObject.FindGameObjectWithTag("Player").transform;
                            foreach (GameObject gm in gsEnv.interactibles) {
                                //FOR DEBUG
                                gm.GetComponent<SpriteRenderer>().color = Color.white;
                            }
                        }


                            
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        Debug.Log("section 2");
                    }
                    catch (MissingReferenceException)
                    {
                        Debug.Log("Missing 1");
                    }
                }
            }

            godUnit++;
            moveAddCounter++;

        }
        godTime++;
        
    }

    public void rewind(bool restart = true, int unit=0) {
        List<bool> rewindedStates;

        gsEnv.oldUpdates = CopyUpdatesList( gsEnv.updates);
        gsEnv.updates.Clear();
        gsEnv.updatesCount = 0;

        //Time management
        if (restart)
        {
            godTime = 0;
            godUnit = 0;
        }
        else {
            godTime -= unit*frequencyKinda;
            godUnit -= unit;
        }

        //Destroy all past selves present
        try
        {
            foreach (GameObject pastSelfClone in GameObject.FindGameObjectsWithTag("timeTraveller"))
            {
                if (IsNotNull(pastSelfClone))
                {
                    Destroy(pastSelfClone);
                }
            }
            pastObjRecord.Clear();
        }
        catch (MissingReferenceException) {
            Debug.Log("Missing 2");
        }



        moveAddCounter = 0;
        generation++;

        
        if (godUnit >= 0) {
            //Create instances of past selves according to their times
            for (int i = 0; i < generation; i++)
            {
                try
                {
                    UnitRef = GetIndexForGodUnit(godRecord[i]);
                    if (UnitRef!=-1) {
                        pastObjRecord.Add(Instantiate(pastSelfPrehab,
                            new Vector2(godRecord[i][UnitRef][1], godRecord[i][UnitRef][2]),
                            Quaternion.identity) as GameObject);
                        mini.followee = pastObjRecord[pastObjRecord.Count - 1].transform;
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Debug.Log("section 3");
                    Debug.Log(godRecord.Count);
                    Debug.Log(godRecord[i].Count);
                    Debug.Log(i);
                    Debug.Log(godUnit);
                }
            }

            //Rewinding the environment to that time
            rewindedStates = gsEnv.computeStatesAtUnit(gsEnv.oldUpdates);

            for (int i = 0; i < gsEnv.states.Count; i++) {
                gsEnv.interactibles[i].GetComponent<StateStorage>().state = rewindedStates[i];
                gsEnv.states[i] = rewindedStates[i];
            }


        }


        godRecord.Add(new List<List<float>>());

    }

    private bool IsNotNull(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        else {
            return true;
        }

    }

    private int GetIndexForGodUnit(List<List<float>> timeLine) {
        for (int i = 0; i < timeLine.Count;i++) {
            if (timeLine[i][0]==godUnit) {
                return i;
            }
        }
        Debug.Log("valid index cannot be found");
        return -1;

    }

    private static List<List<int>> CopyUpdatesList(List<List<int>> list)
    {
        List<List<int>> newList = new List<List<int>>();
        for (int i=0;i<list.Count;i++)
        {
            newList.Add(new List<int>());
            newList[i].Add(list[i][0]);
            newList[i].Add(list[i][1]);
        }
        return newList;
    }
}
