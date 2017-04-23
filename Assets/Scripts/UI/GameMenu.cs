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
	[SerializeField] private GameObject toolsPanel;
	[SerializeField] private GameObject firstRunScreen;

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
				Time.timeScale = 1.0f;
				scoreDisplay.SetActive(true);
				notifierPanel.SetActive(true);
				victoryScreen.SetActive(false);
				pauseMenu.SetActive(false);
				mainMenu.SetActive(false);
				pauseButton.SetActive(true);
				toolsPanel.SetActive(true);
				if (Game.firstRun) {
					showFirstRunScreen();
				}
				break;
			case GameEvent.LevelWon:
				Time.timeScale = 0.0f;
				Game.setState(Game.State.Paused);
				victoryScreen.SetActive(true);
				toolsPanel.SetActive(false);
				pauseButton.SetActive(false);
				if (Game.currentLevel > Game.lastCompletedLevel) {
					Game.lastCompletedLevel = Game.currentLevel;
				}
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

	public void hideFirstRunScreen() {
		if (firstRunScreen != null) {
			firstRunScreen.SetActive(false);
			Game.setState(Game.State.Running);
			Time.timeScale = 1.0f;
		}
	}//	End public method hideFirstRunScreen

	public void pauseGame() {
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
		Game.setState(Game.State.Paused);
		Commentator.raiseEvent(CommentEvent.GamePaused);
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
		Time.timeScale = 1.0f;
		scoreDisplay.SetActive(false);
		notifierPanel.SetActive(false);
		victoryScreen.SetActive(false);
		pauseMenu.SetActive(false);
		mainMenu.SetActive(true);
		levelMenu.SetActive(false);
		optionsMenu.SetActive(false);
		pauseButton.SetActive(false);
		toolsPanel.SetActive(false);
	}//	End public method returnToMenu

	public void showFirstRunScreen() {
		if (firstRunScreen != null) {
			Game.firstRun = false;
			firstRunScreen.SetActive(true);
			Time.timeScale = 0.0f;
			Game.setState(Game.State.Paused);
		}
	}//	End public method showFirstRunScreen

	public void unpauseGame() {
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
		Game.returnToPreviousState();
	}//	End public method unpauseGame

}//	End public class GameMenu
