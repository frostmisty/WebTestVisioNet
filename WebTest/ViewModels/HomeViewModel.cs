using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTest.Entity;

namespace WebTest.ViewModels
{
    public class HomeViewModel
    {
        public int Id;
        public string? Code;
        public string? TipeCostumer;
        public int PointReward;
        public int Diskon;
        public int TotalBelanja;
        public int TotalBayar;

        public DateTime TsCrt;

        public List<SelectListItem> GetJenis;
    }
}