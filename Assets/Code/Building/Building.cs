using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
{
    public BoxCollider SelectCollider;
    public float health = 100;
    public float maxHealth = 100;


    public void Awake()
    {
        health = maxHealth;
    }

    public void Init()
    {
        OnInit();
    }

    public abstract void OnInit();

    public virtual void Select()
    {
        Transform t = V8GameLogic.Instance.SelectBox;
        t.parent = transform;
        t.localPosition = SelectCollider.center;
        t.localScale = SelectCollider.size;
    }

    public virtual void Deselect()
    {
        V8GameLogic.Instance.SelectBox.parent = V8GameLogic.Instance.transform;
        V8GameLogic.Instance.SelectBox.localScale = Vector3.zero;
    }

    public virtual bool OverlapsWithAnotherBuilding()
    {
        return false; //TODO
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual StorageSpace GetStorageSpace() { return null; }

    void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(this.ToString() + " is colliding");
    }

}
