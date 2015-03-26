using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class BuildStation : MonoBehaviour {

	public double WorkerPrice = 1;

	public double TimesGone = 0;

	public int WorkerOnBuild = 1;

	private double _time = 0;

	public Stack<Tuple<WarriorType,double>> Warriors;

	public BuildWorkerEvent BuildChanged;

	public UnityEvent OnBuiltWorker;

	public BuildWarriorEvent OnBuiltWarrior;

	void Start () {
		Warriors = new Stack<Tuple<WarriorType,double>> ();
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

			BuildChanged.Invoke (new BuildWorkerEventData ((_time / GameSetting.Instance.TIME_BUILD_WORKER) * 100));
		}

		if (Warriors.Count != 0) {
			var tuple = Warriors.Pop();
			tuple.Value2 += Time.deltaTime;
			double time  = GameSetting.Instance.TIME_BUILD_WARRIOR[tuple.Value1];

			if(tuple.Value2 < time) {
				Warriors.Push(tuple);
			}else{
				tuple.Value2 = 0;
			}

			OnBuiltWarrior.Invoke(new BuildWarriorrEventData((tuple.Value2/time)*100, tuple.Value1));
		}
	}

	public void BuildWorker(){
		WorkerOnBuild++;
	}

	public void BuildWarrior(WarriorType warrior){

		var stack = new Stack<Tuple<WarriorType,double>> ();

		foreach (var item in Warriors) {
			stack.Push(item);
		}

		stack.Push (new Tuple<WarriorType,double> (warrior, 0));

		foreach (var item in stack) {
			Warriors.Push(item);
		}

	}
}
