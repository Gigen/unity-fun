using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Silo : Building {
    public StorageSpace StorageSpace;
    public override void OnInit() {
	}

	public override void Start () {}

    public override StorageSpace GetStorageSpace() { return StorageSpace; }
}
