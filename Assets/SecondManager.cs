using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondManager : MonoBehaviour {

	List<int> GivenShape = new List<int>();
	int populationSize = 10;
	float GivenFitness;
	public GameObject GivenTarget;
	int GivenBuffer = 0;
	List<SecondTest> PopulationList = new List<SecondTest>();
	public GameObject Executer;
	public GameObject Cube;
	//List<int> sortingTest = new List<int>{8, 3, 9, 1, 6, 2, 13, 10, 9, 9, 9, 1};
	float cutPercent = 0.3f;
	List<int> InfoShape;
	float InfoFitness;
	GameObject InfoTarget;
	int InfoBuffer;
	GameObject InfoCube;


	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < populationSize; i++) {
			GivenShape = new List<int> ();
			int tempLength = Random.Range (3, 7); //min inclusive max exclusive
			for (int j = 0; j < tempLength; j++) {
				GivenShape.Add (Random.Range (2, 8)); //min inclusive max exclusive
			}
			GivenFitness = Random.Range (1f, 100f); //both inclusive
			GivenBuffer = i;
			GameObject TempObject = Instantiate (Executer, new Vector3 (0, 0, 0), Quaternion.identity);
			//GoodTest = new SecondTest(GivenShape, GivenFitness, GivenTarget, GivenBuffer, Cube);
			SecondTest GoodTest = TempObject.AddComponent<SecondTest> ();
			GoodTest.SecondTestSetup (GivenShape, GivenFitness, GivenTarget, GivenBuffer, Cube);
			PopulationList.Add (GoodTest);
		}
		//Debug.Log(PopulationList.Count);
		SortList (PopulationList);
		//Debug.Log(PopulationList[3].fitness);
		Destroy (PopulationList [3].gameObject);
		//Debug.Log(PopulationList[3].fitness);


		//get info about SecondTest
		/*List<int> InfoShape = PopulationList[3].GiveShape();
		float InfoFitness = PopulationList[3].GiveFitness();
		GameObject InfoTarget = PopulationList[3].GiveTarget();
		int InfoBuffer = PopulationList[3].GiveBuffer();
		GameObject InfoCube = PopulationList[3].GiveCube();*/

		FillInfo (3);
		GameObject ASDtest = Instantiate (Executer, new Vector3 (0, 0, 0), Quaternion.identity);
		SecondTest DSAtest = ASDtest.AddComponent<SecondTest> ();
		//DSAtest = PopulationList[3]; //doesn't seem to work
		DSAtest.SecondTestSetup (InfoShape, InfoFitness, InfoTarget, InfoBuffer, InfoCube);
		PopulationList [3] = DSAtest;
		//PopulationList[3].NewNetwork();
		//PopulationList.RemoveAt(3);
		//Debug.Log(PopulationList.Count);

		cutPercent = Mathf.RoundToInt (cutPercent * PopulationList.Count);
		for (int i = 0; i < cutPercent; i++) {
			//PopulationList[i] = PopulationList[PopulationList.Count - i];
			Destroy (PopulationList [i].gameObject);
			FillInfo (PopulationList.Count - i);
			GameObject TempGameObject = Instantiate (Executer, new Vector3 (0, 0, 0), Quaternion.identity);
			SecondTest TempSecondTest = TempGameObject.AddComponent<SecondTest> ();
			TempSecondTest.SecondTestSetup (InfoShape, InfoFitness, InfoTarget, InfoBuffer, InfoCube);
			PopulationList [i] = TempSecondTest;
		}
		//SortList(PopulationList);
		/*for (int i = 0; i < PopulationList.Count; i++){
			Destroy(PopulationList[i].gameObject);
			PopulationList[i].NewNetwork();
		}*/
	}


	public void FillInfo (int indexValue)
	{
		InfoShape = PopulationList[indexValue].GiveShape();
		InfoFitness = PopulationList[indexValue].GiveFitness();
		InfoTarget = PopulationList[indexValue].GiveTarget();
		InfoBuffer = PopulationList[indexValue].GiveBuffer();
		InfoCube = PopulationList[indexValue].GiveCube();
	}

	
	// Update is called once per frame
	void Update () {
		
	}


	void SortList (List<SecondTest> unsortedList)
	{
		for (int i = 1; i < unsortedList.Count; i++) {
			for (int j = 1; j < i + 1; j++) {
				if (unsortedList [i - j].fitness > unsortedList [i - j + 1].fitness) {
					SecondTest tempSecondTest = unsortedList [i - j + 1];
					unsortedList [i - j + 1] = unsortedList [i - j];
					unsortedList [i - j] = tempSecondTest;
				} else {
					break;
				}
			}
		}
		/*for (int i = 0; i < unsortedList.Count; i++){
			Debug.Log(unsortedList[i].fitness);
		}*/
	}
}
