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

	public StoryStation Story;

	void Start () {
		_workers = new List<Worker> ();

		AddWorker (ResourceSource.Wood);

		if (World == null)
			World = GameObject.Find ("Main Camera").GetComponent<WorldStation> ();

		if(Story == null)
			Story = GameObject.Find ("Main Camera").GetComponent<StoryStation> ();
	}

	void Update () {

		if (Story.Paused)
			return;

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

	public void OnBuiltWorker()
	{
		Workers.Add (new Worker ());
	}

	public void AddWorker(ResourceSource resource)
	{
		_workers.Add(new Worker(resource));
	}
}
