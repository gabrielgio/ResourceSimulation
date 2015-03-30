using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkStation : MonoBehaviour {

	private List<Worker> _workers;

	public double Wood = 0;
	public double Rock = 0;
	public double Food = 0;

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

			double clacAm = GameSetting.Instance.WORKER_RATE[item.Type] *Time.deltaTime;

			switch (item.Type) {
			case ResourceSource.Wood:
				double amount = World.ChopTree(clacAm);
				if(amount >=0)
					Wood += amount;
				break;
			case ResourceSource.Rock:
				amount = World.MineRock(clacAm);
				if(amount >= 0)
					Rock += amount;
				break;

			case ResourceSource.Food:
				amount = World.GrabFood(clacAm);
				if(amount >= 0)
					Food += amount;
				break;
			}
		}
	}

	public bool BuildWorker(int amount)
	{
		var tuple = GameSetting.Instance.COST_BUILD_WORKER;

		if (tuple.Value1 < Wood &&
			tuple.Value2 < Rock &&
			tuple.Value3 < Food) {

			Wood -= tuple.Value1;
			Rock -= tuple.Value2;
			Food -= tuple.Value3;

			AddWorker(ResourceSource.Wood);
			return  true;
		}

		return false;
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
