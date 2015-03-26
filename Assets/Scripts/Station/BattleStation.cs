using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStation : MonoBehaviour {

	private List<Warrior> _warriors;

	private List<Warrior> Warrior { get { return _warriors; } }

	// Use this for initialization
	void Start () {
		_warriors = new List<Warrior> ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void AddWarrior(WarriorType warrior)	{
		_warriors.Add (new Warrior(warrior));
	}
}
