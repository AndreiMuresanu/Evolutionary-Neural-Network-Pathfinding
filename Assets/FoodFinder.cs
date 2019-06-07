using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFinder : MonoBehaviour {

	float[][][] net;
	//int[] shape = new int[]{3, 5, 4, 5, 3};
	List<int> shape = new List<int>{25, 5, 5, 4}; //MAKE SURE input value size corresponds with number of elements in the map
	int netSize = 0;
	int netSizeA = 0;
	int netSizeB = 0;
	int shapeLength = 0;
	List<float> outputList = new List<float>();
	public GameObject testCube;
	float temp = 0;
	int Counter = 0;
	bool stopper = false;
	float [,] newmap;
	//float[] inputs;


	// Use this for initialization(not called if object is not activated and not called again if object is reactivated
	void Start ()
	{
		//test
		/*for (int i = 1; i < 6; i++) {
			outputList.Add (i);
			//Debug.Log (outputList);
			if (i == 2 || i == 3) {
				//outputList.Clear();
				outputList.RemoveAt(1);
			}
		}
		for (int j = 0; j < outputList.Count; j++) {
				
			Debug.Log(outputList[j]);
		}*/

		//NewNetwork();
	}

	//!!!IMPORTANT!!!: The net is in terms net [y/i] [x/j] [z/k]. i = y, j = x, k = z 
	void NewNetwork ()
	{
		stopper = true;
		newmap = FoodManager.map;
		//creating the shape of the neural net, can also be done randomly
		shapeLength = shape.Count;
		net = new float[shapeLength][][];
		/*inputs = new float[shape [0]];
		for (int i = 0; i < inputs.Length; i++) {
			Debug.Log(inputs[i]);
		}*/
		for (int i = 0; i < shapeLength; i++) {
			net [i] = new float[(shape [i])][];
		}
		//making radom neuron values for start
		int numOfFirstD = net.GetLength (0);
		for (int i = 0; i < numOfFirstD; i++) {

			netSize = net [i].Length;
			if (i != 0) {
				netSizeA = net [i - 1].Length;
			}
			for (int j = 0; j < netSize; j++) {
				if (i != 0) {
					net [i] [j] = new float[(netSizeA + 1)];
				} else {
					net [i] [j] = new float[1];
				}
				//net [i] [j] = new float[3];

				//initializes values for the threshold
				net [i] [j] [0] = Random.Range (-5f, 5f); //both are inclusive
				//as test
				//net [i] [j] [0] = j + 1;

				//Instantiate (testCube, new Vector3 (j, i, 0), Quaternion.identity);

				netSizeB = net [i] [j].Length;
				for (int k = 0; k < netSizeB; k++) {
					//initializes values for the weights
					if(k != 0){
						net [i] [j] [k] = Random.Range (-1f, 1f); //both are inclusive
						//as test
						//net [i] [j] [k] = k + j;
					}
					//Debug.Log(net [i] [j] [k]);
					//makes a visualization of the net array
					GameObject newCube;
					newCube = Instantiate (testCube, new Vector3 (j, -i, k), Quaternion.identity);
					newCube.name = string.Format("({0},{1},{2})", j, -i, k);
					newCube.transform.parent = transform;
				}
			}
		}
		//initializing inputs
		for (int i = 0; i < newmap.GetLength (0); i++) {
			for (int k = 0; k < newmap.GetLength (1); k++) {
				//newmap [i, k] = Random.Range(-0, 2);
				net [0] [i*newmap.GetLength(0) + k] [0] = newmap [k, i];
				//Debug.Log (net [0] [i] [0]);
				//Debug.Log("working");
			}
		}
		//Debug.Log(net [0] [0] [0]);
		FeedForward ();
	}


	//all weights for each neuron multiplyed by the sending neuron and added with all the other weights going to the reciveing neuron
	// and final sum added with the reciveing neuron value
	void FeedForward ()
	{
		for (int i = 0; i < shape[0]; i++) {
			//Debug.Log (net [0] [i] [0]);
		}
		stopper = true;
		//Debug.Log(shapeLength);
		//goes through each layer
		for (int i = 1; i < shapeLength; i++) {
			 

			//goes through each neuron in the layer
			for (int j = 0; j < shape [i]; j++) {

				temp = 0;
				//goes through all the weights and neurons connecting to a neuron
				for (int k = 0; k < shape [i - 1]; k++) {
					if (i == 1) {
						//Debug.Log("cool");
						temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
					} else {
						//Debug.Log(Counter);
						//Debug.Log(shape[(i - 1)]);
						temp += outputList [(k + Counter - shape [i - 1])] * (net [i] [j] [k + 1]);
					}
				}
				temp += net [i] [j] [0];
				outputList.Add (temp);
			}
			Counter += shape [i];
		}
		//Debug.Log(shape.Count);
		//Debug.Log(shape[shapeLength - 1]);
		//Debug.Log(Counter);
		//Debug.Log(outputList.Count - shape[shapeLength - 1]);

		int value = outputList.Count - shape [shapeLength - 1];
		for (int i = 0; i < value; i++) {
			outputList.RemoveAt (0);
		}
		//Debug.Log(outputList.Count);
		float biggestOutput = 0;
		int biggestIndex = 0;
		for (int i = 0; i < outputList.Count; i++) {
			if (i == 0) {
				biggestOutput = outputList [0];
			} else if (biggestOutput < outputList [i]) {
				biggestOutput = outputList [i];
				biggestIndex = i;
			}
			//Debug.Log(outputList[i]);
			//Debug.Log(biggestOutput);
		}

		//Make move
		for (int i = 0; i < shape [0]; i++) {
			//Debug.Log(i);
			//Debug.Log (net [0] [i] [0]);
			if (net [0] [i] [0] == 1) {
				//Debug.Log(i % newmap.GetLength(0));
				net [0] [i] [0] = 0;
				//up
				if (biggestIndex == 0) {
					if (i > newmap.GetLength (0)) { 
						net [0] [i - newmap.GetLength(0)] [0] = 1f;
					} else {
						//dead
					}
				}
				//right
				if (biggestIndex == 0) {
					if (i % newmap.GetLength(0) != 4) { 
						net [0] [i + 1] [0] = 1f;
					} else {
						//dead
					}
				}
				//down
				if (biggestIndex == 0) {
					if (i < (newmap.GetLength (0) * newmap.GetLength(1)) - newmap.GetLength(0)) { 
						net [0] [i + newmap.GetLength(0)] [0] = 1f;
					} else {
						//dead
					}
				}
				//left
				if (biggestIndex == 0) {
					if (i % newmap.GetLength(0) != 0) { 
						net [0] [i - 1] [0] = 1f;
					} else {
						//dead
					}
				}
			}
		}

		//stopper = false;
	}


	// Update is called once per frame
	void Update () {
		if(FoodManager.dictator == 1 && stopper == false){
			NewNetwork();
		}
	}
}
