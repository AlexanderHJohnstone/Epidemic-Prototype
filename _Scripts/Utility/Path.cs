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
	public int Length { get; private set; }

	//*********************************************************************

	private Path(Tile lastStep, Path previousSteps, double totalCost, int length)
	{
		LastStep = lastStep;
		PreviousSteps = previousSteps;
		TotalCost = totalCost;
		Length = length;
		//Debug.Log ("Step: " + lastStep.Get_X() + " " + lastStep.Get_Y ());
	}

	public Path(Tile start) : this(start, null, 0, 0) {}

	public Path AddStep(Tile step, double stepCost)
	{
		return new Path(step, this, TotalCost + stepCost, Length + 1);
	}

	public int CompareTo(Path obj) {
		Path p = (Path)obj;
		return TotalCost.CompareTo(p.TotalCost);
	}

	public List<Tile> StartToEnd() {
		List<Tile> toReturn = new List<Tile>();
		foreach(Tile t in this) {
			toReturn.Add (t);
		}

		toReturn.Reverse();

		return toReturn;
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
