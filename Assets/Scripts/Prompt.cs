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

	public StringChangedEvent StringChanged;

	public Prompt()
	{
		commands = new List<string> ();
	}

	void Start () {

		if (TextIn != null) {
			TextIn.onEndEdit.AddListener (EditEnd);
		}
		TextIn.ActivateInputField ();
	}

	void Update () {	
	
	}

	public void EditEnd (string arg)
	{
		arg = arg.Replace ("\n", "");

		WriteOnPrompt (Input.Instance.Process (arg));
		commands.Add (arg);
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

	public void BuildWorker()
	{
		EditEnd ("build:worker");
	}

	public void BuildWarrior(string type)
	{
		EditEnd (string.Format ("build:{0}", type));
	}
}