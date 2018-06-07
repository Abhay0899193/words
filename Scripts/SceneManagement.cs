using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	public void SignUp()
	{
		SceneManager.LoadScene (1);
	}

	public void SignIn()
	{
		SceneManager.LoadScene (2);
	}



}
