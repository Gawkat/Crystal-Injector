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
        private Button openButton, cancelButton, processButton, windowProcessButton;

        public ProcessWindow() { // TODO: make gui pretty
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();

            crystal = new Crystal();

            processListBox = new ListBox();

            openButton = new Button();
            cancelButton = new Button();
            processButton = new Button();
            windowProcessButton = new Button();

            SuspendLayout();

            //
            // processListBox
            //
            processListBox.Size = new Size(225, 300);
            processListBox.Location = new Point(0, 0);
            processListBox.MultiColumn = false;
            processListBox.SelectionMode = SelectionMode.One;
            populateWithProcesses(processListBox);
            processListBox.SetSelected(0, true);
            processListBox.MouseDoubleClick += processListBox_MouseDoubleClick;

            //
            // openButton
            //
            openButton.Location = new Point(0, 300);
            openButton.Name = "openButton";
            openButton.Size = new Size(100, 23);
            openButton.TabIndex = 0;
            openButton.Text = "Open Process";
            openButton.Click += openButton_Click;

            //
            // cancelButton
            //
            cancelButton.Location = new Point(100, 300);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(100, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.Click += cancelButton_Click;

            //
            // processButton
            //
            processButton.Location = new Point(0, 350);
            processButton.Name = "processButton";
            processButton.Size = new Size(100, 23);
            processButton.TabIndex = 2;
            processButton.Text = "Process List";
            processButton.Click += processButton_Click;

            //
            // windowProcessButton
            //
            windowProcessButton.Location = new Point(100, 350);
            windowProcessButton.Name = "windowProcessButton";
            windowProcessButton.Size = new Size(100, 23);
            windowProcessButton.TabIndex = 3;
            windowProcessButton.Text = "Window List";
            windowProcessButton.Click += windowProcessButton_Click;

            Controls.Add(processListBox);

            Controls.Add(openButton);
            Controls.Add(cancelButton);
            Controls.Add(processButton);
            Controls.Add(windowProcessButton);

            ResumeLayout();
        }

        // Gets the PID of selected process, sets the PID in Crystal.cs to selected and disposes of the ProcessWindow dialog
        private void openButton_Click(object sender, EventArgs e) {
            if (processListBox.SelectedItem is string) {
                string tempString = (string)processListBox.SelectedItem;
                string[] processString = tempString.Split('-');
                int processID = 0;
                Int32.TryParse(processString[0], out processID);
                string processName = processString[1];
                if (processID != 0) {
                    crystal.setProcessID(processID);
                    crystal.setProcessName(processName);
                    Dispose();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Dispose();
        }

        private void processButton_Click(object sender, EventArgs e) {
            populateWithProcesses(processListBox);
        }

        private void windowProcessButton_Click(object sender, EventArgs e) {
            populateWithWindowProcesses(processListBox);
        }

        private void processListBox_MouseDoubleClick(object sender, EventArgs e) {
            openButton_Click(sender, e);
        }

        // Populates the ListBox with the currently running processes
        private void populateWithProcesses(ListBox listBox) {
            Process[] processList = crystal.getProcesses();
            listBox.Items.Clear();
            foreach (Process process in processList) {
                listBox.Items.Add(process.Id + "-" + process.ProcessName);
            }
            listBox.SetSelected(0, true);
        }

        // Populates the ListBox with the currently opened windows
        private void populateWithWindowProcesses(ListBox listBox) {
            Process[] windowProcessList = crystal.getProcesses();
            listBox.Items.Clear();
            foreach (Process windowProcess in windowProcessList) {
                if (windowProcess.MainWindowTitle.Length > 0) {
                    listBox.Items.Add(windowProcess.Id + "-" + windowProcess.MainWindowTitle);
                }
            }
            listBox.SetSelected(0, true);
        }

    }
}
