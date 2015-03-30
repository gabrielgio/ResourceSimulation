using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class BuildStation : MonoBehaviour {

	public double WorkerPrice = 1;

	public int WorkerOnBuild = 1;

	private double _time = 0;

	public Stack<Tuple<WarriorType,double>> Warriors;

	public PercentageChangedEvent BuildChanged;

	public UnityEvent OnBuiltWorker;

	public WarriorEvent BuildWarriorChanged;

	public WarriorDoneEvent BuildWarriorDone;

	public WorkStation Work;

	void Start () {
		Warriors = new Stack<Tuple<WarriorType,double>> ();

		if (Work == null)
			Work = GameObject.Find ("Main Camera").GetComponent<WorkStation> ();
	}

	void Update () {
		if (WorkerOnBuild != 0) {

			_time += Time.deltaTime;

			if (_time >= GameSetting.Instance.TIME_BUILD_WORKER)
			{
				_time = 0;
				WorkerOnBuild--;
				OnBuiltWorker.Invoke();
			}

			BuildChanged.Invoke (new PercentageChanged ((_time / GameSetting.Instance.TIME_BUILD_WORKER) * 100));
		}

		if (Warriors.Count != 0) {
			var tuple = Warriors.Pop();
			tuple.Value2 += Time.deltaTime;
			double time  = GameSetting.Instance.TIME_BUILD_WARRIOR[tuple.Value1];

			if(tuple.Value2 < time) {
				Warriors.Push(tuple);
			}else{
				tuple.Value2 = 0;
				BuildWarriorDone.Invoke(new WarriorDone(tuple.Value1));
			}

			BuildWarriorChanged.Invoke(new WarriorrEventData((tuple.Value2/time)*100, tuple.Value1));
		}
	}

	public int BuildWorker(int amount)
	{
		var tuple = GameSetting.Instance.COST_BUILD_WORKER;

		int count = 0;

		for (int i = 0; i < amount; i++) {
			if (tuple.Value1 <= Work.Wood &&
				tuple.Value2 <= Work.Rock &&
				tuple.Value3 <= Work.Food) {
						
				Work.Wood -= tuple.Value1;
				Work.Rock -= tuple.Value2;
				Work.Food -= tuple.Value3;
				BuildWorker ();
				count++;
			}
			else
				break;
		}
		
		return count;
	}

	public int BuildWarrior(WarriorType type, int amount)
	{
		var tuple = GameSetting.Instance.COST_BUILD_WARRIOR[type];
		
		int count = 0;
		
		for (int i = 0; i < amount; i++) {
			if (tuple.Value1 <= Work.Wood &&
			    tuple.Value2 <= Work.Rock &&
			    tuple.Value3 <= Work.Food) {
				
				Work.Wood -= tuple.Value1;
				Work.Rock -= tuple.Value2;
				Work.Food -= tuple.Value3;
				BuildWarrior (type);
				count++;
			}
			else
				break;
		}
		
		return count;

	}


	private void BuildWorker(){
		WorkerOnBuild++;
	}

	public void BuildWarrior(WarriorType warrior){

		var stack = new Stack<Tuple<WarriorType,double>> ();

		foreach (var item in Warriors) {
			stack.Push(item);
		}

		stack.Push (new Tuple<WarriorType,double> (warrior, 0));

		Warriors.Clear ();

		foreach (var item in stack) {
			Warriors.Push(item);
		}

	}
}