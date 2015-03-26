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
			break;
		case Info.Rock:
			text.text = workStation.Rock.ToString();
			break;
		case Info.Iron:
			text.text = workStation.Iron.ToString();
			break;
		case Info.AvaliableWood:
			text.text = workStation.AvaliableWood.ToString();
			break;
		case Info.AvaliableRock:
			text.text = workStation.AvaliableRock.ToString();
			break;
		case Info.AvaliableIron:
			text.text = workStation.AvaliableIron.ToString();
			break;
		case Info.Workers:
			text.text = workStation.Workers.ToString();
			break;
		case Info.WorkersOnWood:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Wood).ToString();
			break;
		case Info.WorkersOnRock:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Rock).ToString();
			break;
		case Info.WorkersOnIron:
			text.text = workStation.Workers.Count(x => x.Type == ResourceSource.Iron).ToString();
			break;
		case Info.WorkerOnBuild:
			text.text = buildStation.WorkerOnBuild.ToString();
			break;
		case Info.WarriorOnBuild:
			text.text = buildStation.Warriors.Count().ToString();
			break;
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
	WorkerOnBuild,
	WarriorOnBuild
}
