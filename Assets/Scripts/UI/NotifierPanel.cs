//	NotifierPanel.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class NotifierPanel : EventObserver {

	[SerializeField] private NewsDisplay goodNewsDisplay;
	[SerializeField] private NewsDisplay badNewsDisplay;

	public override void OnNotify(GameEvent myEvent, int data) {
		switch (myEvent) {
			default:
				break;
			case GameEvent.BallOutOfBounds:
				badNewsDisplay.show("Ball is Out of Bounds!");
				break;
			case GameEvent.InvalidPosition:
				badNewsDisplay.show("Cannot Warp to That Position.");
				break;
			case GameEvent.LevelWon:
				//	data = numStrokes
				if (data == 1) {
					goodNewsDisplay.showStatic("HOLE IN ONE!!!");
				} else {
					goodNewsDisplay.showStatic("Level Won!");
				}
				break;
			case GameEvent.LevelLoaded:
				goodNewsDisplay.hide();
				badNewsDisplay.hide();
				break;
			case GameEvent.PlanetKillerInitiated:
				goodNewsDisplay.show("Planet Killer Initiated. Please Select a Target Planet.");
				break;
			case GameEvent.PlanetKillerCanceled:
				goodNewsDisplay.show("Planet Killer Canceled.");
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

}//	End public class NotifierPanel
