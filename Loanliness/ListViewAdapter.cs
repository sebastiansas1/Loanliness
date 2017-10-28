using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Loanliness
{
    internal class ListViewAdapter:BaseAdapter
    {
        private MainActivity mainActivity;
        private List<MessageContent> listMessages;

        public ListViewAdapter(MainActivity mainActivity, List<MessageContent> listMessages)
        {
            this.mainActivity = mainActivity;
            this.listMessages = listMessages;
        }

        public override int Count
        {
            get
            {
                return listMessages.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.Message_Item_List, null);

            TextView message_user, message_content;
            LinearLayout userBox;

            message_user = itemView.FindViewById<TextView>(Resource.Id.txtEmailChat);
            message_content = itemView.FindViewById<TextView>(Resource.Id.txtMessageChat);
            userBox = itemView.FindViewById<LinearLayout>(Resource.Id.userBoxChat);


            if (position > 0)
            {

                if (listMessages[position].Email == listMessages[position - 1].Email)
                {
                    message_content.Text = listMessages[position].Message;
                    userBox.Visibility = ViewStates.Gone;
                }
                else
                {
                    message_user.Text = listMessages[position].Email;
                    message_content.Text = listMessages[position].Message;
                    userBox.Visibility = ViewStates.Visible;
                }
            }

            if (position == 0) {
                message_user.Text = listMessages[position].Email;
                message_content.Text = listMessages[position].Message;
                userBox.Visibility = ViewStates.Visible;
            }

            return itemView;

        }
    }
}