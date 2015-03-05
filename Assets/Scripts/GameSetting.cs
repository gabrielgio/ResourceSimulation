using UnityEngine;
using System.Collections;

public class GameSetting{


	static private GameSetting instance;
	
	public const string TITLE = "TITLE - RESOURCE SIMULATION";


	/// <summary>
	/// Singleton instance of GameSettigs.
	/// </summary>
	/// <value>The instance.</value>
	static public GameSetting Instance 
	{
		get{
			if (instance == null)
				instance = new GameSetting();

			return instance;
		}
	}

	//making constructor private, to be only instance of GameSetting
	private GameSetting()
	{

	}
}
