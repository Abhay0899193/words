using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour {

	private Scene scene;

	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Debug.Log (scene.buildIndex);
			if (scene.buildIndex == 1 || scene.buildIndex == 2) {
				SceneManager.LoadScene (0);
			} else if (scene.buildIndex == 0 || scene.buildIndex == -1) {
				Application.Quit ();
			}
		}

	}
}
