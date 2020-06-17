using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class FormClient : Form
    {
        private const string ClientGuid = "04C22ADA-6E1E-4821-B267-7FA3DCA31DBB";
        private int _clientNumber;
        private readonly List<Subscription> _subscriptions = new List<Subscription>();
        private Semaphore _semaphore;
        

        public FormClient()
        {
            InitializeComponent();
            Disposed += OnDisposed;
        }

        private void  OnDisposed(object sender, EventArgs e)
        {
            _semaphore?.Dispose();
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
        }

        private void btnStopClient_Click(object sender, EventArgs e)
        {
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            if (!Semaphore.TryOpenExisting(ClientGuid, out _semaphore))
            {
                _semaphore = new Semaphore(0, 1000, ClientGuid);
            }
            _clientNumber = _semaphore.Release() + 1;
            Text = "Client-" + _clientNumber;
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
            var subscription = new Subscription(accountName, Text, logControl1);
            _subscriptions.Add(subscription);
            await subscription.SubscribeAsync();
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
    }
}