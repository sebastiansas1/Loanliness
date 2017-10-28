﻿using System;

namespace Loanliness
{
    internal class MessageContent
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }

        public MessageContent() {  }
        public MessageContent(string Email, string Message)
        {
            this.Email = Email;
            this.Message = Message;
            Time = DateTime.Now.ToString("t");
        }

    }
}