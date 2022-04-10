using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NeuralNetworkLibrary;
using static NeuralNetworkLibrary.NeuralNetwork;
using static NeuralNetworkLibrary.MathFunctions;

namespace TestNeuralNetworkLibrary
{
    public class TestMNIST
    {
        public static Gui gui;
        public static NeuralNetwork net;
        public static (float[][] trainingdata, float[][] traininglabels) getData()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            IEnumerable<string> lines = File.ReadAllLines(projectPath+"/datasets/mnist_test.csv");


            string[][] csv = (from line in lines
                              select (line.Split(',')).ToArray()).ToArray(); //array of all the lines


            float[][] trainingdata = new float[csv.Length][];
            float[][] traininglabels = new float[csv.Length][];


            for (int line = 0; line < csv.Length; line++)
            {
                traininglabels[line] = new float[10];
                traininglabels[line][int.Parse(csv[line][0])] = 1f;
                trainingdata[line] = Array.ConvertAll(csv[line].Skip(1).ToArray(), element => float.Parse(element) / 255.0f);
                //csv[line].Skip(1).ToArray()
            }
            return (trainingdata, traininglabels);
        }
        public static float[][] trainingdata;
        public static float[][] traininglabels;
        public static void testminst()
        {
            AllocConsole();


            int epochs = 25;
            var data = getData();
            trainingdata = data.trainingdata;
            traininglabels = data.traininglabels;

            /*            Console.WriteLine("TRAINING DATA ORIG 0 IS " + (x_train[0]));
                        Console.WriteLine("TRAINING LABEL ORIG 0 IS " + (y_train[0]));


                        Console.WriteLine("TRAINING DATA 0 IS " + str(trainingdata[0]));
                        Console.WriteLine("TRAINING LABEL 0 IS " + str(traininglabels[0]));
            */
            //# network
            net = new NeuralNetwork(mse, mse_prime);
            net.add(new CrossConnectedLayer(28 * 28, 100));
            net.add(new FunctionLayer(tanh, tanh_prime));
            net.add(new CrossConnectedLayer(100, 50));
            net.add(new FunctionLayer(tanh, tanh_prime));
            net.add(new CrossConnectedLayer(50, 10));
            net.add(new FunctionLayer(tanh, tanh_prime));

            //train
            net.train(trainingdata.Take(1000).ToArray(), traininglabels.Take(1000).ToArray(), epochs, 0.1f);
            startForm();
            //gui.label1.Text = "0    1    2   3    4   5    6   7    8    9";

            //# test
            //CreateImage()
            //Console.WriteLine(str(trainingdata[1000]));

            /*            Random random = new Random();
                        for (int i = 0; i < 50; i++)
                        {
                            int index = (int)Math.Round(random.NextSingle() * (trainingdata.Length - 1));
                            CreateImage(Make2DArray(trainingdata[index], 28, 28));
                            float[] output = net.predict(trainingdata[index]);
                            Console.WriteLine(str(traininglabels[index]));
                            Console.WriteLine(str(output));
                        }*/

        }
        [STAThread]
        static void startForm()
        {
            Application.EnableVisualStyles();
            gui = new Gui();
            gui.InitializeComponent();
            Application.Run(gui); // or whatever
            //gui.label1.Text = "Training...";

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }





    public class Gui : Form
    {



        
        bool painting = false;
        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                painting = true;
            }
        }
        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                painting = false;
                showPredictedResults(TestMNIST.net);
