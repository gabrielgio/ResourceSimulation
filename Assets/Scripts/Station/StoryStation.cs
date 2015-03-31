using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using SimpleJSON;
using System.Collections.Generic;
using System;
using System.Linq;

public class StoryStation : MonoBehaviour {

	private string _command;

	private JSONNode _jsonNode;

	private Queue<Game> games;

	public bool Paused =  false;

	public Prompt Input;

	void Start () {

		games = new Queue<Game> ();

		TextAsset textFile = (TextAsset)Resources.Load("game", typeof(TextAsset));
		_jsonNode = JSON.Parse (textFile.text);
		
		ParseJson ();

		if (Input != null)
			Input.StringChanged.AddListener (StringChanged);
	}
	
	// Update is called once per frame
	void Update () {

		if (games.Count () == 0)
			return;

		Game game = games.FirstOrDefault ();

		game.Time -= Time.deltaTime;

		if (game.Time <= 0) {
			games.Dequeue();
			Input.EditEnd(game.Command);
		}
	}

	public void Lose()
	{
		Input.EditEnd("pause");
		Input.EditEnd("msg:i:Unfortunately you lost this match, but thanks to play the tutorial. The story mode is comming soon.");
	}

	public void Won()
	{
		Input.EditEnd("pause");
		Input.EditEnd("msg:i:Congratulation you won this match, thanks to play the tutorial. The story mode is comming soon.");
	}

	private void StringChanged(string arg)
	{
		if (arg == _command) {
			Input.EditEnd (arg);
		}
	}

	private void ParseJson()
	{
		foreach (JSONNode item in _jsonNode["game"].AsArray) {
			games.Enqueue(new Game()
			{
				Command = item["command"],
				Time = item["time"].AsDouble
			});
		}
	}
}
