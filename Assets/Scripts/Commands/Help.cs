using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Help : ICmd {


	private ICmd[] _cmds;

	public string[] Functions {
		get {
				return new string[]{"help", "hlp"};
		}
	}

	public Help(ICmd[] cmds)
	{
		_cmds = cmds;
	}

	public string Cmd (params string[] args)
	{
		string returnString = "\nCommands: \n";

		foreach (var item in _cmds) {
			returnString += string.Format("<i>{0}</i>\n", item.Functions.Aggregate((x,y) => x+", "+y));
		}

		returnString += "\n<i>To move a worker from one resource to another type:\n[from]:[to]:[number, optional]\n\nExemples:\n\nwood:food\nfood:wood:4</i>";

		returnString += "\n\n<i>How much to build:\n\nWorker: 1 wood\nMinion: 1 wood\nWarrior: 1 wood and 1 rock\nLegend: 1 of each resource</i>";

		returnString += "\n\n<i>This game has three type of warrior:\n\nMinion: the cheapest and weakest one\nWarrior: intermediate\nLegend the strongest and most expensive</i>";

		return returnString;
	}
}
