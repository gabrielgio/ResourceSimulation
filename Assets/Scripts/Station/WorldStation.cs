using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine.Events;

public class WorldStation : MonoBehaviour {

	private JSONNode _jsonNode;

	private List<World> _worlds;

	public int Index;

	public PercentageChangedEvent WoodChanged;

	public PercentageChangedEvent RockChanged;

	public PercentageChangedEvent FoodChanged;

	public WorldChangedEventTrigger WolrdChanged;
	
	void Start () {

		TextAsset textFile = (TextAsset)Resources.Load("worlds", typeof(TextAsset));
		_jsonNode = JSON.Parse (textFile.text);
		_worlds = new List<World> ();
		LoadWorlds ();
		LoadWorld (Index);
	}

	public void LoadWorlds(){

		foreach (JSONNode item in _jsonNode ["worlds"].AsArray) {
			World world = new World();

			world.Name = item ["name"];

			world.Rock = item ["Rock"].AsDouble;
			world.Wood = item ["Wood"].AsDouble;
			world.Food = item ["Food"].AsDouble;

			world.ARock = item ["Rock"].AsDouble;
			world.AWood = item ["Wood"].AsDouble;
			world.AFood = item ["Food"].AsDouble;

			_worlds.Add(world);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadWorld(int index)
	{
		Index = index;

		WolrdChanged.Invoke (new WorldChangedEventArgs (){CurrentWorld = _worlds[index]});

		ChopTree (0);
		MineRock (0);
		GrabFood (0);
	}

	public double ChopTree(double amount)
	{
		if ((_worlds[Index].AWood - amount) > 0) {
			_worlds[Index].AWood -= amount;
			WoodChanged.Invoke(new PercentageChanged((_worlds[Index].AWood/_worlds[Index].Wood)*100));
			return amount;
		} 

		if (_worlds[Index].AWood == 0)
			return -1;

		_worlds[Index].AWood -= _worlds[Index].AWood;
		WoodChanged.Invoke(new PercentageChanged((_worlds[Index].AWood/_worlds[Index].Wood)*100));
		return _worlds[Index].AWood;
	}

	public double MineRock(double amount)
	{
		if ((_worlds[Index].ARock - amount) > 0) {
			_worlds[Index].ARock -= amount;
			RockChanged.Invoke(new PercentageChanged((_worlds[Index].ARock/_worlds[Index].Rock)*100));
			return amount;
		} 
		
		if (_worlds[Index].ARock == 0)
			return -1;
		
		_worlds[Index].ARock -= _worlds[Index].ARock;
		RockChanged.Invoke(new PercentageChanged((_worlds[Index].ARock/_worlds[Index].Rock)*100));
		return _worlds[Index].ARock;
	}
	
	public double GrabFood(double amount) 
	{
		if ((_worlds[Index].AFood - amount) > 0) {
			_worlds[Index].AFood -= amount;
			FoodChanged.Invoke(new PercentageChanged((_worlds[Index].AFood/_worlds[Index].Food)*100));
			return amount;
		} 
		
		if (_worlds[Index].AFood == 0)
			return -1;
		
		_worlds[Index].AFood -= _worlds[Index].AFood;
		return _worlds[Index].AFood;
	}
}
