using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

//
//Name: Drunk Pc
//Description: Makes eratic mouse movements and keyboard inputs
//Also generates system sounds and fake dialogues to disrup the user
//   1)Threads
//   2)System.Windows.Forms namespace and assembly
//   3)Hidden Application
//
namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;

        /// <summary>
        /// Entry point for program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Drunk PC Application");

            // Check for command line arguments and assign new values accordingly
            if(args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            //Create all threads that manipulate the inputs and outputs to the system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyeboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkenSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkenPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Waiting 10 Seconds before Starting threads");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            //Start all threads
            drunkMouseThread.Start();
            drunkKeyeboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            //Wait for user input to exit
            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while(future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating all threads");
            drunkMouseThread.Abort();
            drunkKeyeboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }

        #region Threads
        /// <summary>
        /// Randomly move the mouse
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Drunken Mouse.exe has Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                if (_random.Next(100) > 50)
                {
                    //Console.WriteLine(Cursor.Position.ToString());

                    moveX = _random.Next(21) - 10;
                    moveY = _random.Next(21) - 10;

                    Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                }
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Generates random keyboard output
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("Drunken Keyeboard.exe has Started");
            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    char key = (char)(_random.Next(25) + 65);

                    if (_random.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }


                    SendKeys.SendWait(key.ToString());
                }
                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// Generates Random Sounds
        /// </summary>
        public static void DrunkenSoundThread()
        {
            Console.WriteLine("Drunken Sound.exe has Started");
            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    switch(_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;

                    }
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Makes random error popups 
        /// </summary>
        public static void DrunkenPopupThread()
        {
            Console.WriteLine("Drunken Popup.exe has Started");
            while (true)
            {
                if (_random.Next(100) > 50)
                {
                    switch (_random.Next(5))
                    {
                        case 0:
                            MessageBox.Show("Internet Explorer has stopped working.", "Internt Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Oops! Google Chrome has encountered an error.", "Google Chrome", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 2:
                            MessageBox.Show("Windows is encountering an error.", "Windows 10", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 3:
                            MessageBox.Show("Office is encountering an error, trying to fix it now...", "Microsoft Office™", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 4:
                            MessageBox.Show("Internet Explorer has crashed, please re-initiate the program.", "Internt Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }

                Thread.Sleep(5000);
            }
        }
        #endregion
    }
}
