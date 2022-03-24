using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T :MonoBehaviour
{
	public static T Instance { get; set; }

	public virtual void Awake()
	{
		if (Instance != null)
			Destroy(gameObject);
			
		DontDestroyOnLoad(gameObject);
		Instance = this as T;
	}
}