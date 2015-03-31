using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Input : Singleton<Input> {


	private List<ICmd> cmds;

	public string Process(string input)
	{
		return ProcessParams (ProcessInput (input));
	}


	public Input()
	{
		cmds = new List<ICmd> ();
		cmds.Add (new Bld ());
		cmds.Add (new Pause ());
		cmds.Add (new Help (cmds.ToArray()));
		cmds.Add (new Add ());
		cmds.Add (new Msg ());
		cmds.Add (new Load ());
		cmds.Add (new Move ());
	}

	public string[] ProcessInput(string input){
		input += " ";
		
		List<string> parans = new List<string> ();
		
		int indexOf = 0;
		
		int size = input.Count (x => x == ':') + 1;
		
		for (int i = 0; i < size ; i++) {
			
			indexOf = input.IndexOf (":");
			
			if(indexOf == -1) {
				parans.Add(input.Substring(0,input.Count()-1));
			} else {
				parans.Add(input.Substring(0,indexOf));
			}
			
			input = input.Substring(indexOf + 1,input.Count() - indexOf - 1);
		}
		
		return parans.ToArray();
	}

	public string ProcessParams(string[] parans)
	{
		ICmd cmd = cmds.FirstOrDefault (x => x.Functions.Contains (parans [0]));

		if (cmd != null)
			return cmd.Cmd (parans);

		return Msg.Instance.Cmd ("msg", "f", parans.Aggregate ((current, next) => current + ":" + next));
	}
}
