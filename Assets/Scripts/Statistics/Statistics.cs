using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statistics : MonoBehaviour {

	public Population Population { get; set; }

	private float start;
	protected float Duration = 0;

	public virtual void OnStepBegin() {
		start = Time.realtimeSinceStartup;
	}
	public virtual void OnStepEnd() {
		Duration += Time.realtimeSinceStartup - start;
	}

	public virtual void OnDone() {}
}
