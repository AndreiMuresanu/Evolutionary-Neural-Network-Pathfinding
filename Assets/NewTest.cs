using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewTest : IComparable<NewTest> {

	public int integer;
	public string name;
	public int[][] testArray;


	public NewTest (int newInteger, string newName, int[][] newTestArray)
	{
		name = newName;
		integer = newInteger;
		testArray = newTestArray;
		Debug.Log("Been Born");
	}


	public int CompareTo (NewTest other)
	{
		if (other == null) {
			return 1;
		}

		return integer - other.integer;
	}


	public int FrameDuty ()
	{
		integer += 1;
		return integer;
	}

	public void Speak (){
		Debug.Log("start");
		for (int i = 0; i < testArray.GetLength (0); i++) {
			for (int k = 0; k < testArray[i].Length; k++) {
				Debug.Log (testArray [i] [k]);
			}
		}
		Debug.Log("end");
	}
}
