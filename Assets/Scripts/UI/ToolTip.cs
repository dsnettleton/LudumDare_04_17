//	ToolTip.cs
//	April 23, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {

	[SerializeField] private GameObject tooltipObject;

	private bool showing = false;

	private void OnMouseEnter() {
		showing = true;
		tooltipObject.SetActive(showing);
	}//	End Unity method OnMouseEnter

	private void OnMouseExit() {
		showing = false;
		tooltipObject.SetActive(showing);
	}//	End Unity method OnMouseExit

}//	End class ToolTip
