using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pump : Building {
	public bool CanPump = true;
	[SerializeField] private Resource Resource;
	[SerializeField] private Animator[] PumpAnimators;
	private StorageSpace StorageSpace;

	public override void OnInit() {
		//create storage space
		StorageSpace = gameObject.AddComponent<StorageSpace>();
		StorageSpace.Capacity = 5f;
		StorageSpace.maxInputs = 0;
	}

	public override void Start () {}

	public override void Update () {
		for (int i = 0; i < PumpAnimators.Length; i++) {
		}
	}
}
