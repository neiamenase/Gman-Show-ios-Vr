using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour {

	string saveFilePath = Application.dataPath+ "/Data/Save.txt";
	// health , scene
	string scenesFilePath = Application.dataPath+ "/Data/SceneOrder.txt";


	public void writeSaveData(int health, int sceneID){
		StreamWriter writer = new StreamWriter (saveFilePath);
		writer.Write(health.ToString() + "," + sceneID.ToString());
		writer.Close ();
	}

	public string[] readSaveData(){
		StreamReader reader = new StreamReader(saveFilePath);
		string[] data = { "" };
		if(!reader.EndOfStream)
		{
			data = reader.ReadLine( ).Split(',');
		}
		reader.Close( ); 
		return data;
	}

	public string getSceneName (int id){
		int i = 0;
		StreamReader reader = new StreamReader(scenesFilePath);
		while(!reader.EndOfStream)
		{
			string sceneName = reader.ReadLine ();
			if (i == id) {
				print (i + "   " + sceneName);
				return sceneName;
			}
			i++;
		}
		reader.Close( );  
		return "Start";
	}

	public int getCurrentSceneID (string Name){
		int i = 0;
		StreamReader reader = new StreamReader(scenesFilePath);
		while(!reader.EndOfStream)
		{
			string sceneName = reader.ReadLine ();
			if (sceneName == Name) {
				return i;
			}
			i++;
		}
		reader.Close( );  
		return 0;
	}
	public int getHealthData(){

		return int.Parse (readSaveData()[0]);
	}

	public int getSceneID(){
		return int.Parse (readSaveData()[1]);
	}

	public int getPlayerHealth(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player == null)
			return 10;
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
		if (playerHealth == null)
			return 10;
		return playerHealth.getCurrentHealth ();
	
	}

	public void loadPreviousScene (){
		int previous = getSceneID() - 1;
		if (previous < 0)
			previous = 0;
		
		writeSaveData (getPlayerHealth(), previous);
		SceneManager.LoadScene(getSceneName(previous));
	}
	public void loadNextScene (){

			int next = getSceneID () + 1;

			writeSaveData (getPlayerHealth (), next);
			SceneManager.LoadScene (getSceneName (next));

	}

	public void loadDeadScene (){
		writeSaveData (10, getSceneID());
		SceneManager.LoadScene("Dead");
	}

	public void loadStartScene (){
		writeSaveData (10, 0);
		SceneManager.LoadScene("Start");
	}

	public void loadRetryScene (){
		SceneManager.LoadScene(getSceneName(getSceneID()));
	}

	public void loadMenuScene (){
		SceneManager.LoadScene("Menu");
	}

}

