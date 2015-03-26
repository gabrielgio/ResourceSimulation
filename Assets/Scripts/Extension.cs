using System;
using System.Linq;

public class Tuple <T1, T2>
{
	public T1 Value1 { get; set; }
	public T2 Value2 { get; set; }
	
	public Tuple()
	{
	}
	
	public Tuple (T1 value1, T2 value2)
	{
		Value1 = value1;
        Value2 = value2;
    }
}

public class Tuple <T1, T2, T3>
{
	public T1 Value1 { get; set; }
	public T2 Value2 { get; set; }
	public T3 Value3 { get; set; }
	
	public Tuple()
	{
	}
	
	public Tuple (T1 value1, T2 value2)
	{
		Value1 = value1;
        Value2 = value2;
    }

	public Tuple (T1 value1, T2 value2, T3 value3)
	{
		Value1 = value1;
		Value2 = value2;
		Value3 = value3;
    }
}

public class Singleton<T> where T : new()
{
	private static T instance; 

	public static T Instance { 
		get {
			if (instance == null)
				instance = new T ();
			return instance;
		}
	}
}

public static class Extentions
{
	public static string FirstCharToUpper(this string input)
	{
		if (String.IsNullOrEmpty(input))
			throw new ArgumentException("ARGH!");
		return input.First().ToString().ToUpper() + input.Substring(1);
	}
}