using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	int testingValue;

	void Update ()
	{
		if (FoodManager.dictator == 1) {
			testingValue += 1;
		} else if (FoodManager.dictator == 2) {
			Debug.Log(testingValue);
		}
	}
}
