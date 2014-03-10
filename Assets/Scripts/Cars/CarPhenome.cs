using UnityEngine;
using System.Collections;

public class CarPhenome : BasePhenome {

	public CarPhenome() {
		Genome = GenomeFactory.CreateGenome(GenomeType.FloatString, (5*8+8) + (8*3+3));
	}
}
