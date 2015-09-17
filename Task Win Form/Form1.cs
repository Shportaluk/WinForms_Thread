using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Win_Form
{
    public partial class Form1 : Form
    {
        public int count = 0;
        public byte[][] b { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void quantity_process_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public void Sum(int id)
        {
            int sum = 0;
            for(int i = 0; i < b[id].Length; i++)
            {
                sum += b[id][i];
                SetValue(id);
            }
        }

        public void For_col(object obj )
        {
            int id = (int)obj;
            
            for ( int i = 0; i < b.Length; i++ )
            {
                Sum(i);
            }
        }

        public void SetValue( int id )
        {
            this.BeginInvoke((Action)(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    ProgressBar p = (listView.Controls[String.Format("progress_bar_{0}", id)] as ProgressBar);
                    if (p.Value != p.Maximum)
                    { p.Value = p.Value + 1; }
                }
            }));
        }

        //----
        private void btn_Start_Click(object sender, EventArgs e)
        {
            int _row;
            int _col;

            Int32.TryParse(col.Text, out _col);
            Int32.TryParse(row.Text, out _row);

            b = new byte[_row][];
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[_col];
                for (int j = 0; j < b[i].Length; j++)
                {
                    b[i][j] = 1;
                }
            }
            

            for (int i = 0; i < _row; i++)
            {
                ProgressBar progress_bar = new ProgressBar();
                progress_bar.Size = new System.Drawing.Size(420, 23);
                progress_bar.Maximum = _col;
                progress_bar.Location = new System.Drawing.Point(0, i * 25);
                progress_bar.Name = String.Format("progress_bar_{0}", i);

                count++;

                listView.Controls.Add(progress_bar);
            }
            for (int i = 0; i < _row; i++)
            {
                Thread t = new Thread(For_col);
                t.Start(i);
            }

        }
    }
}
