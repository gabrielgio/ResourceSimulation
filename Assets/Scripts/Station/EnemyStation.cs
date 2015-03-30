using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using System;

public class EnemyStation : MonoBehaviour {

	private JSONNode _jsonNode;

	private Queue<CommandTime> commands;

	private List<Battle> battles;

	public Prompt Output;

	// Use this for initialization
	void Start () {
		battles = new List<Battle> ();
		commands = new Queue<CommandTime> ();

		TextAsset textFile = (TextAsset)Resources.Load("battles", typeof(TextAsset));
		_jsonNode = JSON.Parse (textFile.text);

		ParseJson ();

		LoadBattle ("enemy 1");
	}

	void Update () {

		if (commands.Count () == 0)
			return;

		CommandTime command = commands.FirstOrDefault ();
		command.Time -= Time.deltaTime;

		if (command.Time <= 0) {
			commands.Dequeue ();
			Output.EditEnd(string.Format("add:e:{0}",command.Type.ToString().ToLower()));
		}

	}

	private void LoadBattle(string name)
	{
		Battle battle = battles.FirstOrDefault (x => x.Name == name);

		if (battle == null)
			return;

		commands.Clear ();

		battle.Commands.ForEach (x => commands.Enqueue (x.Clone()));
	}

	private void ParseJson()
	{
		foreach (JSONNode item in _jsonNode["battles"].AsArray) {
			Battle battle  = new Battle()
			{
				Name = item["name"],
				Health = item["health"].AsDouble
			};

			foreach (JSONNode command in item["battle"].AsArray) {
				battle.Commands.Add(new CommandTime(){
					Type = (WarriorType)Enum.Parse(typeof(WarriorType), command["name"].FirstCharToUpper()),
					Time = command["time"].AsDouble
				});
			}
			battles.Add(battle);
		}
	}
}
