using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameObjectPool<T> : BasePool<T> where T : MonoBehaviour,new(){
	int totleNum = 0;
	bool isGCback = false;
	const int MAX_NUM = 50;
	public override T getObject(string key)
	{
		T t = null;
		int  num = 0;

		if(poolTable.ContainsKey(key))
		{
			Stack<T> stack_pool = (Stack<T>)poolTable[key];
			num = stack_pool.Count;
			if(num > 0)
			{
				t = stack_pool.Pop();
				num--;
			}
		}else{
			Stack<T> stack_pool = new Stack<T>();
			poolTable.Add(key,stack_pool);
		}
		if(t == null)
		{
			t = CreateNew(key);
			t.name += ".clone";
		}


		string type_str = t.gameObject.name;
		type_str = type_str.Substring(0,type_str.LastIndexOf('.'));

		if(timeRecord.ContainsKey(type_str))
		{
			RecardData recard = (RecardData)timeRecord[key];
			recard.canUseNums = num;
			recard.lastTime = Time.time;
			recard.useTimes++;
			recard.nums++;
		}else{
			RecardData recard = new RecardData();
			recard.init();
			recard.useTimes++;
			recard.canUseNums = num;
			recard.nums++;
			timeRecord.Add(key,recard);
		}



		return t;
	}
	public T CreateNew(string path)
	{
		GameObject ob = Resources.Load(path) as GameObject;
		T t = Instantiate(ob,ob.transform.position,ob.transform.rotation) as T;
		totleNum++;
		if(totleNum > MAX_NUM)
		{
			isGCback = true;
		}
		return t;
	}

	public override void recoverObject(T t)
	{
		GameObject ob = t.gameObject;
		ob.SetActive(false);
		string type_str = ob.name.Substring(0,ob.name.LastIndexOf('.'));
		if(timeRecord.ContainsKey(type_str))
		{
			RecardData r = (RecardData)timeRecord[type_str];
			r.canUseNums ++ ;
			Stack<T> stack_t = (Stack<T>)poolTable[r.key];
			stack_t.Push(t);
		}else{
			Debug.LogError("pool does not have the key :"+type_str);
		}
	}
	public void Update()
	{
		doUpdate();
	}

	public override void doUpdate()
	{
		if(isGCback)
		{
			clearPool();
		}
	}
	public void clearPool()
	{

	}

	public override void Destroy ()
	{
		base.Destroy ();
		GameObject ob = null;
		Stack<MonoBehaviour> st = null;
		foreach(DictionaryEntry dt in poolTable)
		{
			st = (Stack<MonoBehaviour>)dt.Value;
			ob = ((MonoBehaviour)st.Pop()).gameObject;
			while(ob != null)
			{
				DestroyObject(ob);
				ob = ((MonoBehaviour)st.Pop()).gameObject;
			}
			ob = null;
			st.Clear();
			st = null;
		}
		poolTable.Clear();
		poolTable = null;

		foreach(DictionaryEntry dt in timeRecord)
		{
			RecardData rd = (RecardData)dt.Value;
			rd = null;
		}
		timeRecord.Clear();
		timeRecord = null;
	}
}



