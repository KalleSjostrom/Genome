using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllOnesStatistics : Statistics {

	private List<float> avg = new List<float>();

	public override void OnStepBegin() {
		base.OnStepBegin();
	}
	public override void OnStepEnd() {
		base.OnStepEnd();
		avg.Add(Population.MaxFitness);
	}

	public override void OnDone() {
		for (int i = 0; i < avg.Count; i++) {
			float mean = avg[i];
			Debug.Log(i + " " + mean);
		}

		List<PhenomeDescription> list = new List<PhenomeDescription>();
		for (int i = 0; i < Population.Size; i++)
			list.Add(Population[i]);
		
		list.Sort();
		for (int i = 0; i < Mathf.Min(5, Population.Size); i++) {
			Debug.Log("Fitness " + list[i].Fitness);
		}
		Debug.Log("Total Duration: " + Duration);
	}
}
