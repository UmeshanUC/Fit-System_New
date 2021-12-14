using FitSystem.Classes.Models;
using System;
using System.Windows;

namespace FitSystem.Classes
{
    public class DetailCardCtx : IDetailCard
    {
        public string CardName { get; set; }
        public string Avatar { get; set; }
        public int TotalEmployed { get; set; }
        public int TodayOnWork { get; set; }
        public int TotalOther { get; set; }
        public string ManageType{ get; set; }
        public int Males { get; set; }
        public int Femails { get; set; }
    }
}