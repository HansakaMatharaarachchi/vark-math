using System;
using _Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Firebase
{
    public class AuthenticationUIManager : Singleton<AuthenticationUIManager>
    {
        [Header("Panels")] [SerializeField] private GameObject startPanel;

        [SerializeField] private TMP_Text signUpLog;
        [SerializeField] private TMP_Text signInLog;
        [SerializeField] private GameObject noConnectivityPanel;

        [Header("Signin Details")] [SerializeField]
        private InputField emailSigninField;

        [SerializeField] private InputField passwordSigninField;

        [Header("Parent signup details")] [SerializeField]
        private InputField parentSignupEmailField;

        [SerializeField] private InputField parentSignupPasswordField;

        [Header("student signup details")] [SerializeField]
        private InputField studentSignupName;

        [SerializeField] private InputField studentSignupAge;

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void ShowStartPanel()
        {
            startPanel.SetActive(true);
        }

        public void ShowNoConnectionPanel()
        {
            noConnectivityPanel.SetActive(true);
        }

        public async void SignInButton()
        {
            signInLog.text = await GameManager.Instance.SignInUser(emailSigninField.text, passwordSigninField.text);
        }

        public async void SignUpButton()
        {
            Debug.Log(studentSignupAge.text + studentSignupName.text + parentSignupEmailField.text + parentSignupPasswordField.text);
            if (studentSignupAge.text == "")
            {
                signUpLog.text = "Student Age Is Empty";
                return;
            }

            if (studentSignupName.text == "")
            {
                signUpLog.text = "Student Name Is Empty";
                return;
            }
            signUpLog.text = await GameManager.Instance.SignUpUser(parentSignupEmailField.text,
                parentSignupPasswordField.text, studentSignupName.text, Convert.ToInt32(studentSignupAge.text));
        }
    }
}