using UnityEngine;
using System.Collections;

public class Load : Singleton<Load>, ICmd {

	public string[] Functions{
		get{
			return new string[]{"load", "ld"};
		}
	}

	public string Cmd(params string[] args)
	{
		if (args.Length <= 1)
			return Msg.Instance.ProcessFeedback ("param_missing");

		int index = 0;

		if(!int.TryParse(args[1], out index))
			return Msg.Instance.ProcessFeedback ("invalid_param");

		WorldStation worldStation = GameObject.Find ("Main Camera").GetComponent<WorldStation> ();
		worldStation.LoadWorld (index);

		return Msg.Instance.ProcessFeedback ("world_loaded");
	}
}
