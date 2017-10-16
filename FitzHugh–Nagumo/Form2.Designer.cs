namespace FitzHugh_Nagumo
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.zedGraph1 = new ZedGraph.ZedGraphControl();
            this.zedGraph2 = new ZedGraph.ZedGraphControl();
            this.zedGraph3 = new ZedGraph.ZedGraphControl();
            this.zedGraph4 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraph1
            // 
            this.zedGraph1.IsShowPointValues = false;
            this.zedGraph1.Location = new System.Drawing.Point(12, 12);
            this.zedGraph1.Name = "zedGraph1";
            this.zedGraph1.PointValueFormat = "G";
            this.zedGraph1.Size = new System.Drawing.Size(550, 400);
            this.zedGraph1.TabIndex = 0;
            // 
            // zedGraph2
            // 
            this.zedGraph2.IsShowPointValues = false;
            this.zedGraph2.Location = new System.Drawing.Point(568, 12);
            this.zedGraph2.Name = "zedGraph2";
            this.zedGraph2.PointValueFormat = "G";
            this.zedGraph2.Size = new System.Drawing.Size(550, 400);
            this.zedGraph2.TabIndex = 1;
            // 
            // zedGraph3
            // 
            this.zedGraph3.IsShowPointValues = false;
            this.zedGraph3.Location = new System.Drawing.Point(12, 418);
            this.zedGraph3.Name = "zedGraph3";
            this.zedGraph3.PointValueFormat = "G";
            this.zedGraph3.Size = new System.Drawing.Size(550, 400);
            this.zedGraph3.TabIndex = 2;
            // 
            // zedGraph4
            // 
            this.zedGraph4.IsShowPointValues = false;
            this.zedGraph4.Location = new System.Drawing.Point(568, 418);
            this.zedGraph4.Name = "zedGraph4";
            this.zedGraph4.PointValueFormat = "G";
            this.zedGraph4.Size = new System.Drawing.Size(550, 400);
            this.zedGraph4.TabIndex = 3;
            this.zedGraph4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1125, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Интервал отображения:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1128, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1128, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 20);
            this.textBox2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1128, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 845);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zedGraph4);
            this.Controls.Add(this.zedGraph3);
            this.Controls.Add(this.zedGraph2);
            this.Controls.Add(this.zedGraph1);
            this.Name = "Form2";
            this.Text = "Результаты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph1;
        private ZedGraph.ZedGraphControl zedGraph2;
        private ZedGraph.ZedGraphControl zedGraph3;
        private ZedGraph.ZedGraphControl zedGraph4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;


    }
}