using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

[Serializable]
public class BuildWorkerEventData : EventArgs {
	
	public double Percentage { get; set; }

	public BuildWorkerEventData()
	{
		
	}

	public BuildWorkerEventData(double percentage)
	{
		Percentage = percentage;
	}
}

[Serializable]
public class BuildWorkerEvent : UnityEvent<BuildWorkerEventData>
{

}