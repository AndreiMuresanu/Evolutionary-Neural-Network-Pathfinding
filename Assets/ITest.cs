using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITest : MonoBehaviour {

	private int number;
	private float [][][] TestingNet;
	private List<int> TestNetShape = new List<int>();
	private GameObject testcube;


	public void ITestSetup (int GivenNumber, List<int> GivenTestNetShape, GameObject GivenTestObject)
	{
		this.number = GivenNumber;
		this.TestNetShape = GivenTestNetShape;
		this.testcube = GivenTestObject;
		CreateArray();
	}

	void CreateArray ()
	{
		//Debug.Log("Start");
		TestingNet = new float[TestNetShape.Count][][];
		for (int i = 0; i < TestNetShape.Count; i++) {
			TestingNet [i] = new float[TestNetShape [i]][]; 
			for (int j = 0; j < TestNetShape [i]; j++) {
				if (i != 0) {
					TestingNet [i] [j] = new float[(TestNetShape [i - 1]) + 1];
				} else {
					TestingNet [i] [j] = new float[1];
				}
				for (int k = 0; k < TestingNet [i] [j].Length; k++) {
					if (k != 0) {
						TestingNet [i] [j] [k] = number;
					} else {
						TestingNet [i] [j] [k] = number + 10;
					}
					GameObject newCube;
					newCube = Instantiate (testcube, new Vector3 (j + (number * 20), -i, k), Quaternion.identity);
					newCube.name = string.Format("({0},{1},{2})", j, -i, k);
					newCube.transform.parent = transform;
					//Debug.Log(TestingNet [i] [j] [k]);
				}
			}
		}
		//Debug.Log("Finish");
	}

	public void PrintArray ()
	{
		//Debug.Log(number);
		Debug.Log("Start");
		for (int i = 0; i < TestNetShape.Count; i++) { 
			for (int j = 0; j < TestNetShape [i]; j++) {
				for (int k = 0; k < TestingNet [i] [j].Length; k++) {
					Debug.Log(TestingNet [i] [j] [k]);
				}
			}
		}
		Debug.Log("Finish");
	}

	public void CoppyAnotherNet(float[][][] GivenTestingNet)
	{
		
	}
}
