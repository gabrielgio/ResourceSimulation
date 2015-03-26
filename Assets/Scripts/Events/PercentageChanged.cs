using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class PercentageChanged : EventArgs {

	public double Percentage { get; set; }

	public PercentageChanged()
	{

	}

	public PercentageChanged(double percentage)
	{
		Percentage = percentage;
	}
}

[Serializable]
public class PercentageChangedEvent : UnityEvent<PercentageChanged>
{
	
}
