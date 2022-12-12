/* 
 * LIBRERIA DI GESTIONE DELLA COMUNICAZIONE SERIALE CON QUIZZETTINO
 */
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzettinoDemo
{
    internal class Quizzettino
    {
        public enum ButtonCode
        {
            Reset = 0,
            Button1, Button2, Button3, Button4, Button5, Button6,
            OKButton, NOButton,
            AutoReset, Sound
        }
        // Comandi seriali
        public string[] ButtonCmd = new string[11] { "R", "1", "2", "3", "4", "5", "6", "+", "-", "A", "S" };

        public static SerialPort Port = new SerialPort();

        public Color[] ButtonColor = new Color[6] { Color.Pink, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Red };

        private bool[] _Button = new bool[6];

        public event EventHandler<QuizzettinoButtonEventArgs>? ButtonEvent;

        private bool _FromSerial = false;

        public string PortName
        {
            get { return Port.PortName; }
            set
            {
                if (value == "") throw new Exception("Missing port name");
                // Cerca la porta tra quelle presenti nel sistema
                string[] ports = SerialPort.GetPortNames();
                bool PortOK = false;
                foreach (string port in ports)
                    if (port == value) PortOK = true;
                if (!PortOK) throw new Exception("Unknown port name");
                Port.PortName = value;
            }
        }

        public int PortSpeed
        {
            get { return Port.BaudRate; }
            set
            { Port.BaudRate = value; }
        }

        public int PortTimeout
        {
            set
            {
                Port.ReadTimeout = value;
                Port.WriteTimeout = value;
            }
            get { return Port.ReadTimeout; }
        }

        public void SetButton(int i, bool state)
        {
            SetButton((ButtonCode)i, state);
        }

        public void SetButton(ButtonCode button, bool state)
        {
            int b = ((int)button);
            if (button >= ButtonCode.Button1 && button <= ButtonCode.Button6) _Button[b - 1] = state;
            if (_FromSerial)
            {   // Se il comando è arrivato da seriale, invoco l'evento al programma di gestione
                ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = button, State = state });
            }
            else
            {   // Se il comando è arrivato dal programma, lo invia via seriale a Quizzettino
                if (Port.IsOpen)
                {
                    string cmd = ButtonCmd[b];
                    // Pulsanti con stato: se disattivo, il comando è in minuscolo
                    if (state = false && (button == ButtonCode.AutoReset || button == ButtonCode.Sound)) cmd = cmd.ToLower();
                    Port.Write(cmd);
                }
            }
        }

        public bool GetButton(int i)
        {
            if (i < 1 || i > 6) return false;
            return _Button[i-1];
        }

        public class QuizzettinoButtonEventArgs : EventArgs
        {
            public ButtonCode Button { get; set; }
            public bool State { get; set; }
        }

        public Quizzettino()
        {
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Handshake = Handshake.None;
            Port.ReadTimeout = PortTimeout;
            Port.WriteTimeout = PortTimeout;
            for (int i = 0; i < 6; ++i)
            {
                SetButton(i, false);
            }
            return;
        }

        public bool Open()
        {
            if (Port.PortName == "") return false;
            if (Port.IsOpen) Port.Close();
            Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Port.Open();
            return true;
        }

        public void Close()
        {
            try
            { if (Port.IsOpen) Port.Close(); }
            catch (Exception)
            { }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            _FromSerial = true;
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting().Replace("\r", "").Replace("\n", "");
            foreach (char c in indata)
            {
                int g = 0;
                switch (c)
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                        // Il giocatore 'c' ha premuto il pulsante!
                        g = c - '0';
                        SetButton(g, true);
                        break;
                    case 'R':  // Reset
                        for (g = 1; g <= 6; ++g)
                            SetButton(g, false);
                        break;
                    case '+': // SI
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.OKButton, State = true });
                        break;
                    case '-': // NO
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.NOButton, State = true });
                        break;
                    case 'A':
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.AutoReset, State = true });
                        break;
                    case 'a':
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.AutoReset, State = false });
                        break;
                    case 'S':
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.Sound, State = true });
                        break;
                    case 's':
                        ButtonEvent?.Invoke(this, new QuizzettinoButtonEventArgs() { Button = ButtonCode.Sound, State = false });
                        break;
                }
            }
            _FromSerial = false;
        }

    }
}
