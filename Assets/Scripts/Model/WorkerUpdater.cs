using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class WorkerUpdater : MonoBehaviour {


	public Info DiseredInfo = Info.Wood;

	private WorkStation workStation;
	private BuildStation buildStation;
	private BattleStation battleStation;


	void Start () {
		workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
		buildStation = GameObject.Find ("Main Camera").GetComponent<BuildStation> ();
		battleStation = GameObject.Find ("Main Camera").GetComponent<BattleStation> ();
	}
	
	// Update is called once per frame
	void Update () {

		Text text = GetComponent<Text> ();

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

		case Info.Legend:
			text.text = battleStation.Warriors.Count(x => x.Type ==  WarriorType.Legend).ToString();
			break;

		case Info.Warrior:
			text.text = battleStation.Warriors.Count(x => x.Type ==  WarriorType.Warrior).ToString();
			break;

		case Info.Minion:
			text.text = battleStation.Warriors.Count(x => x.Type ==  WarriorType.Minion).ToString();
			break;

		case Info.Warriors:
			text.text = battleStation.Warriors.Count().ToString();
			break;
		}
	}
}

public enum Info
{
	Wood,
	Rock,
	Iron,
	Workers,
	WorkersOnWood,
	WorkersOnRock,
	WorkersOnIron,
	PercentageWorker,
	WorkerOnBuild,
	WarriorOnBuild,
	Legend,
	Warrior,
	Minion,
	Warriors
}
