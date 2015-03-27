using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStation : MonoBehaviour {

	public double MaxHealth;

	public Stack<Warrior> Warriors;

	public double Health;

	public double DamegePerSecond;

	public WarriorEvent WarriorHealthChanged;

	public PercentageChangedEvent HealthChanged;
	
	void Start () {
		Warriors = new Stack<Warrior> ();
		Warriors.Push (new Warrior (WarriorType.Minion));
		Warriors.Push (new Warrior (WarriorType.Legend));
		Warriors.Push (new Warrior (WarriorType.Minion));
		Warriors.Push (new Warrior (WarriorType.Minion));
	}

	void Update () {
		if (Warriors.Count != 0) {
			Warrior warrior = Warriors.Pop ();

			if (warrior.ApplyDamage (DamegePerSecond * Time.deltaTime))
				Warriors.Push (warrior);

			WarriorHealthChanged.Invoke (new WarriorrEventData ((warrior.Health / GameSetting.Instance.WARRIOR_HEALTH [warrior.Type]) * 100, warrior.Type));
		} else {
			Health -= DamegePerSecond * Time.deltaTime;
			HealthChanged.Invoke(new PercentageChanged((Health/MaxHealth)*100));
		}
	}

	public void WarriorDone(WarriorDone warrior) {
		var stack = new Stack<Warrior> ();
		
		foreach (var item in Warriors) {
			stack.Push(item);
		}
		
		stack.Push (new Warrior (warrior.Warrior));
		foreach (var item in stack) {
			Warriors.Push(item);
		}
	}
}
