//	AutoRotate.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

public class AutoRotate : MonoBehaviour {

	[SerializeField] private Vector3 rotationAxis = Vector3.forward;
	[SerializeField] private float rotationSpeed = 180.0f;

	private void Update() {
		transform.localRotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis);
	}//	End Unity method Update

}//	End public class AutoRotate
