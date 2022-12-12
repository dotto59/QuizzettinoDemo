using System.IO.Ports;

namespace QuizzettinoDemo
{
    public partial class Form1 : Form
    {
        static Quizzettino quiz = new Quizzettino();
        static private Button[] btnGiocatore = new Button[6];
        static private Label[] lblGiocatore = new Label[6];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                lstPorts.Items.Add(port);
            if (lstPorts.Items.Count == 0)
            {
                MessageBox.Show("Nel sistema non sono presenti porte seriali. Impossibile proseguire!");
                Application.Exit();
                return;
            }
            lstPorts.SelectedIndex = 0;
            for (int c=0; c<=5; ++c)
            {
                lblGiocatore[c] = new Label();
                lblGiocatore[c].Text = "";
                lblGiocatore[c].Width = 75;
                lblGiocatore[c].Height = 15;
                lblGiocatore[c].Top = 18;
                lblGiocatore[c].Left = 12 + c * 80;
                lblGiocatore[c].Visible = true;
                lblGiocatore[c].BackColor = LabelColor(c);
                grpConcorrenti.Controls.Add(lblGiocatore[c]);
                btnGiocatore[c] = new Button();
                btnGiocatore[c].Text = (c + 1).ToString();
                btnGiocatore[c].Width = 75;
                btnGiocatore[c].Height = 23;
                btnGiocatore[c].Top = 36;
                btnGiocatore[c].Left = 12 + c * 80;
                btnGiocatore[c].BackColor = quiz.ButtonColor[c];
                btnGiocatore[c].Visible = true;
                btnGiocatore[c].Enabled = false;
                grpConcorrenti.Controls.Add(btnGiocatore[c]);
                btnGiocatore[c].Click += new EventHandler(ClickButton);
            }
            chkAutoReset.Checked = false;
            chkSuoni.Checked = true;
        }

        private Color LabelColor(int c)
        {
            if (quiz.GetButton(c))
                return quiz.ButtonColor[c-1];
            else
                return Color.Gray;
        }

        private void ClickButton(Object? sender, EventArgs e)
        {
            if (sender == null) return;
            int c = int.Parse(((Button)sender).Text.ToString());
            quiz.SetButton(c, true);            
        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {
            if (btnConnetti.Text == "Disconnetti")
            {
                quiz.Close();
                lstPorts.Enabled = true;
                btnConnetti.Text = "Connetti";
                btnReset.Enabled = false;
                for (int i = 0; i <= 5; ++i)
                {
                    btnGiocatore[i].Enabled = false;
                    lblGiocatore[i].BackColor = LabelColor(i);
                }
                btnSI.Enabled = false;
                btnNO.Enabled = false;
                return;
            }

            // Ok, devo connettermi
            if (lstPorts.SelectedIndex == -1)
            {
                MessageBox.Show("Selezionare una porta seriale dall'elenco");
                return;
            }
            try
            {
                quiz = new Quizzettino();
                // Codice per gli eventi
                quiz.ButtonEvent += ButtonHandler;
                // Imposto la porta
                quiz.PortName = (string)lstPorts.SelectedItem;
                quiz.PortSpeed = 115200;
                quiz.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Errore durante l'apertura della porta " + lstPorts.SelectedText);
                return;
            }

            lstPorts.Enabled = false;
            btnConnetti.Text = "Disconnetti";
            btnReset.Enabled = true;
            for (int i = 0; i <= 5; ++i)
            {
                btnGiocatore[i].Enabled = true;
                lblGiocatore[i].BackColor = LabelColor(i);
            }
            btnSI.Enabled = true;
            btnNO.Enabled = true;
            // Imposta le opzioni
            quiz.SetButton(Quizzettino.ButtonCode.AutoReset, (chkAutoReset.Checked));
            quiz.SetButton(Quizzettino.ButtonCode.Sound, (chkSuoni.Checked));
            // Resetta Quizzettino
            quiz.SetButton(Quizzettino.ButtonCode.Reset, true);

        }

        private void ButtonHandler(object? sender, Quizzettino.QuizzettinoButtonEventArgs e)
        {
            if (sender == null) return;
            int c = (int)e.Button;
            if (c >= 1 && c <= 6)
            {
                // Il giocatore 'c' ha premuto il pulsante!
                lblGiocatore[c-1].BackColor = LabelColor(c);
                return;
            }
            bool value = e.State;
            switch (e.Button)
            {
                case Quizzettino.ButtonCode.Reset:
                    for (int g = 0; g <= 5; ++g)
                        lblGiocatore[g].BackColor = LabelColor(g);
                    break;
                case Quizzettino.ButtonCode.OKButton:
                    btnSI.BackColor = Color.Green;
                    Thread.Sleep(1000);
                    btnSI.BackColor = Color.DarkGreen;
                    break;
                case Quizzettino.ButtonCode.NOButton:
                    btnNO.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    btnNO.BackColor = Color.DarkRed;
                    break;
                case Quizzettino.ButtonCode.AutoReset:
                    chkAutoReset.Checked = e.State;
                    break;
            }

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            quiz.SetButton(Quizzettino.ButtonCode.Reset, true);
        }

        private void btnSI_Click(object sender, EventArgs e)
        {
            quiz.SetButton(Quizzettino.ButtonCode.OKButton, true);
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            quiz.SetButton(Quizzettino.ButtonCode.NOButton, true);
        }

        private void chkAutoReset_CheckedChanged(object sender, EventArgs e)
        {
            quiz.SetButton(Quizzettino.ButtonCode.AutoReset, (chkAutoReset.Checked));
        }

        private void chkSuoni_CheckedChanged(object sender, EventArgs e)
        {
            quiz.SetButton(Quizzettino.ButtonCode.Sound, (chkSuoni.Checked));
        }
    }
}