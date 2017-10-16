using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FitzHugh_Nagumo
{
    public partial class Form1 : Form
    {
        int size_x0, size_y0, size_a0;
        int N, iterations;
        double d, eps;

        List<double> x0  = new List<double>(), y0 = new List<double>(), a0 = new List<double>();



        public Form1()
        {        
            InitializeComponent();

        }

        private int Read_vector(string str, ref List<double> list)
        {
            int count = 0;
            string tmp_str = "";
            string mystr = "";
 
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 48 && str[i] <= 57)
                    mystr += str[i];
                else if (str[i] == '.' || str[i] == ',')
                    mystr += ',';
                else if (str[i] == ' ')
                {
                    mystr += str[i];
                    while (i < str.Length && str[i] == ' ') i++;
                    i--;
                }
            }

            list.Clear();
            for (int i = 0; i < mystr.Length; i++)
            {
                tmp_str += mystr[i];
                if (mystr[i] == ' ' || i == mystr.Length - 1)
                {
                    list.Add(Convert.ToDouble(tmp_str));
                    count++;
                    tmp_str = "";
                }
            }

            return count;
        }

        private void Read_options()
        {
            try
            {
                N = Convert.ToInt32(textBox1.Text);
                iterations = Convert.ToInt32(textBox2.Text);
                d = Convert.ToDouble(textBox6.Text);
                eps = Convert.ToDouble(textBox7.Text);
            }
            catch
            {
                Form4 new_form = new Form4();
                new_form.ShowDialog();
                return;
            }

            string str = "";

            str = textBox3.Text;
            size_x0 = Read_vector(str, ref x0);

            str = textBox4.Text;
            size_y0 = Read_vector(str, ref y0);

            str = textBox5.Text;
            size_a0 = Read_vector(str, ref a0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Read_options();

            if (N == size_x0 && N == size_y0 && N == size_a0)
            {
                Form2 new_form = new Form2(d, eps, x0, y0, a0, iterations, N);
                new_form.ShowDialog();
            }
            else
            {
                Form3 new_form = new Form3(N, size_x0, size_y0, size_a0);
                new_form.ShowDialog();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text|*.txt|Conf|*.config|Conf|*.ini";
            of.InitialDirectory = "";

            if (of.ShowDialog() == DialogResult.OK)
            {
                if (of.FileName != null)
                {
                    string serialized = System.IO.File.ReadAllText(of.FileName);
                    System_FitzHugh_Nagumo my_system = JsonConvert.DeserializeObject<System_FitzHugh_Nagumo>(serialized);

                    N = my_system.count_elements;
                    iterations = my_system.count_iterations;
                    d = my_system.d;
                    eps = my_system.epsilon;

                    textBox1.Text = N.ToString();
                    textBox2.Text = iterations.ToString();
                    textBox6.Text = d.ToString();
                    textBox7.Text = eps.ToString();

                    x0.Clear();
                    y0.Clear();
                    a0.Clear();

                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";

                    for (int i = 0; i < N; i++)
                    {
                        a0.Add(my_system.elements[i].a);
                        x0.Add(my_system.elements[i].x);
                        y0.Add(my_system.elements[i].y);

                        textBox3.Text += x0[i] + " ";
                        textBox4.Text += y0[i] + " ";
                        textBox5.Text += a0[i] + " ";
                    }
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Read_options();

            if (N == size_x0 && N == size_y0 && N == size_a0)
            {
               SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Text|*.txt|Conf|*.config|Conf|*.ini";
                sf.InitialDirectory = "";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    if (sf.FileName != null)
                    {
                        System_FitzHugh_Nagumo my_system = new System_FitzHugh_Nagumo() { count_iterations = iterations, count_elements = N, d = d, epsilon = eps };
                        my_system.elements = new Element[N];
                        for (int i = 0; i < N; i++)
                            my_system.elements[i] = new Element() { a = a0[i], x = x0[i], y = y0[i] };

                        string serialized = JsonConvert.SerializeObject(my_system);
                        if (serialized != null)
                            System.IO.File.WriteAllText(sf.FileName, serialized);
                    }
                }
            }
            
            else
            {
                Form3 new_form = new Form3(N, size_x0, size_y0, size_a0);
                new_form.ShowDialog();
            }

        }
    }

    class System_FitzHugh_Nagumo
    {
        public int count_elements { get; set; }
        public int count_iterations { get; set; }
        public double d { get; set; }
        public double epsilon { get; set; }
        public Element[] elements { get; set; }
    }

    class Element
    {
        public double a { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}
