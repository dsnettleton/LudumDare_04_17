//	AutoPulse.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
public class AutoPulse : MonoBehaviour {

	[SerializeField] private float sizeIncrease = 0.15f;
	[SerializeField] private float pulseSpeed = 1.0f;

	private const float TWO_PI = 6.2831852f;
	private float pulseCounter = 0.0f;
	private Vector3 baseSize;
	private float twoPiPulseSpeed;

	private void Start() {
		// baseSize = Vector3.one + Vector3.one * sizeIncrease;
		Vector3 startSize = transform.localScale;
		baseSize = startSize + startSize * sizeIncrease;
		twoPiPulseSpeed = pulseSpeed * TWO_PI;
	}//	End Unity method Start

	private void Update() {
		transform.localScale = baseSize + (Vector3.one * Mathf.Sin(pulseCounter) * sizeIncrease);
		pulseCounter += twoPiPulseSpeed * Time.deltaTime;
		if (pulseCounter > TWO_PI) {
			pulseCounter -= TWO_PI;
		}
	}//	End Unity method Update

}//	End public class AutoPulse
