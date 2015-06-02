using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Crystal_Injector {

    public partial class ProcessWindow : Form {

        private Crystal crystal;

        private ListBox processListBox;

        public ProcessWindow() {
            InitializeComponent();

            crystal = new Crystal();

            processListBox = new ListBox();

            SuspendLayout();

            // TODO
            //
            // processListBox
            //
            processListBox.Size = new System.Drawing.Size(200, 300);
            processListBox.Location = new System.Drawing.Point(0, 0);
            processListBox.MultiColumn = true;
            processListBox.SelectionMode = SelectionMode.One;

            populateWithProcesses();

            Controls.Add(processListBox);

            ResumeLayout();
        }

        private void populateWithProcesses() {
            Process[] processList = crystal.getProcesses();
            foreach (Process process in processList) {
                processListBox.Items.Add(process.ProcessName);
            }
        }

    }
}
