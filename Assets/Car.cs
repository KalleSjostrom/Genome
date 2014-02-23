using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public float speed = 1;
	private Vector2[] directions = { new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1) };
	private float[] sensorOutputs;
	private float[] weights;

	void Start () {
		sensorOutputs = new float[5];
		weights = new float[5];
		for (int i = 0; i < directions.Length; i++) {
			directions[i].Normalize();
		}
		for (int i = 0; i < weights.Length; i++) {
			weights[i] = Random.value;
		}
	}

	void Update () {
		Vector3 pos = gameObject.transform.position;
		//pos = pos + Vector3.left * speed * Time.deltaTime;
		gameObject.transform.position = pos;

		Quaternion q = gameObject.transform.rotation;

		for (int i = 0; i < directions.Length; i++) {
			Vector2 dir = q * directions[i];
			RaycastHit2D hit = Physics2D.Raycast(pos, dir);

			bool anyHit = hit.collider != null;
			float fraction = anyHit ? hit.fraction : 1;
			sensorOutputs[i] = fraction;
			if (anyHit) {
				Debug.DrawRay(pos, dir, Color.black);
				Debug.DrawRay(pos, dir * fraction, Color.red);
			} else {
				Debug.DrawRay(pos, dir, Color.green);
			}
		}

		UpdateNN(sensorOutputs);
	}

	void UpdateNN(float[] input) {
		float activation = 0;
		for (int i = 0; i < input.Length; i++) {
			activation += input[i] * weights[i];
		}
	}
}
