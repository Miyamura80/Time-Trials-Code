using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRecord 
{
    // Type of effect this is for
    public string type;
    // Start times
    public List<int> startTimes;
    // End times
    public List<int> endTimes;

    public EffectRecord(string type)
    {
        this.type = type;
        startTimes = new List<int>();
        endTimes = new List<int>();
    }

    public void Start(int currentTime)
    {
        if (!InternalIsActive())
        {
            startTimes.Add(currentTime);
        }
    }

    public void End(int currentTime)
    {
        if (InternalIsActive())
        {
            endTimes.Add(currentTime);
        }
    }

    // Checks if the effect is active but only works when the self is the current self
    private bool InternalIsActive()
    {
        return startTimes.Count == endTimes.Count + 1;
    }

    // Checks if the effect is active, for when the self is a past self
    public bool IsActive(int currentTime)
    {
        for(int i = 0; i < startTimes.Count; i++) // loops through every period this effect was active (basically)
        {
            if (currentTime > startTimes[i] && currentTime < endTimes[i]) // if the current time is in this period
            {
                return true;
            }
        }
        // This weird line basically checks if this effect was still active when the self finally time travels away, and if so is the currentTime in that period
        return InternalIsActive() && currentTime > startTimes[startTimes.Count - 1];
    }
}

public class EffectsRecord
{
    public List<EffectRecord> effects;

    public EffectsRecord()
    {
        effects = new List<EffectRecord>();
    }

    public EffectRecord EffectWithName(string type)
    {
        foreach (EffectRecord effect in effects)
        {
            if (effect.type == type)
            {
                return effect;
            }
        }
        Debug.Log("Error");
        return new EffectRecord("Unknown");
    }
}
