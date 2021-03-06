﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Firebase.Auth;
using Android.Gms.Tasks;

namespace Loanliness
{
    [Activity(Label = "LogIn",Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class LoginActivity : AppCompatActivity, IOnCompleteListener
    {
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            auth = FirebaseAuth.Instance;


            var mEmail = FindViewById<EditText>(Resource.Id.txtEmailLogIn);
            var mPass = FindViewById<EditText>(Resource.Id.txtPassLogIn);

            var mBtnReg = FindViewById<Button>(Resource.Id.btnRegLogin);
            var mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignInLogIn);

            mBtnReg.Click += (sender, e) => {
                auth.CreateUserWithEmailAndPassword(mEmail.Text, mPass.Text).AddOnCompleteListener(this);
            };
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Registered Successfully!", ToastLength.Short).Show();
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Registration Failed! Please try again.", ToastLength.Short).Show();
                Finish();
            }
        }
    }
}
