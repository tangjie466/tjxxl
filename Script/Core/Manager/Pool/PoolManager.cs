using UnityEngine;
using System.Collections;

public class PoolManager : Singleton<PoolManager> {
	GameObject goRoot = null;
	GameObjectPool<MonoBehaviour> go_pool = null;
	// Use this for initialization
	public override void Awake ()
	{
		base.Awake ();
		if(goRoot != null)
		{
			goRoot = new GameObject ("PoolManager");
		}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createGameObjectPool()
	{
		GameObject ob = new GameObject("GameObjectPool");
		ob.transform.parent = goRoot.transform;
	}

	public void destroyGameObjectPool()
	{
		if(go_pool != null)
		{
			go_pool.Destroy();
		}
	}

}
