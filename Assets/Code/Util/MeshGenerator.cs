using UnityEngine;
using System.Collections;

public class MeshGenerator {

	public static Mesh Cylinder(Mesh m, int numDivisions, float radius, float height) {
		bool newMesh = false;
		if (m == null) {
			m = new Mesh();
			newMesh = true;
		}
		
		int numVertices = numDivisions*2;
		Vector3[] vertices = new Vector3[numVertices + 2];
		Vector2[] uv = new Vector2[numVertices + 2];
		int[] triangles = new int[numDivisions*3*4]; // 3 indices per triangle, 2 triangles per quad + 2 for cap
		
		for (int i = 0; i < numDivisions; i++) {
			float r = ((float)i/(float)numDivisions) * Mathf.PI * 2;
			float sin = Mathf.Sin(r);
			float cos = Mathf.Cos(r);
			
			vertices[i*2 + 0] = new Vector3(sin*radius,height/2,cos*radius);
			vertices[i*2 + 1] = new Vector3(sin*radius,-height/2,cos*radius);
		}
		vertices[numVertices+0] = new Vector3(0,height/2,0);
		vertices[numVertices+1] = new Vector3(0,-height/2,0);
		
		for (int i = 0; i < numDivisions; i++) {
			float r = ((float)i/(float)numDivisions);
			uv[i*2 + 0] = new Vector2(0,0);
			uv[i*2 + 1] = new Vector2(0,0);
		}
		uv[numVertices+0] = new Vector2(0,0);
		uv[numVertices+1] = new Vector2(0,0);

		for (int i = 0; i < numDivisions; i++) {			
			triangles[i*3*4 + 0] = i*2 + 0;
			triangles[i*3*4 + 1] = i*2 + 1;
			triangles[i*3*4 + 2] = ((i+1)*2 + 0) % numVertices;
			
			triangles[i*3*4 + 3] = i*2 + 1;
			triangles[i*3*4 + 4] = ((i+1)*2 + 1) % numVertices;
			triangles[i*3*4 + 5] = ((i+1)*2 + 0) % numVertices;
			
			triangles[i*3*4 + 6] = numVertices+1;
			triangles[i*3*4 + 7] = ((i+1)*2 + 1) % numVertices;
			triangles[i*3*4 + 8] = i*2 + 1;
			
			triangles[i*3*4 + 9] = numVertices+0;
			triangles[i*3*4 + 10] = i*2 + 0;
			triangles[i*3*4 + 11] = ((i+1)*2 + 0) % numVertices;

		}

		m.vertices = vertices;
		m.uv = uv;
		m.triangles = triangles;
		m.RecalculateNormals();
		return m;
	}

	public static Mesh Tube(Mesh m, int numDivisions, float innerRadius, float outerRadius, float height) {
		bool newMesh = false;
		if (m == null) {
			m = new Mesh();
			newMesh = true;
		} 
		int numVertices = (numDivisions+1)*8;
		Vector3[] vertices = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];
		int[] triangles = new int[numDivisions*3*2*4]; // 3 indices per triangle, 2 triangles per quad, 4 quads per circle

		if (!newMesh) {
			vertices = m.vertices;
		}

		for (int i = 0; i < numDivisions; i++) {
			float r = ((float)i/(float)(numDivisions-1)) * Mathf.PI * 2;
			float sin = Mathf.Sin(r);
			float cos = Mathf.Cos(r);

			vertices[i*8 + 0] = new Vector3(sin*innerRadius,height/2,cos*innerRadius);
			vertices[i*8 + 1] = new Vector3(sin*outerRadius,height/2,cos*outerRadius);
			
			vertices[i*8 + 2] = new Vector3(sin*outerRadius,height/2,cos*outerRadius);
			vertices[i*8 + 3] = new Vector3(sin*outerRadius,-height/2,cos*outerRadius);

			vertices[i*8 + 4] = new Vector3(sin*outerRadius,-height/2,cos*outerRadius);
			vertices[i*8 + 5] = new Vector3(sin*innerRadius,-height/2,cos*innerRadius);

			vertices[i*8 + 6] = new Vector3(sin*innerRadius,-height/2,cos*innerRadius);
			vertices[i*8 + 7] = new Vector3(sin*innerRadius,height/2,cos*innerRadius);
		}
		for (int i = 0; i < (numDivisions-1); i++) {
			float r = ((float)i/(float)numDivisions);
			uv[i*8 + 0] = new Vector2(0,0);
			uv[i*8 + 1] = new Vector2(0,0);
			uv[i*8 + 2] = new Vector2(0,0);
			uv[i*8 + 3] = new Vector2(0,0);
			uv[i*8 + 0] = new Vector2(0,0);
			uv[i*8 + 1] = new Vector2(0,0);
			uv[i*8 + 2] = new Vector2(0,0);
			uv[i*8 + 3] = new Vector2(0,0);
		}
		if (newMesh) {
			for (int i = 0; i < numDivisions-1; i++) {
				for (int x = 0; x < 4; x++) {
					
					triangles[i*4*2*3 + x*3*2 + 0] = i*8 + 0 + x*2;
					triangles[i*4*2*3 + x*3*2 + 1] = i*8 + 1 + x*2;
					triangles[i*4*2*3 + x*3*2 + 2] = (i+1)*8 + 0 + x*2;
					
					triangles[i*4*2*3 + x*3*2 + 3] = i*8 + 1 + x*2;
					triangles[i*4*2*3 + x*3*2 + 4] = (i+1)*8 + 1 + x*2;
					triangles[i*4*2*3 + x*3*2 + 5] = (i+1)*8 + 0 + x*2;
				}
			}
		}
		
		m.vertices = vertices;
		m.uv = uv;
		if (newMesh)
			m.triangles = triangles;
		
		m.RecalculateNormals();
		return m;
	}
}
