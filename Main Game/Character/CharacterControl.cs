using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterControl : MonoBehaviour {


	GameObject playerGo;
	Transform trs;
	private float ypos,xpos;
    public float speed = 0.01f;
    private bool moving;
    private List<int> direction = new List<int>() {0,0};
    public bool up8, down8, left8, right8;
    public Animator animator;
	public int hp, atk, def;
    


    void Start () {
		playerGo = GameObject.FindGameObjectWithTag ("Player");
        trs = playerGo.transform;
        up8 = false;
        down8 = false;
        right8 = false;
        left8 = false;
        moving = false;


    }

    // Update is called once per frame
    private void Update ()
    {
        direction[1] = 0;
        direction[0] = 0;
        moving = false;
        if (Input.GetKey(KeyCode.UpArrow) && !up8)
        {
            moving = true;
            animator.SetBool("moving", true);
            direction[1] = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !right8)
        {
            moving = true;
            animator.SetBool("moving", true);
            direction[0] = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !down8)
        {
            moving = true;
            animator.SetBool("moving", true);
            direction[1] = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !left8)
        {
            moving = true;
            animator.SetBool("moving", true);
            direction[0] = -1;
        }

        if (!moving) {
            animator.SetBool("moving", false);
        }
        
        
	}

    //executed 50 times a second (regardless of framerate)
    private void FixedUpdate()
    {

            ypos = trs.position.y + speed*direction[1];
            xpos = trs.position.x + speed*direction[0];
            trs.transform.position = new Vector2(xpos, ypos);

        
    }

}
