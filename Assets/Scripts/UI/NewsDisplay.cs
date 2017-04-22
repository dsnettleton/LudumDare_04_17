//	NewsDisplay.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewsDisplay : MonoBehaviour {

	[SerializeField] private Image background;
	[SerializeField] private Text newsText;
	[SerializeField] private float messageDuration = 3.0f;
	[SerializeField] private float fadeDuration = 0.5f;
	[SerializeField] private Color textColor;

	private IEnumerator messageCoroutine(string message) {
		showStatic(message);
		yield return new WaitForSeconds(messageDuration);
		if (fadeDuration > 0.0f) {
			float fadeTime = 0.0f;
			while (fadeTime < fadeDuration) {
				fadeTime += Time.deltaTime;
				float percentFade = Mathf.Lerp(1.0f, 0.0f, fadeTime / fadeDuration);
				Color tmp = Color.white;
				tmp.a = percentFade;
				background.color = tmp;
				tmp = textColor;
				tmp.a = percentFade;
				newsText.color = tmp;
				yield return null;
			}//	End while fading
		}
		background.gameObject.SetActive(false);
	}//	End private coroutine method messageCoroutine

	public void show(string message) {
		gameObject.SetActive(true);
		StartCoroutine(messageCoroutine(message));
	}//	End public method show

	public void showStatic(string message) {
		newsText.color = textColor;
		background.color = Color.white;
		newsText.text = message;
		background.gameObject.SetActive(true);
	}//	End public method showStatic

}//	End public class NewsDisplay
