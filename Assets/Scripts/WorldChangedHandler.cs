using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldChangedHandler : MonoBehaviour {


	public Text WorldName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WorldChanged (WorldChangedEventArgs worldArgs)
	{
		if (WorldName != null)
			WorldName.text = worldArgs.CurrentWorld.Name;
	}
}
