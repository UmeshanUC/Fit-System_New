using FITSystem.FITPages;
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

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DashB : Window
    {
        #region Props and Fields
        private enum NavBtns
        {
            home, staff_members, finance, inventory
        }

        private NavBtns ClickedNavBtn;

        #endregion

        public DashB()
        {
            InitializeComponent();

            this.DataContext = new DashB_DataCtx();
            ClickedNavBtn = NavBtns.home;
        }
        #region Event Handlers
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        // Nav Button Click => for all Nav Buttons
        private void SetNavBtnClickedStyle(object sender, RoutedEventArgs e)
        {
            //Clicked Button
            Button _clickedBtn = sender as Button;

            // Deselect All Other
            foreach (var navBtn in stackNavBar.Children)
            {
                (navBtn as Button).Style = stackNavBar.Resources["NavBtnStyle"] as Style;
            }

            _clickedBtn.Style = stackNavBar.Resources["NavButtonClicked"] as Style;

            // Setting the clicked NavBtn
            Enum.TryParse(_clickedBtn.Content.ToString().Replace('/', '_').ToLower(), out ClickedNavBtn);
            NavigatetoPage(ClickedNavBtn);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigate to the Page Based on the clicked button
        /// </summary>
        /// <param name="clickeBtnEnum">Clicked button as a Enumarator, NavBtns</param>
        private void NavigatetoPage(NavBtns clickeBtnEnum)
        {
            switch (clickeBtnEnum)
            {
                case NavBtns.home:
                    frmDashWorkspace.Content = new Dash_Home();
                    break;

                case NavBtns.finance:
                    frmDashWorkspace.Content = new Dash_finance();
                    break;

                case NavBtns.inventory:
                    frmDashWorkspace.Content = new Dash_inventory();
                    break;

                case NavBtns.staff_members:
                    frmDashWorkspace.Content = new Dash_Staff();
                    break;
                default:
                    frmDashWorkspace.Content = new Dash_Home();
                    break;
            }
        }
        #endregion
    }
}
