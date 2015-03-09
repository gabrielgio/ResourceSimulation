using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public class Prompt : MonoBehaviour {

	public Text TextOut;
	public InputField TextIn;
	public Scrollbar Scroll;

	// Use this for initialization
	void Start () {

		if (TextIn != null) {
			TextIn.onEndEdit.AddListener (EditEnd);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void EditEnd (string arg)
	{
		WriteOnPrompt (arg);
	}

	public void WriteOnPrompt(string value)
	{
		if (TextOut != null) {
			TextOut.text += "\n"+ProcessMsg(value);
		}
		
		if (TextIn != null) {
			TextIn.text = "";
			TextIn.ActivateInputField ();
		}
		
		if (Scroll != null) {
			Scroll.value = 0;
		}
	}

	private string ProcessMsg(string msg)
	{

		msg += " ";

		if(msg.Count(x => x == ':') == 0)
		   return CmdMsg("msg", "c", msg);

		List<string> parans = new List<string> ();

		int indexOf = 0;
		int size = msg.Count (x => x == ':') + 1;
		for (int i = 0; i < size ; i++) {

			indexOf = msg.IndexOf (":");

			if(indexOf == -1) {
				parans.Add(msg.Substring(0,msg.Count()-1));
			}
			else{
				parans.Add(msg.Substring(0,indexOf));
			}
			msg = msg.Substring(indexOf + 1,msg.Count() - indexOf - 1);
		}

		return ProcessMsg(parans.ToArray());
	}

	private string ProcessMsg(string[] parans)
	{
		if (parans [0] == "Wood" || parans [0] == "Iron" || parans [0] == "Rock") {
			return CmdMove(parans);
		} else if (parans [0] == "msg") {
			return CmdMsg(parans);
		}

		return CmdMsg("msg","f",parans.Aggregate((current, next) => current + ":" + next));
	}

	public string CmdMsg(params string[] msg)
	{
		if(msg[0] != "msg")
			return CmdMsg("msg","r", "Something is much wrong CmdMsg");

		if (msg.Length == 1) {
			return "<b><i>empty msg</i></b>";
		}
		else if (msg.Length == 2) {
			return "<b><i>" + msg[1] + "</i></b>";

		}else{
			switch(msg[1])
			{
			case "r":
				return "<color=red>" + msg [2] + "</color>";
			case "b":
				return "<color=blue>" + msg [2] + "</color>";
			case "i":
				return "<b><i>" + msg [2] + "<b><i>";
			case "c":
				return CmdMsg("msg","r", msg[2])+ "invalid command";
			case "f":
				return CmdMsg("msg","r", msg[2])+  " command not found";
			default:
				return CmdMsg("msg", "r", "invalid params");
			}
		}
	}

	private void PromptCmd(params string[] parans)
	{
		WriteOnPrompt("\n"+CmdMsg(parans));
	}

	private string CmdMove(params string[] parans)
	{
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();

		if (!(parans [0] == "Wood" || parans [0] == "Iron" || parans [0] == "Rock") ) 
			return CmdMsg("msg","r", "Something is much wrong CmdMove");

		if (!(parans [1] == "Wood" || parans [1] == "Iron" || parans [1] == "Rock"))
			return CmdMsg("msg","r", parans[1] + " is not a valide resource");

		int times = 1;

		if(parans.Length >= 3)
			if(!int.TryParse(parans[2], out times))
			   times = 1;

		ResourceSource preRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [0]);
		ResourceSource posRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [1]);

		PromptCmd ("msg", "b", "Starting to move workers");

		do {
			Worker worker = workStation.Workers.FirstOrDefault (x => x.WorkingOn == preRes);
		
			if (worker == null) {
				PromptCmd ("msg", "r", "  there is no worker working on " + parans [0]);
				break;
			} else {
				worker.WorkingOn = posRes;
				PromptCmd ("msg", "b", "  worker has been moved from " + parans [0] + " to " + parans [1]);
			}
		} while (times-- == 0);

		return CmdMsg ("msg", "b", "Finish to move workores");
	}
}