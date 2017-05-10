using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

	public SquareGrid squareGrid;
	List<Vector3> vertices;
	List<int> triangles;

	public MeshFilter walls;
	private static float wallHeight = 5f;
	private static float baseHeight = 1f;
	private int mapWidth;
	private int mapHeight;
	private float squareSize;


	Dictionary<int, List<Triangle>> trigDictionary = new Dictionary<int, List<Triangle>>();
	List<List<int>> outlines = new List<List<int>>();
	HashSet<int> checkedVertices = new HashSet<int> ();
	HashSet<List<int>> createdEdges = new HashSet<List<int>> ();

	void SquareToTrigsFloor(Square square){ //TODO: Reverse these to form a floor, them cpoy them and reverse order to form a ceiling
		switch (square.configuration) {
		case 15:
			break;


		case 14:
			PointsToMesh (square.left, square.bottom, square.bottomLeft);
			break;
		case 13:
			PointsToMesh (square.bottomRight, square.bottom, square.right);
			break;
		case 11:
			PointsToMesh (square.topRight, square.right, square.top);
			break;
		case 7:
			PointsToMesh (square.topLeft, square.top, square.left);
			break;


		case 12:
			PointsToMesh (square.right, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 9:
			PointsToMesh (square.top, square.topRight, square.bottomRight, square.bottom);
			break;
		case 6:
			PointsToMesh (square.topLeft, square.top, square.bottom, square.bottomLeft);
			break;
		case 3:
			PointsToMesh (square.topLeft, square.topRight, square.right, square.left);
			break;
		case 10:
			PointsToMesh (square.top, square.topRight, square.right, square.bottom, square.bottomLeft, square.left);
			break;
		case 5:
			PointsToMesh (square.topLeft, square.top, square.right, square.bottomRight, square.bottom, square.left);
			break;


		case 8:
			PointsToMesh (square.top, square.topRight, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 4:
			PointsToMesh (square.topLeft, square.top, square.right, square.bottomRight, square.bottomLeft);
			break;
		case 2:
			PointsToMesh (square.topLeft, square.topRight, square.right, square.bottom, square.bottomLeft);
			break;
		case 1:
			PointsToMesh (square.topLeft, square.topRight, square.bottomRight, square.bottom, square.left);
			break;


		case 0:
			PointsToMesh (square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
			checkedVertices.Add (square.topLeft.index);
			checkedVertices.Add (square.topRight.index);
			checkedVertices.Add (square.bottomLeft.index);
			checkedVertices.Add (square.bottomRight.index);
			break;

		}
	}


	void SquareToTrigsCeiling(Square square){ //TODO: Reverse these to form a floor, them cpoy them and reverse order to form a ceiling
		switch (square.configuration) {
		case 15:
			break;


		case 14:
			PointsToMeshC (square.left, square.bottom, square.bottomLeft);
			break;
		case 13:
			PointsToMeshC (square.bottomRight, square.bottom, square.right);
			break;
		case 11:
			PointsToMeshC (square.topRight, square.right, square.top);
			break;
		case 7:
			PointsToMeshC (square.topLeft, square.top, square.left);
			break;


		case 12:
			PointsToMeshC (square.right, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 9:
			PointsToMeshC (square.top, square.topRight, square.bottomRight, square.bottom);
			break;
		case 6:
			PointsToMeshC (square.topLeft, square.top, square.bottom, square.bottomLeft);
			break;
		case 3:
			PointsToMeshC (square.topLeft, square.topRight, square.right, square.left);
			break;
		case 10:
			PointsToMeshC (square.top, square.topRight, square.right, square.bottom, square.bottomLeft, square.left);
			break;
		case 5:
			PointsToMeshC (square.topLeft, square.top, square.right, square.bottomRight, square.bottom, square.left);
			break;


		case 8:
			PointsToMeshC (square.top, square.topRight, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 4:
			PointsToMeshC (square.topLeft, square.top, square.right, square.bottomRight, square.bottomLeft);
			break;
		case 2:
			PointsToMeshC (square.topLeft, square.topRight, square.right, square.bottom, square.bottomLeft);
			break;
		case 1:
			PointsToMeshC (square.topLeft, square.topRight, square.bottomRight, square.bottom, square.left);
			break;


		case 0:
			PointsToMeshC (square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
			checkedVertices.Add (square.topLeft.index);
			checkedVertices.Add (square.topRight.index);
			checkedVertices.Add (square.bottomLeft.index);
			checkedVertices.Add (square.bottomRight.index);
			break;

		}
	}



	/*
	void SquareToTrigsFloor(Square square){ //TODO: Reverse these to form a floor, them cpoy them and reverse order to form a ceiling
		switch (square.configuration) {
		case 0:
			break;


		case 1:
			PointsToMesh (square.left, square.bottom, square.bottomLeft);
			break;
		case 2:
			PointsToMesh (square.bottomRight, square.bottom, square.right);
			break;
		case 4:
			PointsToMesh (square.topRight, square.right, square.top);
			break;
		case 8:
			PointsToMesh (square.topLeft, square.top, square.left);
			break;


		case 3:
			PointsToMesh (square.right, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 6:
			PointsToMesh (square.top, square.topRight, square.bottomRight, square.bottom);
			break;
		case 9:
			PointsToMesh (square.topLeft, square.top, square.bottom, square.bottomLeft);
			break;
		case 12:
			PointsToMesh (square.topLeft, square.topRight, square.right, square.left);
			break;
		case 5:
			PointsToMesh (square.top, square.topRight, square.right, square.bottom, square.bottomLeft, square.left);
			break;
		case 10:
			PointsToMesh (square.topLeft, square.top, square.right, square.bottomRight, square.bottom, square.left);
			break;


		case 7:
			PointsToMesh (square.top, square.topRight, square.bottomRight, square.bottomLeft, square.left);
			break;
		case 11:
			PointsToMesh (square.topLeft, square.top, square.right, square.bottomRight, square.bottomLeft);
			break;
		case 13:
			PointsToMesh (square.topLeft, square.topRight, square.right, square.bottom, square.bottomLeft);
			break;
		case 14:
			PointsToMesh (square.topLeft, square.topRight, square.bottomRight, square.bottom, square.left);
			break;


		case 15:
			PointsToMesh (square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
			checkedVertices.Add (square.topLeft.index);
			checkedVertices.Add (square.topRight.index);
			checkedVertices.Add (square.bottomLeft.index);
			checkedVertices.Add (square.bottomRight.index);
			break;

		}
	}
*/


	void PointsToMesh(params Node[] points){
		AssignVertices (points);

		if (points.Length >= 3)
			GenerateTriangle (points [0], points [1], points [2]);

		if (points.Length >= 4)
			GenerateTriangle (points [0], points [2], points [3]);

		if (points.Length >= 5)
			GenerateTriangle (points [0], points [3], points [4]);

		if (points.Length >= 6)
			GenerateTriangle (points [0], points [4], points [5]);

	}

	void PointsToMeshC(params Node[] points){
		AssignVertices (points);

		if (points.Length >= 3)
			GenerateTriangle (points [2], points [1], points [0]);

		if (points.Length >= 4)
			GenerateTriangle (points [3], points [2], points [0]);

		if (points.Length >= 5)
			GenerateTriangle (points [4], points [3], points [0]);

		if (points.Length >= 6)
			GenerateTriangle (points [5], points [4], points [0]);

	}

	void GenerateTriangle(Node a, Node b, Node c){
		triangles.Add (a.index);
		triangles.Add (b.index);
		triangles.Add (c.index);

		Triangle triangle = new Triangle (a.index, b.index, c.index);
		AddTrigToDictionary(triangle, a.index);
		AddTrigToDictionary(triangle, b.index);
		AddTrigToDictionary(triangle, c.index);
	}

	void AddTrigToDictionary(Triangle triangle, int index){
		if (trigDictionary.ContainsKey (index)) {
			trigDictionary [index].Add (triangle);
		} else {
			List<Triangle> trList = new List<Triangle> ();
			trList.Add (triangle);
			trigDictionary.Add (index, trList);
		}
	}

	bool isExternalEdge(int vertIndexA, int vertIndexB){
		List<Triangle> ListA = trigDictionary [vertIndexA];
		int sharedTriangles = 0;
		foreach (Triangle triangle in ListA) {
			if(triangle.Contains(vertIndexB)){
				sharedTriangles++;
				if(sharedTriangles>1) break;
			}
		}
		return sharedTriangles == 1;
	}

	int GetConnectedEdgeVertex(int indexA){
		List<Triangle> ListA = trigDictionary [indexA];
		foreach (Triangle triangle in ListA) {
			foreach (int vertexB in triangle) {
				if (vertexB == indexA)
					continue;
				if (checkedVertices.Contains (vertexB))
					continue;
				if(isExternalEdge(indexA, vertexB)){
					//print ("Edge: "+indexA + ", " + vertexB);
					return vertexB;
				}
			}
		}

		return -1;
	}

	void CalculateMeshOutlines(){

		int currentCouple;

		for (int current = 0; current < vertices.Count; current++) {
			if (!checkedVertices.Contains (current)) {
				currentCouple = GetConnectedEdgeVertex (current);
				if (currentCouple != -1) {
					checkedVertices.Add (current);

					List<int> newEdge = new List<int> ();
					newEdge.Add (current);
					outlines.Add (newEdge);
					FollowEdge (currentCouple, outlines.Count - 1);
					outlines [outlines.Count - 1].Add (current);
				}
			}
		}
	}

	void FollowEdge(int vertexIndex, int edgesIndex){
		outlines [edgesIndex].Add (vertexIndex);
		checkedVertices.Add (vertexIndex);
		int nextVert = GetConnectedEdgeVertex (vertexIndex);

		if (nextVert != -1) {
			FollowEdge (nextVert, edgesIndex);
		}
	}

	List<int> findCoupledEdge(List<int> edge){
		foreach (List<int> edgeC in outlines) {
			if (edgeC == edge)
				continue;
			if (Vector3.Magnitude (vertices [edge [0]] - vertices [edgeC [0]]) == wallHeight) {
				return edgeC;
			}
		}
		return null;
	}


	struct Triangle : IEnumerable{
		public int VertIndex1;
		public int VertIndex2;
		public int VertIndex3;

		private int[] verticesIndex;

		public Triangle(int a, int b, int c){
			VertIndex1=a;
			VertIndex2=b;
			VertIndex3=c;

			verticesIndex = new int[] {a,b,c};
		}

		public bool Contains(int a){
			return VertIndex1 == a || VertIndex2 == a || VertIndex3 == a;
		}

		public IEnumerator GetEnumerator(){
			return new TrigEnumerator (this);
		}

		private class TrigEnumerator : IEnumerator{
			private int pos = -1;
			private Triangle t;

			public TrigEnumerator(Triangle triangle){
				this.t = triangle;
			}

			public bool MoveNext(){
				if (pos < t.verticesIndex.Length - 1) {
					pos++;
					return true;
				} else
					return false;
			}

			public void Reset(){
				pos = -1;
			}

			public object Current{
				get{
					return t.verticesIndex [pos];
				}
			}
		}
	}

	void AssignVertices(Node[] points){
		for (int i = 0; i < points.Length; i++) {
			if (points [i].index == -1) {
				points [i].index = vertices.Count;
				vertices.Add (points [i].position);
			}
		}
	}

	public void GenerateMesh(int[,] _map, float _size){

		Debug.Assert (wallHeight % baseHeight == 0, "wallHeight must be divisible by baseHeight");

		squareGrid = new SquareGrid (_map, _size);
		vertices = new List<Vector3>();
		triangles = new List<int>();

		mapWidth = _map.GetLength (0);
		mapHeight = _map.GetLength (1);
		squareSize = _size;

		outlines.Clear ();
		checkedVertices.Clear ();
		trigDictionary.Clear ();
		createdEdges.Clear ();

		for (int i = 0; i < squareGrid.gridF.GetLength (0); i++) {
			for (int j = 0; j < squareGrid.gridF.GetLength (1); j++) {
				SquareToTrigsFloor (squareGrid.gridF [i, j]);
			}
		}

		for (int i = 0; i < squareGrid.gridC.GetLength (0); i++) {
			for (int j = 0; j < squareGrid.gridC.GetLength (1); j++) {
				SquareToTrigsCeiling (squareGrid.gridC [i, j]);
			}
		}

		Mesh caveMesh = new Mesh ();
		caveMesh.vertices = vertices.ToArray ();
		caveMesh.triangles = triangles.ToArray ();

		Vector2[] uvs = new Vector2[vertices.Count];
		for (int i = 0; i < vertices.Count; i++) {
			float pX = 10*Mathf.InverseLerp (-mapWidth /2* squareSize, mapWidth /2* squareSize, vertices [i].x);
			float pY = 10*Mathf.InverseLerp (-mapHeight /2* squareSize, mapHeight /2* squareSize, vertices [i].z);
			uvs [i] = new Vector2 (pX, pY);
		}
		caveMesh.uv = uvs;
		caveMesh.RecalculateNormals ();

		CreateWallComplexMesh ();

		gameObject.AddComponent<MeshFilter> ();
		MeshFilter caveMeshF = GetComponent<MeshFilter> ();
		MeshCollider col = gameObject.AddComponent<MeshCollider> ();
		col.sharedMesh = caveMesh;
		caveMeshF.mesh = caveMesh;
	}

	/*
	void CreateWallMesh(){

		CalculateMeshOutlines ();

		List<Vector3> wallVertices = new List<Vector3> ();
		List<int> wallTriagles = new List<int> ();

		Mesh wallMesh = new Mesh ();

		int start;

		foreach (List<int> edge in outlines) {
			for (int i = 0; i < edge.Count - 1; i++) {
				Debug.DrawLine(vertices[edge[i]], vertices[edge[i+1]], Color.red, 50000f, false);
				start = wallVertices.Count;
				wallVertices.Add (vertices[edge[i]]); 	//left
				wallVertices.Add (vertices[edge[i+1]]); //right
				wallVertices.Add (vertices[edge[i]] + Vector3.up * wallHeight); 	//top left
				wallVertices.Add (vertices[edge[i+1]] + Vector3.up * wallHeight); 	//top right

				wallTriagles.Add (start + 0);
				wallTriagles.Add (start + 2);
				wallTriagles.Add (start + 3);

				Debug.DrawLine (wallVertices[start+0], wallVertices[start+2], Color.green, 50000f, false);
				Debug.DrawLine (wallVertices[start+2], wallVertices[start+3], Color.green, 50000f, false);
				Debug.DrawLine (wallVertices[start+3], wallVertices[start+0], Color.green, 50000f, false);

				wallTriagles.Add (start + 3);
				wallTriagles.Add (start + 1);
				wallTriagles.Add (start + 0);

			}
		}

		walls = GetComponentInChildren<MeshFilter> ();

		wallMesh.vertices = wallVertices.ToArray ();
		wallMesh.triangles = wallTriagles.ToArray ();
		walls.mesh = wallMesh;
	}
	*/

	void CreateWallComplexMesh(){

		walls = GetComponentInChildren<MeshFilter> ();
		walls.mesh.Clear (true);
		walls.sharedMesh.Clear(true);
		MeshCollider collider = walls.gameObject.AddComponent<MeshCollider> ();
		collider.sharedMesh.Clear (true);


		CalculateMeshOutlines ();

		List<Vector3> wallVertices = new List<Vector3> ();
		List<int> wallTriagles = new List<int> ();
		List<Vector2> uvs = new List<Vector2> ();

		Mesh wallMesh = new Mesh ();

		int start;

		Vector3 base1, base2;
		base1 = new Vector3();
		base2=new Vector3();
		float pX, pY, diff1, diff2;
		List<int> edge = new List<int> ();
		List<int> edgeC = new List<int> ();

		int repeats = (int)(wallHeight / baseHeight);
		repeats--;

		for(int a=0; a< outlines.Count;a++) {
			edge = outlines[a];
			edgeC = findCoupledEdge (edge);
			diff1 = 0;
			diff2 = 0;

			if (edgeC == null)
				continue;
			if (createdEdges.Contains (edge) || createdEdges.Contains (edgeC))
				continue;
			for (int i = 0; i < edge.Count - 1; i++) { 
				base1 = vertices [edge [i]]; 	//left
				base2 = vertices [edge [i + 1]]; //right
				diff1 = diff2;
				diff2 += Vector3.Magnitude(base2-base1)/(10*squareSize);
				//Debug.DrawLine(vertices[edge[i]], vertices[edge[i+1]], Color.red, 50000f, false);
				//Debug.DrawLine(vertices[edgeC[i]], vertices[edgeC[i+1]], Color.blue, 50000f, false);
				for (int k = 1; k < repeats; k++) {
					start = wallVertices.Count;

					wallVertices.Add (base1); 	//left
					pX = diff1;
					pY = Mathf.InverseLerp (vertices [edge [i]].y, vertices [edgeC [i]].y, base1.y);
					uvs.Add( new Vector2 (pX, pY) );

					wallVertices.Add (base2); //right
					pX = diff2;
					pY = Mathf.InverseLerp (vertices [edge [i+1]].y, vertices [edgeC [i+1]].y, base2.y);
					uvs.Add( new Vector2 (pX, pY) );

					base1 += (Vector3.up * baseHeight);
					base2 += (Vector3.up * baseHeight);
					//diff=base1-base2;

					wallVertices.Add (base1); 	//top left
					pX = diff1;
					pY = Mathf.InverseLerp (vertices [edge [i]].y, vertices [edgeC [i]].y, base1.y);
					uvs.Add( new Vector2 (pX, pY) );

					wallVertices.Add (base2); 	//top right
					pX = diff2;
					pY = Mathf.InverseLerp (vertices [edge [i+1]].y, vertices [edgeC [i+1]].y, base2.y);
					uvs.Add( new Vector2 (pX, pY) );

					wallTriagles.Add (start + 0);
					wallTriagles.Add (start + 2);
					wallTriagles.Add (start + 3);

					//Debug.DrawLine (wallVertices[start+0], wallVertices[start+2], Color.green, 50000f, false);
					//Debug.DrawLine (wallVertices[start+2], wallVertices[start+3], Color.green, 50000f, false);
					//Debug.DrawLine (wallVertices[start+3], wallVertices[start+0], Color.green, 50000f, false);

					wallTriagles.Add (start + 3);
					wallTriagles.Add (start + 1);
					wallTriagles.Add (start + 0);
				}

				start = wallVertices.Count;

				wallVertices.Add (base1); //left
				pX = diff1;
				pY = Mathf.InverseLerp (vertices [edge [i]].y, vertices [edgeC [i]].y, base2.y);
				uvs.Add( new Vector2 (pX, pY) );

				wallVertices.Add (base2); //right
				pX = diff2;
				pY = Mathf.InverseLerp (vertices [edge [i+1]].y, vertices [edgeC [i+1]].y, base2.y);
				uvs.Add( new Vector2 (pX, pY) );

				wallVertices.Add ( vertices[edgeC[i]] ); 	//top left, ceiling
				pX = diff1;
				pY = 1;
				uvs.Add( new Vector2 (pX, pY) );

				wallVertices.Add ( vertices[edgeC[i+1]] ); 	//top right, ceiling
				pX = diff2;
				pY = 1;
				uvs.Add( new Vector2 (pX, pY) );

				wallTriagles.Add (start + 0);
				wallTriagles.Add (start + 2);
				wallTriagles.Add (start + 3);

				wallTriagles.Add (start + 3);
				wallTriagles.Add (start + 1);
				wallTriagles.Add (start + 0);

				createdEdges.Add (edge);
				createdEdges.Add (edgeC);
				  
			}
		}



		wallMesh.vertices = wallVertices.ToArray ();
		wallMesh.triangles = wallTriagles.ToArray ();

		wallMesh.uv = uvs.ToArray ();


		wallMesh.RecalculateNormals ();
		walls.mesh = wallMesh;


		collider.sharedMesh = wallMesh;
	}


	public class SquareGrid {
		public Square[,] gridF; //TODO: Wherever gridF is used to make the floor, use gridC to make the ceiling
		public Square[,] gridC;

		public SquareGrid(int[,] map, float squareSize){

			//TODO: Use Parlin Noise generator in two grids to get height randomization for ceiling and floor. Max diviation to be 1.

			int XnodeCount = map.GetLength(0);
			int YnodeCount = map.GetLength(1);

			float mapWidth = XnodeCount*squareSize;
			float mapHeight = YnodeCount*squareSize;

			ControlNode[,] NodesTableF = new ControlNode[XnodeCount,YnodeCount];
			Vector3 posF;
			ControlNode[,] NodesTableC = new ControlNode[XnodeCount,YnodeCount];
			Vector3 posC;

			for(int i = 0; i<XnodeCount; i++){
				for(int j=0;j<YnodeCount;j++){
					posF = new Vector3(-mapWidth/2 + (i+0.5f)*squareSize, 0, -mapHeight/2 + (j+0.5f)*squareSize);
					NodesTableF[i,j] = new ControlNode(posF, map[i,j]==1, squareSize);
					posC = new Vector3(-mapWidth/2 + (i+0.5f)*squareSize, wallHeight, -mapHeight/2 + (j+0.5f)*squareSize);
					NodesTableC[i,j] = new ControlNode(posC, map[i,j]==1, squareSize);
				}
			}

			gridF = new Square[XnodeCount-1, YnodeCount-1];
			gridC = new Square[XnodeCount-1, YnodeCount-1];

			for(int i = 0; i<XnodeCount-1; i++){
				for(int j=0;j<YnodeCount-1;j++){
					gridF[i,j] = new Square(NodesTableF[i, j], NodesTableF[i+1, j], NodesTableF[i, j+1], NodesTableF[i+1, j+1]);
					gridC[i,j] = new Square(NodesTableC[i, j], NodesTableC[i+1, j], NodesTableC[i, j+1], NodesTableC[i+1, j+1]);
				}
			}


		}
	}

	public class Square{
		public ControlNode topLeft, bottomLeft, topRight, bottomRight;
		public Node top, bottom, left, right;
		public int configuration;

		public Square(ControlNode _bL, ControlNode _bR, ControlNode _tL, ControlNode _tR){
			topLeft = _tL;
			topRight = _tR;
			bottomLeft = _bL;
			bottomRight = _bR;

			top = topLeft.right;
			bottom = bottomLeft.right;
			left = bottomLeft.above;
			right = bottomRight.above;

			configuration = 0;
			if(topLeft.active) configuration+=8;
			if(topRight.active) configuration+=4;
			if(bottomRight.active) configuration+=2;
			if(bottomLeft.active) configuration+=1;
		}
	}

	public class Node {
		public Vector3 position;
		public int index = -1;

		public Node(Vector3 _pos){
			position = _pos;
		}

	}

	public class ControlNode : Node{
		public bool active;
		public Node above, right;

		public ControlNode(Vector3 _pos, bool _act, float sqSize) : base(_pos){
			active = _act;
			above = new Node(position + Vector3.forward*sqSize/2f);
			right = new Node(position + Vector3.right*sqSize/2f);
		}
	}

}
