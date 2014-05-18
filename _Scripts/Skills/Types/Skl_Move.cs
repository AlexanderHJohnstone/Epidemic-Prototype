using UnityEngine;
using System.Collections;

/// <summary>
/// The basic skill "Move" shared by all units. Moves a unit to a tile within the unit's move range.
/// </summary>

public class Skl_Move : Skill {
	
	public Skl_Move(Unit unit) {
		name = "Move";
		owner = unit;
		range = owner.GetMove();
	} 
	
	public bool Perform(Tile moveTile) {
		if(InRange(moveTile)) {
			currentTile = moveTile;
			return true;
		}
		else { return false; }
	}
}
