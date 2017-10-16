using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitzHugh_Nagumo
{
    public partial class Form3 : Form
    {
        public Form3(int N, int size_x0, int size_y0, int size_a0)
        {
            InitializeComponent();
            if (N != size_x0)
                label1.Text = "Неверная размерность начальных значений x";
            if (N != size_y0)
                label2.Text = "Неверная размерность начальных значений y";
            if (N != size_a0)
                label3.Text = "Неверная размерность начальных значений a";
        }
    }
}
