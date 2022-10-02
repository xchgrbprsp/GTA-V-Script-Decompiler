using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decompiler
{
    public partial class ProgressBar : Form
    {
        public int Min;
        public int Max;
        int Value = 0;

        // TODO: there must be a better way than this
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;

                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        public ProgressBar(string name, int min, int max)
        {
            InitializeComponent();
            Text = name;
            Min = min;
            Max = max;
            Value = min;
            progressBar1.Maximum = max;
            progressBar1.Minimum = min;
            progressBar1.Value = min;
        }

        public void SetMax(int max)
        {
            Max = max;
            progressBar1.Maximum = max;
            progressBar1.Update();
        }

        public void SetValue(int value)
        {
            Value = value;
            progressBar1.Value = value;
            progressBar1.Update();
        }

        public void IncrementValue()
        {
            SetValue(Value + 1);
        }
    }
}
