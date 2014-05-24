using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	
	// Statistics
	public string name;
	public int level = 1;
	public int move = 1;
	public int hitpoints = 1;
	public int hpMax = 10;
	public int armor = 1;
	
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
	
	public void InitializeOnMap(Map map, Tile tile) {
		currentMap = map;
		currentTile = tile;
	}
	
	// *** DAMAGE METHODS
	
	public void TakeDamage(int damage) {
		hitpoints -=  damage;
		if(hitpoints <= 0) { isDead = true; }
	}
	
	public void HealDamage(int heal) {
		if(!isDead) {
			hitpoints += heal;
			if(hitpoints > hpMax) { hitpoints = hpMax; }
		}
	}

	// *** ACTIVE SKILL METHODS

	public void AddPassive(string passive, int value) {
		if(!passives.ContainsKey(passive)) { passives.Add (passive, value); }
		else { passives[passive] += value; }
	}

	public void RemovePassive(string passive) {
		passives.Remove(passive);
	}

	public void RemovePassive(string passive, int value) {
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

	public void Selected ()
	{
		Renderer[] tileRender = GetComponentsInChildren<Renderer>();
		foreach ( Renderer r in tileRender ) 
			r.material.color = Globals.Instance.SELECTED_COLOR; 
	}
}
