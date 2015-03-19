using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class WorkerUpdater : MonoBehaviour {


	public Info DiseredInfo = Info.Wood;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Text text = GetComponent<Text> ();
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
		BuildStation buildStation = GameObject.Find ("Main Camera").GetComponent<BuildStation> ();

		switch (DiseredInfo) {
		case Info.Wood:
			text.text = workStation.Wood.ToString();
			return;
		case Info.Rock:
			text.text = workStation.Rock.ToString();
			return;
		case Info.Iron:
			text.text = workStation.Iron.ToString();
			return;
		case Info.AvaliableWood:
			text.text = workStation.AvaliableWood.ToString();
			return;
		case Info.AvaliableRock:
			text.text = workStation.AvaliableRock.ToString();
			return;
		case Info.AvaliableIron:
			text.text = workStation.AvaliableIron.ToString();
			return;
		case Info.Workers:
			text.text = workStation.Workers.ToString();
			return;
		case Info.WorkersOnWood:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Wood).ToString();
			return;
		case Info.WorkersOnRock:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Rock).ToString();
			return;
		case Info.WorkersOnIron:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Iron).ToString();
			return;
		case Info.PercentageWorker:
			if(workStation.Workers.Count(x => !x.IsBuilt) == 0)
				text.text = "0%";
			else
				text.text = string.Format("{0:#}%", workStation.Workers.First(x => !x.IsBuilt).Percentage);	
			return;
		case Info.WorkerOnBuild:
			text.text = buildStation.Workers.Count(x => !x.IsBuilt).ToString();
			return;
		}
	
	}
}

public enum Info
{
	Wood,
	Rock,
	Iron,
	AvaliableWood,
	AvaliableRock,
	AvaliableIron,
	Workers,
	WorkersOnWood,
	WorkersOnRock,
	WorkersOnIron,
	PercentageWorker,
	WorkerOnBuild
}
