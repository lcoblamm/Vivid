using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour 
{
	public static GlobalControl Instance;

	public Mode currMode ; 
	
	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
}