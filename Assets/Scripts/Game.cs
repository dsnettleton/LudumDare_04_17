//	GameState.cs
//	April 22, 2017
//	D. Scott Nettleton

public class Game {

	public enum State {
		Menu,
		Running,
		WaitingOnBall,
		Paused,
		SelectingPlanet,
		SelectingSpace
	}//	End State enum

	public enum Layers : int {
		Default = 0,
		TransparentFX = 1,
		IgnoreRaycast = 2,
		UI = 5,
		UFO = 8,
		GolfBall = 9,
		Planets = 10,
		Gravity = 11,
		ScreenBounds = 12,
		GameBounds = 13,
		Hole = 14
	}//	End Layers enum

	public const int NUM_LEVELS = 3;

	public static int currentLevel = 2;
	public static int lastCompletedLevel = 0;

	private static State currentState = State.Running;
	private static State prevState = State.Running;
	private static float soundVolume = 0.5f;
	private static float musicVolume = 0.25f;
	private static float masterVolume = 1.0f;

	public static float getMasterVolume() { return masterVolume; }
	public static float getMusicVolume() { return musicVolume * masterVolume; }
	public static float getMusicVolumeUnscaled() { return musicVolume; }
	public static float getSoundVolume() { return soundVolume * masterVolume; }
	public static float getSoundVolumeUnscaled() { return soundVolume; }
	public static State getState() { return currentState; }

	public static void returnToPreviousState() {
		State tmpState = prevState;
		prevState = currentState;
		currentState = tmpState;
	}//	End public static method returnToPreviousState

	public static void setMasterVolume(float value) {
		masterVolume = value;
		EventHandler.raiseEvent(GameEvent.VolumeChanged);
	}//	End public static method setMasterVolume

	public static void setMusicVolume(float value) {
		musicVolume = value;
		EventHandler.raiseEvent(GameEvent.VolumeChanged);
	}//	End public static method setMusicVolume

	public static void setSoundVolume(float value) {
		soundVolume = value;
		EventHandler.raiseEvent(GameEvent.VolumeChanged);
	}//	End public static method setSoundVolume

	public static void setState(State value) {
		prevState = currentState;
		currentState = value;
	}//	End public static method setState

}//	End class Game
