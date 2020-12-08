using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raimbow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AssignSettings settings = new AssignSettings();
            settings.Name = "assign_settings";
            settings.Tag = 1;
            settings.Location = new Point(40, 40);
            settings.Visible = true;
            this.Controls.Add(settings);
            GenerikTable generik = new GenerikTable();
            generik.Name = "generikTable";
            generik.Tag = 2;
            generik.Location = new Point(40, 40);
            generik.Visible = false;
            this.Controls.Add(generik);
            settings.linker = generik;
        }

        private void основнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection cont = Controls;

            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 1)
                    {
                        cont[i].Visible = true;
                    }
                    if(a == 2)
                    {
                        cont[i].Visible = false;
                    }
                }
            }
        }

        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control.ControlCollection cont = Controls;

            int len = cont.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if (cont[i].Tag != null)
                {
                    int a = (int)cont[i].Tag;
                    if (a == 1)
                    {
                        cont[i].Visible = false;
                    }
                    if (a == 2)
                    {
                        cont[i].Visible = true;
                    }
                }
            }
        }
    }
}
