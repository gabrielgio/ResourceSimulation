using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class WorldChangedEventArgs : EventArgs {

	public World CurrentWorld { get; set; }
}

[Serializable]
public class WorldChangedEventTrigger : UnityEvent<WorldChangedEventArgs>
{
	
}