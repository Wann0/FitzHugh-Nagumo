using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace FitzHugh_Nagumo
{
    public partial class Form2 : Form
    {     
        const double h = 0.01;
        const double err = 0.000001;

        double eps, d;
        int N = 4;
        int number_iterations = 10000;
        List<double> x0, y0, a;
        List<double> X, Y, T, W;

        public Form2(double d, double eps, List<double> x, List<double> y, List<double> a0, int num_iteration, int size)
        {
            this.N = size;
            InitializeComponent();
            this.d = d;
            this.eps = eps;
            this.number_iterations = num_iteration;
            x0 = x;
            y0 = y;
            a = a0;
           
            RungeKutta();
            DrawGraph(0,num_iteration);
        }

        private void DrawGraph(int times, int timef)
        {
            // Получим панель для рисования
            GraphPane pane1 = zedGraph1.GraphPane;
            GraphPane pane2 = zedGraph2.GraphPane;
            GraphPane pane3 = zedGraph3.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane1.CurveList.Clear();
            pane2.CurveList.Clear();
            pane3.CurveList.Clear();

            // Создадим список точек
            List<PointPairList> list_yx = new List<PointPairList>();
            List<PointPairList> list_yt = new List<PointPairList>();
            List<PointPairList> list_xt = new List<PointPairList>();

            for (int i = 0; i < N; i++)
            {
                list_yx.Add(new PointPairList());
                list_yt.Add(new PointPairList());
                list_xt.Add(new PointPairList());
            }

            // Заполняем список точек
            for (int i = times; i < timef; i++)
            {
                // добавим в список точку
                for (int j = 0; j < N; j++)
                {
                    list_yx[j].Add(X[N * i + j], Y[N * i + j]);
                    list_xt[j].Add(T[i], X[N * i + j]);
                    list_yt[j].Add(T[i], Y[N * i + j]);
                }
            }

            List<LineItem> myCurve_yx = new List<LineItem>();
            List<LineItem> myCurve_yt = new List<LineItem>();
            List<LineItem> myCurve_xt = new List<LineItem>();

            List<Color> myColor = new List<Color>(N);
            if (N <= 4)
            {
                myColor.Add(Color.Blue);
                myColor.Add(Color.Green);
                myColor.Add(Color.Red);
                myColor.Add(Color.Orange);
                myColor.Add(Color.Black);
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    if (i % 3 == 0) myColor.Add(Color.FromArgb(255 - i * 255 / N, 0, 0));
                    else if (i % 3 == 1) myColor.Add(Color.FromArgb(0, 255 - i * 255 / N, 0));
                    else myColor.Add(Color.FromArgb(0, 0, 255 - i * 255 / N));
                }
            }
            //нужно автоматически заполнить список цветами

            for (int i = 0; i < N; i++)
            {
                myCurve_yx.Add(pane1.AddCurve("y[" + (i + 1) + "](x[" + (i + 1) + "])", list_yx[i], myColor[i], SymbolType.None));
                myCurve_yt.Add(pane2.AddCurve("y[" + (i + 1) + "](t)", list_yt[i], myColor[i], SymbolType.None));
                myCurve_xt.Add(pane3.AddCurve("x[" + (i + 1) + "](t)", list_xt[i], myColor[i], SymbolType.None));
            }

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. В противном случае на рисунке будет показана только часть графика, которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph1.AxisChange();
            zedGraph2.AxisChange();
            zedGraph3.AxisChange();

            // Обновляем график
            zedGraph1.Invalidate();
            zedGraph2.Invalidate();
            zedGraph3.Invalidate();
        }

        List<double> Add(List<double> v, double num)
        {
            List<double> res = new List<double>(N);
            for (int i = 0; i < N; i++)
                res.Add(v[i] + num);

            return res;
        }

        List<double> Add(List<double> v, List<double> n)
        {
            List<double> res = new List<double>(N);
            for (int i = 0; i < N; i++)
                res.Add(v[i] + n[i]);

            return res;
        }

        List<double> Mult(List<double> v, double num)
        {
            List<double> res = new List<double>(N);
            for (int i = 0; i < N; i++)
                res.Add(v[i] * num);

            return res;
        }

        void f(ref List<double> res_x, ref List<double> res_y, List<double> x, List<double> y, List<double> a, double eps, double d)
        {
            res_x.Clear();
            res_y.Clear();

            for (int i = 0; i < N; i++)
            {
                int prev = i - 1, next = i + 1;
                if (prev < 0)
                    prev = N - 1;
                if (next > N - 1)
                    next = 0;
                res_x.Add(x[i] - Math.Pow(x[i], 3.0) / 3.0 - y[i] + d * (x[prev] - 2 * x[i] + x[next]));
                res_y.Add(eps * (x[i] + a[i]));
            }
        }

        void RungeKutta()
        {
            double time = 0;
            X = new List<double>(); Y = new List<double>(); T = new List<double>();
            List<double> k1_x = new List<double>(N), k2_x = new List<double>(N), k3_x = new List<double>(N), k4_x = new List<double>(N),
                k1_y = new List<double>(N), k2_y = new List<double>(N), k3_y = new List<double>(N), k4_y = new List<double>(N),
                x_prev = new List<double>(N), x_next = new List<double>(N), y_prev = new List<double>(N), y_next = new List<double>(N),
                tmp_x = new List<double>(N), tmp_y = new List<double>(N);
            x_prev = x0;
            y_prev = y0;
            tmp_x = x0;
            tmp_y = y0;

            for(int i = 0; i < number_iterations; i++){
                x_prev = tmp_x;
                y_prev = tmp_y;

                f(ref k1_x, ref k1_y, x_prev, y_prev, a, eps, d);
                f(ref k2_x, ref k2_y, Add(x_prev, h / 2.0), Add(y_prev, Mult(k1_y, h / 2.0)), a, eps, d);
                f(ref k3_x, ref k3_y, Add(x_prev, h / 2.0), Add(y_prev, Mult(k2_y, h / 2.0)), a, eps, d);
                f(ref k4_x, ref k4_y, Add(x_prev, h), Add(y_prev, Mult(k3_y, h)), a, eps, d);

                x_next = Add(x_prev, Mult(Add(Add(k1_x, Mult(k2_x, 2.0)), Add(Mult(k3_x, 2.0), k4_x)), h / 6.0));
                y_next = Add(y_prev, Mult(Add(Add(k1_y, Mult(k2_y, 2.0)), Add(Mult(k3_y, 2.0), k4_y)), h / 6.0));

                for (int j = 0; j < N; j++)
                {
                    X.Add(x_next[j]);
                    Y.Add(y_next[j]);
                }

                T.Add(time);
                time += h;

                tmp_x = x_next;
                tmp_y = y_next;
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int times = 0; int timef = (int)(number_iterations * h);
            times = Convert.ToInt32(textBox1.Text);
            if (times < 0) times = 0;
            timef = Convert.ToInt32(textBox2.Text);
            if (timef > (int)(number_iterations * h)) timef = (int)(number_iterations * h);
            textBox1.Text = times.ToString();
            textBox2.Text = timef.ToString();
            if (times <= timef)
                DrawGraph((int)(times/h), (int)(timef/h));
        }
    }
}
