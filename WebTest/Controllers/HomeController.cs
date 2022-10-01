using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebTest.Data;
using WebTest.Entity;
using WebTest.Models;
using WebTest.ViewModels;

namespace WebTest.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        public HomeController(ILogger<HomeController> logger,DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<HomeViewModel>>> Index()
        {
            List<HomeViewModel> viewModel = new List<HomeViewModel>();
            var data = await  _context.TrKasir.ToListAsync();
            if(data.Count > 0)
            {
                viewModel = data.Select(x => new HomeViewModel()
                {
                    Id = x.Id,
                    Code = x.Code,
                    TipeCostumer = x.TipeCostumer,
                    PointReward = x.PointReward,
                    Diskon = x.Diskon,
                    TotalBayar = x.TotalBayar,
                    TotalBelanja = x.TotalBelanja,
                    TsCrt = x.TsCrt
                }).OrderBy(x => x.Id).ToList();
            }
            return View(viewModel);
        }

        public async Task<ActionResult<HomeViewModel>> AddOrUpdate(int Id=0)
        {
            var data = _context.TrKasir.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            HomeViewModel viewModel = new HomeViewModel();
            if(data != null)
            {
                viewModel.Id = data.Id;
                viewModel.Code = data.Code;
                viewModel.TipeCostumer = data.TipeCostumer;
                viewModel.PointReward = data.PointReward;
                viewModel.Diskon = data.Diskon;
                viewModel.TotalBayar = data.TotalBayar;
                viewModel.TotalBelanja = data.TotalBelanja;
                
            }
            viewModel.GetJenis = (await GetJenis()).ToList();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate(IFormCollection formCollection)
        {
            HomeViewModel ViewModel = new HomeViewModel();
            ViewModel.Id = int.Parse(formCollection["Id"]);
            ViewModel.Code = formCollection["Code"];
            ViewModel.TipeCostumer = formCollection["TipeCostumer"];
            ViewModel.PointReward = int.Parse(formCollection["PointReward"]);
            ViewModel.TotalBelanja = int.Parse(formCollection["TotalBelanja"]);
            ViewModel.Diskon = int.Parse(formCollection["Diskon"]);
            ViewModel.TotalBayar = int.Parse(formCollection["TotalBayar"]);


            var data = _context.TrKasir.Where(x => x.Id.Equals(ViewModel.Id)).FirstOrDefault();
            string code="";
            if(data == null)
            {
                data = new TrKasir();
                ViewModel.Code = generateCode();
            }
            else
            {
                data.Id = ViewModel.Id;
            }

            data.Code = ViewModel.Code;
            data.TipeCostumer = ViewModel.TipeCostumer;
            data.PointReward = ViewModel.PointReward;
            data.TotalBelanja = ViewModel.TotalBelanja;
            data.Diskon = ViewModel.Diskon;
            data.TotalBayar = ViewModel.TotalBayar;
            data.TsCrt = DateTime.Now;
            try
            {
                if (ViewModel.Id == 0)
                {
                    _context.TrKasir.AddAsync(data);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.TrKasir.Update(data);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                Error();
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _context.TrKasir.Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
            try
            {
                if (data != null)
                {
                    _context.TrKasir.Remove(data);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                Error();
            }
            
            return RedirectToAction("Index");
        }

        public string generateCode()
        {
            string data="";
            var newno = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd").Replace("-","");
            var check= _context.TrKasir.Where(x => x.Code.Substring(0,9).Contains(date))
            .OrderByDescending(x => x.TsCrt).FirstOrDefault();
            if(check != null)
            {
                var s = check.Code.Substring(check.Code.Length - 5, 5);
                var lastno = Convert.ToInt32(check.Code.Substring(check.Code.Length-5,5))+1;
                newno = string.Format("{0:00000}",lastno);
                data = date+"_"+newno;
            }
            else
            {
                newno = "00001";
                data = date + "_" + newno;
            }

            return data;
        }

        public async Task<IEnumerable<SelectListItem>> GetJenis()
        {
            var msJenis = await _context.MsJenis.ToListAsync();
            var item = msJenis.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.JenisName
            }).ToList();
            var first = new SelectListItem() { Value = null, Text = "Select Tipe" };
            item.Insert(0, first);
            return item;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}