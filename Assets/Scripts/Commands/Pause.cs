using UnityEngine;
using System.Collections;

public class Pause : ICmd {

	public string[] Functions {
		get {
			return new string[]{"pause", "unpause"};
		}
	}

	public string Cmd (params string[] args)
	{
		StoryStation storyStation = GameObject.Find ("Main Camera").GetComponent<StoryStation> ();

		storyStation.Paused = args [0] == "pause";

		return "";
	}

}
