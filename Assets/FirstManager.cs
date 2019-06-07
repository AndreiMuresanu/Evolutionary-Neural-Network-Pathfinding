using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstManager : MonoBehaviour {

	public static int Selector = 0;
	List<int> GivenShape  = new List<int>{2, 4, 4};
	int populationSize = 10;
	public GameObject GivenTarget;
	List<FoodSearcher> PopulationList = new List<FoodSearcher>();
	public GameObject Executer;
	public GameObject Cube;
	//List<int> sortingTest = new List<int>{8, 3, 9, 1, 6, 2, 13, 10, 9, 9, 9, 1};
	float cutPercent = 0.2f;
	List<int> InfoShape;
	float InfoFitness;
	GameObject InfoTarget;
	int InfoBuffer;
	GameObject InfoCube;
	int generationNumber = 0;
	int genDuration = 60;
	int counter = 60;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < populationSize; i++){
			GameObject TempObject = Instantiate(Executer, new Vector3(0, 0, 0), Quaternion.identity);
			FoodSearcher GoodTest = TempObject.AddComponent<FoodSearcher>();
			GoodTest.FoodSearcherSetup(GivenShape, GivenTarget, Cube);
			PopulationList.Add(GoodTest);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(counter == genDuration){
			counter = 0;
			Selector = 1;
			if (generationNumber != 0){
				SortList(PopulationList);
				cutPercent = Mathf.RoundToInt(cutPercent * PopulationList.Count);
				for (int i = 0; i < cutPercent; i++){
					PopulationList[i].CopyNetwork(PopulationList[PopulationList.Count - i].net);
				}
				foreach (FoodSearcher searcher in PopulationList){
					searcher.gameObject.transform.position = new Vector3(0, 0, 0);
					searcher.fitness = 0;
					searcher.Mutate();
				}
				GivenTarget.transform.position = new Vector3(Random.Range(15f, 30f), Random.Range(15f, 30f), 0);
			}
		}
		counter++;
	}


	void SortList (List<FoodSearcher> unsortedList)
	{
		for (int i = 1; i < unsortedList.Count; i++) {
			for (int j = 1; j < i + 1; j++) {
				if (unsortedList [i - j].fitness > unsortedList [i - j + 1].fitness) {
					FoodSearcher tempSecondTest = unsortedList [i - j + 1];
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
