using UnityEngine;
using System.Collections;

/// <summary>
/// Ingeborg's Clockwork Pistol skill. A short range attack.
/// </summary>

public class Skl_ClockworkPistol : Skill {
	
	private int baseDamage = 3;
	
	public Skl_ClockworkPistol(Unit unit) {
		name = "Clockwork Pistol";
		owner = unit;
		range = 3;
	} 
	
	public bool Perform(Unit target) {
		if(InRange(target.GetTile())) {
			int revisedDamage = baseDamage - target.GetArmor();
			target.TakeDamage(revisedDamage);
			return true;
		}
		else { return false; }
	}
}
