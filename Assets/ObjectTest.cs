using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest : MonoBehaviour {

	private bool beenSet = false;
	private NewTest ownTest;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (beenSet == true) {
			int tempInt = ownTest.FrameDuty ();
			gameObject.name = tempInt.ToString();
		}
	}

	public void SetupTest (NewTest Trail)
	{
		this.ownTest = Trail;
		beenSet = true;
		ownTest.Speak();
	}
}
