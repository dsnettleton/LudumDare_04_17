//	OptionsMenu.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	[SerializeField] Slider masterVolumeSlider;
	[SerializeField] Slider musicVolumeSlider;
	[SerializeField] Slider soundVolumeSlider;
	[SerializeField] Slider commentVolumeSlider;

	private float prevCommentVolume = 0.5f;

	//	Unity methods

	private void OnEnable() {
		setSliderValues();
	}//	End Unity method OnEnable

	//	Additioanl methods

	public void beginCommentVolume() {
		prevCommentVolume = commentVolumeSlider.value;
	}//	End public method beginCommentVolume

	public void endCommentVolume() {
		float currentCommentVolume = commentVolumeSlider.value;
		if (currentCommentVolume > prevCommentVolume) {
			Commentator.raiseEvent(CommentEvent.CommentsTurnedUp);
		} else if (currentCommentVolume < prevCommentVolume) {
			Commentator.raiseEvent(CommentEvent.CommentsTurnedDown);
		}
		prevCommentVolume = currentCommentVolume;
		endVolumeChange();
	}//	End public method endCommentVolume

	public void endVolumeChange() {
		EventHandler.raiseEvent(GameEvent.VolumeChanged);
	}//	End public method endVolumeChange

	public void setCommentVolume() {
		if (commentVolumeSlider != null) {
			Game.setCommentVolume(commentVolumeSlider.value);
		}
	}//	End public method setCommentVolume

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
		if (commentVolumeSlider != null) {
			commentVolumeSlider.value = Game.getCommentVolumeUnscaled();
			prevCommentVolume = commentVolumeSlider.value;
		}
	}//	End private method setSliderValues

	public void setSoundVolume() {
		if (soundVolumeSlider != null) {
			Game.setSoundVolume(soundVolumeSlider.value);
		}
	}//	End public method setSoundVolume

}//	End class OptionsMenu
