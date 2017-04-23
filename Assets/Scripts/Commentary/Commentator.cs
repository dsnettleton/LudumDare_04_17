//	Commentator.cs
//	April 23, 2017
//	D. Scott Nettleton

using UnityEngine;
using System.Collections;

public enum CommentEvent : int {
	BallTimeout = 0,
	PlanetDestroyed = 1,
	HoleInOne = 2,
	LiningUpShot = 3,
	BallStops = 4,
	ManyStrokes = 5,
	GamePaused = 6,
	CommentsTurnedDown = 7,
	CommentsTurnedUp = 8,
	AlwaysFullPower = 9,
	BallHitsPlayer = 10,
	ShortWarp = 11,
	BallOutOfBounds = 12,
	BallHitsPlanet = 13,
	LevelWon = 14
}//	End CommentEvent enum

[RequireComponent (typeof(AudioSource))]
public class Commentator : MonoBehaviour {

	private static Commentator observer;

	public const int MANY_STROKES_THRESHOLD = 10;
	public const int FULL_POWER_STROKES_THRESHOLD = 5;
	public const float SHORT_WARP_DISTANCE_THRESHOLD = 6.0f;

	private const float MIN_COMMENT_TIME_GAP = 5.0f;

	[SerializeField] private CommentLibrary[] library;
	private AudioSource source;
	private float lastCommentTime = 0.0f;

	//	Unity methods

	private void Start() {
		Commentator.setObserver(this);
		source = GetComponent<AudioSource>();
	}//	End Unity method Start

	//	Additional methods

	private void OnNotify(CommentEvent myEvent) {
		if (library == null || (int)myEvent >= library.Length || source == null) { return; }
		bool volumeChange = (myEvent == CommentEvent.CommentsTurnedUp || myEvent == CommentEvent.CommentsTurnedDown);
		StartCoroutine(playComment(library[(int)myEvent], volumeChange));
		if (source != null && source.isPlaying && volumeChange) {
			source.volume = Game.getCommentVolume();
		}
	}//	End private method OnNotify

	private void playClip(AudioClip clip, bool ignoreTimeGap = true) {
		if (clip == null || source.isPlaying) {
			return;
		}
		float currentTime = Time.unscaledTime;
		if (currentTime - lastCommentTime < MIN_COMMENT_TIME_GAP && !ignoreTimeGap) {
			return;
		}
		lastCommentTime = currentTime;
		source.clip = clip;
		source.volume = Game.getCommentVolume();
		source.Play();
	}//	End private method playClip

	private IEnumerator playComment(CommentLibrary commentSet, bool ignoreTimeGap) {
		if (Random.value <= commentSet.percentChanceOnEvent) {
			if (commentSet.startDelay > 0.0f) {
				yield return new WaitForSeconds(commentSet.startDelay);
			}
			int numComments = commentSet.comments.Length;
			playClip(commentSet.comments[Random.Range(0, numComments)], ignoreTimeGap);
		}//	End if we should play a comment clip
	}//	End private coroutine mehtod playComment

	public static void raiseEvent(CommentEvent myEvent) {
		if (observer != null) {
			observer.OnNotify(myEvent);
		}
	}//	End public static method raiseEvent

	public static void refreshVolume() {
		if (observer != null) {
			observer.source.volume = Game.getCommentVolume();
		}
	}//	End public static method refreshVolume

	public static void setObserver(Commentator my) {
		observer = my;
	}//	End public static method setObserver

}//	End public class Commentator
