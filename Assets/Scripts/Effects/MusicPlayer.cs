//	MusicPlayer.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

	private AudioSource mySource;

	private void Start() {
		mySource = GetComponent<AudioSource>();
		updateVolume();
	}//	End Unity method Start

	private void Update() {
		updateVolume();
	}//	End Unity method Update

	private void updateVolume() {
		mySource.volume = Game.getMusicVolume();
	}//	End private method updateVolume

}//	End public class MusicPlayer
