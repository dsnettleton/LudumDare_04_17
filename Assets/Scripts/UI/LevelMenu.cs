//	LevelMenu.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

	[SerializeField] private Transform levelButtons;

	//	Unity methods

	private void OnEnable() {
		activateButtons();
	}//	End Unity method OnEnable

	//	Additional methods

	private void activateButtons() {
		Button currentButton;
		for (int i = 0, numLevelButtons = levelButtons.childCount; i < numLevelButtons; ++i) {
			currentButton = levelButtons.GetChild(i).gameObject.GetComponent<Button>();
			if (currentButton != null) {
				currentButton.interactable = (i <= Game.lastCompletedLevel);
			}
		}//	End for each level button
	}//	End private method activateButtons

	public void loadLevel(int levelIndex) {
		unloadLevel();
		if (levelIndex > 0 && levelIndex < SceneManager.sceneCountInBuildSettings) {
			Game.currentLevel = levelIndex;
			SceneManager.LoadScene(Game.currentLevel, LoadSceneMode.Additive);
			EventHandler.raiseEvent(GameEvent.LevelLoaded, Game.currentLevel);
		}
	}//	End public method loadLevel

	public void reloadLevel() {
		if (Game.currentLevel > 0 && Game.currentLevel < SceneManager.sceneCountInBuildSettings) {
			SceneManager.UnloadSceneAsync(Game.currentLevel);
			SceneManager.LoadScene(Game.currentLevel, LoadSceneMode.Additive);
			EventHandler.raiseEvent(GameEvent.LevelLoaded, Game.currentLevel);
		}
	}//	End public method reloadLevel

	public void unloadLevel() {
		if (Game.currentLevel > 0 && Game.currentLevel < SceneManager.sceneCountInBuildSettings) {
			SceneManager.UnloadSceneAsync(Game.currentLevel);
			Game.currentLevel = 0;
		}
	}//	End public method unloadLevel

}//	End public class LevelMenu
