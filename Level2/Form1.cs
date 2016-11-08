using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Level2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        List<Person> people = new List<Person>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\Address Book - Ralph"))
                Directory.CreateDirectory(path + "\\Address Book - Ralph");
            if (!File.Exists(path + "\\Address Book - Ralph\\settings.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter(path + "\\Address Book - Ralph\\settings.xml", Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\Address Book - Ralph\\settings.xml");
            foreach (XmlNode xNode in xDoc.SelectNodes("People/Person"))
            {
                Person p = new Person();
                p.Name = xNode.SelectSingleNode("Name").InnerText;
                p.Email = xNode.SelectSingleNode("Email").InnerText;
                p.StreetAddress = xNode.SelectSingleNode("Address").InnerText;
                p.Notes = xNode.SelectSingleNode("Notes").InnerText;
                p.Birthday = DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("Birthday").InnerText));
                people.Add(p);
                listView1.Items.Add(p.Name);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = textBox1.Text;
            p.Email = textBox2.Text;
            p.StreetAddress = textBox3.Text;
            p.Birthday = dateTimePicker1.Value;
            p.Notes = textBox4.Text;
            people.Add(p);
            listView1.Items.Add(p.Name);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                textBox1.Text = people[item.Index].Name;
                textBox2.Text = people[item.Index].Email;
                textBox3.Text = people[item.Index].StreetAddress;
                textBox4.Text = people[item.Index].Notes;
                dateTimePicker1.Value = people[item.Index].Birthday;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Remove();
        }

        void Remove()
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                people.RemoveAt(listView1.SelectedItems[0].Index);
            }
            catch { }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                people[item.Index].Name = textBox1.Text;
                people[item.Index].Email = textBox2.Text;
                people[item.Index].StreetAddress = textBox3.Text;
                people[item.Index].Notes = textBox4.Text;
                people[item.Index].Birthday = dateTimePicker1.Value;
                listView1.SelectedItems[0].Text = textBox1.Text;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xDoc.Load(path + "\\Address Book - Ralph\\settings.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();

            foreach (Person p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xAddress = xDoc.CreateElement("Address");
                XmlNode xNotes = xDoc.CreateElement("Notes");
                XmlNode xBirthday = xDoc.CreateElement("Birthday");
                xName.InnerText = p.Name;
                xEmail.InnerText = p.Email;
                xAddress.InnerText = p.StreetAddress;
                xNotes.InnerText = p.Notes;
                xBirthday.InnerText = p.Birthday.ToFileTime().ToString();
                xTop.AppendChild(xName);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xAddress);
                xTop.AppendChild(xNotes);
                xTop.AppendChild(xBirthday);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save(path + "\\Address Book - Ralph\\settings.xml");
        }
    }

    class Person
    {
        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string StreetAddress
        {
            get;
            set;
        }
        public string Notes
        {
            get;
            set;
        }

        public DateTime Birthday
        {
            get;
            set;
        }
    }
}



