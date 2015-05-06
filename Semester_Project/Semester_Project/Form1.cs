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
        private int rValue = 0;
        private int lValue = 0;
        private int mValue = 0;

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
        
        public bool rInsert(Color iColor)
        {
            switch (rValue)
            {
                case 0:
                    r_box4.BackColor = iColor;
                    rValue++;
                    break;
                case 1:
                    r_box3.BackColor = iColor;
                    rValue++;
                    break;
                case 2:
                    r_box2.BackColor = iColor;
                    rValue++;
                    break;
                case 3:
                    r_box1.BackColor = iColor;
                    rValue++;
                    break;

            }
            return true;
        }

        public Color rShift()
        {
            // Handles right-side shift / displaying variables depending on //
            // assumes removal operation is underway //
            Color topColor = r_box4.BackColor;
            r_box4.BackColor = r_box3.BackColor;
            r_box3.BackColor = r_box2.BackColor;
            r_box1.BackColor = Color.White;
            rValue--;
            return topColor;
            
        }

        public bool lInsert(Color iColor)
        {
            switch (lValue)
            {
                case 0:
                    l_box1.BackColor = iColor;
                    lValue++;
                    break;
                case 1:
                    l_box2.BackColor = iColor;
                    lValue++;
                    break;
                case 2:
                    l_box3.BackColor = iColor;
                    lValue++;
                    break;
                case 3:
                    l_box4.BackColor = iColor;
                    lValue++;
                    break;
            }
            return true;
        }

        public Color lShift()
        {
            // Handles left-side shift / displaying variables depending on //
            // assumes removal operation is underway //
            Color topColor = l_box1.BackColor;
            l_box1.BackColor = l_box2.BackColor;
            l_box2.BackColor = l_box3.BackColor;
            l_box4.BackColor = Color.White;
            lValue--;
            return topColor;
        }

        public bool iMiddle(Color iColor)
        {
            // Inserts value into the top-most part of the middle stack, using the color provided
            switch (mValue)
            {
                case 0:
                    m_box4.BackColor = iColor;
                    mValue++;
                    break;
                case 1:
                    m_box3.BackColor = iColor;
                    mValue++;
                    break;
                case 2:
                    m_box2.BackColor = iColor;
                    mValue++;
                    break;
                case 3:
                    m_box1.BackColor = iColor;
                    mValue++;
                    break;
            }
            return true;
        }

        public Color rMiddle()
        {
            // removes lowest on the stack from the middle, then re-balances everything depending on the current count //
            // returns color at the lowest on the stack //
            Color topColor = m_box4.BackColor;
            m_box4.BackColor = m_box3.BackColor;
            m_box3.BackColor = m_box2.BackColor;
            m_box2.BackColor = m_box1.BackColor;
            m_box1.BackColor = Color.White;
            return topColor;
        }
    }
}
