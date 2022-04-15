using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;

namespace _Scripts.Firebase
{
    public sealed class FirebaseManager
    {
        private FirebaseApp app;
        private FirebaseAuth auth;
        private DatabaseReference databaseReference;
        private FirebaseUser user;

        private FirebaseManager() { }

        //static async method that behave like the constructor
        //since constructors doesnt support async calls
        public static Task<FirebaseManager> Start()
        {
            var instance = new FirebaseManager();
            return instance.InitializeAsync();
        }

        //constructor initializer method
        private async Task<FirebaseManager> InitializeAsync()
        {
            await InitializeFirebase();
            return this;
        }

        //initialises firebase and checks dependencies
        private async Task InitializeFirebase()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                FirebaseDatabase.GetInstance(app).SetPersistenceEnabled(true);
                auth = FirebaseAuth.DefaultInstance;
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);
                Debug.Log("Firebase Init Success");
            }
            else
            {
                Debug.Log($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        }

        //checks if a valid user is already signed in
        public async Task<bool> IsSignedInAsync()
        {
            if (user == null) return false;
            try
            {
                await user.ReloadAsync();
                return user != null;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }

        //Listens to authentication changes
        private void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (auth.CurrentUser == user) return;
            var signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                
                GameManager.Instance.LoadScene(0);
                Debug.Log("Signed out " + user.DisplayName);
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.DisplayName);
            }
        }
        
        //creates a user account for given information
        public async Task<bool> SignUpUserAsync(string email, string password, string userName)
        {
            var signUpStatus = "Oops. Something went wrong. Please try again";
            try
            {
                user = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
                if (user == null) return false;
                var profile = new UserProfile
                {
                    DisplayName = userName
                };
                try
                {
                    await user.UpdateUserProfileAsync(profile);
                }
                catch (Exception)
                {
                    await user.DeleteAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                var firebaseException = (FirebaseException) e.GetBaseException();
                var authError = (AuthError) firebaseException.ErrorCode;

                signUpStatus = authError switch
                {
                    AuthError.InvalidEmail => "Invalid Email",
                    AuthError.EmailAlreadyInUse => "Email Already In Use",
                    AuthError.WeakPassword => "Weak Password",
                    AuthError.MissingEmail => "Missing Email",
                    AuthError.MissingPassword => "Missing Password",
                    _ => signUpStatus
                };
                throw new Exception(signUpStatus);
            }
        }

        //signs in a existing user for valid credentials
        public async Task<bool> SignInUserAsync(string email, string password)
        {
            var signInStatus = "oops something went wrong... Try again..";
            try
            {
                await auth.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception e)
            {
                var firebaseException = (FirebaseException) e.GetBaseException();
                var authError = (AuthError) firebaseException.ErrorCode;

                signInStatus = authError switch
                {
                    AuthError.MissingEmail => "Missing Email",
                    AuthError.MissingPassword => "Missing Password",
                    AuthError.InvalidEmail => "Invalid Email",
                    AuthError.WrongPassword => "Incorrect Password",
                    AuthError.UserNotFound => "User Not Found",
                    _ => signInStatus
                };
                throw new Exception(signInStatus);
            }
        }

        //uploads a given player object to the DB
        public void UploadPlayerData(Player player)
        {
            var json = JsonConvert.SerializeObject(player);
            try
            {
                databaseReference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
                Debug.Log(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //retrieves data of the current firebaseUser
        public async Task<Player> RetrieveUserDataAsync()
        {
            try
            {
                var snapshot = await FirebaseDatabase.DefaultInstance.GetReference("users").Child(user.UserId).GetValueAsync();
                var json = snapshot.GetRawJsonValue();
                Debug.Log(json);
                return JsonConvert.DeserializeObject<Player>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private void SendVerificationEmail()
        {
            if (user != null)
            {
                var errorMessage = "Oops. Something went wrong. Please try again";
                user.SendEmailVerificationAsync().ContinueWithOnMainThread(sendVerificationEmailTask =>
                {
                    if (sendVerificationEmailTask.Exception != null)
                    {
                        var firebaseException =
                            (FirebaseException) sendVerificationEmailTask.Exception.GetBaseException();
                        var error = (AuthError) firebaseException.ErrorCode;

                        errorMessage = error switch
                        {
                            AuthError.Cancelled => "Cancelled",
                            AuthError.InvalidRecipientEmail => "Invalid Recipient Email", //todo handle error
                            AuthError.TooManyRequests => "Too Many Requests",
                            _ => errorMessage
                        };
                        AwaitVerification(false, user.Email, errorMessage);
                    }
                    else
                    {
                        AwaitVerification(true, user.Email, null);
                        Debug.Log("Email Sent");
                    }
                });
            }
        }

        private void AwaitVerification(bool emailSent, string userEmail, string output)
        {
            if (emailSent)
                Debug.Log("Email send" + userEmail);
            else
                Debug.Log("Email send" + userEmail + output);
        }
    }
}