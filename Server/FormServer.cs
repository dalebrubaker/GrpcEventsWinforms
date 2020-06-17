using System;
using System.Windows.Forms;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Server
{
    public partial class FormServer : Form
    {
        private Grpc.Core.Server _server;
        private readonly FormSettings _formSettings = new FormSettings();
        private readonly SynchronizationContextHelper _syncContextHelper;

        public FormServer()
        {
            InitializeComponent();
            _syncContextHelper = new SynchronizationContextHelper();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            var eventsServiceImpl = new EventsServiceImpl(logControl1);
            _server = new Grpc.Core.Server
            {
                Services =
                {
                    EventsService.BindService(eventsServiceImpl)
                },
                Ports = {new ServerPort("localhost", Constants.ServerPort, ServerCredentials.Insecure)}
            };
            _server.Start();
            logControl1.LogMessage("Started server");

            eventsServiceImpl.AccountNameAdded += EventsServiceImplOnAccountNameAdded;
            eventsServiceImpl.AccountNameRemoved += EventsServiceImplOnAccountNameRemoved;
        }

        private void EventsServiceImplOnAccountNameAdded(object sender, string accountName)
        {
            _syncContextHelper.Send(_ =>
            {
                if (cbxAcctNames.Items.Contains(accountName))
                {
                    // duplicate subscription
                    return;
                }
                cbxAcctNames.Items.Add(accountName);
                cbxAcctNames.SelectedIndex = cbxAcctNames.Items.Count - 1;
            });
        }

        private void EventsServiceImplOnAccountNameRemoved(object sender, string accountName)
        {
            _syncContextHelper.Send(_ =>
            {
                cbxAcctNames.Items.Remove(accountName);
                cbxAcctNames.SelectedIndex = cbxAcctNames.Items.Count - 1;
                cbxAcctNames.ResetText();
            });
        }

        private async void btnStopServer_Click(object sender, EventArgs e)
        {
            logControl1.LogMessage("Shutting down server");
            await _server.ShutdownAsync();
            logControl1.LogMessage("Shut down server");
        }

        private void btnSendEvent_Click(object sender, EventArgs e)
        {
            var accountName = (string)cbxAcctNames.SelectedItem;
            var account = Accounts.RequireAccount(accountName);
            account.OnSampleEvent(accountName, "Server");
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            Text = $"Server@localhost:{Constants.ServerPort}";
            Location = _formSettings.FormLocation;
            Size = _formSettings.FormSize;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formSettings.FormLocation = Location;
            _formSettings.FormSize = Size;
            _formSettings.Save();
        }
    }
}