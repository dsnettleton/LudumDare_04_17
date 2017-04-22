//	LevelMenu.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

	[SerializeField] private Transform levelButtons;

	private int currentLevel = 1;
	private int lastCompletedLevel = 0;

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
				currentButton.interactable = (i <= lastCompletedLevel);
			}
		}//	End for each level button
	}//	End private method activateButtons

	public void loadLevel(int levelIndex) {
		if (currentLevel > 0 && currentLevel < SceneManager.sceneCountInBuildSettings) {
			SceneManager.UnloadSceneAsync(currentLevel);
		}
		if (levelIndex > 0 && levelIndex < SceneManager.sceneCountInBuildSettings) {
			currentLevel = levelIndex;
			SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
			EventHandler.raiseEvent(GameEvent.LevelLoaded, currentLevel);
		}
	}//	End public method loadLevel

}//	End public class LevelMenu
