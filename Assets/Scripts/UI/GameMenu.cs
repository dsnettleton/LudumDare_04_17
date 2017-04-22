//	GameMenu.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class GameMenu : EventObserver {

	[SerializeField] private GameObject scoreDisplay;
	[SerializeField] private GameObject notifierPanel;
	[SerializeField] private GameObject victoryScreen;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject levelMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject pauseButton;

	private AudioSource audioSource;
	private AudioClip selectSound;
	private AudioClip alarmSound;

	//	Unity methods

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		selectSound = (AudioClip)Resources.Load("SoundFX/select");
		alarmSound = (AudioClip)Resources.Load("SoundFX/alarm");
	}//	End Unity method Awake

	//	Additional methods

	public override void OnNotify(GameEvent myEvent, int data) {
		switch (myEvent) {
			default:
				break;
			case GameEvent.GamePaused:
				pauseGame();
				break;
			case GameEvent.LevelLoaded:
				Game.setState(Game.State.Running);
				scoreDisplay.SetActive(true);
				notifierPanel.SetActive(true);
				victoryScreen.SetActive(false);
				pauseMenu.SetActive(false);
				mainMenu.SetActive(false);
				pauseButton.SetActive(true);
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

	public void pauseGame() {
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
		Game.setState(Game.State.Paused);
	}//	End public method pauseGame

	public void playLevelSelectSound() {
		audioSource.clip = alarmSound;
		audioSource.volume = Game.getSoundVolume();
		audioSource.Play();
	}//	End public method playLevelSelectSound

	public void playUISound() {
		if (audioSource != null) {
			audioSource.clip = selectSound;
			audioSource.volume = Game.getSoundVolume();
			audioSource.Play();
		}
	}//	End public method playUISound

	public void quitGame() {
		#if UNITY_EDITOR
			if(Application.isEditor) {
				UnityEditor.EditorApplication.isPlaying = false;
			}
		#endif
		Application.Quit();
	}//	End public method quitGame

	public void returnToMenu() {
		Game.setState(Game.State.Running);
		scoreDisplay.SetActive(false);
		notifierPanel.SetActive(false);
		victoryScreen.SetActive(false);
		pauseMenu.SetActive(false);
		mainMenu.SetActive(true);
		levelMenu.SetActive(false);
		optionsMenu.SetActive(false);
		pauseButton.SetActive(false);
	}//	End public method returnToMenu

	public void unpauseGame() {
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
		Game.returnToPreviousState();
	}//	End public method unpauseGame

}//	End public class GameMenu
