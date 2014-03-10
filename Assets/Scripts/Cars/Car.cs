using UnityEngine;
using System.Collections;
using System;

public class Neuron {
	float[] weights;

	public Neuron(int size) {
		weights = new float[size+1];
		for (int i = 0; i < size; i++) {
			weights[i] = UnityEngine.Random.value;
		}
		float t = 0.5f;
		weights[size] = t;
	}
	public Neuron(float[] weights) {
		this.weights = weights;
	}

	public float Output(float[] input) {
		float activation = 0;
		for (int i = 0; i < input.Length; i++) {
			activation += input[i] * weights[i];
		}

		return activation; // Sigmoidal(activation);
	}

	float Sigmoidal(float activation) {
		// 1 / (1 + e^(-a/p)) where p = 1, a is the activation.
		float p = 1;
		return 1 / (1 + Mathf.Exp(-activation/p));
	}
}

public class NeuronLayer {
	private Neuron[] neurons;
	private float[] outputs;

	public NeuronLayer(int nrInputs, int nrOutputs, float[] weights) {
		neurons = new Neuron[nrOutputs];
		outputs = new float[nrOutputs];

		int counter = 0;
		for (int i = 0; i < neurons.Length; i++) {
			int neuronSize = nrInputs + 1;
			float[] dest = new float[neuronSize];
			Array.Copy(weights, counter, dest, 0, neuronSize);
			counter += neuronSize;
			neurons[i] = new Neuron(dest);
		}
	}

	public float[] Evaluate(float[] inputs) {
		for (int i = 0; i < neurons.Length; i++) {
			outputs[i] = neurons[i].Output(inputs);
		}
		return outputs;
	}
}

public class NeuronNet {
	private NeuronLayer[] layers;

	public NeuronNet(int[] layerSizes, float[] initialWeights) {
		// TODO: Clear distinction between the layers.
		DebugAux.Assert(layerSizes.Length >= 2, "Must have at least 2 layers");
		layers = new NeuronLayer[layerSizes.Length-1];


		int counter = 0;

		for (int i = 0; i < layers.Length; i++) {
			int size = (layerSizes[i]+1) * layerSizes[i+1];
			float[] dest = new float[size];
			Array.Copy(initialWeights, counter, dest, 0, size);
			counter += size;
			layers[i] = new NeuronLayer(layerSizes[i], layerSizes[i+1], dest);
		}
	}

	public float[] Evaluate(float[] inputs) {
		for (int i = 0; i < layers.Length; i++) {
			inputs = layers[i].Evaluate(inputs);
		}
		return inputs;
	}
}

public class Car : MonoBehaviour {
	public float speed = 1;
	private Vector2[] directions = { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1) };
	private float[] sensors;
	private float totalDistance;
	private NeuronNet neuronNet;
	private bool isDone;
	private bool initialized = false;
	public float Duration;

	public float TotalDistance { get { return totalDistance; } }

	public void Init(GenomeFloatString genome) {
		neuronNet = new NeuronNet(new int[]{5, 8, 3}, genome.GetArray());
		sensors = new float[5+1];
		for (int i = 0; i < directions.Length; i++) {
			directions[i].Normalize();
		}
		sensors[5] = 0;
		totalDistance = 0;
		Duration = 0;
		isDone = false;
		initialized = true;
	}

	void Update () {
		if (!initialized)
			return;

		Vector3 pos = gameObject.transform.position;
		Quaternion q = gameObject.transform.rotation;

		for (int i = 0; i < directions.Length; i++) {
			Vector2 dir = q * directions[i];
			RaycastHit2D hit = Physics2D.Raycast(pos, dir, 0.5f);

			dir = dir*0.5f;
			bool anyHit = hit.collider != null;
			float fraction = anyHit ? hit.fraction : 1;
			sensors[i] = 1-fraction;
			if (anyHit) {
				Debug.DrawRay(pos, dir, Color.black);
				Debug.DrawRay(pos, dir * fraction, Color.red);
			} else {
				Debug.DrawRay(pos, dir, Color.green);
			}
		}

		float[] outputs = neuronNet.Evaluate(sensors);

		float leftForce = outputs[0];
		float rightForce = outputs[1];
		float speed = outputs[2];
		
		// Convert the outputs to a proportion of how much to turn.
		float leftTheta = leftForce;
		float rightTheta = rightForce;

		// Debug.Log(leftForce + " " + rightForce);
		Vector3 direction = Vector3.Normalize(q*directions[2]);
		float angle = q.eulerAngles.z; // Vector3.Angle(new Vector3(-1, 0, 0), direction);
		// Debug.Log(angle + " " + (angle+(rightTheta - leftTheta) * Time.deltaTime));
		angle += (leftTheta - rightTheta) * Time.deltaTime * 100;
		Vector3 heading = new Vector3(0, 0, 0);
		// float angle = 190;
		heading.x = Mathf.Cos(Mathf.Deg2Rad * angle);
		heading.y = Mathf.Sin(Mathf.Deg2Rad * angle);
		// Debug.DrawRay(pos, heading);
		heading *= speed * Time.deltaTime;
		totalDistance += Vector3.Magnitude(heading);
		transform.position += heading;
		Duration += Time.deltaTime;
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (totalDistance > 25)
			OutsideTrack();
	}

	public void OutsideTrack() {
		isDone = true;
		initialized = false;
		// Debug.Log("Done " + (TotalDistance - Duration));
	}

	public bool IsDone() {
		return isDone;
	}
}
