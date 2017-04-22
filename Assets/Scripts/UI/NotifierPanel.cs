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
				badNewsDisplay.show("Ball out of bounds!");
				break;
			case GameEvent.InvalidPosition:
				badNewsDisplay.show("Cannot warp to that position.");
				break;
			case GameEvent.LevelWon:
				//	data = numStrokes
				if (data == 1) {
					goodNewsDisplay.showStatic("HOLE IN ONE!!!");
				} else {
					goodNewsDisplay.showStatic("Level Won!");
				}
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

}//	End public class NotifierPanel
