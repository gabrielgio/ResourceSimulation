using UnityEngine;
using System.Collections;

public class WorldStation : MonoBehaviour {

	public double Wood;

	public double Rock;

	public double Food;

	//TODO: temporary
	private double _wood;
	
	private double _rock;
	
	private double _food;

	public PercentageChangedEvent WoodChanged;

	public PercentageChangedEvent RockChanged;

	public PercentageChangedEvent FoodChanged;

	// Use this for initialization
	void Start () {
		_wood = Wood;
		_rock = Rock;
		_food = Food;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public double ChopTree(double amount)
	{


		if ((Wood - amount) > 0) {
			Wood -= amount;
			WoodChanged.Invoke(new PercentageChanged((Wood/_wood)*100));
			return amount;
		} 

		if (Wood == 0)
			return -1;

		Wood -= Wood;
		WoodChanged.Invoke(new PercentageChanged((Wood/_wood)*100));
		return Wood;
	}
	
	public double MineRock(double amount)
	{
		if ((Rock - amount) > 0) {
			Rock -= amount;
			RockChanged.Invoke(new PercentageChanged((Rock/_rock)*100));
			return amount;
		} 
		
		if (Rock == 0)
			return -1;
		
		Rock -= Rock;
		RockChanged.Invoke(new PercentageChanged((Rock/_rock)*100));
		return Rock;
	}
	
	public double GrabFood(double amount) 
	{
		if ((Food - amount) > 0) {
			Food -= amount;
			FoodChanged.Invoke(new PercentageChanged((Food/_food)*100));
			return amount;
		} 
		
		if (Food == 0)
			return -1;
		
		Food -= Food;
		FoodChanged.Invoke(new PercentageChanged((Food/_food)*100));
		return Food;
	}
}
