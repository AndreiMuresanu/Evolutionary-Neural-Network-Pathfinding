using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RestartMind :  IComparable<RestartMind> {
	private float [][][] RestartedNet;
	private List<int> personalShape;
	private GameObject TestingCube;


	public RestartMind(List<int> layers, GameObject GivenCube)
    {
        this.personalShape = layers;
        this.TestingCube = GivenCube;

        CreateTheNet();
    }


	public int CompareTo (RestartMind other)
	{
		if (other == null) {
			return 1;
		}

		return 0;
	}


	void CreateTheNet ()
	{
		//Debug.Log("Start");
		RestartedNet = new float[personalShape.Count][][];
		for (int i = 0; i < personalShape.Count; i++) {
			RestartedNet [i] = new float[personalShape [i]][]; 
			for (int j = 0; j < personalShape [i]; j++) {
				if (i != 0) {
					RestartedNet [i] [j] = new float[(personalShape [i - 1]) + 1];
				} else {
					RestartedNet [i] [j] = new float[1];
				}
				for (int k = 0; k < RestartedNet [i] [j].Length; k++) {
					if (k != 0) {
						RestartedNet [i] [j] [k] = UnityEngine.Random.Range (-1f, 1f);
					} else {
						RestartedNet [i] [j] [k] = UnityEngine.Random.Range (-5f, 5f);
					}
					Debug.Log(RestartedNet [i] [j] [k]);
				}
			}
		}
		//Debug.Log("Finish");
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
