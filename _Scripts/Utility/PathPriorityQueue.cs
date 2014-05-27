using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//A prioritized queue of paths for pathfinding.

public class PathPriorityQueue
{
	private List<Path> list = new List<Path>();

	public void Enqueue(Path path)
	{
		list.Add (path);
		list.Sort ();
	}
	public Path Dequeue()
	{
		// will throw if there isn’t any first element!
		var pathToGet = list[0];
		list.Remove(pathToGet);
		return pathToGet;
	}
	public bool IsEmpty
	{
		get{ 
		 	if(list.Count == 0) { return true; }
			else { return false; }
		}
	}
}