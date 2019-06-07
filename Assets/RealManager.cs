using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealManager : MonoBehaviour {

	List<int> NetworkShape = new List<int>{2, 6, 4, 4};
	int populationSize = 120;
	List<RealPerson> PeopleList = new List<RealPerson>();
	public GameObject PersonGameobject;
	Vector3 StartPosition;
	public GameObject TargetObject;
	bool Training = true;
	public int generationNumber = 0;

	float [][][] InfoRealNetwork;
	List<int> InfoRealShape = new List<int>();
	GameObject InfoTargetObject;
	float InfoFitness;


	// Use this for initialization
	void Start () {
		StartPosition = new Vector3(Random.Range (0, 5), Random.Range (0, 5), 0); //min inclusive max exclusive
		TargetObject = Instantiate (TargetObject, new Vector3(Random.Range (15, 25), Random.Range (15, 25), 0), Quaternion.identity);
		for (int i = 0; i < populationSize; i++) {
			RealPerson PersonToAdd = ((GameObject)Instantiate (PersonGameobject, StartPosition, Quaternion.identity)).GetComponent<RealPerson> ();
			PersonToAdd.RealSetup (NetworkShape, TargetObject);
			PeopleList.Add (PersonToAdd);
		}
		//GenerationFunction();
		//SortList(PeopleList);
		/*for (int i = 0; i < 2; i++) {
			PeopleList [i] = PeopleList [PeopleList.Count - 1 - i];
		}
		for (int i = 0; i < populationSize; i++) {
			Debug.Log(PeopleList[i].ReturnFitness());
		}*/
	}


	void GenerationFunction ()
	{
		
	}


	void Substitute (int IndexValue, int LowerIndex)
	{
		InfoRealShape = PeopleList[IndexValue].ReturnShape();
		//InfoRealNetwork = PeopleList[IndexValue].ReturnNetwork();

		InfoTargetObject =  PeopleList[IndexValue].ReturnTarget();
		InfoFitness = PeopleList[IndexValue].ReturnFitness();
		PeopleList[LowerIndex].Fillin(InfoRealShape, InfoTargetObject, InfoFitness);
		PeopleList[LowerIndex].CopyNetwork(PeopleList[IndexValue].ReturnNetwork());
	}


	void SortList (List<RealPerson> unsortedList)
	{
		StartPosition = new Vector3(Random.Range (0, 5), Random.Range (0, 5), 0); //min inclusive max exclusive
		TargetObject.transform.position = new Vector3(Random.Range (15, 25), Random.Range (15, 25), 0);
		for (int i = 1; i < unsortedList.Count; i++) {
			for (int j = 1; j < i + 1; j++) {
				if (unsortedList [i - j].ReturnFitness() > unsortedList [i - j + 1].ReturnFitness()) {
					RealPerson tempSecondTest = unsortedList [i - j + 1];
					unsortedList [i - j + 1] = unsortedList [i - j];
					//Substitute (i - j + 1, i - j);
					unsortedList [i - j] = tempSecondTest;
				} else {
					break;
				}
			}
		}
		/*for (int i = 0; i < unsortedList.Count; i++){
			//Debug.Log(unsortedList[i].ReturnFitness());
			PeopleList[i].FeedForward();
		}*/
		/*for (int i = 0; i < 9; i++) { //Set amount to cut HERE
			//PeopleList [i] = PeopleList [PeopleList.Count - 1 - i];
			Debug.Log(unsortedList[PeopleList.Count - 1 - i].ReturnFitness());
			Substitute(PeopleList.Count - 1 - i, i);
		}*/
		for (int i = 0; i < populationSize; i++) {
			if(i < 20){ //Set amount to cut HERE
				//PeopleList [i] = (PeopleList [PeopleList.Count - 1 - i]);
				//PeopleList [i] = new RealPerson(PeopleList[PeopleList.Count - 1 - i]);
				//PeopleList [i] = new RealPerson();
				//Debug.Log(unsortedList[PeopleList.Count - 1 - i].ReturnFitness());
				Substitute(PeopleList.Count - 1, i); //can replace with PeopleList.Count - 1 - i
			}
			//PeopleList[i].FeedForward();
			//Debug.Log(unsortedList[i].ReturnFitness());
			if(PeopleList[i].ReturnFitness() != 100){ //can remove the perfect fitness condition
				PeopleList[i].MutateNetwork();
			}
			//PeopleList[i].SetFitness(0f);
			PeopleList[i].gameObject.transform.position = StartPosition;
		}
		Training = true;
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (Training == true) { //Set Number of generations HERE (&& generationNumber < x)
			Training = false;
			generationNumber++;
			for (int i = 0; i < populationSize; i++) {
				PeopleList[i].StartTraining();
			}
			Invoke("Timer", 1);
		}
	}

	void Timer(){
		for (int i = 0; i < populationSize; i++) {
			PeopleList[i].StopTraining();
		}
		SortList(PeopleList);
	}

}
