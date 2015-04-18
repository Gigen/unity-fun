using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum PinType {
	Input = 0,
	Output = 1
};

public class BaseGateController : MonoBehaviour {

	private Dictionary<string, float> inputs;
	private Dictionary<string, float> outputs;

	void Start () {
		Time.fixedDeltaTime = 0.001f;
	}

	void addInput(string inputName) {
		inputs.Add (inputName, 0);
	}

	void addOutput(string outputName) {
		outputs.Add (outputName, 0);
	}
}
