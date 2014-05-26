using UnityEngine;
using System.Collections;

public static class PathFind {

	// Returns the shortest path of tiles in Manhattan distance from the currentTile to the supplied tile.
	static public Path FindPath(Tile start, Tile destination)
	{
		var closed = new HashSet<Tile>();
		var queue = new PathQueue();
		queue.Enqueue(0, new Path(start));
		while (!queue.IsEmpty)
		{
			var path = queue.Dequeue();
			if (closed.Contains(path.LastStep))
				continue;
			if (path.LastStep.Equals(destination))
				return path;
			closed.Add(path.LastStep);
			foreach(Tile n in path.LastStep.Neighbours)
			{
				double d = distance(path.LastStep, n);
				var newPath = path.AddStep(n, d);
				queue.Enqueue(newPath.TotalCost + estimate(n), newPath);
			}
		}
		return null;
	}
	
	protected int ManhattanDistance(Tile startTile, Tile endTile) {
		int xDist = Mathf.Abs (endTile.Get_X() - startTile.Get_X());
		int yDist = Mathf.Abs (endTile.Get_Y() - startTile.Get_Y());
		
		return xDist + yDist;
	}
}
