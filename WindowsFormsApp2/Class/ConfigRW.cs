using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
	class ConfigRw
	{
		private static string configxml;
		private static XmlDocument xmlDocument = new XmlDocument();

		public static void Init()
		{
			configxml = Environment.CurrentDirectory + "\\config.xml";
			try
			{
				xmlDocument.Load(configxml);
			}
			catch (Exception e)
			{
				Logger.log(e.Message);
			}
		}

		public static void FirstInit()
		{
			XElement xElement = new XElement(
				new XElement("Server",
					new XElement("ServerName",
						new XElement("IP", "1.2.3.4"),
						new XElement("Port", "27015")
					)
				)
			);
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Encoding = new UTF8Encoding(true),
				Indent = true
			};
			XmlWriter xmlWriter = XmlWriter.Create(configxml, settings);
			xElement.Save(xmlWriter);
			xmlWriter.Flush();
			xmlWriter.Close();
		}

		public static bool CheckXml()
		{
//			try
//			{
//				xmlDocument.Load(configxml);
//			}
//			catch (Exception e)
//			{
//				Logger.log(e.Message);
//				return false;
//			}
			//XmlNodeList nodeList = xmlDocument.GetElementsByTagName("IP");
			XmlNodeList nodeList = xmlDocument.SelectNodes("Server/*");
			if (nodeList.Count > 0)
			{
				return true;
			}
			return false;
		}

		public static List<string> QueryList()
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();

			try
			{
				xmlDocument.Load(configxml);
			}
			catch (Exception e)
			{
				ErrorHandler(e.Message);
				FirstInit();
			}
			XmlNode servername = xmlDocument.DocumentElement;
			//			XmlNodeList serverlist = servername.ChildNodes;
			if (servername != null)
			{
				foreach (XmlNode node in servername.ChildNodes)
				{
					list.Add(node.Name);
				}
				return list;
			}
			else
			{
				return null;
			}
		}

		public static string Query(string server, string item)
		{
			XmlNode node = xmlDocument.SelectSingleNode("Server/" + server + "/" + item);
			if (node != null)
			{
				return node.InnerText;
			}
			else
			{
				return null;
			}
		}

		public static void ErrorHandler(string arg)
		{
			Logger.log(arg);
		}
	}
}