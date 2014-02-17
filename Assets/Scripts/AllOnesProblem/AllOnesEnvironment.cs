using UnityEngine;
using System.Collections;

public class AllOnesEnvironment : AEnvironment {

	public override void FitnessFunction(Population population, GeneticAlgorithm.NextStepDelegate callback) {
		for (int i = 0; i < population.Size; i++) {
			PhenomeDescription pd = population[i];
			pd.Fitness = CalculateFitness(pd.Genome as BaseGenomeBinary);
		}

		CalculateMinMaxTotal(population);
		callback();
	}

	public float CalculateFitness(BaseGenomeBinary genome) {
		int nrOnes = 0;
		for (int i = 0; i < genome.Length; i++)
			nrOnes += genome.IsSet(i) ? 1 : 0;
		return nrOnes;
	}
}
