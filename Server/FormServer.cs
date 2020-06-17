using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public FormServer()
        {
            InitializeComponent();
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
            logControl1.LogMessage("Starting server");
            _server.Start();
            logControl1.LogMessage("Started server");

            eventsServiceImpl.AccountNameAdded += EventsServiceImplOnAccountNameAdded;
        }

        private void  EventsServiceImplOnAccountNameAdded(object sender, string accountName)
        {
            cbxAcctNames.Items.Add(accountName);
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