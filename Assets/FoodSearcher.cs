using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodSearcher : MonoBehaviour {

	public float[][][] net;
	//int[] shape = new int[]{3, 5, 4, 5, 3};
	//private List<int> shape = new List<int>{3, 5, 4, 5, 3};
	private List<int> shape;
	private int netSize = 0;
	private int netSizeA = 0;
	private int netSizeB = 0;
	private int shapeLength = 0;
	private List<float> outputList = new List<float>();
	private GameObject testCube;
	private float temp = 0;
	private int Counter = 0;
	public float fitness = 0;
	private GameObject target;
	private bool BeenBorn = false;


	public void FoodSearcherSetup (List<int> GivenShape, GameObject GivenTarget, GameObject Cube)
	{
		this.shape = GivenShape;
		this.target = GivenTarget;
		testCube = Cube;
		NewNetwork();
		BeenBorn = true;
	}

	public float GiveFitness ()
	{
		return fitness;
	}

	public GameObject GiveTarget ()
	{
		return target;
	}




	public void CopyNetwork (float[][][] otherNet)
	{
		
	}


	public void Mutate ()
	{
		
	}


	//!!!IMPORTANT!!!: The net is in terms net [y/i] [x/j] [z/k]. i = y, j = x, k = z 
	public void NewNetwork ()
	{
		//creating the shape of the neural net, can also be done randomly
		if(net != null){
			Array.Clear(net, 0, net.Length);
		}
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

				//initializes values for the threshold
				net [i] [j] [0] = UnityEngine.Random.Range (-5f, 5f); //both are inclusive

				netSizeB = net [i] [j].Length;
				for (int k = 0; k < netSizeB; k++) {
					//initializes values for the weights
					if(k != 0){
						net [i] [j] [k] = UnityEngine.Random.Range (-1f, 1f); //both are inclusive
					}
					//newCube.transform.SetParent(transform, false);
					//transform.position = new Vector3(5f, 5f, 5f);
				}
			}
		}
	}


	//all weights for each neuron multiplyed by the sending neuron and added with all the other weights going to the reciveing neuron
	// and final sum added with the reciveing neuron value
	void FeedForward ()
	{
		//Debug.Log(shapeLength);
		//goes through each layer
		for (int i = 1; i < shapeLength; i++) {
			 

			//goes through each neuron in the layer
			for (int j = 0; j < shape [i]; j++) {

				temp = 0;
				//goes through all the weights and neurons connecting to a neuron
				//add activation function
				for (int k = 0; k < shape [i - 1]; k++) {
					if (i == 1) {
						//none (not good)
						//temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
						//leaky ReLU, leak slope of 10
						if((net [0] [k] [0]) * (net [1] [j] [k + 1]) > 0){
							temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
						} else {
							temp += ((net [0] [k] [0]) * (net [1] [j] [k + 1])) / 10;
						}
					} else {
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

		int value = outputList.Count - shape[shapeLength - 1];
		for(int i = 0; i < value; i++){
			outputList.RemoveAt(0);
		}
		//Debug.Log(outputList.Count);
		/*for (int i = 0; i < outputList.Count; i++) {
			Debug.Log(outputList[i]);
		}*/
	}


	// Update is called once per frame
	void Update () {
		if(BeenBorn == true){
			if(FirstManager.Selector == 1){
				//do stuff like feedforward
			}
		}
	}
}