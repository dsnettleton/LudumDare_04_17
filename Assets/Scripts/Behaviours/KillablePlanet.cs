//	KillablePlanet.cs
//	April 23, 2017
//	D. Scott Nettleton

using UnityEngine;

public class KillablePlanet : MonoBehaviour {

	[SerializeField] private Color hilightColor;

	private SpriteRenderer mySprite;
	private EffectsPool effectsPool;
	private bool hilighting = false;

	//	Unity methods

	private void Awake() {
		mySprite = gameObject.GetComponentInChildren<SpriteRenderer>();
		effectsPool = GameObject.Find("EffectsPool").GetComponent<EffectsPool>();
	}//	End Unity method Awake

	private void OnMouseEnter() {
		if (Game.getState() == Game.State.SelectingPlanet) {
			hilighting = true;
			mySprite.color = hilightColor;
		}
	}//	End Unity method OnMouseEnter

	private void OnMouseExit() {
		if (Game.getState() == Game.State.SelectingPlanet) {
			hilighting = false;
			mySprite.color = Color.white;
		}
	}//	End Unity method OnMouseExit

	private void OnMouseUpAsButton() {
		if (Game.getState() == Game.State.SelectingPlanet && hilighting) {
			mySprite.color = Color.white;
			destroyMe();
			EventHandler.raiseEvent(GameEvent.PlanetDestroyed);
		}
	}//	End Unity method OnMouseUpAsButton

	//	Additional methods

	private void destroyMe() {
		effectsPool.playEffect(EffectsPool.Effect.PlanetDestroyed, transform.position, transform.localScale);
		gameObject.SetActive(false);
	}//	End private method destroyMe

}//	End public class KillablePlanet
