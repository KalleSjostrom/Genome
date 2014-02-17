using UnityEngine;
using System.Collections;

public class CardPhenome : BasePhenome {

	public CardPhenome() {
		Genome = GenomeFactory.CreateGenome(GenomeType.RawBinaryString32, 10);
	}
}
