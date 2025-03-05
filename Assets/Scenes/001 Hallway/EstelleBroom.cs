
using UnityEngine;

public class EstelleBroom : ClickableUIMessage {
	[SerializeField] private AudioEvent _startBroomEvent;
	
	protected override void PerformAction(Collider2D hitCollider) {
		if (GameManager.EnergySourcePickedUp) {
			_startBroomEvent.Play(SoundPlayer.GetSFXSource);
			
		} else {
			base.PerformAction(hitCollider);	
		}
	}
}
