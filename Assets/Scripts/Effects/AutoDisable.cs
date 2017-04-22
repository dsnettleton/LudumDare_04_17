//	AutoDisable.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class AutoDisable : MonoBehaviour {

	[SerializeField] private float maxLifetime = 5.0f;
	private float currentLifetime = 0.0f;

	private void OnEnable() {
		currentLifetime = 0.0f;
	}//	End Unity method OnEnable

	private void Update() {
		currentLifetime += Time.deltaTime;
		if (currentLifetime >= maxLifetime) {
			gameObject.SetActive(false);
		}
	}//	End Unity method Update

}//	End public class AutoDisable
