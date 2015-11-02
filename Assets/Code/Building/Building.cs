using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour {
	public float health = 100;
	public float maxHealth = 100;

	public void Awake () {
		health = maxHealth;
		OnInit();
	}

	public abstract void OnInit();

	public virtual void Start () {}

	public virtual void Update () {}
}
