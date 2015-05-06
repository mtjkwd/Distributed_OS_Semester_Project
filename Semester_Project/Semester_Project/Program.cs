using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Semester_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mutual_Exclusion_Form());
        }
    }

    public class BridgeInfo
    {
        public BridgeInfo(bool RequestA, bool CSA, DateTime timeStampA, string directionA, int threadNrA)
        {
            request = RequestA;
            CS = CSA;
            timeStamp = timeStampA;
            direction = directionA;
            threadNr = threadNrA;
        }
        public bool request;
        public bool CS;
        public DateTime timeStamp;
        public string direction;
        public int threadNr;
    };
    
    public class MutexHandler
    {
        // class variables //
        private List<string> lHandles; // Handles nodes on left side
        private List<string> mHandles; // Handles middle in-transit nodes
        private List<string> rHandles; // Handles nodes on right side
        private Mutual_Exclusion_Form parentForm;

        private Thread[] workerThreads = new Thread[4];
        private List<BridgeInfo> sharedMemory = new List<BridgeInfo>(); // Shared memory between the processes to help them decide when to enter the bridge //
        Mutex sharedMemoryLock = new Mutex(false, "sharedMemoryLock");


        // class constructor //
        public MutexHandler(bool case1, Mutual_Exclusion_Form form)
        {
            // if true, then use case 1 operation //
            // Ricart & Agrawalas mutual exclusion algorithm, one person on bridge at a time.  Everyone eventually allowed to cross (via queue) //
            parentForm = form;
            populateHandles(case1); // Create handles data structure, needed for all operational cases //
            this.setInitialData(); // Sets inital thread data in the shared memory //
            // Common init code between case 1 and case 2 //
            parentForm.rInsert(Color.Red);
            parentForm.rInsert(Color.Red);
            parentForm.lInsert(Color.Blue);
            parentForm.lInsert(Color.Blue);
            
            // Specific case code execution //
            if (case1)
            {
                // FOR CASE 1 -> EXECUTE MUTUAL EXCLUSION ALGORITHM //
                Richart_Agrawala(form);

            }
            // if false, design protocol 2 states that bridge crossings allowed directionally in sync, but no job indefinately prevented from crossing
            else
            {
                // FOR CASE 2 -> EXECUTE MUTUAL EXCLUSION ALGORITHM //
                Multi_Bridge(form);

            }
        }

        private void populateHandles(bool case1)
        {
            // initialize the lists / vectors if they are not already initialized //
            if (lHandles == null)
            {
                lHandles = new List<string>();
            }
            if (mHandles == null)
            {
                mHandles = new List<string>();
            }
            if (rHandles == null)
            {
                rHandles = new List<string>();
            }

            // populate that stuff //
            lHandles.Add("l_box1");
            lHandles.Add("l_box2");
            lHandles.Add("l_box3");
            lHandles.Add("l_box4");
            rHandles.Add("r_box1");
            rHandles.Add("r_box2");
            rHandles.Add("r_box3");
            rHandles.Add("r_box4");
            if (case1)
            {
                mHandles.Add("m_box1");
                mHandles.Add("m_box2");
                mHandles.Add("m_box3");
                mHandles.Add("m_box4");
            }
            else
            {
                mHandles.Add("m_box1");
            }
        }

        private void setInitialData()
        {
            for (int i = 0; i < 2; i++)
            {
                sharedMemory.Add(new BridgeInfo(false, false, DateTime.Now, "L", i));
            }
            for (int j = 2; j < 4; j++)
            {
                sharedMemory.Add(new BridgeInfo(false, false, DateTime.Now, "R", j));
            }

        }

        private bool enterBridge(int seqNr)
        {
            foreach (BridgeInfo info in sharedMemory)
            {
                if (info.threadNr != seqNr)
                {
                    if (!info.CS)
                    {
                        if ((DateTime.Compare(info.timeStamp, sharedMemory[seqNr].timeStamp) < 0)) // t1 is earlier than t2 //
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // class functions
        public void resetGUI() // public for testing, should set back to private when finished. //
        {
            foreach (string item in lHandles)
            {
                TextBox itemBox = parentForm.Controls[item.ToString()] as TextBox;
                itemBox.Text = string.Empty;
            }
            foreach (string item in mHandles)
            {
                TextBox itemBox = parentForm.Controls[item.ToString()] as TextBox;
                itemBox.Text = string.Empty;
            }
            foreach (string item in rHandles)
            {
                TextBox itemBox = parentForm.Controls[item.ToString()] as TextBox;
                itemBox.Text = string.Empty;
            }
            // Resets the GUI interface to run another instance when options change via user GUI input //
        }

        public void Richart_Agrawala(Mutual_Exclusion_Form form)
        {
            workerThreads[0] = new Thread(() => Richart_Thread(0));
            workerThreads[1] = new Thread(() => Richart_Thread(1));
            workerThreads[2] = new Thread(() => Richart_Thread(2));
            workerThreads[3] = new Thread(() => Richart_Thread(3));

            foreach (Thread worker in workerThreads)
            {
                worker.Start();
            }

        }

        public void Multi_Bridge(Mutual_Exclusion_Form form)
        {
            workerThreads[0] = new Thread(() => Multi_Thread(0));
            workerThreads[1] = new Thread(() => Multi_Thread(1));
            workerThreads[2] = new Thread(() => Multi_Thread(2));
            workerThreads[3] = new Thread(() => Multi_Thread(3));

            foreach (Thread worker in workerThreads)
            {
                worker.Start();
            }

        }

        void Richart_Thread(int number)
        {
            // function code goes here for a thread //
            int myThreadNr = number;
            while (true) // infinitely execute unil terminated //
            {   
                Thread.Sleep(parentForm.getSliderValue()); // sleeps for ms value indicated on slider (100ms - 500ms range) //
                if (sharedMemoryLock.WaitOne())
                {
                    if (!sharedMemory[myThreadNr].request)
                    {
                        // Have critical section to shared memory, set shared memory for bridge lock, then release Mutex and give other threads opportunity to respond //
                        sharedMemory[myThreadNr].request = true;
                        sharedMemory[myThreadNr].timeStamp = DateTime.Now;
                        sharedMemoryLock.ReleaseMutex();
                    }
                    else
                    {
                        sharedMemoryLock.ReleaseMutex();
                    }
                }
                // only needed if entering the bridge //
                bool result = false;
                string direction = string.Empty;
                // ^^ only needed if entering the bridge //

                if (sharedMemoryLock.WaitOne())
                {
                    result = enterBridge(myThreadNr);
                    if (result)
                    {
                        sharedMemory[myThreadNr].CS = true;
                        sharedMemory[myThreadNr].request = false;
                        direction = sharedMemory[myThreadNr].direction;
                    }
                    sharedMemoryLock.ReleaseMutex();
                }
                
                // go onto the bridge if true //
                if (result)
                {
                    switch (direction)
                    {
                        case "L":
                            Color col = parentForm.rShift();
                            parentForm.iMiddle(col);
                            
                            Thread.Sleep(parentForm.getSliderValue());
                            
                            Color colT = parentForm.rMiddle();
                            parentForm.lInsert(colT);

                            if (sharedMemoryLock.WaitOne())
                            {
                                sharedMemory[myThreadNr].CS = false;
                                sharedMemory[myThreadNr].direction = "R";
                                sharedMemoryLock.ReleaseMutex();
                            }

                            break;
                        case "R":
                            Color col2 = parentForm.lShift();
                            parentForm.iMiddle(col2);

                            Thread.Sleep(parentForm.getSliderValue());

                            Color colT2 = parentForm.rMiddle();
                            parentForm.rInsert(colT2);
                            
                            if (sharedMemoryLock.WaitOne())
                            {
                                sharedMemory[myThreadNr].CS = false;
                                sharedMemory[myThreadNr].direction = "L";
                                sharedMemoryLock.ReleaseMutex();
                            }
                            break;
                    }
                }
            }
        }

        void Multi_Thread(int number)
        {
            // function code goes here for a thread //
            int myThreadNr = number;
            while (true) // infinitely execute unil terminated //
            {
                Thread.Sleep(parentForm.getSliderValue()); // sleeps for ms value indicated on slider (100ms - 500ms range) //
                if (sharedMemoryLock.WaitOne())
                {
                    // Have critical section to shared memory, set shared memory for bridge lock, then release Mutex and give other threads opportunity to respond //
                    sharedMemory[myThreadNr].request = true;
                    sharedMemory[myThreadNr].timeStamp = DateTime.Now;
                    sharedMemoryLock.ReleaseMutex();

                }

            }
        }
        
    }
}
