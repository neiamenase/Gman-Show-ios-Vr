using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find ("Canvas");
		Button[] buttons = canvas.GetComponentsInChildren<Button> ();
		string[] sceneNames = getSceneNames ();
		for (int i = 0; i < 10; i++) {
			Text[] t = buttons [i].GetComponentsInChildren<Text> ();
			t[0].text = sceneNames [i];
			buttons [i].onClick.AddListener(delegate() { loadScene(); });
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void loadScene(){
		Text t = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>()[0];
		ChangeScene cs = new ChangeScene ();
		cs.writeSaveData (10, cs.getCurrentSceneID(t.text));
		SceneManager.LoadScene(t.text);

		
	}

	private string[] getSceneNames(){
		string[] sceneNames = new string[10];
		string scencesFilePath = Application.dataPath+ "/Data/SceneOrder.txt";
		StreamReader reader = new StreamReader(scencesFilePath);
		for(int i = 0; !reader.EndOfStream && i <10; i++)
		{
			sceneNames[i] = reader.ReadLine ();
		}
		reader.Close( );  
		return sceneNames;
	}
}
