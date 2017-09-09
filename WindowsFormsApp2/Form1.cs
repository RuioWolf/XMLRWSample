using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp2
{
	public partial class Form1 : Form
	{
		//Init
		private static string servername;

		public Form1()
		{
			ConfigRw.Init();

			InitializeComponent();

			listBox1.DataSource = ConfigRw.QueryList();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ConfigRw.FirstInit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (ConfigRw.CheckXml())
			{
				MessageBox.Show("Verify Success");
			}
			else
			{
				MessageBox.Show("XML Error");
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
//			foreach (string listbox in ConfigRw.QueryList())
//			{
			ConfigRw.LoadConfig();
			listBox1.DataSource = ConfigRw.QueryList();
//			}
		}

		private void listBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			servername = listBox1.SelectedItem.ToString();
			textBox1.Text = servername;
//			textBox2.Text = ConfigRw.QuerySingleItem(servername, "IP");
//			textBox3.Text = ConfigRw.QuerySingleItem(servername, "Port");
			List<string> info = ConfigRw.Query(servername);
			textBox2.Text = info[0];
			textBox3.Text = info[1];
		}

		private void button4_Click(object sender, EventArgs e)
		{
			foreach (string msg in ConfigRw.Query(listBox1.SelectedItem.ToString()))
			{
				MessageBox.Show(msg);
			}
//			ConfigRw.Query(listBox1.SelectedItem.ToString());
		}

		private void button7_Click(object sender, EventArgs e)
		{
		}
	}
}