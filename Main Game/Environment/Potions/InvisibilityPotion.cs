using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPotion : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playerSprite;
    private NewGodScript god;
    private EffectRecord record;

    private bool isInvisible;
    private int startTime;

    public Color invisibleColor;
    public int duration;
    public FutureSelfParadox pastSelfPrehab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
        god = GameObject.Find("gOD").GetComponent<NewGodScript>();
        print(god.playerRecord);
        print(god.playerRecord.effectsRecord);
        record = god.playerRecord.effectsRecord.EffectWithName("Invisible");
    }

    private void FixedUpdate()
    {
        if (isInvisible)
        {
            WhileInvisible();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BeginInvisible();
        }
    }

    // When you just turn invisible
    private void BeginInvisible()
    {
        isInvisible = true;
        record.Start(god.godTime);
        startTime = god.godTime; // sets the starttime of the potion
        foreach (GameObject pastSelf in god.activeSelves) // makes you invisible to all currently appearing pastselves
        {
            pastSelf.GetComponent<FutureSelfParadox>().enabled = false;
        }
        pastSelfPrehab.enabled = false; // make you invisible to any new pastselves that appear
    }

    // While you're invisible
    private void WhileInvisible()
    {
        playerSprite.color = invisibleColor; // constantly changes your colour to be transluscent
        
        if (god.godTime > startTime + duration) // if the potion's time runs out
        {
            EndInvisible();
        }
    }
    
    // When you stop being invisible
    private void EndInvisible()
    {
        isInvisible = false;
        record.End(god.godTime);
        playerSprite.color = Color.white; // change colour back to normal
        foreach (GameObject pastSelf in god.activeSelves) // makes you visible to all currently appearing pastselves
        {
            pastSelf.GetComponent<FutureSelfParadox>().enabled = true;
        }
        pastSelfPrehab.enabled = true; // make you visible to any new pastselves that appear
    }

}
