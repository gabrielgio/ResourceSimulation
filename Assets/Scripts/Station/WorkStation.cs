using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkStation : MonoBehaviour {

	private List<Worker> _workers;

	public double Wood = 0;
	public double Rock = 0;
	public double Iron = 0;

	public List<Worker> Workers{ get { return _workers; } }

	public WorldStation World;
	
	// Use this for initialization
	void Start () {
		_workers = new List<Worker> ();

		AddWorker (ResourceSource.Wood);

		if (World == null)
			World = GameObject.Find ("Main Camera").GetComponent<WorldStation> ();
	}

	void Update () {

		foreach (var item in _workers) {
			switch (item.Type) {
			case ResourceSource.Wood:
				double amount = World.ChopTree(item.WorkRate*Time.deltaTime);
				if(amount >=0)
					Wood += amount;
				break;
			case ResourceSource.Rock:
				amount = World.MineRock(item.WorkRate*Time.deltaTime/10);
				if(amount >= 0)
					Rock += amount;
				break;

			case ResourceSource.Iron:
				amount = World.GrabFood(item.WorkRate*Time.deltaTime/100);
				if(amount >= 0)
					Iron += amount;
				break;
			}
		}
	}

	public void OnBuiltWorker()
	{
		Workers.Add (new Worker ());
	}

	public void AddWorker(ResourceSource resource)
	{
		_workers.Add(new Worker(resource));
	}
}
