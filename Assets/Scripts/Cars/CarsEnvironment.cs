using UnityEngine;
using System.Collections;

public class CarsEnvironment : AEnvironment {

	public GameObject CarPrefab;

	bool simulating = false;
	Car[] cars;
	Population population;
	GeneticAlgorithm.NextStepDelegate doneCallback;

	void Start() {
	}

	public override void FitnessFunction(Population population, GeneticAlgorithm.NextStepDelegate callback) {
		this.population = population;
		InitiateNewSimulation();
		simulating = true;
		doneCallback = callback;
	}

 	void InitiateNewSimulation() {
		if (cars != null) {
			for (int i = 0; i < cars.Length; i++) {
				Destroy(cars[i].gameObject);
			}
		}
		cars = new Car[population.Size];
		for (int i = 0; i < population.Size; i++) {
			PhenomeDescription pd = population[i];
			cars[i] = Helper.Instansiate<Car>(CarPrefab);
			cars[i].Init(pd.Genome as GenomeFloatString);
		}
	}

	void Update() {
		if (!simulating)
			return;

		// Update cars...
		bool everyoneDone = true;
		for (int i = 0; i < cars.Length; i++) {
			everyoneDone = everyoneDone && cars[i].IsDone();
			if (!everyoneDone)
				break;
		}

		if (everyoneDone) {
			simulating = false;
			for (int i = 0; i < population.Size; i++) {
				PhenomeDescription pd = population[i];
				pd.Fitness = CalculateFitness(cars[i]);
			}
			
			CalculateMinMaxTotal(population);
			Debug.Log(population.MaxFitness + " " + population.MinFitness + " " + population.TotalFitness);
			doneCallback();
		}
	}

	public float CalculateFitness(Car car) {
		return car.TotalDistance;
	}
}
