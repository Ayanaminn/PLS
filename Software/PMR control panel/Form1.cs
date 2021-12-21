using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Media; //For notifications

namespace PMR_control_panel
{
    public partial class Form1 : Form
    {
        byte[] data = new byte[1];//Create a byte array of length 1 (1 elenment), store the value in the array, and use the Write overload to write a byte array
        private int counter; //counter to control the cycle times
        private int lightIndex;//control the light status
        

        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
            
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ScrollToCaret();
            textBox1.Text = "Initializing...\r\n";
            timer2.Enabled = true;
            timer2.Interval = 1000;// Pretend system is intializing
            Connect.Enabled = false;  //Disable connect button
            Disconnect.Enabled = false;  //Disable disconnect button
            On.Enabled = false; //Disable LED1 ON button
            Off.Enabled = false; //Disable LED1 OFF button
            Start.Enabled = false; //Disable LED1 OFF button
            trackBar1.Enabled = false; //Disable LED1 brightness
            numericUpDown1.Enabled = false; //Disable light interval set up
            numericUpDown2.Enabled = false; // Disable dark interval set up
            numericUpDown3.Enabled = false; // Disable cycle times set up
        }

        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }


        private void InitializeTimer()
        {
            counter = 0;
            lightIndex = 0;

        }


//----------------------------------Light stimulus cycle control---------------------------------------------------------------------------------//        
        
            //function for switch the light status
        private void UpdateLight()
        {
            // using numerical input as light time interval set
            decimal t_1 = numericUpDown1.Value;
            int interval_1 = Convert.ToInt32(t_1);
            // using numerical input as dark time interval set
            decimal t_2 = numericUpDown2.Value;
            int interval_2 = Convert.ToInt32(t_2);

            lightIndex = 1 - lightIndex; // loop lightindex between 1 and 0

          

            if (lightIndex.Equals(1)) //light cycle
            {
                timer1.Interval = interval_1 *1000;  // Defult unit is miliseconds, *1000 unit is seconds, * 60000 unit is minutes

                byte PWM;
                PWM = Convert.ToByte(trackBar1.Value);
                data[0] = PWM; // or 0 or the value from pwm
                serialPort1.Write(data, 0, 1);

                //light interval indicator : on
                rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape1.FillColor = Color.Yellow;
                //dark interval indicator : off
                rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape2.FillColor = Color.Transparent;

                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "Light cycle now. \r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
            else //dark cycle
            {
                timer2.Interval = interval_2 * 1000;  //*1000 unit is seconds, * 60000 unit is minutes

                data[0] = 0; // or 0 or the value from pwm
                serialPort1.Write(data, 0, 1);

                //light interval indicator : off
                rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape1.FillColor = Color.Transparent;
                //dark interval indicator : on
                rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape2.FillColor = Color.Black;

                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "Dark cycle now. \r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private void timer1_Tick(object sender,EventArgs e)
        {
            // using numerical input as time cycle set
            decimal n = numericUpDown3.Value;
            int cycle = Convert.ToInt32(n);

            if(counter >= cycle * 2) // one light interval + one dark interval make one full cycle
            {
                timer1.Enabled = false; // Exit time loop
                counter = 0;
                Start.Enabled = true;
                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "All photo cycle finished.Please reset the system. \r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                //light interval indicator : off
                rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape1.FillColor = Color.Red;
                //dark interval indicator : off
                rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape2.FillColor = Color.Red;
                //loop activate indicator : off
                rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape3.FillColor = Color.Red;

                label5.Text = "Trial finished, please reset the system.";

            }
            else
            {
                UpdateLight(); //Activate time loop
                counter = counter + 1; // Increment counter.
                
                //loop activate indicator : on
                rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape3.FillColor = Color.Green;
            }
        }


        private void Start_Click(object sender, EventArgs e)
        {
            if(Start.Text == "Reset")
            {
                Start.Text = "Start";
                //Reinitialize the timer
                timer1.Dispose();
                timer1 = new System.Windows.Forms.Timer();

                label5.Text = " ";

                On.Enabled = true;
                Off.Enabled = true;
                trackBar1.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;

                //light interval indicator : off
                rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape1.FillColor = Color.Red;
                //dark interval indicator : off
                rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape2.FillColor = Color.Red;
                //loop activate indicator : off
                rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                rectangleShape3.FillColor = Color.Red;

                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "Photo cycle has been reset. \r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();

            }
            else
            {
                
                timer1.Enabled = true;
                this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
                Start.Text = "Reset";
                On.Enabled = false;
                Off.Enabled = false;
                Start.Enabled = false;
                trackBar1.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;

                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "Photo cycle started. \r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();

 
            }
  
        }

//-----------------------------------------Port connection---------------------------------------------------------------------------------------//
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            textBox1.Text = "G'day Mate!\r\n";
            timer2.Enabled = false;// Intializing is done do not need timer 2 anymore

            Connect.Enabled = true;

            //The "disconnected" indicator： off
            rectangleShape5.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape5.FillColor = Color.Red;

            //The LED1 OFF status indicator： off
            rectangleShape7.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape7.FillColor = Color.Red;

            //The light interval OFF status indicator ： off
            rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape1.FillColor = Color.Red;

            //The dark interval OFF status indicator ： off
            rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape2.FillColor = Color.Red;

            //The cycle indicator OFF status indicator ：off
            rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape3.FillColor = Color.Red;
        }

    

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    textBox1.Text += "Please select port settings.\r\n";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();

                    Connect.Enabled = false;  //Disable connect button
                    Disconnect.Enabled = true;  //Enable disconnect button
                    On.Enabled = true; //Enable LED1 ON button
                    Off.Enabled = false; //Disable LED1 OFF button
                    Start.Enabled = true;  //Enable Start photocycle button
                    numericUpDown1.Enabled = true; //Enable light interval set up
                    numericUpDown2.Enabled = true; // Enable dark interval set up
                    numericUpDown3.Enabled = true; // Enable cycle times set up

                    //The connected indicator : On
                    rectangleShape4.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                    rectangleShape4.FillColor = Color.Green;
                    //The "disconnected" indicator : off
                    rectangleShape5.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                    rectangleShape5.FillColor = Color.Transparent;
                    //To BE ADD

                    textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                    textBox1.Text += "Device is now connected to computer.\r\n";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
                textBox1.Text += "Invaild Port.\r\n";
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();   //close the serial communication
            Connect.Enabled = true;  //Enable connect button again
            Disconnect.Enabled = false;  // Disable disconnect button
            On.Enabled = false; //Disable LED1 ON button
            Off.Enabled = false; //Disable LED1 OFF button
            Start.Enabled = false;  //Disable Start photocycle button
            trackBar1.Enabled = false; //Disable LED1 brightness
            numericUpDown1.Enabled = false; //Disable light interval set up
            numericUpDown2.Enabled = false; // Disable dark interval set up
            numericUpDown2.Enabled = false; // Disable cycle times set up


            //The connected indicator 
            rectangleShape4.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape4.FillColor = Color.Transparent;
            //The disconnected indicator 
            rectangleShape5.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape5.FillColor = Color.Red;
            //The light interval status indicator 
            rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape1.FillColor = Color.Red;
            //The dark interval status indicator 
            rectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape2.FillColor = Color.Red;
            //The loop activate status indicator 
            rectangleShape3.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape3.FillColor = Color.Red;

            //TOBE added
            textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
            textBox1.Text += "Device is now disconnected from computer.\r\n";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = " "; //Empty previous message in log box
        }

//--------------------------------------Manual control--------------------------------------------------------------------------------------------//

        private void On_Click(object sender, EventArgs e)
        {
            byte PWM;
            PWM = Convert.ToByte(trackBar1.Value);
            data[0] = PWM; // or 0 or the value from pwm
            serialPort1.Write(data, 0, 1);

            //Write(byte[] data, int offset, int length)
            //To use them, you provide an array of bytes or characters which will be written to the serial port. 
            //The offset parameter just specifies what element of the array to start at 
            //i.e. If you pass it 0, it will start at the very beginning of the array; 
            //if you pass 1 it will start at the second element. 
            //The length parameter is simply the length of the array.

            Off.Enabled = true; //Enable LED1 OFF button
            trackBar1.Enabled = true;

            //The LED1 on status indicator 
            rectangleShape6.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape6.FillColor = Color.Green;
            //The LED1 OFF status indicator 
            rectangleShape7.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape7.FillColor = Color.Transparent;

            textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
            textBox1.Text += "LED1 is on now. \r\n";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void Off_Click(object sender, EventArgs e)
        {
            data[0] = 0; // or 0 or the value from pwm
            serialPort1.Write(data, 0, 1);

            trackBar1.Enabled = false;

            //The LED1 on status indicator 
            rectangleShape6.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape6.FillColor = Color.Transparent;
            //The LED1 OFF status indicator 
            rectangleShape7.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            rectangleShape7.FillColor = Color.Red;

            textBox1.Text += DateTime.Now.ToString("yy/MM/dd H:mm:ss ttt \r\n");
            textBox1.Text += "LED1 is off now. \r\n";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                byte PWM;
                PWM = Convert.ToByte(trackBar1.Value);
                data[0] = PWM;
                serialPort1.Write(data, 0, 1);
                label11.Text = Convert.ToString(PWM);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                DialogResult dialog = MessageBox.Show("Please disconnect serial port before exit", "Remind", MessageBoxButtons.OK);
                if (dialog == DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {


           

        }
    }
}
