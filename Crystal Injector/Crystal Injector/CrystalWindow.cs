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

    public partial class CrystalWindow : Form {

        private Button dllButton, processButton, injectButton;

        private Label versionLabel, gitHubLabel;

        private Crystal crystal;

        public CrystalWindow() { // TODO: make gui pretty, status label
            crystal = new Crystal();

            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            // Buttons
            dllButton = new Button();
            processButton = new Button();
            injectButton = new Button();

            // Labels
            versionLabel = new Label();
            gitHubLabel = new Label();

            SuspendLayout();

            // dllButton
            dllButton.Location = new Point(0, 0);
            dllButton.Name = "dllButton";
            dllButton.Size = new Size(75, 23);
            dllButton.TabIndex = 0;
            dllButton.Text = "Choose DLL";
            dllButton.Click += dllButton_Click;

            // processButton
            processButton.Location = new Point(0, 50);
            processButton.Name = "processButton";
            processButton.Size = new Size(100, 23);
            processButton.TabIndex = 1;
            processButton.Text = "Choose Process";
            processButton.Click += processButton_Click;

            // injectButton
            injectButton.Location = new Point(0, 100);
            injectButton.Name = "injectButton";
            injectButton.Size = new Size(100, 23);
            injectButton.TabIndex = 2;
            injectButton.Text = "Inject";
            injectButton.Click += injectButton_Click;

            // versionLabel
            versionLabel.Text = "Version: " + crystal.getVersion();
            versionLabel.Size = new Size(versionLabel.PreferredWidth, versionLabel.PreferredHeight);
            versionLabel.Location = new Point(0, Height - versionLabel.Height); // TODO: remove hardcode?

            // gitHubLabel
            gitHubLabel.MouseEnter += gitHubLabel_MouseEnter;
            gitHubLabel.MouseLeave += gitHubLabel_MouseLeave;
            gitHubLabel.Click += gitHubLabel_Click;
            gitHubLabel.Text = "View on GitHub";
            gitHubLabel.Size = new Size(gitHubLabel.PreferredWidth, gitHubLabel.PreferredHeight);
            gitHubLabel.Location = new Point(100, Height - 60); // TODO: location

            Controls.Add(dllButton);
            Controls.Add(processButton);
            Controls.Add(injectButton);

            Controls.Add(versionLabel);
            Controls.Add(gitHubLabel);

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

        private void gitHubLabel_MouseEnter(object sender, EventArgs e) {
            gitHubLabel.Cursor = Cursors.Hand;
        }

        private void gitHubLabel_MouseLeave(object sender, EventArgs e) {
            gitHubLabel.Cursor = Cursors.Default;
        }

        private void gitHubLabel_Click(object sender, EventArgs e) {
            Process.Start(crystal.getGitHubPage());
        }

    }
}
