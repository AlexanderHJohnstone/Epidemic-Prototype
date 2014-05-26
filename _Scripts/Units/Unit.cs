using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	
	// Statistics
	public string name;							// The unit's display name.
	public int level = 1;						// The unit's experience level.
	public int move = 1;						// How many tiles this unit can move.
	public int hitpoints = 1;					// The unit's current hitpoints.
	public int hpMax = 10;						// The unit's maximum hitpoints.
	public int armor = 1;						// The unit's current armor rating.
	
	// Skill Array
	protected List<Skill> skills;
	protected Dictionary<string, int> passives;
	
	// State Flags
	public bool isDead = false;
	
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

	public void Set_Selected ()
	{
		Renderer[] tileRender = GetComponentsInChildren<Renderer>();
		foreach ( Renderer r in tileRender ) 
			r.material.color = Globals.Instance.SELECTED_COLOR; 
	}
}
