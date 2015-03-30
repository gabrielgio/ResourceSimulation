using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameSetting : Singleton<GameSetting> {
	
	private JSONNode _jsonNode;

	public const string TITLE = "TITLE - RESOURCE SIMULATION";

	public double TIME_BUILD_WORKER	{ get { return _jsonNode ["build"] ["worker_time"].AsDouble; } }

	public Tuple<double,double,double> COST_BUILD_WORKER {
		get{
			double value1 = _jsonNode ["build"] ["worker_cost"] ["Wood"].AsDouble;
			double value2 = _jsonNode ["build"] ["worker_cost"] ["Rock"].AsDouble;
			double value3 = _jsonNode ["build"] ["worker_cost"] ["Food"].AsDouble;
			return new Tuple<double, double, double> (value1, value2, value3);
		}
	}

	public TimeBuildWarrior TIME_BUILD_WARRIOR { get; private set; }

	public CostBuildWarrior COST_BUILD_WARRIOR { get; private set; }

	public WarriorHealth WARRIOR_HEALTH { get; private set; }

	public WarriorDamage WARRIOR_DAMAGE { get; private set; }

	public WorkRate WORKER_RATE { get; private set; }

	public GameSetting()
	{
		TextAsset textFile = (TextAsset)Resources.Load("settings", typeof(TextAsset));
		_jsonNode = JSON.Parse (textFile.text);
		TIME_BUILD_WARRIOR = new TimeBuildWarrior (_jsonNode);
		COST_BUILD_WARRIOR = new CostBuildWarrior (_jsonNode);
		WARRIOR_HEALTH = new WarriorHealth (_jsonNode);
		WARRIOR_DAMAGE = new WarriorDamage (_jsonNode);
		WORKER_RATE = new WorkRate (_jsonNode);
	}
}

public class TimeBuildWarrior 
{
	private JSONNode _jsonNode;

	public double this[WarriorType type]
	{
		get{
			return _jsonNode ["build"] [type.ToString ()].AsDouble;;
		}
	}

	public TimeBuildWarrior(JSONNode jsonNode)
	{
		_jsonNode = jsonNode;
	}
}

public class WarriorHealth
{
	private JSONNode _jsonNode;
	
	public double this[WarriorType type]
	{
		get{
			return _jsonNode ["batlle"]["health"][type.ToString()].AsDouble;
		}
	}
	
	public WarriorHealth(JSONNode jsonNode)
	{
		_jsonNode = jsonNode;
	}
}

public class WarriorDamage
{
	private JSONNode _jsonNode;
	
	public double this[WarriorType type]
	{
		get{
			return _jsonNode ["batlle"]["damage"][type.ToString()].AsDouble;
		}
	}
	
	public WarriorDamage(JSONNode jsonNode)
	{
		_jsonNode = jsonNode;
	}
}

public class WorkRate
{
	private JSONNode _jsonNode;
	
	public double this[ResourceSource type]
	{
		get{
			return _jsonNode ["work"]["worker_rate"][type.ToString()].AsDouble;

		}
	}

	public WorkRate(JSONNode jsonNode)
	{
		_jsonNode = jsonNode;
	}
}

public class CostBuildWarrior
{
	private JSONNode _jsonNode;

	public Tuple<double,double,double> this[WarriorType type]
	{
		get{
			double value1 = _jsonNode ["build"]["warrior_cost"][type.ToString()]["Wood"].AsDouble;
			double value2 = _jsonNode ["build"]["warrior_cost"][type.ToString()]["Rock"].AsDouble;
			double value3 = _jsonNode ["build"]["warrior_cost"][type.ToString()]["Food"].AsDouble;
			return new Tuple<double, double, double>(value1, value2, value3);
		}
	}

	public CostBuildWarrior(JSONNode jsonNode)
	{
		_jsonNode = jsonNode;
	}
}



