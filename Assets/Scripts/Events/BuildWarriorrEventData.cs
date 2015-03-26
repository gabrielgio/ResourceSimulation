using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class BuildWarriorrEventData : EventArgs {

	public double Percentage { get; set; }

	public WarriorType Warrior { get; set; }
	
	public BuildWarriorrEventData()
	{
		
	}
	
	public BuildWarriorrEventData(double percentage)
	{
		Percentage = percentage;
	}

	public BuildWarriorrEventData(double percentage, WarriorType warrior)
	{
		Percentage = percentage;
		Warrior = warrior;
	}
}

[Serializable]
public class BuildWarriorEvent : UnityEvent<BuildWarriorrEventData>
{
	
}