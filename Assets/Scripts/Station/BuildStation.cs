using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuildStation : MonoBehaviour {

	public double WorkerPrice = 1;

	public List<Worker> Workers = new List<Worker> ();

	public double TimesGone = 0;

	public List<Warrior> Warriors = new List<Warrior> ();

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Worker worker = Workers.Where (x => !x.IsBuilt).FirstOrDefault ();
		
		if (worker != null) {
			worker.ApplyTimeBuild (Time.deltaTime);
			
			if (worker.IsBuilt) {
				GetComponent<WorkStation> ().AddWorker (worker.Type);
			}
		}
		 
		Warrior warrior = Warriors.Where (x => !x.IsBuilt).FirstOrDefault ();
		
		if (warrior != null) {
			warrior.ApplyTimeBuild (Time.deltaTime);
			
			if (warrior.IsBuilt) {
				GetComponent<BattleStation> ().AddWarrior (warrior.Type);
			}
		}
	}

	public void BuildWorker(){
		Workers.Add(new Worker(ResourceSource.Wood));
	}

	public void BuildWarrior(WarriorType warrior){
		Warriors.Add (new Warrior (warrior));
	}
}
