using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Input : Singleton<Input> {

	public string Process(string input)
	{
		return ProcessParams (ProcessInput (input));
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
		if (parans [0] == "wood" || parans [0] == "iron" || parans [0] == "rock") {
			return Move.Instance.CmdMove (parans);
		} else if (parans [0] == "msg") {
			return Msg.Instance.CmdMsg (parans);
		} else if (parans [0] == "build") {
			return Bld.Instance.CmdBuild (parans);
		} else {
			return Msg.Instance.CmdMsg ("msg", "f", parans.Aggregate ((current, next) => current + ":" + next));
		}
	}
}
