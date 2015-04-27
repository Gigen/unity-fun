using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct GateData {
	float value;
	BaseGateController gateScript;
}

public class BaseGateController : MonoBehaviour {

	private Dictionary<string, GateData> inputs;
	private Dictionary<string, GateData> outputs;

	void Start () {
		Time.fixedDeltaTime = 0.001f;
	}

	void addInput(string inputName) {
		inputs.Add (inputName, new GateData());
	}

	void addOutput(string outputName) {
		outputs.Add (outputName, new GateData());
	}
}
