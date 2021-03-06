﻿using UnityEngine;
using System.Collections;

public class PhenomeDescription : System.IComparable<PhenomeDescription> {

	private BaseGenome genome;
	private float fitness;
	
	public BaseGenome Genome { get { return genome; }  set { genome = value; } }
	public float Fitness { get { return fitness; } set { fitness = value; } }

	private BasePhenome phenome;
	
	public PhenomeDescription(BaseGenome genome) {
		this.genome = genome;
		// phenome = 
	}
	
	public int CompareTo(PhenomeDescription p) {
		return fitness.CompareTo(p.fitness) * -1;
	}
}