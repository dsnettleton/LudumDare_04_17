//	EventHandler.cs
//	April 22, 2017
//	D. Scott Nettleton

using System.Collections.Generic;

public enum GameEvent {
	StrokeTaken,
	BallOutOfBounds,
	InvalidPosition,
	LevelWon,
	GamePaused,
	LevelLoaded,
	VolumeChanged,
	PlanetKillerInitiated,
	PlanetKillerCanceled,
	PlanetDestroyed
}//	End enum GameEvent

public class EventHandler {

	private static List<EventObserver> observers = new List<EventObserver>();
	private static int numObservers = 0;

	public static void addObserver(EventObserver my) {
		observers.Add(my);
		++numObservers;
	}//	End static method addObserver

	public static void removeObserver(EventObserver my) {
		observers.Remove(my);
		--numObservers;
	}//	End static method removeObserver

	public static void raiseEvent(GameEvent myEvent, int data = 0) {
		for (int i = 0; i < numObservers; ++i) {
			observers[i].OnNotify(myEvent, data);
		}//	End for each event observer
	}//	End static method raiseEvent

}//	End class EventHandler
