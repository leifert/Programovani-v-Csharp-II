using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Xml;
using System.Xml.Linq;

namespace cv6text
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"^[A-Za-z0-9\.\-]+@[A-Za-z0-9\.\-]+\.[a-z]{2,5}$";
            Regex regex = new Regex(pattern);


            string txt = "asfas@asds.cz";
            if (regex.IsMatch(txt))
            {
                Console.WriteLine("Je email");
            }
            else
            {
                Console.WriteLine("Neni email");
            }

            Regex reg2 = new Regex(@"(https?):\/\/(([A-Za-z0-9\.\-]+)\.)?([A-Za-z0-9\-]+\.[A-Za-z]{2,5})(\?|\$|\/)");
            string[] urls = {"https://katedrainformatiky.cz/cs/pro-uchazece/zamereni-studia",
                "http://katedrainformatiky.cz/",
                "https://katedrainformatiky.cz?page=5",
                "https://page.katedrainformatiky.cz?url=http://test.cz/" };

            foreach (string url in urls)
            {
                Console.WriteLine(url);
                Match match = reg2.Match(url);
                if (!match.Success)
                {
                    Console.WriteLine("Není url");
                    continue;
                }
                Console.WriteLine(match.Groups[1].Value);
                Console.WriteLine(match.Groups[3].Value);
                Console.WriteLine(match.Groups[4].Value);

                Console.WriteLine();
                Console.WriteLine();
            }

            Regex reg3 = new Regex(@"\{([A-Za-z]+)\}");
            string text = "Ahoj {name}. Tvá objednávka „{orderName}“ v ceně {price} byla úspěšně uhrazena.";


            string newTxt = reg3.Replace(text, (Match m) =>
            {
                return m.Groups[1].Value.ToUpper();
            });

            Console.WriteLine(newTxt);
            Console.WriteLine();

            List<Customer>customers = new List<Customer>()
            {
                new Customer(){Id = 1, Name = "Franta Kocourek", Age = 54},
                new Customer(){Id = 2, Name = "Rudy Kovanda", Age = 23},
                new Customer(){Id = 3, Name = "Jan", Age = 34}
            };


            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            XmlElement root = doc.CreateElement("customers");
            doc.AppendChild(root);

            foreach (Customer customer in customers)
            {
                XmlElement customerNode = doc.CreateElement("customer");
                root.AppendChild(customerNode);

                XmlAttribute idAttribute = doc.CreateAttribute("id");
                idAttribute.Value = customer.Id.ToString();
                customerNode.Attributes.Append(idAttribute);
                //XmlElement idNode = doc.CreateElement("íd");
                //customerNode.AppendChild(idNode);
                //idNode.AppendChild(doc.CreateTextNode(customer.Id.ToString()));

                XmlElement nameNode = doc.CreateElement("name");
                customerNode.AppendChild(nameNode);
                nameNode.AppendChild(doc.CreateTextNode(customer.Name));

                XmlElement ageNode = doc.CreateElement("age");
                customerNode.AppendChild(ageNode);
                ageNode.AppendChild(doc.CreateTextNode(customer.Age.ToString()));
            }


            doc.Save("test.xml");


            XmlDocument docLoad = new XmlDocument();

            docLoad.Load("test.xml");
            Console.WriteLine(docLoad.ChildNodes[1].Name);
            Console.WriteLine();

            //docLoad.ChildNodes[1].RemoveChild(docLoad.ChildNodes[1].ChildNodes[1]);


            foreach (XmlNode node in docLoad.SelectNodes("/customers/customer[@id=2]"))
            {
                string name = node.SelectSingleNode("name/text()")?.Value;
                string age = node.SelectSingleNode("age/text()")?.Value;
                Console.WriteLine(name + ": " + age);
            }

        }
    }
}