/*                float[] tempthing = TestMNIST.trainingdata[(int)(((new Random().NextDouble()) * TestMNIST.trainingdata.Length) - 1)];
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    for (int i = 0; i < 28; i++)
                    {
                        for (int j = 0; j < 28; j++)
                        {
                            Color color = Color.FromArgb((int)(tempthing[(j * 28) + i] * 255),
                                (int)(tempthing[(j * 28) + i] * 255),
                                (int)(tempthing[(j * 28) + i] * 255));
                            g.FillRectangle(new SolidBrush(color), new Rectangle(i * 10, j * 10, 10, 10));

                        }
                    }

                }*/
            }
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            painting = false;
            showPredictedResults(TestMNIST.net);
        }
        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (painting)
            {
                Brush centerBrush = new SolidBrush(Color.White);
                Image image = pictureBox1.Image;
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(centerBrush, new Rectangle((int)(MathF.Floor(e.X / 10f)+0) * 10, (int)(MathF.Floor(e.Y / 10f)+0) * 10, 10, 10));
                    g.FillRectangle(centerBrush, new Rectangle((int)(MathF.Floor(e.X / 10f)+1) * 10, (int)(MathF.Floor(e.Y / 10f)+0) * 10, 10, 10));
                    g.FillRectangle(centerBrush, new Rectangle((int)(MathF.Floor(e.X / 10f)+0) * 10, (int)(MathF.Floor(e.Y / 10f)+1) * 10, 10, 10));
                    g.FillRectangle(centerBrush, new Rectangle((int)(MathF.Floor(e.X / 10f)-1) * 10, (int)(MathF.Floor(e.Y / 10f)+0) * 10, 10, 10));
                    g.FillRectangle(centerBrush, new Rectangle((int)(MathF.Floor(e.X / 10f)+0) * 10, (int)(MathF.Floor(e.Y / 10f)-1) * 10, 10, 10));

                    //if(((Bitmap)pictureBox1.Image).GetPixel((int)MathF.Floor(e.X / 10f) * 10, (int)MathF.Floor(e.Y / 10f) * 10))
                }
                pictureBox1.Image = image;
            }
        }

        private void button1Click(object sender, EventArgs e)
        {
            Bitmap empty = new Bitmap(280, 280);

            for (int i = 0; i < 280; i++)
            {
                for (int j = 0; j < 280; j++)
                {
                    empty.SetPixel(i, j, Color.Black);
                }
            }
            Brush brush = new SolidBrush(Color.Black);
            using (Graphics g = Graphics.FromImage(empty))
            {
                g.FillRectangle(brush, new Rectangle(0,0,280,280));
            }

            pictureBox1.Image = empty;
        }
        private void showPredictedResults(NeuralNetwork net)
        {
            /*            using (Bitmap bmp = (Bitmap)pictureBox1.Image)
                        {*/
            if (net == null) return;
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            float[] input = new float[28 * 28];
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    Console.WriteLine(bmp.GetPixel(i * 10, j * 10).R);
                    input[(j * 28) + i] = bmp.GetPixel(i * 10, j * 10).R / 255f;
/*                    if (bmp.GetPixel(i*10, j*10).R == 0) {
                        input[(j * 28) + i] = 0;
                    }
                    else
                    {
                        input[(j * 28) + i] = 1;
                    }*/
                }
            }
            float[] output = net.predict(input);
            //Console.WriteLine("new output is " + String.Join(",", output));
            bar0.Height = (int)(output[0] * 200f);
            bar0.Top = 12 + (200 - (int)(output[0] * 200f));
            bar1.Height = (int)(output[1] * 200f);
            bar1.Top = 12 + (200 - (int)(output[1] * 200f));
            bar2.Height = (int)(output[2] * 200f);
            bar2.Top = 12 + (200 - (int)(output[2] * 200f));
            bar3.Height = (int)(output[3] * 200f);
            bar3.Top = 12 + (200 - (int)(output[3] * 200f));
            bar4.Height = (int)(output[4] * 200f);
            bar4.Top = 12 + (200 - (int)(output[4] * 200f));
            bar5.Height = (int)(output[5] * 200f);
            bar5.Top = 12 + (200 - (int)(output[5] * 200f));
            bar6.Height = (int)(output[6] * 200f);
            bar6.Top = 12 + (200 - (int)(output[6] * 200f));
            bar7.Height = (int)(output[7] * 200f);
            bar7.Top = 12 + (200 - (int)(output[7] * 200f));
            bar8.Height = (int)(output[8] * 200f);
            bar8.Top = 12 + (200 - (int)(output[8] * 200f));
            bar9.Height = (int)(output[9] * 200f);
            bar9.Top = 12 + (200 - (int)(output[9] * 200f));
        //}
        }




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

        public void InitializeComponent()
        {
            //this.Load += new EventHandler(this.onLoad);

            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            this.button1 = new Button();
            this.bar0 = new Panel();
            this.bar1 = new Panel();
            this.bar2 = new Panel();
            this.bar3 = new Panel();
            this.bar4 = new Panel();
            this.bar5 = new Panel();
            this.bar6 = new Panel();
            this.bar7 = new Panel();
            this.bar8 = new Panel();
            this.bar9 = new Panel();
            this.label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 280);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Image = new Bitmap(280, 280);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseLeave += new EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.bar9);
            this.panel1.Controls.Add(this.bar8);
            this.panel1.Controls.Add(this.bar7);
            this.panel1.Controls.Add(this.bar6);
            this.panel1.Controls.Add(this.bar5);
            this.panel1.Controls.Add(this.bar4);
            this.panel1.Controls.Add(this.bar3);
            this.panel1.Controls.Add(this.bar2);
            this.panel1.Controls.Add(this.bar1);
            this.panel1.Controls.Add(this.bar0);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Text = "DEL";
            this.button1.Location = new System.Drawing.Point(295, 275);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 20);
            this.button1.TabIndex = 1;
            this.button1.Click += new EventHandler(this.button1Click);
            // 
            // bar0
            // 
            this.bar0.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar0.BackColor = System.Drawing.Color.Black;
            this.bar0.Location = new System.Drawing.Point(309, 12);
            this.bar0.Margin = new System.Windows.Forms.Padding(0);
            this.bar0.Name = "bar0";
            this.bar0.Size = new System.Drawing.Size(25, 200);
            this.bar0.TabIndex = 1;
            // 
            // bar1
            // 
            this.bar1.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar1.BackColor = System.Drawing.Color.Black;
            this.bar1.Location = new System.Drawing.Point(359, 12);
            this.bar1.Margin = new System.Windows.Forms.Padding(0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(25, 200);
            this.bar1.TabIndex = 2;
            // 
            // bar2
            // 
            this.bar2.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar2.BackColor = System.Drawing.Color.Black;
            this.bar2.Location = new System.Drawing.Point(409, 12);
            this.bar2.Margin = new System.Windows.Forms.Padding(0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(25, 200);
            this.bar2.TabIndex = 3;
            // 
            // bar3
            // 
            this.bar3.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar3.BackColor = System.Drawing.Color.Black;
            this.bar3.Location = new System.Drawing.Point(459, 12);
            this.bar3.Margin = new System.Windows.Forms.Padding(0);
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(25, 200);
            this.bar3.TabIndex = 4;
            // 
            // bar4
            // 
            this.bar4.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar4.BackColor = System.Drawing.Color.Black;
            this.bar4.Location = new System.Drawing.Point(509, 12);
            this.bar4.Margin = new System.Windows.Forms.Padding(0);
            this.bar4.Name = "bar4";
            this.bar4.Size = new System.Drawing.Size(25, 200);
            this.bar4.TabIndex = 5;
            // 
            // bar5
            // 
            this.bar5.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar5.BackColor = System.Drawing.Color.Black;
            this.bar5.Location = new System.Drawing.Point(559, 12);
            this.bar5.Margin = new System.Windows.Forms.Padding(0);
            this.bar5.Name = "bar5";
            this.bar5.Size = new System.Drawing.Size(25, 200);
            this.bar5.TabIndex = 6;
            // 
            // bar6
            // 
            this.bar6.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar6.BackColor = System.Drawing.Color.Black;
            this.bar6.Location = new System.Drawing.Point(609, 12);
            this.bar6.Margin = new System.Windows.Forms.Padding(0);
            this.bar6.Name = "bar6";
            this.bar6.Size = new System.Drawing.Size(25, 200);
            this.bar6.TabIndex = 7;
            // 
            // bar7
            // 
            this.bar7.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar7.BackColor = System.Drawing.Color.Black;
            this.bar7.Location = new System.Drawing.Point(659, 12);
            this.bar7.Margin = new System.Windows.Forms.Padding(0);
            this.bar7.Name = "bar7";
            this.bar7.Size = new System.Drawing.Size(25, 200);
            this.bar7.TabIndex = 8;
            // 
            // bar8
            // 
            this.bar8.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar8.BackColor = System.Drawing.Color.Black;
            this.bar8.Location = new System.Drawing.Point(709, 12);
            this.bar8.Margin = new System.Windows.Forms.Padding(0);
            this.bar8.Name = "bar8";
            this.bar8.Size = new System.Drawing.Size(25, 200);
            this.bar8.TabIndex = 9;
            // 
            // bar9
            // 
            this.bar9.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.bar9.BackColor = System.Drawing.Color.Black;
            this.bar9.Location = new System.Drawing.Point(759, 12);
            this.bar9.Margin = new System.Windows.Forms.Padding(0);
            this.bar9.Name = "bar9";
            this.bar9.Size = new System.Drawing.Size(25, 200);
            this.bar9.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(309, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 46);
            this.label1.TabIndex = 11;
            this.label1.Text = "0    1    2   3    4   5    6   7    8    9";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

            button1Click(null, null);
        }


        private PictureBox pictureBox1;
        private Panel panel1;
        private Button button1;
        public Label label1;
        private Panel bar9;
        private Panel bar8;
        private Panel bar7;
        private Panel bar6;
        private Panel bar5;
        private Panel bar4;
        private Panel bar3;
        private Panel bar2;
        private Panel bar1;
        private Panel bar0;
    }
}
