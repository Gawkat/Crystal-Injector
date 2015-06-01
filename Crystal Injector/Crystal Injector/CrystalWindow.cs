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

        public CrystalWindow() {
            InitializeComponent();

            dllButton = new Button();
            processButton = new Button();
            injectButton = new Button();

            SuspendLayout();

            // 
            // dllButton
            // 
            dllButton.Cursor = System.Windows.Forms.Cursors.Default;
            dllButton.Location = new System.Drawing.Point(0, 0);
            dllButton.Name = "dllButton";
            dllButton.Size = new System.Drawing.Size(75, 23);
            dllButton.TabIndex = 0;
            dllButton.Text = "Choose DLL";
            dllButton.UseVisualStyleBackColor = true;
            dllButton.Click += dllButton_Click;

            Controls.Add(dllButton);

            ResumeLayout();
        }

        private void CrystalWindow_Load(object sender, EventArgs e) {

        }

        private void dllButton_Click(object sender, EventArgs e) {
            // TODO
            MessageBox.Show("pressed button");
        }

        private void processButton_Click(object sender, EventArgs e) {
            // TODO
        }

        private void injectButton_Click(object sender, EventArgs e) {
            // TODO
        }

    }
}
