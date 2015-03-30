using UnityEngine;
using System.Collections;
using System;
using System.Linq;
public class Move : Singleton<Move>, ICmd {


	public string[] Functions{
		get{
			return new string[]{"wood", "food", "rock"};
		}
	}

	public string Cmd(params string[] args)
	{
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
		
		if (!(args [0] == "wood" || args [0] == "food" || args [0] == "rock")) 
			return ProcessFeedback("wrong","CmdMove");
		
		if (!(args [1] == "wood" || args [1] == "food" || args [1] == "rock"))
			return ProcessFeedback ("invalid_resource", args [1]);
		
		int times = 1;
		int movedWorker = 0;
		
		if (args.Length >= 3)
			int.TryParse (args [2], out times);
		
		
		ResourceSource preRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), args [0].FirstCharToUpper());
		ResourceSource posRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), args [1].FirstCharToUpper());
		
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
			return Msg.Instance.ProcessFeedback ("no_worker", args [0]);
		else
			return Msg.Instance.ProcessFeedback ("moved_worker", movedWorker.ToString(), args [0], args [1]);
	}

	private string ProcessFeedback(string code, params string[] msgs)
	{
		return Msg.Instance.ProcessFeedback (code, msgs);
	}
}
