using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AllInOneSDK;

namespace AllInOneSampleApp
{
    public partial class MainPage : ContentPage, PaymentCallback
    {
        public string mid { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string txnToken { get; set; }
        public string message { get; set; }

        public bool isStaging { get; set; }
        public bool restrictAppInvoke { get; set; }
        public bool isApiInProgress { get; set; }

        public MainPage()
        {
            mid = "";
            amount = "";
            txnToken = "";
            setMessage("");
            isStaging = false;
            restrictAppInvoke = false;
            isApiInProgress = true;
            InitializeComponent();

            BindingContext = this;

        }

        public void error(string errorMessage)
        {
            setMessage("error : " + errorMessage);
            AllInOnePlugin.DestroyInstance();
        }

        public void success(Dictionary<string, object> dictionary)
        {
            string value = "{ ";
            foreach (string key in dictionary.Keys)
            {
                value += key + " : " + dictionary[key] + " , ";

            }
            value += " }";
            setMessage(value);
            AllInOnePlugin.DestroyInstance();
        }

        void setMessage(string msg)
        {
            message = "Message: -> " + msg;
            OnPropertyChanged(nameof(message));
        }

        void StartTransaction_Clicked(object sender, EventArgs e)
        {
            if (txnToken.Equals(""))
            {
                return;
            }
            AllInOnePlugin.startTransaction(orderId, mid, txnToken, amount, "", isStaging, restrictAppInvoke, this);
        }
        
        void IsStagingChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            isStaging = e.Value;
        }
        void AppInvokeRestrictionChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            restrictAppInvoke = e.Value;
        }
    }
}
