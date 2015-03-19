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

	private List<string> commands;
	private int indexOfLine = 0;


	public Prompt()
	{
		commands = new List<string> ();
	}

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
		commands.Add (arg);
		indexOfLine = 0;
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
		if (parans [0] == "wood" || parans [0] == "iron" || parans [0] == "rock") {
			CmdMove (parans);
			return;
		} else if (parans [0] == "msg") {
			CmdMsg (parans);
			return;
		} else if (parans [0] == "build") {
			CmdBuild(parans);
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
				WriteOnPrompt("<color=red>" + msg[2]+ "</color> invalid command");
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

	private void CmdBuild (params string[] parans)
	{
		if (parans [0] != "build") {
			CmdMsg ("msg", "r", "Something is much wrong CmdBuild");
			return;
		}

		if (parans.Length == 1) {
			CmdMsg ("msg", "r", "param is missing");
		}

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
			{
				CmdMsg ("msg", "r", "Not enough wood to build a worker");
				return;
			}
			else{
				CmdMsg ("msg", "b", string.Format("{0} workers has added to build", builtWorker));
				return;
			}

		}
		CmdMsg ("msg", "r", "Invalid params.");
	}

	private void CmdMove(params string[] parans)
	{
		WorkStation workStation = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();

		if (!(parans [0] == "wood" || parans [0] == "iron" || parans [0] == "rock")) 
			CmdMsg ("msg", "r", "Something is much wrong CmdMove");

		if (!(parans [1] == "wood" || parans [1] == "iron" || parans [1] == "rock"))
			CmdMsg ("msg", "r", parans [1] + " is not a valide resource");

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

		if(movedWorker == 0)
			CmdMsg ("msg", "r", "there is no worker working on " + parans [0]);
		else if (movedWorker == 1)
			CmdMsg ("msg", "b", "A workers has been moved from " + parans [0] + " to " + parans [1]);
		else
			CmdMsg ("msg", "b", movedWorker.ToString()+" workers has been moved from " + parans [0] + " to " + parans [1]);
	}

	private string FirstCharToUpper(string input)
	{
		if (String.IsNullOrEmpty(input))
			throw new ArgumentException("ARGH!");
		return input.First().ToString().ToUpper() + input.Substring(1);
	}
}