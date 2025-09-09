using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaTourDeNeuille
{
    public partial class Responsive : Form
    {
        public Responsive()
        {
            InitializeComponent();
        }


        private void Responsive_SizeChanged(object sender, EventArgs e)
        {
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
        }
    }
}
