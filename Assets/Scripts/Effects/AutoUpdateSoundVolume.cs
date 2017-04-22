//	AutoUpdateSoundVolume.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AutoUpdateSoundVolume : MonoBehaviour {

	private void OnEnable() {
		GetComponent<AudioSource>().volume = Game.getSoundVolume();
	}
	
}//	End public class AutoUpdateSoundVolume
