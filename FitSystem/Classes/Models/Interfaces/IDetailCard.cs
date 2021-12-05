using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITSystem.Classes.Models
{
    interface IDetailCard
    {
        string CardName { get; set; }
        string Avatar { get; set; }
        int TotalEmployed { get; set; }
        int TodayOnWork { get; set; }
        int Males { get; set; }
        int Femails { get; set; }
        string ManageType { get; set; }
    }
}   
