using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pump : Building {
	public bool CanPump = true;
	[SerializeField] private Resource Resource;
	[SerializeField] private Animator PumpAnimator;
	private StorageSpace StorageSpace;
    private float pumpedAmount;
    private bool WaitingForFlush = false;
    private bool Pumping = false;

	public override void OnInit() {
        //create storage space
        StorageSpace = gameObject.AddComponent<StorageSpace>();
		StorageSpace.Capacity = 5f;
		StorageSpace.maxInputs = 0;
        Pumping = true;
	}

	public override void Start () {}

    public override void Update() {
        if (WaitingForFlush)
        {
            CheckFlush();
        } else
        {
            int pumping = CanItPump();
            PumpAnimator.SetFloat("PumpSpeed", pumping * 1f, 0.4f, Time.deltaTime);
            if (pumping == 1) { 
                pumpedAmount += 1.0f * Time.deltaTime;
            }
        }
    }

    public int CanItPump(){
        bool p = CanPump && Pumping; 
        return p ? 1 : 0;
    }

    public void SetWaitingForFlush()
    {
        WaitingForFlush = true;
        Pumping = false;
    }

    public void CheckFlush(){
        if ((StorageSpace.Stored + pumpedAmount) < StorageSpace.Capacity) {
            PumpAnimator.SetTrigger("CanFlush");
            WaitingForFlush = false;
        }
    }

    public void Flush() {
        StorageSpace.Stored += pumpedAmount;
        pumpedAmount = 0;
        Pumping = true;
    }
}
