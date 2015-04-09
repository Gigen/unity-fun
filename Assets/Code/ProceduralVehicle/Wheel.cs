using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wheel : MonoBehaviour {

	// Use this for initialization

	[SerializeField] private float _TireWidth = 225; //mm
	[SerializeField] private float _TireAspectRatio = 40; //calculate height by *_TireWidth
	[Range(0.0f, 1.0f)] [SerializeField] private float _SideWallSize = 0.5f;
	[Range(0.0f, 1.0f)] [SerializeField] private float _FlatPart = 0.75f;
	[Range(0.0f, 1.0f)] [SerializeField] private float _Curvature = 0.54f;
	[SerializeField] private float _RimDiameter = 18; //inch
	[SerializeField] private float _RimWidth = 7.5f; //inch
	[SerializeField] private float _RimEt = 55f; //mm

	public bool Mirror = false;


	[SerializeField] private int divisions = 15;

	private const int NUM_CIRCULAR_DIVISIONS = 64;

	private GameObject Tire;
	private MeshFilter TireMeshFilter;
	private BezierSpline TireSpline;

	private GameObject RimArch;
	private MeshFilter RimArchMeshFilter;
	private MeshFilter RimCenterMeshFilter;


	void Start () {
		UpdateTireBezier();
		CreateWheel();
	}

	public float TireWidth
	{
		get { return _TireWidth * Helper.MmToUU; }
	}

	public float TireHeight
	{
		get { return (_TireWidth * (_TireAspectRatio/100f)) * Helper.MmToUU; }
	}
	public float RimRadius
	{
		get { return _RimDiameter *0.5f * Helper.InchToMilimeter * Helper.MmToUU; }
	}
	public float RimWidth
	{
		get { return _RimWidth * Helper.InchToMilimeter * Helper.MmToUU; }
	}
	public float RimEt
	{
		get { 
			if (Mirror) return -1 * _RimEt * Helper.MmToUU;
			else return _RimEt * Helper.MmToUU;
		}
	}
	public float TotalWheelRadius {
		get { return TireHeight + RimRadius; }
	}

	public void CreateWheel() {
		// Generate tire
		Tire = Helper.CreateMeshObject("Tire",GenerateTireMesh(),Resources.Load ("Vehicle/Tire_MAT") as Material);
		Tire.transform.parent = transform;
		Tire.transform.localRotation = Quaternion.identity;
		Tire.transform.localScale = Vector3.one;
		TireMeshFilter = Tire.GetComponent<MeshFilter>();

		//Generate rim
		GameObject rimCenter = Helper.CreateMeshObject("Rim",MeshGenerator.Cylinder(null,NUM_CIRCULAR_DIVISIONS/4,60*Helper.MmToUU,15*Helper.MmToUU),Resources.Load ("Vehicle/Default_MAT") as Material);
		RimCenterMeshFilter = rimCenter.GetComponent<MeshFilter>();
		rimCenter.transform.parent = transform;
		rimCenter.transform.localPosition =  Vector3.zero;
		rimCenter.transform.localRotation = Quaternion.identity;
		rimCenter.transform.localScale = Vector3.one;

		RimArch = Helper.CreateMeshObject("RimArch",MeshGenerator.Tube(null,NUM_CIRCULAR_DIVISIONS,RimRadius-20*Helper.MmToUU,RimRadius,RimWidth+5*Helper.MmToUU),Resources.Load ("Vehicle/Default_MAT") as Material);
		RimArchMeshFilter = RimArch.GetComponent<MeshFilter>();
		RimArch.transform.parent = rimCenter.transform;
		RimArch.transform.localPosition =  Vector3.zero;
		RimArch.transform.localRotation = Quaternion.identity;
		RimArch.transform.localScale = Vector3.one;
	}

	public void UpdateTireBezier() {
		if (TireSpline == null) {
			//first time, create spline
			TireSpline = gameObject.AddComponent<BezierSpline>();
			TireSpline.Reset();
			TireSpline.AddCurve();
			TireSpline.AddCurve();
			TireSpline.AddCurve();
			TireSpline.AddCurve();
		}
		float SmoothValue = 0.025f;

		Vector3 p1 = new Vector3(RimRadius,RimWidth/2,0);
		TireSpline.SetControlPoint(0,p1);
		TireSpline.SetControlPoint(1,p1+new Vector3(Mathf.Min (SmoothValue,TireHeight*_SideWallSize),0,0));
		//Mathf.Max (-value,(WheelDiameter+TireHeight*_SideWallSize) - _WheelDiameter)
		Vector3 p2 = new Vector3(RimRadius+TireHeight*_SideWallSize,TireWidth/2,0);
		TireSpline.SetControlPoint(3,p2);
		TireSpline.SetControlPoint(2,p2+new Vector3(Mathf.Max (-SmoothValue,-TireHeight*_SideWallSize),0,0));
		TireSpline.SetControlPoint(4,p2+new Vector3(TireHeight*(1-_SideWallSize)*_Curvature,0,0));
		
		Vector3 p3 = new Vector3(RimRadius+TireHeight,(TireWidth/2)*_FlatPart,0);
		TireSpline.SetControlPoint(6,p3);
		TireSpline.SetControlPoint(5,p3+new Vector3(0,(TireWidth/2)*(1-_FlatPart)*_Curvature,0));
		TireSpline.SetControlPoint(7,p3+new Vector3(0,Mathf.Max (-SmoothValue,Mathf.Clamp01(-p3.y)),0));
		
		Vector3 p4 = new Vector3(RimRadius+TireHeight,(-TireWidth/2)*_FlatPart,0);
		TireSpline.SetControlPoint(9,p4);
		TireSpline.SetControlPoint(8,p4+new Vector3(0,Mathf.Min (SmoothValue,Mathf.Clamp01(-p4.y)),0));
		TireSpline.SetControlPoint(10,p4+new Vector3(0,(-TireWidth/2)*(1-_FlatPart)*_Curvature,0));
		
		Vector3 p5 = new Vector3(RimRadius+TireHeight*_SideWallSize,-TireWidth/2,0);
		TireSpline.SetControlPoint(12,p5);
		TireSpline.SetControlPoint(11,p5+new Vector3(TireHeight*(1-_SideWallSize)*_Curvature,0,0));
		TireSpline.SetControlPoint(13,p5+new Vector3(Mathf.Max (-SmoothValue,-TireHeight*_SideWallSize),0,0));
		
		Vector3 p6 = new Vector3(RimRadius,-RimWidth/2,0);
		TireSpline.SetControlPoint(15,p6);
		TireSpline.SetControlPoint(14,p6+new Vector3(Mathf.Min (SmoothValue,TireHeight*_SideWallSize),0,0));
	}
	public Mesh GenerateTireMesh() {
		Mesh m = new Mesh();
		int div = divisions+1;
		int numVertices = NUM_CIRCULAR_DIVISIONS*div;
		int[] triangles = new int[NUM_CIRCULAR_DIVISIONS*3*2*(div-1)];

		UpdateTireMesh(m);
		
		for (int i = 0; i < NUM_CIRCULAR_DIVISIONS; i++) {
			for (int x = 0; x < div-1; x++) {
				triangles[i*3*2*(div-1) + x*3*2 + 0] = i*div + 0 + x ;
				triangles[i*3*2*(div-1) + x*3*2 + 1] = i*div + 1 + x;
				triangles[i*3*2*(div-1) + x*3*2 + 2] = (i+1)*div + 0 + x;
				
				triangles[i*3*2*(div-1) + x*3*2 + 3] = i*div + 1 + x;
				triangles[i*3*2*(div-1) + x*3*2 + 4] = (i+1)*div + 1 + x;
				triangles[i*3*2*(div-1) + x*3*2 + 5] = (i+1)*div + 0 + x;
			}
		}
		m.triangles = triangles;
		m.RecalculateNormals();
		return m;
	}

	public void UpdateTireMesh(Mesh m) {
		int div = divisions +1;

		int numVertices = (NUM_CIRCULAR_DIVISIONS+1)*div;
		Vector3[] vertices = new Vector3[numVertices];
		Vector2[] uv1 = new Vector2[numVertices];
		Vector2[] uv2 = new Vector2[numVertices];

		Vector3[] divPositions = new Vector3[div];
		for (int d = 0; d < div; d++) { 
			divPositions[d] = TireSpline.GetLocalPoint(d/(float)(div-1));
		}
		for (int i = 0; i < (NUM_CIRCULAR_DIVISIONS+1); i++) {
			float r = ((float)i/(float)NUM_CIRCULAR_DIVISIONS) * Mathf.PI * 2;
			float sin = Mathf.Sin(r);
			float cos = Mathf.Cos(r);
			
			for (int d = 0; d < div; d++) {
				vertices[i*div + d] = new Vector3(sin*divPositions[d].x,divPositions[d].y,cos*divPositions[d].x);
			}
		}
		
		float ratioTread = Mathf.Round((RimRadius+TireHeight) * 2 * Mathf.PI / TireWidth);
		float ratioRadius = 1f/((RimRadius+TireHeight)*2f);
		for (int i = 0; i < (NUM_CIRCULAR_DIVISIONS+1); i++) {
			float r = ((float)i/(float)NUM_CIRCULAR_DIVISIONS);
			float sin = Mathf.Sin(r*Mathf.PI*2) * ratioRadius;
			float cos = Mathf.Cos(r*Mathf.PI*2) * ratioRadius;
			r= r*ratioTread;
			for (int d = 0; d < div; d++) {
				uv2[i*div + d] = new Vector2(sin*divPositions[d].x+0.5f, cos*divPositions[d].x+0.5f);
				uv1[i*div + d] = new Vector2(divPositions[Mathf.Clamp(d,(divisions/5),divisions - (divisions/5))].y / TireWidth + 0.5f, r);
			}
		}

		m.vertices = vertices;
		m.uv = uv1;
		m.uv2 = uv2;
		m.RecalculateNormals();
	}

	void Update () {
		UpdateTireBezier();
		UpdateTireMesh(TireMeshFilter.sharedMesh);
		Tire.transform.localPosition = new Vector3(0,-RimEt,0);
		RimArch.transform.localPosition = Tire.transform.localPosition;
		MeshGenerator.Tube(RimArchMeshFilter.sharedMesh, NUM_CIRCULAR_DIVISIONS,RimRadius-20*Helper.MmToUU,RimRadius,RimWidth+5*Helper.MmToUU);
	}
}
