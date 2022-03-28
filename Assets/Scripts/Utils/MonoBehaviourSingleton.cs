using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T :MonoBehaviour
{
	public static T Instance { get; private set; }

	public virtual void Awake()
	{
		if (Instance == null)
			Instance = this as T;
		else if (Instance == this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}
}