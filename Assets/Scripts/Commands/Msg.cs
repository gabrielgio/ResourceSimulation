using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using System;

public class Msg : Singleton<Msg>, ICmd {

	public string[] Functions{
		get{
			return new string[]{"msg"};
		}
	}

	private Dictionary<string,string> _presets;

	private Dictionary<string, Tuple<string,string>> _msgs;

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

	public string ProcessFeedback(string code, params string[] msgs) {
		if (_msgs.ContainsKey (code)) {
			string premsg = string.Format(_msgs[code].Value2, msgs);
			return Cmd("msg",_msgs[code].Value1, premsg);
		}

		return Cmd("msg",_msgs["deafult"].Value1, string.Format(_msgs["deafult"].Value2, msgs));
	}

	public string Cmd(params string[] msg)
	{
		if(msg[0] != "msg")
			return ProcessFeedback("wrong","CmdMsg");
		
		if (msg.Length == 1)
			return Cmd("msg","i","empty msg");
		
		if (msg.Length == 2)
			return Cmd ("msg", "i", msg [1]);

		if (_presets.ContainsKey (msg [1]))
			return string.Format (_presets [msg [1]], msg [2]);

		return string.Format (_presets ["default"], msg [1]);

    }
}