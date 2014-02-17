using UnityEngine;
using System.Collections;

public class AllOnesPhenome : BasePhenome {

	public AllOnesPhenome() {
		Genome = GenomeFactory.CreateGenome(GenomeType.RawBinaryString64, 64);
	}
}
