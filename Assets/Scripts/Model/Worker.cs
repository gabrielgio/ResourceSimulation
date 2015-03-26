using UnityEngine;
using System.Collections;

public class Worker {
	public ResourceSource Type { set; get; }

	public double WorkRate {
		get {
			return GameSetting.Instance.WORKER_RATE;
		}
	}

	public Worker(ResourceSource type)
	{
		Type = type;
	}

	public Worker()
	{
		Type = ResourceSource.Wood;
	}
}
