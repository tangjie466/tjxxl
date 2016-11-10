using UnityEngine;
using System.Collections;
#region MonoBehaviour
public class Singleton<T> : MonoBehaviour where T:MonoBehaviour {

	private static T _instance = null;
	const string SingletonManagerName = "Singletons";
	static GameObject root = null;
	public static T Instance
	{
		get{
			if(root == null)
			{
				root = GameObject.Find(SingletonManagerName);
				if(root == null)
				{
					root = new GameObject(SingletonManagerName);
					DontDestroyOnLoad(root);
				}
			}
			if(_instance == null)
			{
				GameObject go = new GameObject(typeof(T).Name);
				_instance = go.AddComponent<T>();
				go.transform.parent = root.transform;
				DontDestroyOnLoad(go);
			}
			return _instance;
		}
	}
	public void DestroySelf()
	{
		if (_instance != null) {
			DestroyObject(_instance.gameObject);
			_instance = null;
		}
	}

	public virtual void Awake()
	{
		if(root == null)
		{
			root = GameObject.Find(SingletonManagerName);
			if(root == null)
			{
				root = new GameObject(SingletonManagerName);
				DontDestroyOnLoad(root);
			}
		}
		if (_instance == null) {
			GameObject go = new GameObject (typeof(T).Name);
			_instance = go.AddComponent<T> ();
			go.transform.parent = root.transform;
			DontDestroyOnLoad (go);

		} else {
			DestroyObject(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
#endregion

#region Normal
public class NormalSingleton<T> where T : class,new()
{
	private static T _instance;
	public static T Instance{
		get{
			if(_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}

	public void DestroySelf()
	{
		_instance = default(T);
	}

}

#endregion


