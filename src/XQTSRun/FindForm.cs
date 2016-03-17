using System;
using System.Windows.Forms;

namespace XQTSRun
{
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }

        public int RowIndex { get; set; }

        public String Value
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public event EventHandler FindNext
        {
            add
            {
                button1.Click += value;
            }
            remove
            {
                button1.Click -= value;
            }
        }
    }
}
