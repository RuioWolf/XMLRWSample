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
			LoadConfig();
		}

		public static void LoadConfig()
		{
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
				new XElement("ServerConfig",
					new XElement("Server", new XAttribute("name", "Server1"),
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
			XmlNodeList nodeList = xmlDocument.SelectNodes("ServerConfig/*");
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

			XmlNode servername = xmlDocument.DocumentElement;
			if (servername != null)
			{
				foreach (XmlNode node in servername)
				{
					foreach (XmlAttribute xa in node.Attributes)
					{
						list.Add(xa.Value);
					}
				}
				return list;
			}
			else
			{
				return null;
			}
		}

		public static string QuerySingleItem(string qservername, string item)
		{
			//			XmlNode node = xmlDocument.SelectSingleNode("ServerConfig/" + servername + "/" + item);
			//			if (node != null)
			//			{
			//				return node.InnerText;
			//			}
			//			else
			//			{
			//				return null;
			//			}
			XmlNode servername = xmlDocument.DocumentElement;
			if (servername != null)
			{
				string result = string.Empty;
				foreach (XmlNode node in servername)
				{
					foreach (XmlAttribute xa in node.Attributes)
					{
//						list.Add(xa.Value);
						if (xa.Value == qservername)
						{
							foreach (XmlNode res in node.ChildNodes)
							{
								if (res.Name == item)
								{
									result = res.InnerText;
								}
							}
						}
					}
				}
				return result;
			}
			else
			{
				return null;
			}
		}

		public static List<string> Query(string qservername)
		{
			List<string> list = new List<string>();
			XmlNodeList lnode = xmlDocument.SelectNodes("ServerConfig/*");

			if (lnode != null)
			{
				foreach (XmlElement sname in lnode)
				{
					if (sname.GetAttribute("name") == qservername)
					{
						foreach (XmlNode item in sname.ChildNodes)
						{
							list.Add(item.InnerText);
						}
					}
				}
				return list;
			}
			else
			{
				return null;
			}
		}

		public static void EditValve(string servername, string item, string valve)
		{
			XmlNode node = xmlDocument.SelectSingleNode("Server/" + servername + "/" + item);
			node.InnerText = valve;
			xmlDocument.Save(configxml);
		}

		public static void EditName(string oldname, string newname)
		{
			XmlNode node = xmlDocument.DocumentElement;
			foreach (XmlNode oldNode in node.ChildNodes)
			{
				if (oldNode.Name == oldname)
				{
					XmlNode n = oldNode;
				}
			}
		}

		public static void ErrorHandler(string arg)
		{
			Logger.log(arg);
		}
	}
}