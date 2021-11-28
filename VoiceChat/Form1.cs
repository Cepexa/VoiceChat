using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceChat
{

    public partial class Form1 : Form
    {
        //Подключены ли мы
        private bool connected;
        //сокет отправитель
        Socket client;

        Direct direct;
        //поток для прослушивания входящих сообщений
        Thread in_thread;
        //сокет для приема (протокол UDP)
        Socket listeningSocket;
        //string myIp;
        public Form1()
        {
            InitializeComponent();
            clients = new List<Client>();

            direct = new Direct();
            //добавляем код обработки нашего голоса, поступающего на микрофон
            direct.input.DataAvailable += Voice_Input;
            //сокет для отправки звука
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            connected = true;
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            IPAddress[] AddressAr = null;
            String ServerHostName = "";
            try
            {
                ServerHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostByName(ServerHostName);
                AddressAr = ipEntry.AddressList;
                foreach (var it in AddressAr)
                    comboBox1.Items.Add(it.ToString());
                comboBox1.Text = comboBox1.Items[0].ToString();
                tbName.Text = ServerHostName.ToString();
                //myIp = comboBox1.Text;
            }
            catch (Exception) { }

            if (AddressAr == null || AddressAr.Length < 1)
            {//
                MessageBox.Show("Unable to get local address ... Error");
            }
        }

        private void Listening()
        {
            //Прослушиваем по адресу
            //listeningSocket.Bind(new IPEndPoint(IPAddress.Parse(myIp), int.Parse(tbPortMy.Text)));
            listeningSocket.Bind(myEndPoint);
            //начинаем воспроизводить входящий звук
            direct.output.Play();
            //адрес, с которого пришли данные
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
            //бесконечный цикл
            while (connected == true)
            {
                try
                {
                    //промежуточный буфер
                    byte[] data = new byte[65535];
                    //получено данных
                    int received = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    //добавляем данные в буфер, откуда output будет воспроизводить звук
                    try { direct.bufferStream.AddSamples(data, 0, received); }
                    catch { }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Voice_Input(object sender, WaveInEventArgs e)
        {
            try
            {
                //Подключаемся к удаленному адресу
                //IPEndPoint remote_point = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(tbPort.Text));
                //посылаем байты, полученные с микрофона на удаленный адрес
                //client.SendTo(e.Buffer, remote_point);
                client.SendTo(e.Buffer, corrEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void linked()
        {
            try
            {
                direct.input.StartRecording();
                //создаем поток для прослушивания
                in_thread = new Thread(new ThreadStart(Listening));
                //запускаем его
                in_thread.Start();
            }
            catch (Exception ex)
            {
                string msg = "Error when starting recording";
                var mmex = ex as NAudio.MmException;
                if (mmex != null && mmex.Result == NAudio.MmResult.InvalidParameter && mmex.Message.IndexOf("waveInOpen") > -1)
                    msg += "warning about AV software";
                MessageBox.Show(ex.Message + "\n" + msg);
            }
        }
        void unlinked()
        {
            direct.input.DataAvailable -= Voice_Input;
            direct.output.Stop();
            connected = false;
            direct.closeDirect();
            listeningSocket.Close();
            listeningSocket.Dispose();
            client.Dispose();
            client.Close();
            in_thread?.Suspend();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            linked();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            unlinked();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //myIp = comboBox1.Text;
        }
        bool calling = false;
        uint ID;
        EndPoint myEndPoint;
        EndPoint corrEndPoint;
        Socket mySocket;
        string[] list;
        List<Client> clients;
        bool stopped;
        bool disconnect = false;
        Thread task = null;
        private void btnLink_Click(object sender, EventArgs e)
        {
            try
            {
                stopped = false;
                disconnect = false;
                if (tbName.Text == "")
                {
                    MessageBox.Show("Имя не может быть пустым!");
                    return;
                }
                EndPoint endPoint = new IPEndPoint(IPAddress.Parse(tbIpServ.Text), int.Parse(tbPortServ.Text));
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var data = Encoding.UTF8.GetBytes(tbName.Text);
                socket.Connect(endPoint);
                socket.Send(data);
                var buffer = new byte[256];
                var size = 0;
                var answer = new StringBuilder();
                do
                {
                    size = socket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (socket.Available > 0);
                ID = uint.Parse(answer.ToString().Split(' ')[0]);
                string strEndPoint = answer.ToString().Split(' ')[1];
                myEndPoint = new IPEndPoint(IPAddress.Parse(strEndPoint.ToString().Split(':')[0]),
                            int.Parse(strEndPoint.ToString().Split(':')[1]));
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                task = new Thread(new ThreadStart(received));
                task.Start();
                btnLink.Enabled = false;
                btnExit.Enabled = true;
                tbIpServ.Enabled = false;
                tbPortServ.Enabled = false;
                tbName.Enabled = false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void received()
        {
            while (!stopped)
            {
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mySocket.Bind(myEndPoint);
                mySocket.Listen(10);
                while (!mySocket.Poll(1, new SelectMode())||disconnect) { }
                var s = mySocket.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                StringBuilder data = new StringBuilder();
                do
                {
                    size = s.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (mySocket.Available > 0);
                list = data.ToString().Split(' ');
                if (list[list.Length - 1] != "")
                {
                    setEndPointForConnect(data.ToString(),0);
                    list = null;
                    calling = true;
                }
                s.Send(Encoding.UTF8.GetBytes("Успех"));
                s.Shutdown(SocketShutdown.Both);
                s.Close();
                mySocket.Close();
            }
        }
        void updateList()
        {
            clients.Clear();
            listBox1.Items.Clear();
            foreach (var item in list)
            {
                if (item != "")
                {
                    var newClient = new Client(uint.Parse(item.Split('-')[0]), item.Split('-')[1]);
                    clients.Add(newClient);
                    listBox1.Items.Add(newClient.getInfo());
                }
            }
            list = null;
        }
        uint findClient(string info){
            return clients.Find(i => i.getInfo() == info).ID;
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            var idCor = findClient(listBox1.Text);
            if (idCor == ID)
            {
                MessageBox.Show("Нельзя звонить самому себе!");
                return;
            }
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(tbIpServ.Text), int.Parse(tbPortServ.Text)+1);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var data = Encoding.UTF8.GetBytes(ID + " " + idCor);
            socket.Connect(endPoint);
            socket.Send(data);
            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();
            do
            {
                size = socket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (socket.Available > 0);
            setEndPointForConnect(answer.ToString());
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            linked();
            btnCloseCall.Enabled = true;
        }
        void setEndPointForConnect(string str,int a = 1)
        {
            string endPointStr = str.Split(' ')[a];
            corrEndPoint = new IPEndPoint(IPAddress.Parse(endPointStr.Split(':')[0]),
                                            int.Parse(endPointStr.Split(':')[1]));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(list != null)
                updateList();
            btnAnswer.Enabled = calling;
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            linked();
            btnCloseCall.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            stopped = true;
            disconnect = true;
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(tbIpServ.Text), int.Parse(tbPortServ.Text));
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var data = Encoding.UTF8.GetBytes(ID.ToString());
            socket.Connect(endPoint);
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
            task?.Suspend();
            btnLink.Enabled = true;
            btnExit.Enabled = false;
            tbIpServ.Enabled = true;
            tbPortServ.Enabled = true;
            tbName.Enabled = true;
        }

        private void btnCloseCall_Click(object sender, EventArgs e)
        {
            btnCloseCall.Enabled = false;
            btnCall.Enabled = true;
            btnAnswer.Enabled = false;
            calling = false;
            unlinked();
        }
    }
    class Client {
        public uint ID;
        public string name;
        public Client(uint id,string name)
        {
            this.ID = id;
            this.name = name;
        }
        public string getInfo()
        {
            return ID +" - " + name;
        }
        
    }
    class Direct
    {
        const int D = 32000; //частота дискретизации 32000 Гц(32кГц)
        const int W = 32; //ширина сэмпла - 32 битa
        const int Ch = 1; //кол-во каналов (1-моно, 2 стерео)
        //поток для нашей речи
        public WaveIn input;
        //поток для речи собеседника
        public WaveOut output;
        //буфферный поток для передачи через сеть
        public BufferedWaveProvider bufferStream;
        public Direct()
        {
            //создаем поток для записи нашей речи
            input = new WaveIn();
            //определяем его формат - частота дискретизации D, ширина сэмпла - W, канал - Ch
            input.WaveFormat = new WaveFormat(D, W, Ch);

            //создаем поток для прослушивания входящего звука
            output = new WaveOut();
            //создаем поток для буферного потока и определяем у него такой же формат как и потока с микрофона
            bufferStream = new BufferedWaveProvider(new WaveFormat(D, W, Ch));
            //привязываем поток входящего звука к буферному потоку
            output.Init(bufferStream);
        }
        public void closeDirect()
        {
            if (output != null)
            {
                output.Stop();
                output.Dispose();
                output = null;
            }
            if (input != null)
            {
                input.Dispose();
                input = null;
            }
            bufferStream = null;
        }
    }
}
