using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorkerUpdater : MonoBehaviour {


	public Info DiseredInfo = Info.Wood;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Text text = GetComponent<Text> ();
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();

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
	Workers
}
