//	EventObserver.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class EventObserver : MonoBehaviour {

	private void OnEnable() {
		EventHandler.addObserver(this);
	}//	End Unity method OnEnable

	private void OnDisable() {
		EventHandler.removeObserver(this);
	}//	End Unity method OnDisable

	virtual public void OnNotify(GameEvent myEvent, int data) { }

}//	End abstract base class EventObserver
