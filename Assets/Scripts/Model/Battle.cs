using System.Collections.Generic;

public class Battle
{
	public string Name { get; set; }
	public double Health { get; set; }
	public List<CommandTime> Commands { get; set; }

	public Battle()
	{
		Commands = new List<CommandTime> ();
	}
}

public class CommandTime
{
	public WarriorType Type { get; set; }
	public double Time { get; set; }

	public CommandTime Clone()
	{
		return new CommandTime (){Time = Time, Type = Type};
	}
}