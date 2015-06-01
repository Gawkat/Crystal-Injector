using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Crystal_Injector {
    class Crystal {

        public Process[] getProcesses() {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList) {
                Debug.WriteLine("Process: {0} ID: {1}", process.ProcessName, process.Id);
            }
            return processList;
        }

        public void inject() {
            // TODO
        }
    }
}
