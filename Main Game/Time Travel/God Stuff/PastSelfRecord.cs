using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastSelfRecord
{
    // Start time of the pastself's exitence (in "global time")
    public int startTime;
    // End time of the pastself's existence (in "global time")
    public int endTime;
    // List of all the pastself's positions as 2d vectors
    public List<Vector2> positions;
    // Basically needed for a very clever way to find the right pastself from the "activeSelves" list in NewGodScript
    public int index;
    // A record of when each effects are active
    public EffectsRecord effectsRecord;

    // Creating the thing
    public PastSelfRecord(int startTime) 
    {
        this.startTime = startTime;
        positions = new List<Vector2>();
        effectsRecord = new EffectsRecord();
        effectsRecord.effects.Add(new EffectRecord("Invisible"));
    }

    // Method for adding a new position to the list
    public void Add(Vector2 newPosition)
    {
        positions.Add(newPosition);
    }

    // Method for checking if the pastself should currently be active
    public bool isActive(int currentTime)
    {
        return currentTime > startTime && currentTime < endTime;
    }

    // Method for using the record (to move a pastself)
    public void Use(int currentTime, GameObject PastSelf)
    {
        int subjectiveTime = currentTime - startTime;
        Vector2 newPosition = positions[subjectiveTime]; // find the new position of the pastself
        PastSelf.transform.position = newPosition; // set the new position of the pastself
    }

    public void UseEffects(int currentTime, GameObject PastSelf)
    {
        Debug.Log("Attempted to use effect");
        foreach (EffectRecord effect in effectsRecord.effects)
        {
            switch (effect.type)
            {
                case "Invisible":
                    Debug.Log("Attempted to use this invisibility");
                    if (effect.IsActive(currentTime))
                    {
                        Debug.Log("Invisibility is active");
                        Color tempColor = Color.white;
                        tempColor.a = 0.2f;
                        PastSelf.GetComponent<SpriteRenderer>().color = tempColor;
                    }
                    else if (effect.endTimes.IndexOf(currentTime) != -1)
                    {
                        Debug.Log("Invisibility ends");
                        PastSelf.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    break;
                //... more effects
                default:
                    Debug.Log("Default??!?!?!?!");
                    break;
            }
        }
    }
}