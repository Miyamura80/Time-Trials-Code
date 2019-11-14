using System.Collections.Generic;
using UnityEngine;

public class godScriptEnvironment : MonoBehaviour
{
    CharacterControl cctrl;
    godScript gs;

    //Environmental stuff
    public List<bool> states = new List<bool>();
    List<bool> initialConditions = new List<bool>();
    public List<GameObject> interactibles = new List<GameObject>();
    public List<List<int>> updates = new List<List<int>>();   //Format of update should be [ [index,time] ]
    public List<List<int>> oldUpdates = new List<List<int>>();
    public int updatesCount;
    private List<int> tempUpdateIndexes;
    private int stopIndexEnv;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gimmick in GameObject.FindGameObjectsWithTag("gimmick"))
        {
            initialConditions.Add(gimmick.GetComponent<StateStorage>().state);
            states.Add(gimmick.GetComponent<StateStorage>().state);
            interactibles.Add(gimmick);
        }
        foreach (GameObject gimmick in GameObject.FindGameObjectsWithTag("gimmickNonWall"))
        {
            initialConditions.Add(gimmick.GetComponent<StateStorage>().state);
            states.Add(gimmick.GetComponent<StateStorage>().state);
            interactibles.Add(gimmick);
        }
        updatesCount = 0;
        cctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
        gs = GameObject.FindGameObjectWithTag("god").GetComponent<godScript>();

    }

    void FixedUpdate()
    {
        if (gs.godTime % gs.frequencyKinda == 0)
        {
            printStates();
            

            if (gs.generation > 0)
            {//IF ALL GOES WRONG PUT OLDUPDATES BACK TO UPDATES

                //Environment update
                tempUpdateIndexes = updateIndex(oldUpdates, gs.godUnit);
                if (tempUpdateIndexes.Count != 0)
                {
                    for (int i = 0; i < tempUpdateIndexes.Count; i++)
                    {
                        int b = tempUpdateIndexes[i];
                        bool a = interactibles[oldUpdates[b][0]].GetComponent<StateStorage>().state;
                        interactibles[oldUpdates[b][0]].GetComponent<StateStorage>().state = !a;
                        states[oldUpdates[b][0]] = !a;
                        //TODO: Gotta put smth in which is better than this
                        cctrl.up8 = false;
                        cctrl.down8 = false;
                        cctrl.right8 = false;
                        cctrl.left8 = false;
                        
                    }
                }
            }

            stopIndexEnv = updateIndexForGodUnit(gs.godUnit, updates);
            //Adding environmental gimmick information if any variation occurs
            for (int i = 0; i < interactibles.Count; i++)
            {
                if (interactibles[i].GetComponent<StateStorage>().state != states[i])
                {
                    updates.Add(new List<int>());
                    updates[updatesCount].Add(i);
                    updates[updatesCount].Add(gs.godUnit);
                    updatesCount++;
                    states[i] = !(states[i]);
                }
            }
        }
    }


    public List<bool> computeStatesAtUnit(List<List<int>> particularUpdates)
    {
        int stopIndex = 0;
        int tempIndexForGimmick;
        List<bool> computedStates = new List<bool>();

        //Initialize states back to initial conditions
        for (int j = 0; j < states.Count; j++)
        {
            computedStates.Add(initialConditions[j]);
        }

        stopIndex = updateIndexForGodUnit(gs.godUnit, particularUpdates);

        //Update every index to time
        for (int j = 0; j < stopIndex; j++)
        {
            tempIndexForGimmick = particularUpdates[j][0];
            computedStates[tempIndexForGimmick] = !(computedStates[tempIndexForGimmick]);
        }
        return computedStates;
    }

    private int updateIndexForGodUnit(int unit, List<List<int>> particularUpdates)
    {
        int i = 0;
        int stopIndex = 0;

        //Determine which index to stop at
        if ((particularUpdates.Count - 1 >= 0) && (particularUpdates[particularUpdates.Count - 1][1] > unit))
        {
            while (unit > particularUpdates[i][1])
            {
                i++;
                stopIndex++;
            }
        }
        else
        {
            stopIndex = particularUpdates.Count;
        }
        return stopIndex;
    }

    //Returns a list of indexes that should be changed at a specific godUnit
    private List<int> updateIndex(List<List<int>> updatesList, int unit)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < updatesList.Count; i++)
        {
            if (updatesList[i][1] == unit)
            {
                indexes.Add(i);
            }
        }
        return indexes;
    }

    private void printStates() {
        string stateBoiz="";
        for (int i=0;i<states.Count;i++) {
            if (states[i])
            {
                stateBoiz += "1";

            }
            else {
                stateBoiz += "0";
            }
        }
        print(stateBoiz);
    }
}
