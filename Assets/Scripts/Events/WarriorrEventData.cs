using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class WarriorrEventData : EventArgs {

	public double Percentage { get; set; }

	public WarriorType Warrior { get; set; }
	
	public WarriorrEventData()
	{
		
	}
	
	public WarriorrEventData(double percentage)
	{
		Percentage = percentage;
	}

	public WarriorrEventData(double percentage, WarriorType warrior)
	{
		Percentage = percentage;
		Warrior = warrior;
	}
}

[Serializable]
public class WarriorEvent : UnityEvent<WarriorrEventData>
{
	
}