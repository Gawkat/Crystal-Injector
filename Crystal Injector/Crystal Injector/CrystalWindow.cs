using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystal_Injector {

    public partial class CrystalWindow : Form {

        private Button dllButton, processButton, injectButton;

        private Crystal crystal;

        public CrystalWindow() { // TODO: make gui pretty, add link to github, status label, add version label
            crystal = new Crystal();

            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            dllButton = new Button();
            processButton = new Button();
            injectButton = new Button();

            SuspendLayout();

            // 
            // dllButton
            // 
            dllButton.Location = new System.Drawing.Point(0, 0);
            dllButton.Name = "dllButton";
            dllButton.Size = new System.Drawing.Size(75, 23);
            dllButton.TabIndex = 0;
            dllButton.Text = "Choose DLL";
            dllButton.Click += dllButton_Click;

            // 
            // processButton
            // 
            processButton.Location = new System.Drawing.Point(0, 50);
            processButton.Name = "processButton";
            processButton.Size = new System.Drawing.Size(100, 23);
            processButton.TabIndex = 1;
            processButton.Text = "Choose Process";
            processButton.Click += processButton_Click;

            // 
            // injectButton
            // 
            injectButton.Location = new System.Drawing.Point(0, 100);
            injectButton.Name = "injectButton";
            injectButton.Size = new System.Drawing.Size(100, 23);
            injectButton.TabIndex = 2;
            injectButton.Text = "Inject";
            injectButton.Click += injectButton_Click;

            Controls.Add(dllButton);
            Controls.Add(processButton);
            Controls.Add(injectButton);

            ResumeLayout();
        }

        private void CrystalWindow_Load(object sender, EventArgs e) {

        }

        private void dllButton_Click(object sender, EventArgs e) {
            OpenFileDialog dllDialog = new OpenFileDialog();

            dllDialog.Filter = "DLL Files (*.dll)|*.dll";
            dllDialog.FilterIndex = 1;

            dllDialog.Multiselect = false;

            if (dllDialog.ShowDialog() == DialogResult.OK) {
                crystal.setdllPath(dllDialog.FileName);
            }
        }

        private void processButton_Click(object sender, EventArgs e) {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.ShowDialog();
        }

        private void injectButton_Click(object sender, EventArgs e) {
            if (crystal.getProcessID() != 0 && crystal.getdllPath() != null) {
                crystal.inject(crystal.getProcessID(), crystal.getdllPath());
            } else {
                // TODO: ?
            }
        }

    }
}