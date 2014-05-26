using UnityEngine;
using System.Collections;

// An immutable stack of Tiles for Pathfinding.

public class Path : IEnumerable
{
	public Tile LastStep { get; private set; }
	public Path PreviousSteps { get; private set; }
	public double TotalCost { get; private set; }

	//*********************************************************************

	private Path(Tile lastStep, Path previousSteps, double totalCost)
	{
		LastStep = lastStep;
		PreviousSteps = previousSteps;
		TotalCost = totalCost;
	}

	public Path(Tile start) : this(start, null, 0) {}

	public Path AddStep(Tile step, double stepCost)
	{
		return new Path(step, this, TotalCost + stepCost);
	}

	public IEnumerator GetEnumerator()
	{
		for (Path p = this; p != null; p = p.PreviousSteps)
			yield return p.LastStep;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}
}
