using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Add : ICmd {

	public string[] Functions {
		get {
			return new string[]{"add"};
		}
	}

	public string Cmd (params string[] args)
	{
		if(args.Length > 3)
			return Msg.Instance.ProcessFeedback("param_missing");

		if(args[1] != "e")
			return Msg.Instance.ProcessFeedback("invalid_param", args[1]);

		if(args[2].FirstCharToUpper() != WarriorType.Legend.ToString() && 
		   args[2].FirstCharToUpper() != WarriorType.Warrior.ToString() && 
		   args[2].FirstCharToUpper() != WarriorType.Minion.ToString())
			return Msg.Instance.ProcessFeedback("invalid_param", args[2]);

		BattleStation battleStation = GameObject.Find ("Main Camera").GetComponent<BattleStation> ();

		battleStation.AddEnemy ((WarriorType)Enum.Parse (typeof(WarriorType), args [2].FirstCharToUpper()));

		return Msg.Instance.ProcessFeedback ("enemy_build", args [2].FirstCharToUpper ());
	}

}
