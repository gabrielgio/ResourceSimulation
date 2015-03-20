using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using System;

public class Msg 
{
	private static Msg instance;

	private Dictionary<string,string> _presets;
	private Dictionary<string, Tuple<string,string>> _msgs;

	public static Msg Instance {
		get{
			if(instance == null)
				instance = new Msg ();
			return instance;
		}
	}

	public Msg(){
		_presets = new Dictionary<string, string> ();
		_msgs = new Dictionary<string, Tuple<string,string>> ();

		TextAsset textFile = (TextAsset)Resources.Load("msg", typeof(TextAsset));
		var json = JSON.Parse (textFile.text);

		foreach (JSONNode item in json["preset"].AsArray) {
			_presets.Add(item["code"], item["msg"]);
		}

		foreach (JSONNode item in json["feedback"].AsArray) {
			_msgs.Add(item["code"], new Tuple<string,string>(item["msgcode"], item["msg"]));
		}
	}

	public string Process(string msg){
		msg += " ";
		
		List<string> parans = new List<string> ();
		
		int indexOf = 0;

		int size = msg.Count (x => x == ':') + 1;

		for (int i = 0; i < size ; i++) {
			
			indexOf = msg.IndexOf (":");
			
			if(indexOf == -1) {
				parans.Add(msg.Substring(0,msg.Count()-1));
            } else {
                parans.Add(msg.Substring(0,indexOf));
            }

            msg = msg.Substring(indexOf + 1,msg.Count() - indexOf - 1);
        }

		return ProcessMsg(parans.ToArray());
	}

	private string ProcessMsg(string[] parans)
	{
		if (parans [0] == "wood" || parans [0] == "iron" || parans [0] == "rock") {
			return CmdMove (parans);

		} else if (parans [0] == "msg") {
			return CmdMsg (parans);
		} else if (parans [0] == "build") {
			return CmdBuild (parans);
		} else {
			return CmdMsg ("msg", "f", parans.Aggregate ((current, next) => current + ":" + next));
		}
	}

	private string ProcessFeedback(string code, params string[] msgs) {
		if (_msgs.ContainsKey (code)) {
			string premsg = string.Format(_msgs[code].Value2, msgs);
			return CmdMsg("msg",_msgs[code].Value1, premsg);
		}

		return CmdMsg("msg",_msgs["deafult"].Value1, string.Format(_msgs["deafult"].Value2, msgs));
	}

	public string CmdMsg(params string[] msg)
	{
		if(msg[0] != "msg")
			return ProcessFeedback("wrong","CmdMsg");
		
		if (msg.Length == 1)
			return CmdMsg("msg","i","empty msg");
		
		if (msg.Length == 2)
			return CmdMsg ("msg", "i", msg [1]);

		if (_presets.ContainsKey (msg [1]))
			return string.Format (_presets [msg [1]], msg [2]);

		return string.Format (_presets ["default"], msg [1]);

    }
	private string CmdBuild (params string[] parans)
	{
		if (parans [0] != "build")
			return ProcessFeedback("wrong","CmdBuild");

		if (parans.Length == 1) 
			return ProcessFeedback("param_missing");
		
		int times = 1;
		int builtWorker = 0;
		
		if (parans.Length >= 3)
			int.TryParse (parans [2], out times);
		
		if (parans [1] == "worker") {
			WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
			BuildStation buildStation = GameObject.Find ("Main Camera").GetComponent<BuildStation> ();
			while (times-- != 0) {
				if (workStation.Wood > buildStation.WorkerPrice) {
					workStation.Wood -= buildStation.WorkerPrice;
					buildStation.BuildWorker ();
					builtWorker++;
				}
				else{
					break;
				}
			}
			
			if(builtWorker == 0)
				return ProcessFeedback ("not_enough_resource","Wood","Worker");
			else
				return ProcessFeedback("worker_build", builtWorker.ToString());
		}

		return ProcessFeedback("invalid_param");
	}
	
	private string CmdMove(params string[] parans)
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
		

		ResourceSource preRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), FirstCharToUpper(parans [0]));
		ResourceSource posRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), FirstCharToUpper (parans [1]));
		
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
			return ProcessFeedback ("no_worker", parans [0]);
		else
			return ProcessFeedback ("moved_worker", movedWorker.ToString(), parans [0], parans [1]);
    }
    
    private string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}

public class Tuple <T1, T2>
{
	public T1 Value1 { get; set; }
	public T2 Value2 { get; set; }
	
	public Tuple()
	{
	}
	
	public Tuple (T1 value1, T2 value2)
	{
		Value1 = value1;
		Value2 = value2;
	}
}
