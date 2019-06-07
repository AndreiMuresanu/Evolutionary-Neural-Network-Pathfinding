using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

	public static float[,] map = new float[5, 5];
	int xco = 0;
	int yco = 0;
	public static int dictator = 0;
	public GameObject testObject;
	GameObject clone;

	
	// Use this for initialization
	void Start ()
	{
		//Creating the map
		//player position
		xco = Random.Range (0, map.GetLength (0)); //min inclusive, max exclusive
		yco = Random.Range (0, map.GetLength (1)); //min inclusive, max exclusive
		map [xco, yco] = 1;
		//food position
		while (1 == 1) {
			xco = Random.Range (0, map.GetLength (0)); //min inclusive, max exclusive
			yco = Random.Range (0, map.GetLength (1)); //min inclusive, max exclusive
			if(map [xco, yco] == 0){
				map [xco, yco] = 2;
				break;
			}
		}
		/*for (int i = 0; i < map.GetLength(0); i++){
			for (int j = 0; j < map.GetLength(1); j++){
				Debug.Log(map [i, j]);
			}
		}*/

		//Make test
		testObject = Instantiate (testObject, new Vector3 (0, 0, 0), Quaternion.identity);
		testObject.name = string.Format("Andrei");
		testObject.transform.parent = transform;


		//Instantiate population



		//Beguin Sim
		dictator = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.name == "0"){
			dictator = 0;
		}
		if(gameObject.name == "1"){
			dictator = 1;
		}
		if(gameObject.name == "2"){
			dictator = 2;
		}
		if(gameObject.name == "3"){
			clone = Instantiate (testObject, new Vector3 (0, 0, 0), Quaternion.identity);
			clone.name = string.Format("AndreiA");
			clone.transform.parent = transform;
		}
	}
}
