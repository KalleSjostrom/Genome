using UnityEngine;
using System.Collections;

public abstract class BasePhenome : MonoBehaviour {

	private PhenomeDescription description;
	public PhenomeDescription Description { get { return description; } set { description = value; } }

	private BaseGenome genome;
	public BaseGenome Genome { get { return genome; } set { genome = value; } }
}