using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// An immutable stack of Tiles for Pathfinding.

public class Path : IEnumerable, IComparable<Path>
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
		//Debug.Log ("Step: " + lastStep.Get_X() + " " + lastStep.Get_Y ());
	}

	public Path(Tile start) : this(start, null, 0) {}

	public Path AddStep(Tile step, double stepCost)
	{
		return new Path(step, this, TotalCost + stepCost);
	}

	public int CompareTo(Path obj) {
		Path p = (Path)obj;
		return TotalCost.CompareTo(p.TotalCost);
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
