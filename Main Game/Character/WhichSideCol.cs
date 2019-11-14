using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichSideCol : MonoBehaviour {

	public string dir8;

	CharacterControl cctrl;
	// Use this for initialization
	void Start () {
		cctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();

	}
	



	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Wall"||col.gameObject.tag=="gimmick") {
			
			switch (dir8) {
			case "up":
				cctrl.up8 = false;
				break;
			case "down":
				cctrl.down8 = false;
				break;
			case "right":
				cctrl.right8 = false;
				break;
			case "left":
				cctrl.left8 = false;
				break;
		
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "gimmick") {

			switch (dir8) {
			case "up":
				cctrl.up8 = true;
				break;
			case "down":
				cctrl.down8 = true;
				break;
			case "right":
				cctrl.right8 = true;
				break;
			case "left":
				cctrl.left8 = true;
				break;


			}
		}
	}



}
