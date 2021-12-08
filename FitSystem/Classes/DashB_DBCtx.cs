using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FitSystem
{
    public partial class DashB
    {
        // DashB Data Context
        private class DashB_DataCtx : INotifyPropertyChanged
        {
            public Timer MainTimer { get; set; }
            public string TodayDate{ get; set; }
            public string _TimeNow;
            public string TimeNow { get { return _TimeNow; } set { _TimeNow = value; OnPropertyChanged(); } }

            public DashB_DataCtx()
            {
                TodayDate = DateTime.Now.Date.ToString("yyyy / MM / dd");
                MainTimer = new Timer(delegate { TimeNow = DateTime.Now.ToString("hh:mm:ss tt"); }, null, 0, 1000);
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
 