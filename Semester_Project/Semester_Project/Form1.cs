﻿using System;
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
                if (currentHandler != null)
                {
                    currentHandler.resetGUI();
                    currentHandler = new MutexHandler(false, this);

                }
                else
                {
                    currentHandler = new MutexHandler(true, this);
                }
            }
            else if (ModeSelect.Text == "Mode 2")
            {
                if (currentHandler != null)
                {
                    currentHandler.resetGUI();
                    currentHandler = new MutexHandler(true, this);
                }
                else
                {
                    currentHandler = new MutexHandler(false, this);
                }
            }
        }
        
        public bool rInsert(Color iColor)
        {
            switch (rValue)
            {
                case 0:
                    Action setColor4 = () => r_box4.BackColor = iColor;
                    this.Invoke(setColor4, null);
                    rValue++;
                    break;
                case 1:
                    Action setColor3 = () => r_box3.BackColor = iColor;
                    this.Invoke(setColor3, null);
                    rValue++;
                    break;
                case 2:
                    Action setColor2 = () => r_box2.BackColor = iColor;
                    this.Invoke(setColor2, null);
                    rValue++;
                    break;
                case 3:
                    Action setColor1 = () => r_box1.BackColor = iColor;
                    this.Invoke(setColor1, null);
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

            Action moveBox4 = () => r_box4.BackColor = r_box3.BackColor;
            this.Invoke(moveBox4, null);
            Action moveBox3 = () => r_box3.BackColor = r_box2.BackColor;
            this.Invoke(moveBox3, null);
            Action moveBox2 = () => r_box2.BackColor = r_box1.BackColor;
            this.Invoke(moveBox2, null);

            Action action = () => r_box1.BackColor = Color.White;
            this.Invoke(action, null);

            rValue--;
            return topColor;
            
        }

        public bool lInsert(Color iColor)
        {
            switch (lValue)
            {
                case 0:
                    Action setColor1 = () => l_box1.BackColor = iColor;
                    this.Invoke(setColor1, null);
                    lValue++;
                    break;
                case 1:
                    Action setColor2 = () => l_box2.BackColor = iColor;
                    this.Invoke(setColor2, null);
                    lValue++;
                    break;
                case 2:
                    Action setColor3 = () => l_box3.BackColor = iColor;
                    this.Invoke(setColor3, null);
                    lValue++;
                    break;
                case 3:
                    Action setColor4 = () => l_box4.BackColor = iColor;
                    this.Invoke(setColor4, null);
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
            
            Action setBox1 = () => l_box1.BackColor = l_box2.BackColor;
            this.Invoke(setBox1, null);
            Action setBox2 = () => l_box2.BackColor = l_box3.BackColor;
            this.Invoke(setBox2, null);
            Action setBox3 = () => l_box3.BackColor = l_box4.BackColor;
            this.Invoke(setBox3, null);

            Action action = () => l_box4.BackColor = Color.White;
            this.Invoke(action, null);
            
            lValue--;
            return topColor;
        }

        public bool iMiddle(Color iColor)
        {
            // Inserts value into the top-most part of the middle stack, using the color provided
            switch (mValue)
            {
                case 0:
                    Action setColor4 = () => m_box4.BackColor = iColor;
                    this.Invoke(setColor4, null);
                    mValue++;
                    break;
                case 1:
                    Action setColor3 = () => m_box3.BackColor = iColor;
                    this.Invoke(setColor3, null);
                    mValue++;
                    break;
                case 2:
                    Action setColor2 = () => m_box2.BackColor = iColor;
                    this.Invoke(setColor2, null);
                    mValue++;
                    break;
                case 3:
                    Action setColor1 = () => m_box1.BackColor = iColor;
                    this.Invoke(setColor1, null);
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
            Action setBox4 = () => m_box4.BackColor = m_box3.BackColor;
            this.Invoke(setBox4, null);
            Action setBox3 = () => m_box3.BackColor = m_box2.BackColor;
            this.Invoke(setBox3, null);
            Action setBox2 = () => m_box2.BackColor = m_box1.BackColor;
            this.Invoke(setBox2, null);

            Action action = () => m_box1.BackColor = Color.White;
            this.Invoke(action, null);

            mValue--;
            return topColor;
        }

        public int getSliderValue()
        {
            int value = 0;
            Action getSlider = () => value = SpeedBar.Value;
            this.Invoke(getSlider, null);
            return value;
        }
    }
}
