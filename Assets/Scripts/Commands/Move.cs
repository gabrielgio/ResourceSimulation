using UnityEngine;
using System.Collections;
using System;
using System.Linq;
public class Move : Singleton<Move> {
	public string CmdMove(params string[] parans)
	{
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
		
		if (!(parans [0] == "wood" || parans [0] == "iron" || parans [0] == "rock")) 
			return ProcessFeedback("wrong","CmdMove");
		
		if (!(parans [1] == "wood" || parans [1] == "iron" || parans [1] == "rock"))
			return ProcessFeedback ("invalid_resource", parans [1]);
		
		int times = 1;
		int movedWorker = 0;
		
		if (parans.Length >= 3)
			int.TryParse (parans [2], out times);
		
		
		ResourceSource preRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [0].FirstCharToUpper());
		ResourceSource posRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [1].FirstCharToUpper());
		
		while (times-- != 0) {
			Worker worker = workStation.Workers.FirstOrDefault (x => x.Type == preRes);
			
			if (worker == null)
				break;
			else {
				worker.Type = posRes;
				movedWorker++;
			}
		}

		if (movedWorker == 0)
			return Msg.Instance.ProcessFeedback ("no_worker", parans [0]);
		else
			return Msg.Instance.ProcessFeedback ("moved_worker", movedWorker.ToString(), parans [0], parans [1]);
	}

	private string ProcessFeedback(string code, params string[] msgs)
	{
		return Msg.Instance.ProcessFeedback (code, msgs);
	}
}
