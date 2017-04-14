﻿using System;

using UIKit;

using System.Threading.Tasks;
using Square.SocketRocket;

using NomadCode.BotFramework;

namespace Agencies.iOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController (IntPtr handle) : base (handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            BotClient.Shared.ReadyStateChanged += handleBotClientReadyStateChanged;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);

            Task.Run (async () =>
            {
                try
                {
                    await BotClient.Shared.ConnectSocketAsync ();
                }
                catch (Exception ex)
                {
                    Log.Error (ex.Message);
                    throw;
                }
            });
        }

        void handleBotClientReadyStateChanged (object sender, SocketStateChangedEventArgs e)
        {
            Log.Debug ($"{e.SocketState}");

            switch (e.SocketState)
            {
                case SocketStates.Open:
                    BotClient.Shared.SendMessage ("Hello World");
                    break;
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}