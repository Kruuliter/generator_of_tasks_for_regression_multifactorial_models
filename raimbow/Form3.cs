using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace raimbow
{
    public partial class Form3 : Form
    {
        private List<Thread> threads;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string[] texts, Dictionary<int, Save_date> dict, string view)
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dict.Count;
            visualAsync(texts, dict, view);
            this.Show();
        }

        private async void visualAsync(string[] texts, Dictionary<int, Save_date> dict, string view)
        {
            await Task.Run(() => readExcel(texts, dict, view));
            button2.Visible = true;
        }

        private void readExcel(string[] str, Dictionary<int, Save_date> dict, string view)
        {
            threads = new List<Thread>();
            ReadExcelFormuls readExcelFormuls = new ReadExcelFormuls();

            int count = 5;
            foreach (KeyValuePair<int, Save_date> item in dict)
            {

                switch (view)
                {
                    case "XLSX":
                        {
                            threads.Add(new Thread(new ThreadStart(() => new ReadExcelFormuls().reading(str, item.Key, item.Value))));
                        }
                        break;
                    case "TXT":
                        {
                            threads.Add(new Thread(new ThreadStart(() => new WriteTXT().reading(str, item.Key, item.Value))));
                        }
                        break;
                }

                threads[threads.Count - 1].Start();
                if (item.Key % count == 0)
                {
                    wait_thread(threads);
                }
            }
            wait_thread(threads);
        }

        private void wait_thread(List<Thread> list_thread)
        {
            int i = 0;
            while (list_thread.Count > 0)
            {
                if (i >= list_thread.Count)
                {
                    i = 0;
                }

                if (list_thread[i].IsAlive == false)
                {
                    list_thread.RemoveAt(i);
                    BeginInvoke(new Action(() => { progressBar1.Value += 1; }));
                }

                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (threads.Count > 0)
            {
                foreach (Thread thread in threads)
                {
                    thread.Abort();
                }
            }
            this.Close();
        }
    }
}
