using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkStation : MonoBehaviour {

	private List<Worker> workers;

	public double Wood = 0;
	public double Rock = 0;
	public double Iron = 0;

	public double AvaliableWood = 1000;
	public double AvaliableRock = 100;
	public double AvaliableIron = 10;

	public int Workers{ get { return workers.Count; } }
	
	// Use this for initialization
	void Start () {
		workers = new List<Worker> ();
		AddWorker (ResourceSource.Wood);
		AddWorker (ResourceSource.Wood);
		AddWorker (ResourceSource.Wood);
		AddWorker (ResourceSource.Wood);

		
		AddWorker (ResourceSource.Iron);
		AddWorker (ResourceSource.Iron);
		AddWorker (ResourceSource.Iron);
		AddWorker (ResourceSource.Iron);
		AddWorker (ResourceSource.Iron);


		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
		AddWorker (ResourceSource.Rock);
	}
	
	// Update is called once per frame
	void Update () {

		foreach (var item in workers) {
			switch (item.WorkingOn) {
			case ResourceSource.Wood:
				ChopTree(item.WorkRate*Time.deltaTime);
				break;
			case ResourceSource.Rock:
				MineRock(item.WorkRate*Time.deltaTime/10);
				break;

			case ResourceSource.Iron:
				MineIron(item.WorkRate*Time.deltaTime/100);
				break;
			}
		}
	}

	public void ChopTree(double amount)
	{
		Wood += amount;
		AvaliableWood -= amount;
	}

	public void MineRock(double amount)
	{
		Rock += amount;
		AvaliableRock -= amount;
	}

	public void MineIron(double amount) 
	{
		Iron += amount;
		AvaliableIron -= amount;
	}

	public void AddWorker(ResourceSource resource)
	{
		workers.Add(new Worker(){ WorkingOn = resource});
	}
}
