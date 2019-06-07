using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingByIManager : MonoBehaviour {

	List<int> TestShape = new List<int>{5, 8, 6, 7, 2, 4};
	int personalInt = 0;
	int populationSize = 5;
	List<ITest> ListOfPeople = new List<ITest>();
	public GameObject PersonObject;
	public GameObject CubeObject;


	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < populationSize; i++) {
			ITest PersonToAdd = ((GameObject)Instantiate (PersonObject, new Vector3 (i * 10, 0, 0), Quaternion.identity)).GetComponent<ITest> ();
			PersonToAdd.ITestSetup (i, TestShape, CubeObject);
			ListOfPeople.Add (PersonToAdd);
		}
		//cut
		for (int i = 0; i < 2; i++) {
			ListOfPeople [i] = ListOfPeople [ListOfPeople.Count - 1 - i];
		}
		for (int i = 0; i < populationSize; i++) {
			ListOfPeople[i].PrintArray();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
