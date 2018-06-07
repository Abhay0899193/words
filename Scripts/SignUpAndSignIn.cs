using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;

public class SignUpAndSignIn : MonoBehaviour {


	public InputField userName;
	public InputField password;
	public InputField email;
	public Text console;

	Firebase.Auth.FirebaseAuth auth;
	Firebase.Auth.FirebaseUser user;

	void Start()
	{
		InitializeFirebase ();
	}

	void InitializeFirebase() {
		console.text = "Setting up Firebase Auth";
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
	}
		
	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				console.text = "Signed out " + user.UserId;
			}
			user = auth.CurrentUser;
			if (signedIn) {
				console.text = "Signed in " + user.UserId;
			}
		}
	}

	void OnDestroy() {
		auth.StateChanged -= AuthStateChanged;
		auth = null;
	}

	public void SignInButton()
	{
		auth.SignInWithEmailAndPasswordAsync(email.text,password.text).ContinueWith(task =>
		{
				if (task.IsCanceled) {
					console.text = "SignInWithEmailAndPasswordAsync was canceled.";
					return;
				}
				if (task.IsFaulted) {
					console.text = "SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception;
					return;
				}
				Firebase.Auth.FirebaseUser newUser = task.Result;
				console.text = "Sign In as "+newUser.DisplayName;
			//SceneManager.LoadScene(4);	
		});
	}

	public void SignUpButton()
	{
		auth.CreateUserWithEmailAndPasswordAsync(email.text,password.text).ContinueWith(obj =>
		{
				if (obj.IsCanceled) {
					console.text = "CreateUserWithEmailAndPasswordAsync was canceled.";
					return;
				}
				if (obj.IsFaulted) {
					console.text ="CreateUserWithEmailAndPasswordAsync encountered an error: " + obj.Exception;
					return;
				}
				Firebase.Auth.FirebaseUser newUser = obj.Result;
				
				console.text = "Sign Up as "+newUser.DisplayName;
			//SceneManager.LoadScene(3);	
		});
	}

//	void UpdateProfile()
//	{
//		Firebase.Auth.FirebaseUser user = auth.CurrentUser;
//		if (user != null)
//		{
//			Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
//				DisplayName = userName.text
//			};
//			user.UpdateUserProfileAsync (profile).ContinueWith (task => {
//				if (task.IsCanceled) {
//					console.text = "CreateUserWithEmailAndPasswordAsync was canceled.";
//					return;
//				}
//				if (task.IsFaulted) {
//					console.text = "CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception;
//					return;
//				}
//
//				console.text = "Updated";
//			});
//		}
//	}
}
