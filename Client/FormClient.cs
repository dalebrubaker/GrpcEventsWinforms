﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common;
using Grpc.Core;

namespace Client
{
    public partial class FormClient : Form
    {
        private const string ClientGuid = "04C22ADA-6E1E-4821-B267-7FA3DCA31DBB";
        private int _clientNumber;
        private readonly List<Subscription> _subscriptions = new List<Subscription>();
        private Semaphore _semaphore;
        private Metadata _headers;
        private readonly FormSettings _formSettings = new FormSettings();

        public FormClient()
        {
            InitializeComponent();
            Disposed += OnDisposed;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            _semaphore?.Dispose();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            Location = _formSettings.FormLocation;
            Size = _formSettings.FormSize;
            if (!Semaphore.TryOpenExisting(ClientGuid, out _semaphore))
            {
                _semaphore = new Semaphore(0, 1000, ClientGuid);
            }
            _clientNumber = _semaphore.Release() + 1;
            Text = "Client" + _clientNumber;
            _headers = new Metadata
            {
                {Constants.Requester, Text}
            };
        }

        private async void btnSubscribeEvent_Click(object sender, EventArgs e)
        {
            var accountName = txtAccountName.Text;
            if (string.IsNullOrEmpty(accountName))
            {
                MessageBox.Show(this, "Enter an account name to which to subscribe events.");
                return;
            }
            if (_subscriptions.Exists(x => x.AccountName == accountName))
            {
                MessageBox.Show(this, "Duplicate account names are not allowed.");
                return;
            }
            var subscription = new Subscription(accountName, Text, logControl1, _headers, RemoveSubscription);
            var success = await subscription.SubscribeAsync();
            if (success)
            {
                _subscriptions.Add(subscription);
            }
        }

        private void RemoveSubscription(string accountName)
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                var subscription = _subscriptions[i];
                if (subscription.AccountName == accountName)
                {
                    _subscriptions.RemoveAt(i--);
                    return;
                }
            }
        }

        public async void btnUnsubscribeEvent_Click(object sender, EventArgs e)
        {
            var accountName = txtAccountName.Text;
            if (string.IsNullOrEmpty(accountName))
            {
                MessageBox.Show(this, "Enter an account name from which to unsubscribe events.");
                return;
            }
            var subscription = _subscriptions.FirstOrDefault(x => x.AccountName == accountName);
            if (subscription == null)
            {
                MessageBox.Show(this, $"Not currently subscribed to {accountName}");
                return;
            }
            _subscriptions.Remove(subscription);
            await subscription.UnsubscribeAsync();
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formSettings.FormLocation = Location;
            _formSettings.FormSize = Size;
            _formSettings.Save();
        }
    }
}