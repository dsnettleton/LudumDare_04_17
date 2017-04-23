//	AutoOrbit.cs
//	April 23, 2017
//	D. Scott Nettleton

using UnityEngine;

public class AutoOrbit : MonoBehaviour {

	[SerializeField] private Vector3 positionToOrbit;
	[SerializeField] private float orbitSpeed;

	private void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(positionToOrbit, 0.5f);
	}//	End Unity method OnDrawGizmos

	private void Update() {
		transform.RotateAround(positionToOrbit, Vector3.forward, orbitSpeed * Time.deltaTime);
	}//	End Unity method Update

}//	End public class AutoOrbit
