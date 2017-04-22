//	StrokeCounter.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

public class StrokeCounter : EventObserver {

	[SerializeField] private Text strokeText;

	public override void OnNotify(GameEvent myEvent, int data) {
		switch (myEvent) {
			default:
				break;
			case GameEvent.StrokeTaken:
				//	data = numStrokes
				strokeText.text = data.ToString();
				break;
		}//	End event type switch
	}//	End EventObserver method OnNotify

}//	End public class StrokeCounter
