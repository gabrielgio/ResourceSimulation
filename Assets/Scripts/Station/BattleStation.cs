using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStation : MonoBehaviour {

	private List<Warrior> Warrior;

	// Use this for initialization
	void Start () {
		Warrior = new List<Warrior> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void WarriorDone(WarriorDone warrior) {
		Warrior.Add (new Warrior(warrior.Warrior));
	}
}
