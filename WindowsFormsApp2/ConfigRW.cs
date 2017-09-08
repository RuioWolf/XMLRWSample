using System;
using System.Collections.Generic;
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

		public static void Init()
		{
			ConfigRw.configxml = Environment.CurrentDirectory + "\\config.xml";
		}

		public static void FirstInit()
		{
			XElement xElement = new XElement(
				new XElement("Server",
					new XElement("ServerName",
						new XElement("IP", "1.2.3.4"),
						new XElement("Port", "27015"),
						new XElement("dir", ""),
						new XElement("srcds", ""),
						new XElement("srcdsargs", "")
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
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(configxml);
			}
			catch (Exception e)
			{
				return false;
			}
			//XmlNodeList nodeList = xmlDocument.GetElementsByTagName("IP");
			XmlNodeList nodeList = xmlDocument.SelectNodes("Server/*");
			if (nodeList.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}