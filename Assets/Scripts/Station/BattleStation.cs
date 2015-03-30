using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleStation : MonoBehaviour {

	public double MaxHealth;

	public double EnemyMaxHealth;

	public Stack<Warrior> Warriors;

	public Stack<Warrior> EnemyWarriors;

	public double Health;

	public double EnemyHealth;

	public WarriorEvent WarriorHealthChanged;

	public WarriorEvent EnemyWarriorHealthChanged;

	public PercentageChangedEvent HealthChanged;

	public PercentageChangedEvent EnemyHealthChanged;
	
	void Start () {
		Warriors = new Stack<Warrior> ();
		EnemyWarriors = new Stack<Warrior> ();
	}

	void Update () {

		if (Warriors.Count == 0 && EnemyWarriors.Count >= 1) {

			Warrior enemyWarrior = EnemyWarriors.Pop();
			EnemyWarriors.Push(enemyWarrior);
			
			double damage = EnemyWarriors.Sum(x =>  GameSetting.Instance.WARRIOR_DAMAGE[x.Type] * Time.deltaTime);
			
			Health -= damage;
			
			HealthChanged.Invoke (new PercentageChanged ((Health / MaxHealth) * 100));

			
		} else if (Warriors.Count >= 1 && EnemyWarriors.Count == 0) {

			Warrior warrior = Warriors.Pop();
			Warriors.Push(warrior);

			double damage = Warriors.Sum(x =>  GameSetting.Instance.WARRIOR_DAMAGE[x.Type] * Time.deltaTime);

			EnemyHealth -= damage;

			EnemyHealthChanged.Invoke(new PercentageChanged((EnemyHealth/EnemyMaxHealth)*100));

		}else if (Warriors.Count >= 1 && EnemyWarriors.Count >= 1) {

			Warrior enemyWarrior = EnemyWarriors.Pop();
			Warrior warrior = Warriors.Pop();

			double damage = Warriors.Sum(x =>  GameSetting.Instance.WARRIOR_DAMAGE[x.Type] * Time.deltaTime);
			double enemyDamage = EnemyWarriors.Sum(x =>  GameSetting.Instance.WARRIOR_DAMAGE[x.Type] * Time.deltaTime);

			if(warrior.ApplyDamage(enemyDamage))
				Warriors.Push(warrior);
				
			if(enemyWarrior.ApplyDamage(damage))
				EnemyWarriors.Push(enemyWarrior);

			WarriorHealthChanged.Invoke (new WarriorrEventData ((warrior.Health / GameSetting.Instance.WARRIOR_HEALTH [warrior.Type]) * 100, warrior.Type));
			EnemyWarriorHealthChanged.Invoke (new WarriorrEventData ((enemyWarrior.Health / GameSetting.Instance.WARRIOR_HEALTH [warrior.Type]) * 100, enemyWarrior.Type));
		}

	}

	public void AddEnemy(WarriorType type)
	{
		var stack = new Stack<Warrior> ();

		foreach (var item in EnemyWarriors) {
			stack.Push(item);
		}
		
		stack.Push (new Warrior (type));

		EnemyWarriors.Clear ();

		foreach (var item in stack) {
			EnemyWarriors.Push(item);
		}
	}


	public void WarriorDone(WarriorDone warrior) {
		var stack = new Stack<Warrior> ();
		
		foreach (var item in Warriors) {
			stack.Push(item);
		}
		
		stack.Push (new Warrior (warrior.Warrior));

		Warriors.Clear ();

		foreach (var item in stack) {
			Warriors.Push(item);
		}
	}
}
