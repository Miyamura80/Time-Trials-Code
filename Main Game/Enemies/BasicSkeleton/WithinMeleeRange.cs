using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinMeleeRange : MonoBehaviour
{

    GameOver gamOv;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gamOv = GameObject.FindGameObjectWithTag("Player").GetComponent<GameOver>();
        anim = this.transform.parent.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnTriggerEnter2D(Collider2D col) {
        
        if (col.gameObject.tag=="PickUp") {
            anim.SetBool("attacking",true);
            gamOv.killed = true;
        }

    }
}
