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
		ProcessMsg (arg);
	}

	public void WriteOnPrompt(string value)
	{
		if (TextOut != null) {
			TextOut.text += "\n"+value;
		}
		
		if (TextIn != null) {
			TextIn.text = "";
			TextIn.ActivateInputField ();
		}
		
		if (Scroll != null) {
			Scroll.value = 0;
		}
	}

	private void ProcessMsg(string msg)
	{

		msg += " ";

		if (msg.Count (x => x == ':') == 0) {
			CmdMsg ("msg", "c", msg);
			return;
		}

		List<string> parans = new List<string> ();

		int indexOf = 0;
		int size = msg.Count (x => x == ':') + 1;
		for (int i = 0; i < size ; i++) {

			indexOf = msg.IndexOf (":");

			if(indexOf == -1) {
				parans.Add(msg.Substring(0,msg.Count()-1));
			}
			else
			{
				parans.Add(msg.Substring(0,indexOf));
			}
			msg = msg.Substring(indexOf + 1,msg.Count() - indexOf - 1);
		}

		ProcessMsg (parans.ToArray	 ());
	}

	private void ProcessMsg(string[] parans)
	{
		if (parans [0] == "Wood" || parans [0] == "Iron" || parans [0] == "Rock") {
			 CmdMove(parans);
			return;
		} else if (parans [0] == "msg") {
			CmdMsg(parans);
			return;
		}

		CmdMsg("msg","f",parans.Aggregate((current, next) => current + ":" + next));
	}

	public void CmdMsg(params string[] msg)
	{
		if(msg[0] != "msg")
			CmdMsg("msg","r", "Something is much wrong CmdMsg");

		if (msg.Length == 1) {
			WriteOnPrompt("<b><i>empty msg</i></b>");
		}
		else if (msg.Length == 2) {
			WriteOnPrompt("<b><i>" + msg[1] + "</i></b>");

		}else{
			switch(msg[1])
			{
			case "r":
				WriteOnPrompt("<color=red>" + msg [2] + "</color>");
				break;
			case "b":
				WriteOnPrompt("<color=blue>" + msg [2] + "</color>");
				break;
			case "i":
				WriteOnPrompt("<b><i>" + msg [2] + "</i></b>");
				break;
			case "c":
				CmdMsg("msg","r", msg[2]+ " invalid command");
				break;
			case "f":
				CmdMsg("msg","r", msg[2]+  " command not found");
				break;
			default:
				CmdMsg("msg", "r", "invalid params");
				break;
			}
		}
	}

	private void CmdMove(params string[] parans)
	{
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();

		if (!(parans [0] == "Wood" || parans [0] == "Iron" || parans [0] == "Rock")) 
			CmdMsg ("msg", "r", "Something is much wrong CmdMove");

		if (!(parans [1] == "Wood" || parans [1] == "Iron" || parans [1] == "Rock"))
			CmdMsg ("msg", "r", parans [1] + " is not a valide resource");

		int times = 1;
		int movedWorker = 0;

		if (parans.Length >= 3)
			int.TryParse (parans [2], out times);
			

		ResourceSource preRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [0]);
		ResourceSource posRes = (ResourceSource)Enum.Parse (typeof(ResourceSource), parans [1]);

		while (times-- != 0) {
			Worker worker = workStation.Workers.FirstOrDefault (x => x.WorkingOn == preRes);
		
			if (worker == null)
				break;
			else {
				worker.WorkingOn = posRes;
				movedWorker++;
			}
		}

		if(movedWorker == 0)
			CmdMsg ("msg", "r", "there is no worker working on " + parans [0]);
		else if (movedWorker == 1)
			CmdMsg ("msg", "b", "A workers has been moved from " + parans [0] + " to " + parans [1]);
		else
			CmdMsg ("msg", "b", movedWorker.ToString()+" workers has been moved from " + parans [0] + " to " + parans [1]);
	}
}