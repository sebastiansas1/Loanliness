using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Auth;
using Firebase;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;

namespace Loanliness
{
    [Activity(Label = "Loanliness", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, IValueEventListener
    {
        private FirebaseClient firebase;
        private List<MessageContent> listMessages = new List<MessageContent>();
        private ListView listChat;
        private EditText editChat;
        private Button sendIt;
        private Button logOut;
        public int MyResultCode = 1;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            this.Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);

            editChat = FindViewById<EditText>(Resource.Id.txtInputMessageChat);
            listChat = FindViewById<ListView>(Resource.Id.listOfMessageChat);
            sendIt = FindViewById<Button>(Resource.Id.sendMessage);
            logOut = FindViewById<Button>(Resource.Id.btnLogOutChat);

            var options = new FirebaseOptions.Builder()
                                             .SetApplicationId("1:1058907657016:android:91dad6ec07dc7809")
                                             .SetApiKey("AIzaSyDlo65lHiha_zKx-FGeX_KKusWmv0CnSpE")
                                             .SetDatabaseUrl("https://loanliness-4097c.firebaseio.com")
                                             .SetGcmSenderId("1058907657016")
                                             .Build();
            var firebaseApp = FirebaseApp.InitializeApp(this, options);

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
            FirebaseDatabase.Instance.GetReference("chats").AddValueEventListener(this);

            //mSend.Click += MSend_Click;

            sendIt.Click +=  (sender, e) => 
            {
                PostMessage();
                DisplayChatMessage();  
            };

            logOut.Click += (sender, e) => 
            {
                StartActivity(typeof(LoginActivity));
            };


            if (FirebaseAuth.Instance.CurrentUser == null)
            {
                StartActivityForResult(new Android.Content.Intent(this, typeof(LoginActivity)), MyResultCode);
            }
            else
            {
                Toast.MakeText(this, "Welcome " + FirebaseAuth.Instance.CurrentUser.Email, ToastLength.Short).Show();
                DisplayChatMessage();
            }
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            DisplayChatMessage();
        }

        private async void DisplayChatMessage()
        {
            listMessages.Clear();
            var items = await firebase.Child("chats").OnceAsync<MessageContent>();
            foreach (var item in items)
                listMessages.Add(item.Object);
            ListViewAdapter adapter = new ListViewAdapter(this, listMessages);
            listChat.Adapter = adapter;
        }

        private async void PostMessage()
        {
            var items = await firebase.Child("chats").PostAsync(new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, editChat.Text));
            editChat.Text = "";
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

