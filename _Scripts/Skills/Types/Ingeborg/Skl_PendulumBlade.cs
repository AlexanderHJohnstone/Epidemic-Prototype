using UnityEngine;
using System.Collections;

/// <summary>
/// Ingeborg's Pendulum Axe skill. A melee attack that gains damage on each swing.
/// </summary>

public class Skl_PendulumBlade : Skill {
		
	private int baseDamage = 2;
	private int pendulumDamage = 0;			// Cumulative damage added from each swing.
	private int pendulumMaxAdd = 6;			// Maximum damage bonus from swing count.
		
	public Skl_PendulumBlade(Unit unit) {
		name = "Pendulum Blade";
		owner = unit;
		range = 1;
		pendulumDamage = 0;
	} 
	
	public bool Perform(Unit target) {
		if(InRange(target.GetTile())) {
			int pendulumAdd = pendulumDamage + owner.GetPassive("Accelerated");
			if(pendulumAdd > pendulumMaxAdd) { pendulumAdd = pendulumMaxAdd; }

			int revisedDamage = baseDamage + pendulumAdd - target.GetArmor();

			target.TakeDamage(revisedDamage);
			pendulumDamage++;
			return true;
		}
		else { return false; }
	}
}
