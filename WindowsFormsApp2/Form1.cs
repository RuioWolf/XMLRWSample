using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static WindowsFormsApp2.ConfigRw;

namespace WindowsFormsApp2
{
	public partial class Form1 : Form
	{
		//Init
		string cfg = Environment.CurrentDirectory + "\\config.xml";

		bool isxmlok;

		ConfigRw c=new ConfigRw();

		public Form1()
		{
			ConfigRw.Init();
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ConfigRw.FirstInit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (CheckXml())
			{
				MessageBox.Show("Verify Success");
			}
			else
			{
				MessageBox.Show("XML Error");
			}
		}

		//        public static void main(String[] args)
		//        {
		//            
		//        }
	}
}