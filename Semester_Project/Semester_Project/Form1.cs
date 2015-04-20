using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semester_Project
{
    public partial class Mutual_Exclusion_Form : Form
    {
        // private variables for the form class exclusively //
        // Note that the MutexHandler class inherets this class //

        private MutexHandler currentHandler;

        public Mutual_Exclusion_Form()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ModeSelect_ValueUpdate(object sender, EventArgs e)
        {
            // Event Handler runs when the program execution variable changes //
            // Class must be re-initilized to handle this new case //
            if (ModeSelect.Text == "Mode 1")
            {
                currentHandler = new MutexHandler(true, this);
            }
            else if (ModeSelect.Text == "Mode 2")
            {
                if (currentHandler != null)
                {
                    currentHandler.resetGUI();
                }
            }
        }
    }
}
