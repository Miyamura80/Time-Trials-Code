using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	CharacterControl cctrl;


	// Use this for initialization
	void Start () {
		cctrl = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterControl> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="PickUp"){
			cctrl.hp += 1;
			Destroy (this.gameObject);
		}

	}

}
