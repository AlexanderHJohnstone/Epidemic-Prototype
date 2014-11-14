using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Pathing {

	// Returns the shortest path of tiles in Manhattan distance from the currentTile to the supplied tile.
	public static Path FindPath(Tile start, Tile destination)
	{
		var closed = new HashSet<Tile>();
		var queue = new PathPriorityQueue();
		queue.Enqueue(new Path(start));
		while (!queue.IsEmpty)
		{
			var path = queue.Dequeue();
			if (closed.Contains(path.LastStep))
				continue;
			if (path.LastStep.Equals(destination)) {
				/*int count = 0;
				foreach(Tile t in path) {
					Debug.Log(count + ": " + t.Get_X() + "," + t.Get_Y());
					count++;
				}*/
				return path;
			}
			closed.Add(path.LastStep);
			foreach(Tile n in path.LastStep.Get_OpenNeighbors())
			{
				double d = 1 + ManhattanDistance(n, destination);
				var newPath = path.AddStep(n, d);
				queue.Enqueue(newPath);
			}
		}
		return null;
	}
	
	public static int ManhattanDistance(Tile startTile, Tile endTile) {
		int xDist = Mathf.Abs (endTile.Get_X() - startTile.Get_X());
		int yDist = Mathf.Abs (endTile.Get_Y() - startTile.Get_Y());
		
		return xDist + yDist;
	}
}
