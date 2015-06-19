using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inobix.Windows.Components.IBscanner.Demo;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        BarcodeScanner ibscanner = new BarcodeScanner();

        public Form1()
        {
            InitializeComponent();

            ibscanner.OnScanningCompleted += new BarcodeScanner.ScanningCompletedEventHandler(ibscanner_OnScanningCompleted); 
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            tbResult.Clear();
            ibscanner.EnableMultiThreading = true;

            string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Path.Combine(AppPath, "test_images");

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ibscanner.ReadFromFile(openFileDialog1.FileName);
            }
        }

        void ibscanner_OnScanningCompleted()
        {
            foreach (BarcodeItem barcode in ibscanner.BarcodesList.Values)
            {
                // Get barcode value  
                string codeValue = barcode.Barcode;

                // Get barcode type  
                string codeType = barcode.CodeType.ToString();

                string coordinateX = barcode.CenterX.ToString();
                string coordinateY = barcode.CenterY.ToString();
                tbResult.AppendText("Value: " + codeValue + " Type: " + codeType + " X: " + coordinateX + " Y: " + coordinateY + Environment.NewLine);
                
            }
        }  
    }
}
