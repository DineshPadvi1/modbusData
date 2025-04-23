using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniproject.Classes;
namespace Uniproject.UtilityTools
{
    public partial class UniBox : Form
    {
        public UniBox(string message)
        {
            InitializeComponent();
            txtMessage.Text = message;

        }

        

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UniBox_Load(object sender, EventArgs e)
        {

        }
    }
}
