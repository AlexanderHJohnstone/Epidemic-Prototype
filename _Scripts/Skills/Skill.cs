using UnityEngine;
using System.Collections;

public abstract class Skill {
	
	// Statistics
	public string name;				// The name of the skill.
	public string desc = "";		// The description of the skill.
	public int range;				// The range of the skill in tiles from the user.
	public int uses = -1;				// The number of uses a skill has left. -1 = infinite uses.
	public int capacity = -1;			// The maximum and initial number of uses an item has. -1 = infinite.
	
	// Holder References
	public Unit owner;
	
	// Sprite References
	public string icon;
	
	// **********************************************************************
	
	// GETTER METHODS

	public string GetName() { return name; }
	public int GetRange() { return range; }
	public int GetUses() { return uses; }
	public int GetCapacity() { return capacity; }
	public Unit GetOwner() { return owner; }
	public string GetIcon() { return icon; }
	
	// DISPLAY METHODS
	
	public void ShowRange() {
		Map ownerMap = owner.GetMap();
		Tile ownerTile = owner.GetTile();
		Tile targetTile;
		
		for(int x = ownerTile.Get_X() - range; x < ownerTile.Get_X() + range; x++) {
			for(int y = ownerTile.Get_Y() - range; y < ownerTile.Get_Y() + range; y++) {
				targetTile = ownerMap.Get_Tile(x, y);
				if(targetTile.Get_Open()) {
					targetTile.Set_Highlighted(true);
				}
			}
		}
	}
	
	// ACTION METHODS
	
	// Must be overriden. Returns true if the skill executes successfully.
	public abstract bool Perform();
	
	// VALIDATION METHODS
	
	// Returns true if the passed tile is within range of this skill, measured from the skill's owner.
	public bool InRange(Tile targetTile) {
		bool validTarget = false;
		
		for(int x = targetTile.Get_X() - range; x < targetTile.Get_X() + range; x++) {
			for(int y = targetTile.Get_Y() - range; y < targetTile.Get_Y() + range; y++) {
				if(targetTile.Get_Open()) {
					validTarget = true;
				}
			}
		}
		
		return validTarget;
	}
}
