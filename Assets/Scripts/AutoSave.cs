//	AutoSave.cs
//	April 22, 2017
//	D. Scott Nettleton

using UnityEngine;
using System.IO;

public class AutoSave : EventObserver {

	private const int NUM_HEADER_LINES = 4;
	private enum FileLines : int {
		LastCompletedLevel,
		SoundVolume,
		MusicVolume,
		MasterVolume
	}//	End FileLines enum

	private int[] levelStrokes;

	//	Unity methods

	private void Start() {
		levelStrokes = new int[Game.NUM_LEVELS];
		for (int i = 0; i < Game.NUM_LEVELS; ++i) {
			levelStrokes[i] = 0;
		}//	End for each level
		loadData();
	}//	End Unity method Start

	//	Addtional methods

	public override void OnNotify(GameEvent myEvent, int data) {
		switch (myEvent) {
			default:
				break;
			case GameEvent.LevelWon:
				if (Game.currentLevel > Game.lastCompletedLevel) {
					Game.lastCompletedLevel = Game.currentLevel;
				}
				int levelIndex = Game.currentLevel - 1;
				if (levelIndex < Game.NUM_LEVELS && (levelStrokes[levelIndex] > data || levelStrokes[levelIndex] == 0)) {
					levelStrokes[levelIndex] = data;
				}
				saveData();
				break;
			case GameEvent.VolumeChanged:
				saveData();
				break;
		}//	End event type switch
	}//	End public EventObserver method OnNotify

	public void loadData() {
		string filePath = Application.persistentDataPath+"/saveFile.dat";
		if (!File.Exists(filePath)) {
			return;
		}
		try {
			string[] fileContents = File.ReadAllLines(filePath);
			if (fileContents.Length < 4) {
				Debug.Log("Invalid settings file.");
				return;
			}
			Game.lastCompletedLevel = int.Parse(fileContents[(int)FileLines.LastCompletedLevel]);
			Game.setSoundVolume(float.Parse(fileContents[(int)FileLines.SoundVolume]));
			Game.setMusicVolume(float.Parse(fileContents[(int)FileLines.MusicVolume]));
			Game.setMasterVolume(float.Parse(fileContents[(int)FileLines.MasterVolume]));
			for (int i = NUM_HEADER_LINES, numLines = fileContents.Length; i < numLines && i < Game.NUM_LEVELS; ++i) {
				levelStrokes[i] = int.Parse(fileContents[i]);
			}
		}//	End try block
		catch (System.UnauthorizedAccessException err) { Debug.LogError(err.Message); }
		catch (System.IO.PathTooLongException err) { Debug.LogError(err.Message); }
		catch (System.IO.DirectoryNotFoundException err) { Debug.LogError(err.Message); }
		catch (System.ArgumentException err) { Debug.LogError(err.Message); }
		catch (System.NotSupportedException err) { Debug.LogError(err.Message); }
	}//	End public method loadData

	public void saveData() {
		string filePath = Application.persistentDataPath+"/saveFile.dat";
		string[] fileContents = new string[NUM_HEADER_LINES + Game.NUM_LEVELS];
		fileContents[0] = Game.lastCompletedLevel.ToString();
		fileContents[1] = Game.getSoundVolumeUnscaled().ToString();
		fileContents[2] = Game.getMusicVolumeUnscaled().ToString();
		fileContents[3] = Game.getMasterVolume().ToString();
		for (int i = 0; i < Game.NUM_LEVELS; ++i) {
			fileContents[NUM_HEADER_LINES + i] = levelStrokes[i].ToString();
		}
		try {
			File.WriteAllLines(filePath, fileContents);
		}
		catch (System.UnauthorizedAccessException err) { Debug.LogError(err.Message); }
		catch (System.IO.PathTooLongException err) { Debug.LogError(err.Message); }
		catch (System.IO.DirectoryNotFoundException err) { Debug.LogError(err.Message); }
		catch (System.ArgumentException err) { Debug.LogError(err.Message); }
		catch (System.NotSupportedException err) { Debug.LogError(err.Message); }
	}//	End public method saveData

}//	End public class AutoSave
