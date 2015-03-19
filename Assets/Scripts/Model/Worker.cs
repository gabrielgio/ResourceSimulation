using UnityEngine;
using System.Collections;

public class Worker {

	public const double TIME = 5;

	private double _timeIn = 0;
	
	public bool IsBuilt {
		get {

			return (TIME - _timeIn) <= 0;
		}
	}
	
	public ResourceSource Type { set; get; }

	public double WorkRate {
		get {
			return 0.016;
		}
	}

	public double Percentage 
	{
		get{
			return (_timeIn/TIME)*100;
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
