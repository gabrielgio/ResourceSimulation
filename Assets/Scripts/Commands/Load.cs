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
		if (args.Length <= 2)
			return Msg.Instance.ProcessFeedback ("param_missing");

		int index = 0;

		if (args [1] == "w") {

			if (!int.TryParse (args [2], out index))
				return Msg.Instance.ProcessFeedback ("invalid_param");

			WorldStation worldStation = GameObject.Find ("Main Camera").GetComponent<WorldStation> ();
			worldStation.LoadWorld (index);

			return Msg.Instance.ProcessFeedback ("world_loaded");
		} else if (args [1] == "e") {
			EnemyStation enemyStation = GameObject.Find ("Main Camera").GetComponent<EnemyStation> ();

			if (!int.TryParse (args [2], out index))
				enemyStation.LoadBattle(index);
			else
				enemyStation.LoadBattle(args[2]);

			return Msg.Instance.ProcessFeedback ("enemy_loaded");
		}

		return Msg.Instance.ProcessFeedback ("invalid_param");
	}
}
