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
		Text strokeText;
		for (int i = 0, numLevelButtons = levelButtons.childCount; i < numLevelButtons; ++i) {
			currentButton = levelButtons.GetChild(i).gameObject.GetComponent<Button>();
			if (currentButton != null) {
				currentButton.interactable = (i <= Game.lastCompletedLevel);
				strokeText = currentButton.gameObject.GetComponentInChildren<Text>();
				if (strokeText != null) {
					strokeText.text = levelText(i);
				}
			}//	End if we have a valid Button
		}//	End for each level button
	}//	End private method activateButtons

	private string levelText(int levelIndex) {
		if (Game.levelStrokes == null) { return ""; }
		if (levelIndex < 0 || levelIndex >= Game.levelStrokes.Length) { return ""; }
		int numStrokes = Game.levelStrokes[levelIndex];
		if (numStrokes <= 0) {
			return "";
		} else if (numStrokes == 1) {
			return "<color=lime>1</color>";
		} else if (numStrokes <= 5) {
			return "<color=yellow>"+numStrokes.ToString()+"</color>";
		} else if (numStrokes < 10) {
			return "<color=#FF8000FF>"+numStrokes.ToString()+"</color>";
		} else {
			return "<color=red>"+numStrokes.ToString()+"</color>";
		}
	}//	End private method levelText

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
