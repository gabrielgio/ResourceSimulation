using UnityEngine;
using System.Collections;
using System;

public class Warrior{

	private double _timeIn = 0;

	public double Health { private set; get; }

	public WarriorType Type { private set; get; }
		
	public Warrior(WarriorType type)
	{
		Type = type;
		Health = GameSetting.Instance.WARRIOR_HEALTH [type];
	}

	public void ApplyTimeBuild(double time)
	{	
		_timeIn += time;
	}

	public bool ApplyDamage(double damage)
	{
		Health -= damage;
		return Health >= 0;
	}
}

[Serializable]
public enum WarriorType
{
	Minion,
	Warrior,
	Legend
}
