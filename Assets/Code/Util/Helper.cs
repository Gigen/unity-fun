using UnityEngine;
using System.Collections;

public class Helper {
	
	public const float InchToMilimeter = 25.4f;
	public const float MmToUU = 0.001f; //0.001

	public static GameObject CreateMeshObject(string name, Mesh mesh, Material m) {
		GameObject g = new GameObject();
		g.name = name;
		MeshFilter meshFilter = g.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = g.AddComponent<MeshRenderer>();
		meshRenderer.material = Material.Instantiate(m) as Material;
		meshFilter.mesh = mesh;
		return g;
	}
}
