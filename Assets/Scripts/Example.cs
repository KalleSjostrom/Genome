using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {
	/*public Material mat;
	private Vector3 startVertex;
	private Vector3 mousePos;
	void Update() {
		mousePos = Input.mousePosition;
		if (Input.GetKeyDown(KeyCode.Space))
			startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
		
	}
	void OnPostRender() {
		if (!mat) {
			Debug.LogError("Please Assign a material on the inspector");
			return;
		}
		GL.PushMatrix();
		mat.SetPass(0);
		// GL.LoadOrtho();
		GL.LoadIdentity();
		// GL.LoadProjectionMatrix(this.camera.projectionMatrix);
		// GL.MultMatrix(this.camera.transform.localToWorldMatrix);
		GL.LoadProjectionMatrix(GL.modelview);

		GL.Begin(GL.LINES);
		GL.Color(Color.red);
		GL

		GL.Vertex(new Vector3(0, 0, 0));
		GL.Vertex(new Vector3(1, 1, 0));

		GL.End();
		GL.PopMatrix();
	}
	public Example() {
		startVertex = new Vector3(0, 0, 0);
	}*/
}