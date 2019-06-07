using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	float[][][] net;
	//int[] shape = new int[]{3, 5, 4, 5, 3};
	List<int> shape = new List<int>{3, 5, 4, 5, 3};
	int netSize = 0;
	int netSizeA = 0;
	int netSizeB = 0;
	int shapeLength = 0;
	List<float> outputList = new List<float>();
	public GameObject testCube;
	float temp = 0;
	int Counter = 0;


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
		NewNetwork();
	}

	//!!!IMPORTANT!!!: The net is in terms net [y/i] [x/j] [z/k]. i = y, j = x, k = z 
	void NewNetwork ()
	{
		//creating the shape of the neural net, can also be done randomly
		shapeLength = shape.Count;
		net = new float[shapeLength][][];
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
		//Debug.Log(net [0] [0] [0]);
		FeedForward ();
	}


	//all weights for each neuron multiplyed by the sending neuron and added with all the other weights going to the reciveing neuron
	// and final sum added with the reciveing neuron value
	void FeedForward ()
	{
		//Debug.Log(shapeLength);
		//goes through each layer
		//set counter to 0
		//clear output list
		for (int i = 1; i < shapeLength; i++) {
			 

			//goes through each neuron in the layer
			for (int j = 0; j < shape [i]; j++) {

				temp = 0;
				//goes through all the weights and neurons connecting to a neuron
				//add activation function
				for (int k = 0; k < shape [i - 1]; k++) {
					if (i == 1) {
						//Debug.Log("cool");
						//none (not good)
						//temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
						//leaky ReLU, leak slope of 10
						if((net [0] [k] [0]) * (net [1] [j] [k + 1]) > 0){
							temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
						} else {
							temp += ((net [0] [k] [0]) * (net [1] [j] [k + 1])) / 10;
						}
					} else {
						//Debug.Log(Counter);
						//Debug.Log(shape[(i - 1)]);
						//none (not good)
						//temp += outputList [(k + Counter - shape[i - 1])] * (net [i] [j] [k + 1]);
						//leaky ReLU, leak slope of 10
						if(outputList [(k + Counter - shape[i - 1])] * (net [i] [j] [k + 1]) > 0){
							temp += outputList [(k + Counter - shape[i - 1])] * (net [i] [j] [k + 1]);
						} else{
							temp += (outputList [(k + Counter - shape[i - 1])] * (net [i] [j] [k + 1]))/10;
						}
					}
				}
				temp += net [i] [j] [0];
				outputList.Add (temp);
			}
			Counter += shape[i];
		}
		//Debug.Log(shape.Count);
		//Debug.Log(shape[shapeLength - 1]);
		//Debug.Log(Counter);
		//Debug.Log(outputList.Count - shape[shapeLength - 1]);

		int value = outputList.Count - shape[shapeLength - 1];
		for(int i = 0; i < value; i++){
			outputList.RemoveAt(0);
		}
		//Debug.Log(outputList.Count);
		for (int i = 0; i < outputList.Count; i++) {
			Debug.Log(outputList[i]);
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
