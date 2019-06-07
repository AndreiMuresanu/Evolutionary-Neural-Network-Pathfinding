using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewManager : MonoBehaviour {

	int population = 4;
	//int generation = 0;
	List<NewTest> TestList;
	public static int TestSpeaker = 0;
	List<ObjectTest> GameObjectList; //ExecutionerList
	bool done = false;
	bool Sdone = false;
	bool Tdone = false;
	bool Fdone = false;
	public GameObject TestObject;
	int[][] testShape = new int[][] {new int[]{1, 2, 3}, new int[]{1, 2}, new int[]{1, 2, 3, 4}};


	// Use this for initialization
	void Start ()
	{
		//Initialization
		TestList = new List<NewTest> ();

		for (int i = 0; i < population; i++) {
			//insert net creation here
			NewTest Trail = new NewTest (i, i.ToString(), testShape);
			TestList.Add(Trail);
		}

		MakeExecuters();
	}
	
	// Update is called once per frame
	void Update () {
		//Training
		if(gameObject.name == "0" && done == false){
			//MakeExecuters();
			done = true;
		}
		if(gameObject.name == "1" && Sdone == false){
			Categorize();
			Sdone = true;
		}
		if(gameObject.name == "2" && Tdone == false){
			ChangeTest();
			Tdone = true;
		}
		if(gameObject.name == "3" && Fdone == false){
			ChangeTest();
			Fdone = true;
		}
	}

	private void MakeExecuters(){
		GameObjectList = new List<ObjectTest>();

		for (int i = 0; i < TestList.Count; i++){
			ObjectTest person = ((GameObject)Instantiate(TestObject, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<ObjectTest>();
			person.SetupTest(TestList[i]);
			GameObjectList.Add(person);
		}
	}

	private void Categorize ()
	{
		//TestList.Sort();
		TestList.RemoveAt(2);
		for (int i = 0; i < GameObjectList.Count; i++){
			GameObject.Destroy(GameObjectList[i].gameObject);
		}
		MakeExecuters();
		NewTest DoubleTrail = TestList[0];
		TestList.Add(DoubleTrail);
		ObjectTest person = ((GameObject)Instantiate(TestObject, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<ObjectTest>();
		person.SetupTest(TestList[TestList.Count - 1]);
		GameObjectList.Add(person);
		for (int i = 0; i < TestList.Count; i++){
			TestList[i].Speak();
		}
	}

	private void ChangeTest ()
	{
		for (int i = 0; i < TestList.Count; i++) {
			for (int j = 0; j < TestList[i].testArray.GetLength (0); j++) {
				for (int k = 0; k < TestList[i].testArray[j].Length; k++) {
					TestList[i].testArray [j] [k] = 5;
				}
			}
		}
	}
}
