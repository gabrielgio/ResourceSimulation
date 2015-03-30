using UnityEngine;
using System.Collections;

public class Worker {

	public ResourceSource Type { set; get; }

	public Worker(ResourceSource type)
	{
		Type = type;
	}

	public Worker()
	{
		Type = ResourceSource.Wood;
	}
}
