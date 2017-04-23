//	ToolsPanel.cs
//	April 23, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

public class ToolsPanel : EventObserver {

	[SerializeField] private Button planetKillerButton;

	private SpaceShip player;

	//	Unity methods

	private void Start() {
		findPlayer();
	}//	End Unity method Start

	//	Additional methods

	public override void OnNotify(GameEvent myEvent, int data) {
		switch (myEvent) {
			default:
				break;
			case GameEvent.LevelLoaded:
				findPlayer();
				break;
			case GameEvent.PlanetDestroyed:
				Game.setState(Game.State.Running);
				player.incrementStrokes(2);
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

	private void findPlayer() {
		GameObject ufo = GameObject.Find("UFO");
		player = ufo.GetComponent<SpaceShip>();
	}//	End private method findPlayer

	public void initiatePlanetKiller() {
		if (player == null) {
			findPlayer();
		}
		if (Game.getState() == Game.State.Running && player != null) {
			Game.setState(Game.State.SelectingPlanet);
			EventHandler.raiseEvent(GameEvent.PlanetKillerInitiated);
		} else if (Game.getState() == Game.State.SelectingPlanet) {
			Game.setState(Game.State.Running);
			EventHandler.raiseEvent(GameEvent.PlanetKillerCanceled);
		}
	}//	End public method initiatePlanetKiller

}//	End public class ToolsPanel
