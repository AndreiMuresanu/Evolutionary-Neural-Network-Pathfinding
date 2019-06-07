using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondTest : MonoBehaviour {

	private float[][][] net;
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
	public float fitness;
	private int Buffer;
	private GameObject target;
	private bool BeenBorn = false;


	public void SecondTestSetup (List<int> GivenShape, float GivenFitness, GameObject GivenTarget, int GivenBuffer, GameObject Cube)
	{
		this.shape = GivenShape;
		this.fitness = GivenFitness;
		this.target = GivenTarget;
		this.Buffer = GivenBuffer;
		testCube = Cube;
		NewNetwork();
		BeenBorn = true;
	}

	public List<int> GiveShape ()
	{
		return shape;
	}

	public float GiveFitness ()
	{
		return fitness;
	}

	public GameObject GiveTarget ()
	{
		return target;
	}

	public int GiveBuffer ()
	{
		return Buffer;
	}

	public GameObject GiveCube ()
	{
		return testCube;
	}


	// Use this for initialization(not called if object is not activated and not called again if object is reactivated
	/*void Start ()
	{
		NewNetwork();
	}*/

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
				//net [i] [j] = new float[3];

				//initializes values for the threshold
				net [i] [j] [0] = UnityEngine.Random.Range (-5f, 5f); //both are inclusive
				//as test
				//net [i] [j] [0] = j + 1;

				//Instantiate (testCube, new Vector3 (j, i, 0), Quaternion.identity);

				netSizeB = net [i] [j].Length;
				for (int k = 0; k < netSizeB; k++) {
					//initializes values for the weights
					if(k != 0){
						net [i] [j] [k] = UnityEngine.Random.Range (-1f, 1f); //both are inclusive
						//as test
						//net [i] [j] [k] = k + j;
					}
					//Debug.Log(net [i] [j] [k]);
					//makes a visualization of the net array
					GameObject newCube;
					newCube = Instantiate (testCube, new Vector3 (j + (Buffer * 20), -i, k), Quaternion.identity);
					newCube.name = string.Format("({0},{1},{2})", j, -i, k);
					newCube.transform.parent = transform;
					//newCube.transform.SetParent(transform, false);
					//transform.position = new Vector3(5f, 5f, 5f);
				}
			}
		}
		//Debug.Log("Done Birthing");
		//Debug.Log(net [0] [0] [0]);
		//FeedForward ();
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
				for (int k = 0; k < shape [i - 1]; k++) {
					if (i == 1) {
						//Debug.Log("cool");
						temp += (net [0] [k] [0]) * (net [1] [j] [k + 1]);
					} else {
						//Debug.Log(Counter);
						//Debug.Log(shape[(i - 1)]);
						temp += outputList [(k + Counter - shape[i - 1])] * (net [i] [j] [k + 1]);
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
		if(BeenBorn == true){
			//do stuff like feedforward
		}
	}
}
