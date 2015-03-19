using UnityEngine;
using System.Collections;

public class Warrior{

	public const double TIME_MINION = 60;

	public const double TIME_WARRIOR = 60 * 2;

	public const double TIME_LEGEND = 60 * 3;

	private double _timeIn = 0;

	private WarriorType _type;

	public bool IsBuilt {
		get {
			switch (Type) {
			case WarriorType.Minion:
				return (TIME_MINION - _timeIn) <= 0;

			case WarriorType.Warrior:
				return (TIME_WARRIOR - _timeIn) <= 0;

			case WarriorType.Legend:
				return (TIME_LEGEND - _timeIn) <= 0;

			default:
				return false;
			}
		}
	}

	public WarriorType Type { private set; get; }
		
	public Warrior(WarriorType type)
	{
		Type = type;
	}

	public void ApplyTimeBuild(double time)
	{	
		_timeIn += time;
	}
}

public enum WarriorType
{
	Minion,
	Warrior,
	Legend
}
