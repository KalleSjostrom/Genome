using UnityEngine;
using System.Collections;

public class ComputeShaderRunner : MonoBehaviour {

	public ComputeShader shader;
	private RenderTexture testing;

	// Use this for initialization
	void Start () {
		testing = new RenderTexture(128, 128, 8);
		testing.enableRandomWrite = true;
		testing.Create();

		// Fill genome with random..
		int kernel = shader.FindKernel("FillRandom");
		shader.SetTexture(kernel, "Result", testing);
		shader.Dispatch(kernel, 32, 32, 1);


	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, 100, 100), testing);
	}
}
