using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class FirebaseWrapper
    {
        private const string PROJECT_ID = "thirty-day-hero";
        private const string AUTH_DOMAIN = "thirty-day-hero.firebaseapp.com";
        private const string DATA_URL = "https://thirty-day-hero.firebaseio.com/";
        private const string API_KEY = "AIzaSyDHcCjJupg3v6rLAkwFcPuatQyamVVuE9M";

        public event EventHandler Updated;
        
        public bool IsLoggedIn => _user != null;
        public bool IsAnonymous => _user?.IsAnonymous ?? false;

        public UserInfo UserInfo => _user?.Info;

        public string UserId => _user?.Uid;
        public string UserName => _user == null
            ? ""
            : _user.IsAnonymous
                ? $"Anon:{_user.Uid}"
                : string.IsNullOrWhiteSpace(_user.Info.DisplayName)
                    ? _user.Info.Email
                    : _user.Info.DisplayName;
        
        private User _user = null;

        private readonly FirebaseAuthClient _authClient;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly RestClient _dataClient;
        private readonly IRestSerializer _restSerializer;

        public FirebaseWrapper(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;

            _authClient = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = API_KEY,
                AuthDomain = AUTH_DOMAIN,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
                UserRepository = new FileUserRepository("User")
            });
            SetUser(_authClient.User);

            _dataClient = new RestClient(DATA_URL)
            {
                ThrowOnAnyError = true,
                ThrowOnDeserializationError = true,
                FailOnDeserializationError = true
            };

            _restSerializer = new NewtonWrapper(_serializerSettings);
            Func<IDeserializer> getRestSerializer = () => _restSerializer;
            _dataClient.AddHandler("application/json", getRestSerializer);
            _dataClient.AddHandler("text/json", getRestSerializer);
            _dataClient.AddHandler("text/x-json", getRestSerializer);
            _dataClient.AddHandler("text/javascript", getRestSerializer);
            _dataClient.AddHandler("*+json", getRestSerializer);
            _dataClient.AddHandler(ContentType.Json, getRestSerializer);
            foreach (string contentType in ContentType.JsonAccept)
            {
                _dataClient.AddHandler(contentType, getRestSerializer);
            }
        }

        private void SetUser(User user)
        {
            _user = user;
            Updated?.Invoke(this, EventArgs.Empty);
        }

        private void SetUser(UserCredential credential)
        {
            SetUser(credential?.User);
        }

        public async Task<(bool successful, string message)> TrySignIn(string email, string password)
        {
            if (IsLoggedIn)
                return (false, "Already logged in.");

            try
            {
                var result = await _authClient.FetchSignInMethodsForEmailAsync(email);
                if (!result.UserExists)
                {
                    return (false, $"No account found for email {email}.");
                }

                if (result.SignInProviders.Contains(FirebaseProviderType.EmailAndPassword))
                {
                    var credential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                    SetUser(credential);
                }
                else
                {
                    return (false, "Unable to sign in using email+pass authentication.");
                }
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Unhandled error occured during sign in -> {ex.Reason}");
            }

            if (IsLoggedIn)
            {
                return (true, $"Signed in as {UserName}.");
            }
            else
            {
                return (false, "Failed to sign in using provided details.");
            }
        }

        public async Task<(bool successful, string message)> TryCreateAnon()
        {
            if (IsLoggedIn)
                return (false, "Already logged in.");

            try
            {
                var credential = await _authClient.SignInAnonymouslyAsync();
                SetUser(credential);
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Unhandled error occured during sign up -> {ex.Reason}");
            }

            if (IsLoggedIn)
            {
                return (true, $"Signed in as {UserName}.");
            }
            else
            {
                return (false, "Failed to create anonymous account.");
            }
        }

        public async Task<(bool successful, string message)> TryCreateAccount(string displayName, string email, string password)
        {
            if (IsLoggedIn)
                return (false, "Already logged in.");

            try
            {
                var credential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
                SetUser(credential);
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Unhandled error occured during sign up -> {ex.Reason}");
            }

            if (IsLoggedIn)
            {
                return (true, $"Signed in as {UserName}.");
            }
            else
            {
                return (false, "Failed to create account.");
            }
        }

        public async Task<(bool successful, string message)> TryPromoteAnonAccount(string userName, string email, string password)
        {
            if (!IsAnonymous)
                return (false, "Only anonymous accounts can be linked.");

            try
            {
                var emailCredential = EmailProvider.GetCredential(email, password);
                var newUserCred = await _user.LinkWithCredentialAsync(emailCredential);
                if (newUserCred != null && !newUserCred.User.IsAnonymous)
                {
                    await newUserCred.User.ChangeDisplayNameAsync(userName);

                    SetUser(newUserCred);
                }
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Unhandled error occured during linking up -> {ex.Reason}");
            }

            if (!IsAnonymous)
            {
                return (true, $"Signed in as {UserName}");
            }
            else
            {
                return (false, "Failed to link anonymous account.");
            }
        }

        public async Task<(bool successful, string message)> TryResetPassword(string email)
        {
            if (IsLoggedIn)
                return (false, "Already logged in.");

            try
            {
                await _authClient.ResetEmailPasswordAsync(email);
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Unhandled error occured during reset -> {ex.Reason}");
            }

            return (true, $"Reset email sent to {email}.");
        }

        public async Task<bool> SignOut()
        {
            if (!IsLoggedIn)
                return false;

            try
            {
                await _authClient.SignOutAsync();
                SetUser((User) null);
            }
            catch (FirebaseAuthException)
            {
                return false;
            }

            return true;
        }

        public async Task<T> ReadData<T>(string path)
        {
            try
            {
                var request = CreateRequest(path);
                var response = await _dataClient.GetAsync<T>(request);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        public async Task<bool> WriteData<T>(string path, T value)
        {
            try
            {
                var request = CreateRequest(path, value);
                var response = await _dataClient.PutAsync<T>(request);
                return response != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private RestRequest CreateRequest(string path, object value = null)
        {
            var request = new RestRequest(path)
            {
                JsonSerializer = _restSerializer
            };

            if (value != null)
            {
                request.AddJsonBody(value);
            }

            return request;
        }
    }
}