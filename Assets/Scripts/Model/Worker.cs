using UnityEngine;
using System.Collections;

public class Worker {

	private double _timeIn = 0;
	
	public bool IsBuilt {
		get {

			return (GameSetting.Instance.TimeToBuildWorker - _timeIn) <= 0;
		}
	}
	
	public ResourceSource Type { set; get; }

	public double WorkRate {
		get {
			return GameSetting.Instance.WorkerRate;
		}
	}

	public double Percentage 
	{
		get{
			return (_timeIn/GameSetting.Instance.TimeToBuildWorker)*100;
		}
	}
	
	public Worker(ResourceSource type)
	{
		Type = type;
	}
	
	public void ApplyTimeBuild(double time)
	{	
		_timeIn += time;
	}
}
