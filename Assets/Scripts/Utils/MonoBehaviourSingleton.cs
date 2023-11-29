using UnityEngine;

namespace Utils
{
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T :MonoBehaviour
	{
		public static T Instance { get; private set; }

		public virtual void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this as T;
		
			DontDestroyOnLoad(gameObject);
		}
	}
}