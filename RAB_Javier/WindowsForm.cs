using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAB_Javier
{
    // so the probelm is this line : public partial class WindowsForm : Form
    // tienes que anadir lo de abajo
    public partial class WindowsForm : System.Windows.Forms.Form
    {
        public WindowsForm()
        {
            InitializeComponent();
        }
    }
}
