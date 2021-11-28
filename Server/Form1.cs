using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Server
{
    public partial class Form1 : Form
    {
        bool stopped = true;
        string ipServ;
        string portServ;
        EndPoint endPointServ;
        Socket socket;
        EndPoint endPointConnect;
        Socket socketConnect;
        List<Client> clients;
        public Form1()
        {
            InitializeComponent();
            try
            {
                foreach (var it in Dns.GetHostByName(Dns.GetHostName()).AddressList)
                    cbIpServ.Items.Add(it.ToString());
                cbIpServ.Text = cbIpServ.Items[0].ToString();
                clients = new List<Client>(10);
            }
            catch (Exception) { }
        }
        void registredClient()
        {
            while (!stopped)
            {
                endPointServ = new IPEndPoint(IPAddress.Parse(ipServ), int.Parse(portServ));
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(endPointServ);
                socket.Listen(10);
                while (!socket.Poll(1, new SelectMode())) { }
                var s = socket.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                StringBuilder data = new StringBuilder();
                do
                {
                    size = s.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (socket.Available > 0);
                string name = data.ToString();
                uint id;
                if (uint.TryParse(name, out id))
                {
                    clients.Remove(clients.Find(i => i.ID == id));
                    s.Send(Encoding.UTF8.GetBytes("Успех"));
                }
                else
                {
                    Client newClient = new Client(s.RemoteEndPoint, name);
                    clients.Add(newClient);
                    s.Send(Encoding.UTF8.GetBytes(newClient.ID.ToString() + " " + newClient.endPoint));
                }
                s.Shutdown(SocketShutdown.Both);
                s.Close();
                socket.Close();
                sendListClients();
            }
        }
        void connectedClient()
        {
            while (!stopped)
            {
                endPointConnect = new IPEndPoint(IPAddress.Parse(ipServ), int.Parse(portServ) + 1);
                socketConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketConnect.Bind(endPointConnect);
                socketConnect.Listen(10);
                while (!socketConnect.Poll(1, new SelectMode())) { }
                var s = socketConnect.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                StringBuilder data = new StringBuilder();
                do
                {
                    size = s.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (s.Available > 0);
                string[] ids = data.ToString().Split(' ');
                uint idFirst = uint.Parse(ids[0]);
                uint idSecond = uint.Parse(ids[1]);
                BindingClients bindingClients = new BindingClients(
                                            clients.Find(i => i.ID == idFirst),
                                            clients.Find(i => i.ID == idSecond));
                s.Send(Encoding.UTF8.GetBytes(bindingClients.GetInfo()));
                calling(bindingClients);
                s.Shutdown(SocketShutdown.Both);
                s.Close();
                socketConnect.Close();
            }
        }
        void calling(BindingClients bindingClients)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var data = Encoding.UTF8.GetBytes(bindingClients.GetInfo());
                socket.Connect(bindingClients.clients[1].endPoint);
                socket.Send(data);
                var buffer = new byte[256];
                var size = 0;
                var answer = new StringBuilder();
                do
                {
                    size = socket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
        void sendListClients()
        {
            var list = GetInfoList();
            foreach (var client in clients)
            {
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        var data = Encoding.UTF8.GetBytes(list);
                        socket.Connect(client.endPoint);
                        socket.Send(data);
                        var buffer = new byte[256];
                        var size = 0;
                        var answer = new StringBuilder();
                        do
                        {
                            size = socket.Receive(buffer);
                            answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                        } while (socket.Available > 0);
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    sendListClients();
                }
            }
        }
        string GetInfoList()
        {   
            string result = "";
            foreach (var client in clients)
            {
                result += client.ID + "-" + client.name+" ";
            }
            return result;
        }
            Task thead1 = null;
            Task thead2 = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(stopped)
            { connect(); }
            else
            { disconnect(); }
        }
       private void connect() {
                stopped = false;
                btnStart.Text = "Stop";
                ipServ = cbIpServ.Text;
                portServ = tbPortServ.Text;
                thead1 = Task.Run(() => registredClient());
                thead2 = Task.Run(() => connectedClient());
        }
        private void disconnect() {
            if (!stopped)
            {
                stopped = true;
                btnStart.Text = "Start";
                try
                {
                    thead1.Dispose();
                    thead2.Dispose();
                }
                catch { }
                if (socket.Connected)
                    socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                if (socket.Connected)
                    socketConnect.Shutdown(SocketShutdown.Both);
                socketConnect.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnect();
        }
    }



    class Client
    {
        public uint ID { get; set; }
        public EndPoint endPoint;
        public string name;
        static uint nextId = 1;
        public Client(EndPoint endPoint,string name)
        {
            this.endPoint = endPoint;
            this.name = name;
            ID = nextId++;
        }
    }
    class BindingClients
    {
        const int countClients = 2;
        public Client[] clients = new Client[countClients];
        public BindingClients(Client firstClient, Client secondClient)
        {
            clients[0] = firstClient;
            clients[1] = secondClient;
        }
        public string GetInfo()
        {
            return clients[0].endPoint.ToString()+" "+ clients[1].endPoint.ToString();
        }
    }
}
