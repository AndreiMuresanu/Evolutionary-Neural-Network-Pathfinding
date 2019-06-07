using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPerson : MonoBehaviour {

	private float [][][] RealNetwork;
	private List<int> RealShape = new List<int>();
	private GameObject TargetObject;
	private float RealFitness = 0;
	private List<float> outputList = new List<float>();
	private float temp = 0;
	private int Counter = 0;
	private bool isTraining = false;
	private bool Finished = false;
	private bool SafeStart = true;
	//private float RealTest = 0;


	public void RealSetup (List<int> GivenTestNetShape, GameObject GivenTarget)
	{
		this.RealShape = GivenTestNetShape;
		this.TargetObject = GivenTarget;
		RealFitness = Random.Range (50, 100);
		//RealTest = Random.Range (50, 100);
		CreateArray();
	}


	public List<int> ReturnShape ()
	{
		return RealShape;
	}


	public float[][][] ReturnNetwork ()
	{
		return RealNetwork;
	}


	public GameObject ReturnTarget ()
	{
		return TargetObject;
	}


	public void Fillin (List<int> otherShape, GameObject otherObject, float otherFitness)
	{
		RealShape = otherShape;
		//RealNetwork = otherNetwork;
		TargetObject = otherObject;
		RealFitness = otherFitness;
	}


	public void StartTraining(){
		isTraining = true;
		Finished = false;
		SafeStart = true;
	}


	public void StopTraining(){
		isTraining = false;
	}


	void Update ()
	{
		if(Finished == false && isTraining == true && SafeStart == true){
			SafeStart = false;
			//Set input values
			//difference in values
			RealNetwork[0][0][0] = TargetObject.transform.position.x - gameObject.transform.position.x; //difference in x value
			RealNetwork[0][1][0] = TargetObject.transform.position.y - gameObject.transform.position.y; //difference in y value
			//coordinates
			/*RealNetwork[0][0][0] = TargetObject.transform.position.x;
			RealNetwork[0][1][0] = TargetObject.transform.position.y;
			RealNetwork[0][2][0] = gameObject.transform.position.x;
			RealNetwork[0][3][0] = gameObject.transform.position.y;*/
			FeedForward();
		}
	}


	public void CopyNetwork(float[][][] NetToCopy){
		CreateArray();
		for (int i = 0; i < RealShape.Count; i++) {
			for (int j = 0; j < RealShape [i]; j++) {
				for (int k = 0; k < RealNetwork [i] [j].Length; k++) {
					RealNetwork[i][j][k] = NetToCopy[i][j][k];
                }
            }
        }
	}


	public void MutateNetwork ()
	{
		for (int i = 0; i < RealShape.Count; i++) {
			for (int j = 0; j < RealShape [i]; j++) {
				for (int k = 0; k < RealNetwork [i] [j].Length; k++) {
					
					if (k != 0) {
						float randomNumber = UnityEngine.Random.Range(0f, 100f);
						//mutate weight value 
						if (randomNumber <= 0.2f)
	                    { //if 1
	                      //flip sign of weight
							RealNetwork [i] [j] [k] *= -1f;
	                    }
						else if (randomNumber <= 0.4f)
	                    { //if 2
	                      //pick random weight between -1 and 1
							RealNetwork [i] [j] [k] = UnityEngine.Random.Range(-1f, 1f);
	                    }
						else if (randomNumber <= 0.6f)
	                    { //if 3
	                      //randomly increase by 0% to 100%
	                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
							RealNetwork [i] [j] [k] *= factor;
	                    }
						else if (randomNumber <= 0.8f)
	                    { //if 4
	                      //randomly decrease by 0% to 100%
	                        float factor = UnityEngine.Random.Range(0f, 1f);
							RealNetwork [i] [j] [k] *= factor;
	                    }
					} else {
						float randomNumber = UnityEngine.Random.Range(0f, 100f);
						//mutate threshold value 
						if (randomNumber <= 0.2f)
	                    { //if 1
	                      //flip sign of weight
							RealNetwork [i] [j] [0] *= -1f;
	                    }
						else if (randomNumber <= 0.4f)
	                    { //if 2
	                      //pick random weight between -5 and 5
							RealNetwork [i] [j] [0] = UnityEngine.Random.Range(5f, 5f);
	                    }
						else if (randomNumber <= 0.6f)
	                    { //if 3
	                      //randomly increase by 0% to 100%
	                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
							RealNetwork [i] [j] [0] *= factor;
	                    }
						else if (randomNumber <= 0.8f)
	                    { //if 4
	                      //randomly decrease by 0% to 100%
	                        float factor = UnityEngine.Random.Range(0f, 1f);
							RealNetwork [i] [j] [0] *= factor;
	                    }
					}
					//Debug.Log(RealNetwork [i] [j] [k]);
				}
			}
		}
		//Debug.Log(RealTest);
	}


	public void SetFitness (float newFitness)
	{
		RealFitness = newFitness;
	}


	void CreateArray ()
	{
		//Debug.Log("Start");
		RealNetwork = new float[RealShape.Count][][];
		for (int i = 0; i < RealShape.Count; i++) {
			RealNetwork [i] = new float[RealShape [i]][]; 
			for (int j = 0; j < RealShape [i]; j++) {
				if (i != 0) {
					RealNetwork [i] [j] = new float[(RealShape [i - 1]) + 1];
				} else {
					RealNetwork [i] [j] = new float[1];
				}
				for (int k = 0; k < RealNetwork [i] [j].Length; k++) {
					if (k != 0) {
						RealNetwork [i] [j] [k] = Random.Range (-1f, 1f);
					} else {
						RealNetwork [i] [j] [k] = Random.Range (-5f, 5f);
					}
					//Debug.Log(RealNetwork [i] [j] [k]);
				}
			}
		}
		//Debug.Log("Finish");
	}

	public void FeedForward ()
	{
		outputList.Clear();
		temp = 0;
		Counter = 0;
		//goes through each layer
		for (int i = 1; i < RealShape.Count; i++) {
			 

			//goes through each neuron in the layer
			for (int j = 0; j < RealShape [i]; j++) {

				temp = 0;
				//goes through all the weights and neurons connecting to a neuron
				for (int k = 0; k < RealShape [i - 1]; k++) {
					if (i == 1) {
						//leaky ReLU, leak slope of 10
						if((RealNetwork [0] [k] [0]) * (RealNetwork [1] [j] [k + 1]) > 0){
							temp += (RealNetwork [0] [k] [0]) * (RealNetwork [1] [j] [k + 1]);
						} else {
							temp += ((RealNetwork [0] [k] [0]) * (RealNetwork [1] [j] [k + 1])) / 10;
						}
					} else {
						//leaky ReLU, leak slope of 10
						if(outputList [(k + Counter - RealShape[i - 1])] * (RealNetwork [i] [j] [k + 1]) > 0){
							temp += outputList [(k + Counter - RealShape[i - 1])] * (RealNetwork [i] [j] [k + 1]);
						} else{
							temp += (outputList [(k + Counter - RealShape[i - 1])] * (RealNetwork [i] [j] [k + 1])) / 10;
						}
					}
				}
				temp += RealNetwork [i] [j] [0];
				outputList.Add (temp);
			}
			Counter += RealShape[i];
		}

		int value = outputList.Count - RealShape[RealShape.Count - 1];
		for(int i = 0; i < value; i++){
			outputList.RemoveAt(0);
		}
		//Find largest output
		int MemoryOfIndex = 0;
		float MemoryOfOutput = outputList[0];
		for (int i = 1; i < outputList.Count; i++) {
			if(outputList[i] > MemoryOfOutput){
				MemoryOfIndex = i;
				MemoryOfOutput = outputList[i];
			}
			//Debug.Log(outputList[i]);
		}
		//Debug.Log(outputList[MemoryOfIndex]);
		//Do action
		//0 up, 1 right, 2 down, 3 left 
		if(MemoryOfIndex == 0){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);
		} else if(MemoryOfIndex == 1){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);
		} else if(MemoryOfIndex == 2){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);
		} else if(MemoryOfIndex == 3){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);
		}
		if(gameObject.transform.position != TargetObject.transform.position){
			RealFitness = -1 * (Vector3.Distance(gameObject.transform.position, TargetObject.transform.position));
		} else{
			RealFitness = 100;
			//Finished = true; //optional
		}
		SafeStart = true;
	}

	public float ReturnFitness ()
	{
		return RealFitness;
	}
}
