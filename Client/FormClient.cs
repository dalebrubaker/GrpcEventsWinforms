using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class FormClient : Form
    {
        private static int _sClientNumber;
        private int _clientNumber;
        private readonly List<Subscription> _subscriptions = new List<Subscription>();

        public FormClient()
        {
            InitializeComponent();
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
        }

        private void btnStopClient_Click(object sender, EventArgs e)
        {
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            _clientNumber = _sClientNumber++;
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
    }
}