using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdatesContainer
{
    // List of things to update
    public List<GameObject> gimms;
    // Corresponding List of their initial conditions
    public List<bool> initialStates;

    // List of times of updates (initially empty)
    public List<int> times;
    // Corresponding list of what's being updated (as an index of gimms, rather than a GameObject)
    private List<int> thingIndexes;

    // Creating the thing
    public UpdatesContainer(List<GameObject> gimms, List<bool> initialStates)
    {
        this.gimms = gimms;
        this.initialStates = initialStates;
        times = new List<int>();
        thingIndexes = new List<int>();
    }

    // Method to add an update to the record
    public void Add(int time, GameObject gimm)
    {
        int gimmIndex = gimms.IndexOf(gimm);
        int index = IndexToInsert(time);

        times.Insert(index, time);
        thingIndexes.Insert(index, gimmIndex);
    }

    // Method to find the right index to add a time so that it fits (each element larger than the last)
    private int IndexToInsert(int timeToInsert)
    {
        for (int i = 0; i < times.Count; i++)
        {
            if (timeToInsert < times[i])
            {
                return i;
            }
        }
        return times.Count;
    }
    
    // Method that returns the boolean states of every gimmick at a specific time unit in a list
    public List<bool> StatesAtTime(int time)
    {
        List<bool> currentConditions = new List<bool>(initialStates);
        int currentIndex = 0;
        while (currentIndex < times.Count && times[currentIndex] < time)
        {
            currentConditions[thingIndexes[currentIndex]] = ! currentConditions[thingIndexes[currentIndex]];
            currentIndex++;
        }
        return currentConditions;
    }

    // Method that returns the boolean state of a specific gimmick at a specific time unit
    public bool StateAtTime(int time, GameObject gimm)
    {
        return StatesAtTime(time)[gimms.IndexOf(gimm)];
    }

    // Method that applies any updates to gimmicks at specified time (on behalf of the PastSelves) when you're back in time
    public void ApplyUpdates(int currentTime)
    {
        List<int> indexesOfUpdate = IndexesOf(times, currentTime); // a list of the indexes of all the updates that happened in this time

        foreach (int indexOfUpdate in indexesOfUpdate)
        {
            StateStorage stateStorage = gimms[thingIndexes[indexOfUpdate]].GetComponent<StateStorage>(); // gets the StateStorage script of the thing being updated
            Debug.Log("Update Applied");
            stateStorage.state = !stateStorage.state; // changes its state (without adding this update to an UpdateContainer)
        }
    }

    public UpdatesContainer DeepCopy()
    {
        UpdatesContainer copy = new UpdatesContainer(gimms, initialStates);
        copy.times.AddRange(times);
        copy.thingIndexes.AddRange(thingIndexes);
        return copy;
    }

    private List<int> IndexesOf(List<int> list, int element)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == element)
            {
                indexes.Add(i);
            }
        }
        return indexes;
    }
}
