using UnityEngine;
using System.Collections;

/// <summary>
/// Ingeborg's Clockwork Accelerator skill. Increases pendulum blade damage when used on self.
/// </summary>

public class Skl_Accelerator : Skill {
	
	private int pendulumIncrease = 2;
	
	public Skl_Accelerator(Unit unit) {
		name = "Clockwork Accelerator";
		uses = 2;
		capacity = 2;
		owner = unit;
		range = 0;
	} 

	public bool Perform(Unit target) {
		if(InRange(target.GetTile()) && target == owner) {
			owner.AddPassive("Accelerated" , pendulumIncrease);
			return true;
		}
		else { return false; }
	}
}
