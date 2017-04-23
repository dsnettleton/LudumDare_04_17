//	SpaceShip.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class SpaceShip : MonoBehaviour {

	[SerializeField] private SpriteRenderer targetingReticule;
	[SerializeField] private float mouseDragRadius = 10.0f;
	[SerializeField] private GameObject warpEffect;

	public const float RADIUS = 2.2f;

	private const float MIN_RETICULE_LENGTH = 0.5f;
	private const float MAX_RETICULE_LENGTH = 1.5f;
	private const float LAUNCH_POWER = 40.0f;
	private const float WARP_DELAY = 0.5f;

	private const int BALL_POOL_SIZE = 5;
	private GolfBall[] ballPool;
	private int poolIndex = 0;

	private bool liningUpShot = false;
	private Camera mainCamera;
	private int numStrokes = 0;
	private int consecutiveStrokesAtFullPower = 0;

	private AudioSource audioSource;
	private AudioClip launchClip;
	private AudioClip warpClip;

	//	Unity methods

	private void Start() {
		if (targetingReticule == null) {
			Debug.LogError("Targeting Reticule transform has not been set.");
		}
		targetingReticule.gameObject.SetActive(false);

		if (warpEffect == null) {
			Debug.LogError("Must set a warp effect for this object.");
		}

		mainCamera = Camera.main;
		if (mainCamera == null) {
			Debug.LogError("Could not find main camera.");
		}

		GameObject golfBallPrefab = (GameObject)Resources.Load("Prefabs/GolfBall");
		ballPool = new GolfBall[BALL_POOL_SIZE];
		for (int i = 0; i < BALL_POOL_SIZE; ++i) {
			GameObject tmpObj = GameObject.Instantiate(golfBallPrefab);
			ballPool[i] = tmpObj.GetComponent<GolfBall>();
			tmpObj.SetActive(false);
		}//	End for each ball in the object pool

		audioSource = GetComponent<AudioSource>();
		launchClip = (AudioClip)Resources.Load("SoundFX/launch");
		warpClip = (AudioClip)Resources.Load("SoundFX/wormhole");
	}//	End private Unity method Start

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, mouseDragRadius);
	}//	End private Unity method OnDrawGizmos

	private void OnMouseDown() {
		if (Game.getState() == Game.State.Running && !liningUpShot) {
			liningUpShot = true;
			Vector2 shotVector = getShotVector();
			targetingReticule.gameObject.SetActive(true);
			setTargetingReticule(shotVector);
			Commentator.raiseEvent(CommentEvent.LiningUpShot);
		}//	End if we are able to select a target
	}//	End private Unity method OnMouseDown

	private void OnMouseDrag() {
		if (liningUpShot) {
			liningUpShot = true;
			Vector2 shotVector = getShotVector();
			setTargetingReticule(shotVector);
		}//	End if we're currently lining up a shot
	}//	End private Unity method OnMouseDrag

	private void OnMouseUp() {
		if (liningUpShot) {
			targetingReticule.gameObject.SetActive(false);
			fire(getShotVector());
		}
		liningUpShot = false;
	}//	End private Unity method OnMouseUp

	//	Additional Methods

	private void fire(Vector2 shotVector) {
		GolfBall myBall = nextInPool();
		if (myBall == null) { return; }
		incrementStrokes();
		if (shotVector.sqrMagnitude > 0.99f) {
			++consecutiveStrokesAtFullPower;
			if (consecutiveStrokesAtFullPower >= Commentator.FULL_POWER_STROKES_THRESHOLD) {
				Commentator.raiseEvent(CommentEvent.AlwaysFullPower);
			}
		} else {
			consecutiveStrokesAtFullPower = 0;
		}
		playLaunchClip();
		myBall.launch(transform.position, shotVector * LAUNCH_POWER, this);
		Game.setState(Game.State.WaitingOnBall);
	}//	End private method fire

	public int getNumStrokes() { return numStrokes; }

	//	Returns a directional vector scaled in range of magnitude = (0.1, 1.0)
	private Vector2 getShotVector() {
		if (mouseDragRadius <= 0.0f) {
			Debug.LogError("Please set a positive radius for the shot lineup range.");
		}
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = transform.position.z;
		mousePos = mainCamera.ScreenToWorldPoint(mousePos);
		Vector2 mouseVec = (Vector2)(mousePos - transform.position);
		float dragDistance = mouseVec.magnitude;
		mouseVec.Normalize();
		if (dragDistance > mouseDragRadius) { dragDistance = mouseDragRadius; }
		mouseVec *= Mathf.Lerp(0.1f, 1.0f, dragDistance / mouseDragRadius);
		return -mouseVec;
	}//	End private method getShotVector

	public void incrementStrokes(int numToIncrement = 1) {
		numStrokes += numToIncrement;
		EventHandler.raiseEvent(GameEvent.StrokeTaken, numStrokes);
		if (numStrokes >= Commentator.MANY_STROKES_THRESHOLD) {
			Commentator.raiseEvent(CommentEvent.ManyStrokes);
		}
	}//	End public method incrementStrokes

	private GolfBall nextInPool() {
		int attempts = 0;
		while (attempts < BALL_POOL_SIZE && ballPool[poolIndex].gameObject.activeSelf) {
			++attempts;
			++poolIndex;
			if (poolIndex >= BALL_POOL_SIZE) { poolIndex = 0; }
		}
		if (attempts >= BALL_POOL_SIZE) {
			return null;
		}
		return ballPool[poolIndex];
	}//	End private method nextPool

	public void playLaunchClip() {
		if (audioSource != null && launchClip != null) {
			audioSource.clip = launchClip;
			audioSource.volume = Game.getSoundVolume();
			audioSource.Play();
		}
	}//	End public method playLaunchClip

	public void playWarpClip() {
		if (audioSource != null && warpClip != null) {
			audioSource.clip = warpClip;
			audioSource.volume = Game.getSoundVolume();
			audioSource.Play();
		}
	}//	End public method playWarpClip

	private void setTargetingReticule(Vector2 shotVector) {
		float shotPower = shotVector.magnitude;
		Transform reticuleTransform = targetingReticule.transform;
		if (shotPower >= 0.99f) {
			targetingReticule.color = Color.red;
		} else {
			targetingReticule.color = Color.white;
		}

		float reticuleLength = Mathf.Lerp(MIN_RETICULE_LENGTH, MAX_RETICULE_LENGTH, shotPower);
		Vector3 reticuleScale = Vector3.one;
		reticuleScale.x = reticuleLength;
		reticuleTransform.localScale = reticuleScale;

		Vector3 reticulePosition = Vector3.right * (shotPower * mouseDragRadius * 0.5f);
		reticuleTransform.localPosition = reticulePosition;
		reticuleTransform.rotation = Quaternion.identity;

		float reticuleAngle = Mathf.Atan2(shotVector.y, shotVector.x) * Mathf.Rad2Deg;
		// reticuleAngle += 180.0f;
		reticuleTransform.RotateAround(reticuleTransform.parent.position, Vector3.forward, reticuleAngle);
	}//	End private method setTargetingReticule

	private IEnumerator warpCoroutine(Vector2 newPos) {
		yield return new WaitForSeconds(WARP_DELAY);
		if (Vector2.Distance(newPos, (Vector2)transform.position) < Commentator.SHORT_WARP_DISTANCE_THRESHOLD) {
			Commentator.raiseEvent(CommentEvent.ShortWarp);
		}
		warpEffect.SetActive(true);
		playWarpClip();
		yield return new WaitForSeconds(WARP_DELAY);
		transform.position = (Vector3)newPos;
	}//	End private coroutine method warpCoroutine

	public void warpToPosition(Vector2 newPos) {
		StartCoroutine(warpCoroutine(newPos));
	}//	End private coroutine method warpToPosition

}//	End SpaceShip class
