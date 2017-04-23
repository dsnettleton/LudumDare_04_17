//	AutoUpdateSoundVolume.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AutoUpdateSoundVolume : MonoBehaviour {

	private void OnEnable() {
		AudioSource[] sources = GetComponents<AudioSource>();
		for (int i = 0, numSources = sources.Length; i < numSources; ++i) {
			sources[i].volume = Game.getSoundVolume();
		}
	}//	End Unity method OnEnable

}//	End public class AutoUpdateSoundVolume
