using FitSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitSystem.FITControls
{
    /// <summary>
    /// Interaction logic for AddEmpExtender.xaml
    /// </summary>
    public partial class AddEmpExtender : UserControl
    {
        public AddEmpExtender()
        {
            InitializeComponent();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.Key == Key.Return)
            //{
            //    Employee employee = (DataContext as Employee);
            //    MessageBox.Show(employee.BaseSalary.ToString() + "\n" + employee.OtherFacts);
            //}
        }
    }
}
