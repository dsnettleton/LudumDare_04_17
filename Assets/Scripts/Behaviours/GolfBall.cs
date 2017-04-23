//	GolfBall.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class GolfBall : MonoBehaviour {

	private const float LAUNCH_COLLIDER_TIMEOUT = 3.0f;
	private const float MAX_LIFETIME = 10.0f;
	private const float MAX_TIME_STOPPED = 0.3f;
	private const float MIN_SQR_MAGNITUDE = 0.03f;
	private const float SCREEN_BOUNDS_X = 58.0f;
	private const float SCREEN_BOUNDS_Y = 32.0f;

	private Rigidbody2D body2D;
	private float lifetime = 0.0f;
	private float timeStopped = 0.0f;
	private bool spaceshipCollisionsDisabled = false;
	private EffectsPool effectsPool;
	private SpaceShip parentShip;
	[SerializeField] private AudioSource bounceSound;

	//	Unity methods

	private void Awake() {
		body2D = GetComponent<Rigidbody2D>();
		effectsPool = GameObject.Find("EffectsPool").GetComponent<EffectsPool>();
	}//	End Unity method Awake

	private void OnCollisionEnter2D(Collision2D coll) {
		int otherLayer = coll.gameObject.layer;
		if (otherLayer == (int)Game.Layers.UFO) {
			killMe();
		} else if (otherLayer == (int)Game.Layers.Planets) {
			bounceSound.volume = Game.getSoundVolume();
			bounceSound.Play();
		} else if (otherLayer == (int)Game.Layers.Hole) {
			EventHandler.raiseEvent(GameEvent.LevelWon, parentShip.getNumStrokes());
			killMe();
		}
	}//	End Unity method OnCollisionEnter2D

	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == (int)Game.Layers.GameBounds) {
			killMe();
		}
	}//	End Unity method OnTriggerExit2D

	private void Update() {
		lifetime += Time.deltaTime;
		if (spaceshipCollisionsDisabled && lifetime > LAUNCH_COLLIDER_TIMEOUT) {
			enableSpaceshipCollisions();
		}
		if (body2D.velocity.sqrMagnitude <= MIN_SQR_MAGNITUDE) {
			timeStopped += Time.deltaTime;
			if (timeStopped >= MAX_TIME_STOPPED) {
				killMe();
			}
		} else {
			timeStopped = 0.0f;
		}
		if (lifetime >= MAX_LIFETIME) {
			killMe();
		}
	}//	End Unity method Update

	//	Addtional methods

	private Vector2 calcNearestWarp() {
		Vector3 pos = transform.position;
		int planetsLayerMask = (1 << (int)Game.Layers.Planets);
		Collider2D overlap = Physics2D.OverlapCircle(pos, SpaceShip.RADIUS, planetsLayerMask);
		int maxAttempts = 5;
		int numAttempts = 1;
		while (overlap != null && numAttempts < maxAttempts) {
			pos += SpaceShip.RADIUS * (pos - overlap.transform.position).normalized;
			overlap = Physics2D.OverlapCircle(pos, SpaceShip.RADIUS, planetsLayerMask);
			++numAttempts;
		}//	End while we are overlapping on our position
		if (overlap == null) {
			return pos;
		}
		EventHandler.raiseEvent(GameEvent.InvalidPosition);
		return parentShip.transform.position;
	}//	End private method calcNearestWarp

	private void disableSpaceshipCollisions() {
		spaceshipCollisionsDisabled = true;
		Physics2D.IgnoreLayerCollision((int)Game.Layers.UFO, (int)Game.Layers.GolfBall, true);
	}//	End private method disableSpaceshipCollisions

	private void enableSpaceshipCollisions() {
		spaceshipCollisionsDisabled = false;
		Physics2D.IgnoreLayerCollision((int)Game.Layers.UFO, (int)Game.Layers.GolfBall, false);
	}//	End private method enableSpaceshipCollisions

	public bool inScreenBounds() {
		float posX = transform.position.x;
		float posY = transform.position.y;
		return (posX >= -SCREEN_BOUNDS_X && posX <= SCREEN_BOUNDS_X && posY >= -SCREEN_BOUNDS_Y && posY <= SCREEN_BOUNDS_Y);
	}//	End public method inScreenBounds

	public void killMe() {
		effectsPool.playEffect(EffectsPool.Effect.BallExplosion, transform.position);
		if (inScreenBounds()) {
			Vector2 closestPosition = calcNearestWarp();
			parentShip.warpToPosition(closestPosition);
		} else {
			EventHandler.raiseEvent(GameEvent.BallOutOfBounds);
		}
		Game.setState(Game.State.Running);
		gameObject.SetActive(false);
	}//	End public method killMe

	public void launch(Vector3 fromPos, Vector2 launchVelocity, SpaceShip _parentShip) {
		parentShip = _parentShip;
		transform.position = fromPos;
		lifetime = 0.0f;
		disableSpaceshipCollisions();
		gameObject.SetActive(true);
		body2D.velocity = launchVelocity;
	}//	End public method launch

	public void pull(Vector2 targetPosition, float strength) {
		Vector2 adustedVelocity = body2D.velocity;
		Vector2 pullDirection = (targetPosition - (Vector2)transform.position).normalized;
		adustedVelocity += pullDirection * strength;
		body2D.velocity = adustedVelocity;
	}//	End public method pull

}//	End public GolfBall class
