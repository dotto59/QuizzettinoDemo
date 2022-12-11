using System.IO.Ports;

namespace QuizzettinoDemo
{
    public partial class Form1 : Form
    {
        static SerialPort serialPort = new SerialPort();
        static private Button[] btnGiocatore = new Button[6];
        static private Label[] lblGiocatore = new Label[6];
        static private bool[] Acceso = new bool[6];
        static private bool DaSeriale = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                lstPorts.Items.Add(port);
            if (lstPorts.Items.Count > 0) lstPorts.SelectedIndex = 0;
            for (int c=0; c<=5; ++c)
            {
                btnGiocatore[c] = new Button();
                btnGiocatore[c].Text = (c + 1).ToString();
                btnGiocatore[c].Width = 75;
                btnGiocatore[c].Height = 23;
                btnGiocatore[c].Top = 76;
                btnGiocatore[c].Left = 12 + c * 80;
                btnGiocatore[c].BackColor = ButtonColor(c);
                btnGiocatore[c].Visible = true;
                btnGiocatore[c].Enabled = false;
                this.Controls.Add(btnGiocatore[c]);
                btnGiocatore[c].Click += new EventHandler(ClickButton);
                lblGiocatore[c] = new Label();
                lblGiocatore[c].Text = "";
                lblGiocatore[c].Width = 75;
                lblGiocatore[c].Height = 15;
                lblGiocatore[c].Top = 58;
                lblGiocatore[c].Left = 12 + c * 80;
                lblGiocatore[c].Visible = true;
                Acceso[c] = false;
                lblGiocatore[c].BackColor = LabelColor(c);
                this.Controls.Add(lblGiocatore[c]);
            }
            chkAutoReset.Checked = false;
            chkSuoni.Checked = true;
        }

        private Color ButtonColor(int c)
        {
            if (c == 0) return Color.Pink;
            if (c == 1) return Color.Blue;
            if (c == 2) return Color.Green;
            if (c == 3) return Color.Yellow;
            if (c == 4) return Color.Orange;
            return Color.Red;
        }

        private Color LabelColor(int c)
        {
            if (Acceso[c])
                return ButtonColor(c);
            else
                return Color.Gray;
        }

        private void ClickButton(Object sender, EventArgs e)
        {
            //int c = int.Parse(((Button)sender).Text.ToString()) - 1;
            //Acceso[c] = !Acceso[c];
            //lblGiocatore[c].BackColor = LabelColor(c, Acceso[c]);
            serialPort.Write(((Button)sender).Text);
        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {
            if (btnConnetti.Text == "Disconnetti")
            {
                serialPort.Close();
                lstPorts.Enabled = true;
                btnConnetti.Text = "Connetti";
                btnReset.Enabled = false;
                for (int i = 0; i <= 5; ++i)
                {
                    btnGiocatore[i].Enabled = false;
                    Acceso[i] = false;
                    lblGiocatore[i].BackColor = LabelColor(i);
                }
                btnSI.Enabled = false;
                btnNO.Enabled = false;
                return;
            }
            try
            {
                serialPort.PortName = ("" + lstPorts.SelectedItem);
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;

                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                serialPort.Open();

                serialPort.Write("R");
            }
            catch (Exception)
            {
                MessageBox.Show("Errore durante l'apertura della porta " + serialPort.PortName);
                return;
            }

            lstPorts.Enabled = false;
            btnConnetti.Text = "Disconnetti";
            btnReset.Enabled = true;
            for (int i = 0; i <= 5; ++i)
            {
                btnGiocatore[i].Enabled = true;
                Acceso[i] = false;
                lblGiocatore[i].BackColor = LabelColor(i);
            }
            btnSI.Enabled = true;
            btnNO.Enabled = true;
            if (chkAutoReset.Checked)
                serialPort.Write("A");
            else
                serialPort.Write("a");
            if (chkSuoni.Checked)
                serialPort.Write("S");
            else
                serialPort.Write("s");
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            DaSeriale = true;
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting().Replace("\r", "").Replace("\n", "");
            foreach (char c in indata)
            {                
                switch (c)
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                        // Il giocatore 'c' ha premuto il pulsante!
                        int i = c - '0' - 1;
                        Acceso[i] = true;
                        lblGiocatore[i].BackColor = LabelColor(i);
                        //for (int g = 0; g <= 5; ++g)
                        //    btnGiocatore[g].Enabled = false;
                        break;
                    case 'R':  // Reset
                        for (int g = 0; g <= 5; ++g)
                        {
                            Acceso[g] = false;
                            lblGiocatore[g].BackColor = LabelColor(g);
                        }
                        break;
                    case '+': // SI
                        btnSI.BackColor = Color.Green;
                        Thread.Sleep(1000);
                        btnSI.BackColor = Color.DarkGreen;
                        break;
                    case '-': // NO
                        btnNO.BackColor = Color.Red;
                        Thread.Sleep(1000);
                        btnNO.BackColor = Color.DarkRed;
                        break;
                    case 'A':
                        chkAutoReset.Checked = true;
                        break;
                    case 'a':
                        chkAutoReset.Checked = false;
                        break;
                }
            }
            DaSeriale = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            serialPort.Write("R");
        }

        private void btnSI_Click(object sender, EventArgs e)
        {
            serialPort.Write("+");
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            serialPort.Write("-");
        }

        private void chkAutoReset_CheckedChanged(object sender, EventArgs e)
        {
            if (!DaSeriale && serialPort.IsOpen)
            {
                if (chkAutoReset.Checked)
                    serialPort.Write("A");
                else
                    serialPort.Write("a");
            }
        }

        private void chkSuoni_CheckedChanged(object sender, EventArgs e)
        {
            if (!DaSeriale && serialPort.IsOpen)
            {
                if (chkSuoni.Checked)
                    serialPort.Write("S");
                else
                    serialPort.Write("s");
            }
        }
    }
}