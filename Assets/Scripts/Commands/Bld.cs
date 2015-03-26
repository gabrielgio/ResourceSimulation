using UnityEngine;
using System.Collections;
using System;

public class Bld : Singleton<Bld> {
	public string CmdBuild (params string[] parans)
	{
		if (parans [0] != "build")
			return Msg.Instance.ProcessFeedback("wrong","CmdBuild");
		
		if (parans.Length == 1) 
			return Msg.Instance.ProcessFeedback("param_missing");
		
		int times = 1;
		int builts = 0;
		
		if (parans.Length >= 3)
			int.TryParse (parans [2], out times);
		
		if (parans [1] == "worker") {
			WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
			BuildStation buildStation = GameObject.Find ("Main Camera").GetComponent<BuildStation> ();
			while (times-- != 0) {
				if (workStation.Wood > buildStation.WorkerPrice) {
					workStation.Wood -= buildStation.WorkerPrice;
					buildStation.BuildWorker ();
					builts++;
				}
				else{
					break;
				}
			}
			
			if(builts == 0)
				return Msg.Instance.ProcessFeedback ("not_enough_resource","Wood","Worker");
			else
				return Msg.Instance.ProcessFeedback("worker_build", builts.ToString());
		}
		
		string upParams =  parans [1].FirstCharToUpper();
		
		if (upParams == WarriorType.Legend.ToString () || upParams == WarriorType.Warrior.ToString () || upParams == WarriorType.Minion.ToString ()) {
			
			WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
			BuildStation buildStation = GameObject.Find ("Main Camera").GetComponent<BuildStation> ();
			while (times-- >= 0) {
				if (workStation.Wood > buildStation.WorkerPrice) {
					workStation.Wood -= buildStation.WorkerPrice;
					buildStation.BuildWarrior((WarriorType)Enum.Parse(typeof(WarriorType), upParams));
					builts++;
				}
				else{
					break;
				}
			}
			
			if(builts == 0)
				return Msg.Instance.ProcessFeedback ("not_enough_resource","Wood","Warrior");
			else
				return Msg.Instance.ProcessFeedback("warrior_build", builts.ToString());
		}
		
		return Msg.Instance.ProcessFeedback("invalid_param");
	}
}
