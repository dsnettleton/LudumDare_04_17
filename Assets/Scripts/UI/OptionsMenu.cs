//	OptionsMenu.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	[SerializeField] Slider masterVolumeSlider;
	[SerializeField] Slider musicVolumeSlider;
	[SerializeField] Slider soundVolumeSlider;

	//	Unity methods

	private void OnEnable() {
		setSliderValues();
	}//	End Unity method OnEnable

	//	Additioanl methods

	public void setMasterVolume() {
		if (masterVolumeSlider != null) {
			Game.setMasterVolume(masterVolumeSlider.value);
		}
	}//	End public method setMasterVolume

	public void setMusicVolume() {
		if (musicVolumeSlider != null) {
			Game.setMusicVolume(musicVolumeSlider.value);
		}
	}//	End public method setMusicVolume

	private void setSliderValues() {
		if (masterVolumeSlider != null) {
			masterVolumeSlider.value = Game.getMasterVolume();
		}
		if (musicVolumeSlider != null) {
			musicVolumeSlider.value = Game.getMusicVolumeUnscaled();
		}
		if (soundVolumeSlider != null) {
			soundVolumeSlider.value = Game.getSoundVolumeUnscaled();
		}
	}//	End private method setSliderValues

	public void setSoundVolume() {
		if (soundVolumeSlider != null) {
			Game.setSoundVolume(soundVolumeSlider.value);
		}
	}//	End public method setSoundVolume

}//	End class OptionsMenu
