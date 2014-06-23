using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private HubConnection _hub;
        private IHubProxy _hubProxy;
        private SynchronizationContext _context;

        public Form1()
        {
            InitializeComponent();
            SetupSignalR();
            label1.Text = "Hello from Windows Form";
        }

        private async void SetupSignalR()
        {
            _hub = new HubConnection("http://localhost:23377");
            _hubProxy = _hub.CreateHubProxy("MessageHub");
            _context = SynchronizationContext.Current;

            await _hub.Start();

            _hubProxy.On<string>("hello", (msg) =>
                _context.Post(delegate
                {
                    label1.Text = msg;
                }, null));
        }

        private async void Broadcast()
        {
            await _hubProxy.Invoke("Hello", new object[] { "Hello from a Windows Form!" });
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Broadcast();
        }
    }
}
