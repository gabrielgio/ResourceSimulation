using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangedNotice : MonoBehaviour {

	public Text Output;

	void Start()
	{
		if (Output == null)
			Output = GetComponent<Text> ();
	}

	public void Changed(string value)
	{
		if (Output == null)
			return;
		Output.text = value;
	}
}
