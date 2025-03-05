
using UnityEngine;

public class EstelleBroom : ClickableUIMessage {
	protected override void PerformAction(Collider2D hitCollider) {
		if (GameManager.EnergySourcePickedUp) {
			
		} else {
			base.PerformAction(hitCollider);	
		}
	}
}
