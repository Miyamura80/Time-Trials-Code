using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGodScriptEnvironment : MonoBehaviour
{
    // Create an UpgradesContainer, CharacterControl and godScript
    public UpdatesContainer updates;
    public UpdatesContainer oldUpdates;
    NewGodScript god;

    // Start is called before the first frame update
    void Start()
    {
        // Create list of gimmicks
        List<GameObject> tempGimms = new List<GameObject>();
        tempGimms.AddRange(GameObject.FindGameObjectsWithTag("gimmick"));
        tempGimms.AddRange(GameObject.FindGameObjectsWithTag("gimmickNonWall"));

        // Create list of their initial states
        List<bool> tempInitialStates = new List<bool>();
        foreach (GameObject tempGimm in tempGimms)
        {
            tempInitialStates.Add(tempGimm.GetComponent<StateStorage>().state);
        }

        // Create updates from these two
        updates = new UpdatesContainer(tempGimms, tempInitialStates);
        oldUpdates = new UpdatesContainer(tempGimms, tempInitialStates);
        oldUpdates = new UpdatesContainer(tempGimms, tempInitialStates);

        // Makes god reference to the level's godScript
        god = GameObject.Find("gOD").GetComponent<NewGodScript>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % god.timeBetweenRecords == 0)
        {
            if (oldUpdates.times.Count > 0)
            {
                oldUpdates.ApplyUpdates(god.godTime);
            }
        }
    }

    public void Rewind(int restartTime)
    {
        // This sets oldUpdates to be the previous set of updates
        oldUpdates = updates.DeepCopy();

        // This brings the gimmicks to the correct states
        List<bool> newStates = updates.StatesAtTime(god.godTime);
        for (int i = 0; i < updates.gimms.Count; i++)
        {
            updates.gimms[i].GetComponent<StateStorage>().state = newStates[i];
        }
    }
}
