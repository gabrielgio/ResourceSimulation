using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class WarriorDone : EventArgs {

	public WarriorType Warrior { get ; set; }

	public WarriorDone()
	{

	}

	public WarriorDone(WarriorType warrior)
	{
		Warrior = warrior;
	}

}

[Serializable]
public class WarriorDoneEvent : UnityEvent<WarriorDone>
{
	
}