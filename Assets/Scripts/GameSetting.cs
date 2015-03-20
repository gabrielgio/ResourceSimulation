using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameSetting{
	
	private static GameSetting instance;

	private JSONNode jsonNode;

	public const string TITLE = "TITLE - RESOURCE SIMULATION";


	public double TimeToBuildWorker	{ get { return jsonNode ["build"] ["worker_time"].AsDouble; } }

	public double WorkerRate { get { return jsonNode ["work"] ["worker_rate"].AsDouble; } }

	static public GameSetting Instance 
	{
		get{
			if (instance == null)
				instance = new GameSetting();

			return instance;
		}
	}


	private GameSetting()
	{
		TextAsset textFile = (TextAsset)Resources.Load("settings", typeof(TextAsset));
		jsonNode = JSON.Parse (textFile.text);

		Debug.Log (jsonNode ["work"] ["worker_rate"].AsDouble);
		Debug.Log (jsonNode ["build"] ["worker_time"].AsDouble);
	}
}
