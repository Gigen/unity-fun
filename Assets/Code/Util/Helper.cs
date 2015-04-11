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
	public static bool FindCircleCircleIntersections(
		float cx0, float cy0, float radius0,
		float cx1, float cy1, float radius1,
		out Vector2 intersection1, out Vector2 intersection2)
	{
		// Find the distance between the centers.
		float dx = cx0 - cx1;
		float dy = cy0 - cy1;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		
		// See how many solutions there are.
		if (dist > radius0 + radius1)
		{
			Debug.LogError("1 - Circles not touching");
			intersection1 = Vector2.zero;
			intersection2 = Vector2.zero;
			return false;
		}
		else if (dist < Mathf.Abs(radius0 - radius1))
		{
			// No solutions, one circle contains the other.
			Debug.LogError("2 - No solutions, one circle contains the other.");
			intersection1 = Vector2.zero;
			intersection2 = Vector2.zero;
			return false;
		}
		else if ((dist == 0) && (radius0 == radius1))
		{
			
			Debug.LogError("3 - Circles are same");
			intersection1 = Vector2.zero;
			intersection2 = Vector2.zero;
			return false;
		}
		else
		{
			// Find a and h.
			float a = (radius0 * radius0 -
			            radius1 * radius1 + dist * dist) / (2 * dist);
			float h = Mathf.Sqrt(radius0 * radius0 - a * a);
			
			// Find P2.
			float cx2 = cx0 + a * (cx1 - cx0) / dist;
			float cy2 = cy0 + a * (cy1 - cy0) / dist;
			
			// Get the points P3.
			intersection1 = new Vector2(
				(float)(cx2 + h * (cy1 - cy0) / dist),
				(float)(cy2 - h * (cx1 - cx0) / dist));
			intersection2 = new Vector2(
				(float)(cx2 - h * (cy1 - cy0) / dist),
				(float)(cy2 + h * (cx1 - cx0) / dist));

			return true;
		}
	}
}
