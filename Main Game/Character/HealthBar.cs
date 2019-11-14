using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {


	public float maxHP, currentHP;
	Animator anim;

	// Use this for initialization
	void Start () {
		currentHP = maxHP;
		anim = GetComponentInParent<Animator> ();



	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)&&currentHP>0){
			currentHP -= 1;
		}
		if(Input.GetKeyDown(KeyCode.R)){
			currentHP = maxHP;
			anim.SetBool("dead", false);
		}

		updateDamage ();

	}


	void updateDamage (){
		
		this.gameObject.transform.localScale = new Vector3 ((currentHP / maxHP), 1, 1);
			
	}
}
