using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.IO.Ports;
using System.Threading;


namespace wpfweldcontroller
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Normal;
            this.Topmost = true;

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            canvas1.Visibility = System.Windows.Visibility.Visible;
            canvas4.Visibility = System.Windows.Visibility.Hidden;
          
        }

      
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            canvas1.Visibility = System.Windows.Visibility.Visible;
            canvas1.Visibility = System.Windows.Visibility.Hidden;
            canvas4.Visibility = System.Windows.Visibility.Visible;
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            canvas4.Visibility = System.Windows.Visibility.Hidden;
            canvas1.Visibility = System.Windows.Visibility.Visible;
        }

        private void button19_Click(object sender, RoutedEventArgs e)
        {
            canvas4.Visibility = System.Windows.Visibility.Hidden;
            canvas1.Visibility = System.Windows.Visibility.Visible;
        }

        List<String> lis = new List<string>();

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = "csv file|*.csv";
            if (ofd.ShowDialog() == true)
            {
                label14.Content = ofd.FileName;

                Console.WriteLine(ofd.FileName);

                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
              
              

                lis = new List<String>();

                while (sr.Peek() > 0)
                {
                 
                    lis.Add(sr.ReadLine());

                }
                Console.WriteLine(lis.Count);
                  for (int i = 0; i <lis.Count; i++) {
						   Console.WriteLine(lis[i]);
				 }

            }
        }
        SerialPort port;
        private bool ComPortIsOpen = false;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
           
                String comname = comboBox2.SelectionBoxItem.ToString();

                String botelv = comboBox3.SelectionBoxItem.ToString();

                if (ComPortIsOpen == false)
                {

                    if (ComOpen(comname, botelv))
                    {
                        MessageBox.Show(comname + "串口已连接！");
                        button1.Content = "断开";
                        ComPortIsOpen = true;
                        port.DataReceived += new SerialDataReceivedEventHandler(ComRec);
                       
                    
                    }
                    else
                    {
                        MessageBox.Show("无法打开串口,请检测此串口是否有效或被其他占用！");
                        ComPortIsOpen = false;
                        
                    }

                }
                else
                {
                    ComClose();
                    ComPortIsOpen = false;
                    button1.Content = "连接";
                    MessageBox.Show("串口连接断开！");

                }
        }


        private void ComSend(String data)
        {
           
            Console.WriteLine(data + "\r\n");
            port.WriteLine(data + "\r\n");
           Thread.Sleep(100);




        }
    
        private void ComRec(object sender, SerialDataReceivedEventArgs e)
        {
            String rec = port.ReadLine();
            Console.WriteLine(rec);
        }


        private bool ComOpen(String comname, String botelv)
        {
            try
            {

                port = new SerialPort(comname, int.Parse(botelv), Parity.None, 8, StopBits.One);
                port.ReadTimeout = 2000;
                port.WriteTimeout = 2000;

                port.ReadBufferSize = 1024;
                port.WriteBufferSize = 1024;

                port.Open();
                return true;
            }
            catch
            {
              
                return false;
            }
        }

        private void ComClose()
        {
            port.Close();
        
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ComSend(textBox9.Text);
           

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ComSend("3v-100");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ComSend("2v-300");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            ComSend("2v300");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ComSend("3v100");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            ComSend("1v-300");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            ComSend("1v300");
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            ComSend("4v-100");
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            ComSend("4v100");
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            ComSend("1HO");
            ComSend("2HO");
            ComSend("3HO");
            ComSend("4HO");
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            ComSend("1V0");
            ComSend("2V0");
            ComSend("3V0");
            ComSend("4V0");
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            ComSend("1LA0");
            ComSend("2LA0");
            ComSend("3LA0");
            ComSend("4LA0");

            ComSend("1SP200");
            ComSend("2SP200");
            ComSend("3SP200");
            ComSend("4SP200");

            ComSend("1NP");
            ComSend("2NP");
            ComSend("3NP");
            ComSend("4NP");

            ComSend("1M");
            ComSend("2M");
            ComSend("3M");
            ComSend("4M");
        }

      private bool deviceisopen = false;
        private void button14_Click(object sender, RoutedEventArgs e)
        {
            if (deviceisopen == false)
            {
                ComSend("1EN");
                ComSend("2EN");
                ComSend("3EN");
                ComSend("4EN");

                button14.Content = "关";
                deviceisopen = true;
            }
            else
            {
                ComSend("1DI");
                ComSend("2DI");
                ComSend("3DI");
                ComSend("4DI");
                button14.Content = "开";
                deviceisopen = false;
            }
        }

        private void button18_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lis.Count; i++)
            {

                List<String> vec = new List<String>();
                String str = lis[i];
                String[] arrstr = str.Split(';');
                for (int j = 1; j < arrstr.Length; j++)
                {
                    vec.Add(arrstr[j]);

                }

                for (int k = 0; k < vec.Count; k++)
                {
                    if (vec[k] != "")
                    {
                        ComSend(vec[k]);

                    }
                }


            }		
        }

      

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

      
      
    }
}
