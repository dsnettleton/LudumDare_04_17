//	GravitationalInfluence.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class GravitationalInfluence : MonoBehaviour {

	[SerializeField] private float gravStrength = 1.0f;
	[SerializeField] private bool ignoreDistance = false;
	private float influenceRadius;
	private float insideRadius;

	//	Unity methods

	private void Awake() {
		CircleCollider2D myCollider = GetComponent<CircleCollider2D>();
		influenceRadius = myCollider.radius;
		if (influenceRadius <= 0.0f) {
			Debug.LogError("You must have a positive radius for this circle collider.");
		}
		myCollider = transform.parent.gameObject.GetComponent<CircleCollider2D>();
		insideRadius = myCollider.radius;
		if (insideRadius >= influenceRadius) {
			Debug.LogError("Inside radius must be smaller than influence radius.");
		}
	}//	End Unity method Awake

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.layer == (int)Game.Layers.GolfBall) {
			GolfBall ballScript = other.gameObject.GetComponent<GolfBall>();
			if (ballScript != null) {
				float distanceScalar = 1.0f;
				if (!ignoreDistance) {
					distanceScalar = Vector2.Distance((Vector2)transform.position, (Vector2)ballScript.transform.position);
					distanceScalar = 1.0f - Mathf.Clamp01((distanceScalar - insideRadius) / (influenceRadius - insideRadius));
				}
				float pullStrength = gravStrength * distanceScalar * distanceScalar * Time.fixedDeltaTime;
				ballScript.pull((Vector2)transform.position, pullStrength);
			}
		}//	End if the object is a golfball
	}//	End Unity method OnTriggerStay2D

}//	End class GravitationalInfluence
