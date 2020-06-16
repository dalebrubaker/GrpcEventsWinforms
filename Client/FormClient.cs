using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Grpc.Core;
using GrpcEvents;

namespace Client
{
    public partial class FormClient : Form
    {
        private readonly EventsService.EventsServiceClient _client;
        private AsyncDuplexStreamingCall<SubscribeRequest, SampleEventArgsMessage> _call;
        private static int _sClientNumber;
        private int _clientNumber;
        private readonly List<string> _accountNamesSubscribed = new List<string>();

        public FormClient()
        {
            InitializeComponent();
            var channel = new Channel("localhost:" + Constants.ServerPort, ChannelCredentials.Insecure);
            _client = new EventsService.EventsServiceClient(channel);
            Disposed += OnDisposed;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            _call.Dispose();
            _call = null;
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
        }

        private async void btnStopClient_Click(object sender, EventArgs e)
        {
            var request = new SubscribeRequest
            {
                StartStop = false
            };
            logControl1.LogMessage($"{Text} is unsubscribing to event {request.AccountName}");
            await _call.RequestStream.WriteAsync(request);
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
            if (_accountNamesSubscribed.Contains(accountName))
            {
                MessageBox.Show(this, "Duplicate account names are not allowed.");
                return;
            }
            if (_call == null)
            {
                _ = Task.Run(StartReceivingEvents);
            }
            _accountNamesSubscribed.Add(accountName);
            var request = new SubscribeRequest
            {
                StartStop = true,
                AccountName = accountName
            };
            logControl1.LogMessage($"{Text} is subscribing to event {request.AccountName}");
            try
            {
                await _call.RequestStream.WriteAsync(request);
            }
            catch (RpcException ex)
            {
                logControl1.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }

        private async Task StartReceivingEvents()
        {
            try
            {
                using (_call = _client.Subscribe())
                {
                    logControl1.LogMessage($"{Text} is waiting for events...");
                    while (await _call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var sampleEventArgsMessage = _call.ResponseStream.Current;
                        var sampleEventArgs = sampleEventArgsMessage.ToSampleEventArgs();
                        var msg = $"Received {sampleEventArgs}";
                        logControl1.LogMessage(msg);
                        var account = new Account(sampleEventArgs.AccountName);
                        account.OnSampleEvent(sampleEventArgs);
                        msg = $"Re-threw event client-side {sampleEventArgs}";
                        logControl1.LogMessage(msg);
                    }
                }
            }
            catch (RpcException ex)
            {
                logControl1.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }

        private async void btnUnsubscribeEvent_Click(object sender, EventArgs e)
        {
            var accountName = txtAccountName.Text;
            if (string.IsNullOrEmpty(accountName))
            {
                MessageBox.Show(this, "Enter an account name from which to unsubscribe events.");
                return;
            }
            if (!_accountNamesSubscribed.Contains(accountName))
            {
                MessageBox.Show(this, $"Not currently subscribed to {accountName}");
                return;
            }
            _accountNamesSubscribed.Remove(accountName);
            var request = new SubscribeRequest
            {
                StartStop = true,
                AccountName = txtAccountName.Text
            };
            try
            {
                logControl1.LogMessage($"{Text} is un-subscribing from event {request.AccountName}");
                await _call.RequestStream.WriteAsync(request);
            }
            catch (RpcException ex)
            {
                logControl1.LogMessage(ex.Message);
                _call = null;
                throw;
            }
        }
    }
}