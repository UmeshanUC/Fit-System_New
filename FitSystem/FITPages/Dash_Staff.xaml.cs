using FITSystem.Classes;
using FITSystem.Database;
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

namespace FITSystem.FITPages
{
    /// <summary>
    /// Interaction logic for Dash_Staff.xaml
    /// </summary>
    public partial class Dash_Staff : Page
    {
        public Dash_Staff()
        {
            InitializeComponent();
            cardMed.DataContext = CreateDetailCardCtx("Medical Officers");
            cardMembers.DataContext = CreateDetailCardCtx("Members");
            cardTrainers.DataContext = CreateDetailCardCtx("Trainers");
            cardOther.DataContext = CreateDetailCardCtx("Other Staff");
        }

        private DetailCardCtx CreateDetailCardCtx(string personType)
        {
            int WorkRoleID = Global.PersonTypeToWorkRoleID(personType);

            if (WorkRoleID == -1)
            {
                MessageBox.Show("Error in PersonType.");
                return null;
            }

            DetailCardCtx detailCardCtx;
            using (FitDb db = new FitDb())
            {
                var thisTypePersonList = db.PersonSet.Where(p => p.WorkRoleID == WorkRoleID);
                detailCardCtx = new DetailCardCtx()
                {
                    CardName = personType,
                    TotalEmployed = thisTypePersonList.Count(),
                    TodayOnWork = thisTypePersonList.Count(p=>p.TodayPresence),
                    Males = thisTypePersonList.Count(p=>p.Gender == "Male"),
                    Femails = thisTypePersonList.Count(p=>p.Gender == "Female"),
                    ManageType = personType,

                };
            }
            return detailCardCtx;
        }

        private void DetailCard_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
