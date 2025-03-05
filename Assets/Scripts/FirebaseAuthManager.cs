using System.Collections;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{
    private static FirebaseAuthManager _instance = null;

    private FirebaseAuth _auth;
    private FirebaseUser _user;

    private FirebaseFirestore _firestore;

    [SerializeField] private TMP_InputField _emailInputField;

    [SerializeField] private TMP_InputField _passwordInputField;

    // Start is called before the first frame update
    void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _firestore = FirebaseFirestore.DefaultInstance;
    }

    public void SignUp()
    {
        _auth.CreateUserWithEmailAndPasswordAsync(_emailInputField.text, _passwordInputField.text).ContinueWith(
            task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Sign up cancelled");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("Sign up failed");
                    return;
                }

                var user = task.Result;
                Debug.Log("Sign up completed");
            });
    }

    public void Login()
    {
        bool isSuccess = false;
        _auth.SignInWithEmailAndPasswordAsync(_emailInputField.text, _passwordInputField.text).ContinueWithOnMainThread(
            task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log("Login failed");
                    StartCoroutine(LoginCoroutine(isSuccess));
                    return;
                }

                if (task.IsCompleted)
                {
                    isSuccess = true;
                    StartCoroutine(LoginCoroutine(isSuccess));
                    Debug.Log("Login completed");
                }
            });
    }

    private IEnumerator LoginCoroutine(bool isSuccess)
    {
        yield return new WaitForSeconds(0.1f);
        // 다음수업시간 Success , Failed 구현예정
    }
}