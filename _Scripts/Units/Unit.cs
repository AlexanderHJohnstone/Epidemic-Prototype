using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	
	// Statistics
	public string name;							// The unit's display name.
	public int level = 1;						// The unit's experience level.
	public int move = 7;						// How many tiles this unit can move.
	public int hitpoints = 1;					// The unit's current hitpoints.
	public int hpMax = 10;						// The unit's maximum hitpoints.
	public int armor = 1;						// The unit's current armor rating.
	
	// Skill Array
	protected List<Skill> skills;
	protected Dictionary<string, int> passives;
	
	// State Flags
	public bool isDead = false;
	public bool selected = false;
	
	// Map Data
	protected Tile currentTile;
	protected Map currentMap;
	
//*****************************************************************************************
	
	// *** GETTER METHODS
	public string GetName() { return name; }
	public int GetLevel() { return level; }
	public int GetMove() { return move; }
	public int GetHP() { return hitpoints; }
	public int GetHPMax() { return hpMax; }
	public int GetArmor() { return armor; }
	public Tile GetTile() { return currentTile; }
	public Map GetMap() { return currentMap; }

	// *** SETTER METHODS
	public void SetTile(Tile newTile) { currentTile = newTile; }
	
// *** INITIALIZATION METHODS 

	// Initializes this unit to a position on a given map.
	public void InitializeOnMap(Map map, Tile tile) {
		currentMap = map;
		currentTile = tile;
	}
	
// *** DAMAGE METHODS

	// Makes the unit take int in damage. Kills the unit (isDead = true) if hitpoints goes below 0.
	public void TakeDamage(int damage) {
		hitpoints -=  damage;
		if(hitpoints <= 0) { isDead = true; }
	}

	// Heals hitpoints on the unit up to hpMax.
	public void HealDamage(int heal) {
		if(!isDead) {
			hitpoints += heal;
			if(hitpoints > hpMax) { hitpoints = hpMax; }
		}
	}

// *** PASSIVE SKILL METHODS

	// Adds a passive skill and value entry into the unit's passive dictionary. 
	public void IncreasePassive(string passive, int value) {
		if(!passives.ContainsKey(passive)) { passives.Add (passive, value); }
		else { passives[passive] += value; }
	}

	// Removes a passive from the passive dictionary and it's associated value.
	public void RemovePassive(string passive) {
		passives.Remove(passive);
	}

	// Decreases a given passive entry by the supplied value. If the value decreases the passive to 0 or less, the passive entry is removed.
	public void DecreasePassive(string passive, int value) {
		if(passives.ContainsKey(passive)) { 
			passives[passive] -= value;
			if(passives[passive] <= 0) { passives.Remove(passive); }
		}
	}

	public int GetPassive(string passive) {
		if(passives.ContainsKey(passive)) { return passives[passive]; }
		else { return 0; }
	}

	public bool HasPassive(string passive) {
		if(passives.ContainsKey(passive)) { return true; }
		else { return false; }
	}

	public bool HasPassiveValue(string passive, int value) {
		if(passives.ContainsKey(passive)) {
			if(passives[passive] == value) { return true; }
			else { return false; }
		}
		else { return false; }
	}

	// Returns false if the unit can not move to the given tile.
	public bool MoveTo(Tile target) {
		if(target.Get_Open()) {
			Path pathToTile = Pathing.FindPath(currentTile, target);

			// Unit cannot move that far.
			if(pathToTile.Length > move) { 
				Set_Selected(false);
				return false;
			}

			// Unit performs the move.
			else {
				currentTile.Set_Open(true);
				AnimateOnPath(pathToTile);
				this.currentTile = target;
				target.Set_Open (false);
				return true;
			}
		}

		return false;
	}

	// MOVE TO UNIT ANIMATOR
	protected void AnimateOnPath(Path animPath) {
		float movetime = .3f;
		float count = 0;

		Set_Selected(false);

		foreach(Tile t in animPath.StartToEnd()) {
			iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3(t.gameObject.transform.position.x, t.gameObject.transform.position.y, t.gameObject.transform.position.z), "time", movetime, "delay", movetime * count));
			count++;
		}
	}


	// Highlights all tiles that this unit can path to in a given range.
	protected void DisplayMovableTiles(int range, bool show) {
		Tile markTile;

		if(!show) {
			for(int x = currentTile.Get_X() - range; x <= currentTile.Get_X() + range; x++) {
				for(int y = currentTile.Get_Y() - range; y <= currentTile.Get_Y() + range; y++) {
					if(x >= 0 && x < currentMap.GetMaxX() && y >= 0 && y < currentMap.GetMaxY()) { 
						markTile = currentMap.Get_Tile(x, y);
						markTile.Set_Selected(false);
					}
				}
			}
		}

		else if (show) {
			for(int x = currentTile.Get_X() - range; x <= currentTile.Get_X() + range; x++) {
				for(int y = currentTile.Get_Y() - range; y <= currentTile.Get_Y() + range; y++) {
					if(x >= 0 && x < currentMap.GetMaxX() && y >= 0 && y < currentMap.GetMaxY()) { 
						markTile = currentMap.Get_Tile(x, y);

						if(Pathing.ManhattanDistance(currentTile, markTile) <= range) {
							Path p = Pathing.FindPath(currentTile, markTile);
							if(p != null && p.Length <= range) { 
								print ("MXY: " + markTile.Get_X () + " " + markTile.Get_Y ());
								markTile.Set_Selected(show);
							}
						}
					}
				}
			}
		}
	}

	public void Set_Selected (bool value)
	{
		selected = value;

		Renderer[] tileRender = GetComponentsInChildren<Renderer>();

		if(selected) {
			foreach ( Renderer r in tileRender ) 
				r.material.color = Globals.Instance.SELECTED_COLOR; 
			DisplayMovableTiles(move, true);
		}
		else {
			foreach ( Renderer r in tileRender ) 
				r.material.color = Globals.Instance.NEUTRAL_COLOR; 
			DisplayMovableTiles(move, false);
		}
	}
}
