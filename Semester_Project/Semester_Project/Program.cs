using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    
    public class MutexHandler
    {
        // class variables //
        private List<string> lHandles; // Handles nodes on left side
        private List<string> mHandles; // Handles middle in-transit nodes
        private List<string> rHandles; // Handles nodes on right side

        // class constructor //
        public MutexHandler(bool case1)
        {
            // if true, then use case 1 operation //
            // Ricart & Agrawalas mutual exclusion algorithm, one person on bridge at a time.  Everyone eventually allowed to cross (via queue) //
            populateHandles(); // Create handles data structure, needed for all operational cases //
            if (case1)
            {

            }
            // if false, design protocol 2 states that bridge crossings allowed directionally in sync, but no job indefinately prevented from crossing
            else
            {

            }
        }

        private void populateHandles()
        {

        }

        // class functions
        private bool resetGUI()
        {
            // Resets the GUI interface to run another instance when options change via user GUI input //
            return false;
        }

        
    }
}
