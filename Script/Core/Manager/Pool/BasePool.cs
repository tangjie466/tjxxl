using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface IPool<T> 
{
	T getObject(string key);
	void recoverObject(T t);
	void doUpdate();
}

public class BasePool<T> : MonoBehaviour,IPool<T>{
	public class RecardData
	{
		public float beginTime;//开始使用时间
		public float lastTime;//上次使用时间
		public float useTimes;//使用次数
		public float nums; //实例个数
		public float canUseNums;//池中可用数量
		public string key;//池中的查找键
		public RecardData()
		{
			beginTime = 0;
			lastTime = 0;
			useTimes = 0;
			nums = 0;
			canUseNums = 0;
			key = string.Empty;
		}
		public void init()
		{
			float cur_time = Time.time;;
			beginTime = cur_time;
			lastTime = cur_time;
			useTimes = 1;
			nums = 1;
			canUseNums = 0;
			key = string.Empty;
		}
	};

	protected Hashtable poolTable = new Hashtable (); // 对象池
	protected Hashtable timeRecord = new Hashtable(); //记录池中每种对象的使用信息

	protected int max = 50;//池中最大尺寸

	public void setMax(int max)
	{
		this.max = max;
	}
	public virtual T getObject(string t)
	{


		return default(T);
	}

	public virtual void recoverObject(T t)
	{

	}
	public virtual void doUpdate()
	{

	}

	public virtual void Destroy()
	{

	}
}
