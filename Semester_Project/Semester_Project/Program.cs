﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    
    public class MutexHandler
    {
        // class variables //
        private List<string> lHandles; // Handles nodes on left side
        private List<string> mHandles; // Handles middle in-transit nodes
        private List<string> rHandles; // Handles nodes on right side
        private Form parentForm;

        // class constructor //
        public MutexHandler(bool case1, Mutual_Exclusion_Form form)
        {
            // if true, then use case 1 operation //
            // Ricart & Agrawalas mutual exclusion algorithm, one person on bridge at a time.  Everyone eventually allowed to cross (via queue) //
            parentForm = form;
            populateHandles(case1); // Create handles data structure, needed for all operational cases //
            // Common init code between case 1 and case 2 //
            TextBox l_box1 = parentForm.Controls["l_box1"] as TextBox;
            l_box1.Text = "Task 1";
            TextBox l_box2 = parentForm.Controls["l_box2"] as TextBox;
            l_box2.Text = "Task 2";
            TextBox r_box1 = parentForm.Controls["r_box1"] as TextBox;
            r_box1.Text = "Task 3";
            TextBox r_box2 = parentForm.Controls["r_box2"] as TextBox;
            r_box2.Text = "Task 4";
            // Specific case code execution //
            if (case1)
            {
                // FOR CASE 1 -> EXECUTE MUTUAL EXCLUSION ALGORITHM //

            }
            // if false, design protocol 2 states that bridge crossings allowed directionally in sync, but no job indefinately prevented from crossing
            else
            {
                // FOR CASE 2 -> EXECUTE MUTUAL EXCLUSION ALGORITHM //


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

        

        
    }
}
