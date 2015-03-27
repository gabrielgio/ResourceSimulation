using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBarWarrior : MonoBehaviour {

	public Text TextWarrior;

	private RectTransform value;
	private RectTransform panel;
	private double size = 0;
	
	void Start () {
		panel = GetComponent<RectTransform> ();
		value = panel.GetChild (0).GetComponent<RectTransform> ();
		size = value.sizeDelta.x;
		
	}
	
	public void OnPercentageChange(WarriorrEventData data)
	{
		value.sizeDelta = new Vector2 ((float)(size * data.Percentage/100), value.sizeDelta.y);

		if (TextWarrior != null)
			TextWarrior.text = data.Warrior.ToString ();
	}
}