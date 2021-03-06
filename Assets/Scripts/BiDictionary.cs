﻿using System;
using System.Collections.Generic;
using System.Text;

public class BiDictionary<TFirst, TSecond>
{
	IDictionary<TFirst, IList<TSecond>> firstToSecond = new Dictionary<TFirst, IList<TSecond>>();
	IDictionary<TSecond, IList<TFirst>> secondToFirst = new Dictionary<TSecond, IList<TFirst>>();

	private static IList<TFirst> EmptyFirstList = new TFirst[0];
	private static IList<TSecond> EmptySecondList = new TSecond[0];

	public void Add(TFirst first, TSecond second)
	{
		IList<TFirst> firsts;
		IList<TSecond> seconds;
		if (!firstToSecond.TryGetValue(first, out seconds))
		{
			seconds = new List<TSecond>();
			firstToSecond[first] = seconds;
		}
		if (!secondToFirst.TryGetValue(second, out firsts))
		{
			firsts = new List<TFirst>();
			secondToFirst[second] = firsts;
		}
		seconds.Add(second);
		firsts.Add(first);
	}

	public void Remove(TFirst first)
	{
		IList<TSecond> second;
		if (!firstToSecond.TryGetValue(first, out second))
			throw new ArgumentException("first");

		firstToSecond.Remove(first);
		secondToFirst.Remove(second[0]);
	}

	public bool ContainsKey(TFirst first)
	{
		return firstToSecond.ContainsKey(first);
	}

	// Note potential ambiguity using indexers (e.g. mapping from int to int)
	// Hence the methods as well...
	public IList<TSecond> this[TFirst first]
	{
		get { return GetValue(first); }
	}

	public IList<TFirst> this[TSecond second]
	{
		get { return GetKey(second); }
	}

	public IList<TSecond> GetValue(TFirst first)
	{
		IList<TSecond> list;
		if (!firstToSecond.TryGetValue(first, out list))
		{
			return EmptySecondList;
		}
		return new List<TSecond>(list); // Create a copy for sanity
	}

	public IList<TFirst> GetKey(TSecond second)
	{
		IList<TFirst> list;
		if (!secondToFirst.TryGetValue(second, out list))
		{
			return EmptyFirstList;
		}
		return new List<TFirst>(list); // Create a copy for sanity
	}
}