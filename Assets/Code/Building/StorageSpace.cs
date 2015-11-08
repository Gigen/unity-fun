using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Resource {
	WATER = 0,
	GOLD = 1
}

public class ResourceData {
	public static Color GetResourceColor(Resource r) {
		switch(r) {
		case Resource.GOLD:
			return Color.yellow;
			break;
		case Resource.WATER:
			return Color.blue;
			break;
		}
		return Color.black;
	}
}

public class StorageSpace : MonoBehaviour {
	public Resource Resource;
	public float Capacity = 10f;
	public float Stored = 0f;
	public float maxInputs = 1;
	public float maxOutputs = 1;
	public float inputSpeed = 1;
	public float outputSpeed = 1;
	public List<StorageSpace> Inputs = new List<StorageSpace>();
	public List<StorageSpace> Outputs = new List<StorageSpace>();
    public Transform VisualRepresentationTransform;
    

    public float Fullness
    {
        get { return Mathf.Clamp01(Stored / Capacity); }
    }

    public float FreeStorage
    {
        get { return Capacity-Stored; }
    }

    public void Fill(float amount)
    {
        Stored += amount;
        if (Stored > Capacity)
            Debug.LogError("Stored more than capacity!");
    }
    public void Remove(float amount)
    {
        Stored -= amount;
        if (Stored < 0)
            Debug.LogError("Negative Stored value!");
    }

    static void Transfer(StorageSpace output, StorageSpace input, float amount)
    {
        output.Remove(amount);
        input.Fill(amount);
    }

    public void Update()
    {
        ManageOutputs();
        VisualRepresentation();
    }

    public void VisualRepresentation()
    {
        if (VisualRepresentationTransform == null)
            return;
        VisualRepresentationTransform.localScale = new Vector3(1, Fullness, 1);

    }

    public void ManageOutputs()
    {
        if (Stored <= 0)
            return;
        List<StorageSpace> emptierOutputs = new List<StorageSpace>(Outputs.Count);
        foreach (StorageSpace output in Outputs)
        {
            if (output.Fullness < Fullness)
                emptierOutputs.Add(output);
        }
        if (emptierOutputs.Count > 0)
        {
            float outputRate = outputSpeed / emptierOutputs.Count;
            float packetSize = outputRate * Time.deltaTime;
            if ((packetSize * emptierOutputs.Count) > Stored)
                packetSize = Stored / emptierOutputs.Count;
            foreach (StorageSpace output in emptierOutputs)
            {
                if (FreeStorage < packetSize)
                {
                    Transfer(this, output, output.FreeStorage);
                    //TODO Adjust packet size for next
                } else
                {
                    Transfer(this, output, packetSize);
                }
            }
        }
    }

    public static bool Connect(Building output, Building input)
    {
        StorageSpace outputStorageSpace = output.GetStorageSpace();
        StorageSpace inputStorageSpace = input.GetStorageSpace();
        if (outputStorageSpace && inputStorageSpace)
        {
            return Connect(outputStorageSpace, inputStorageSpace);
        }
        return false;
    }

    public static bool Connect(StorageSpace output, StorageSpace input)
    {
        if (output.Outputs.Count >= output.maxOutputs)
            return false;
        if (input.Inputs.Count >= input.maxInputs)
            return false;
        output.Outputs.Add(input);
        input.Inputs.Add(output);
        return false;
    }
}
