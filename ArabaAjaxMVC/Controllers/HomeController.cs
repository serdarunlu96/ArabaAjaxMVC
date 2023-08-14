using ArabaAjaxMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArabaAjaxMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static List<Araba> arabalar = new List<Araba>
        {
            new Araba{ Id = 1, Marka = "Audi" , Model = "A3", Renk = "Beyaz", ResimUrl = "https://imganuncios.mitula.net/medium/audi_a3_2002_galeriden_audi_a3_sportback_1_6_ambiente_2002_model_i_zmir_369_000_km_beyaz_7450112678027669771.jpg"},
            new Araba{ Id = 2, Marka = "Peugeot", Model = "206", Renk = "Siyah" , ResimUrl = "https://arbstorage.mncdn.com/ilanfotograflari/2023/07/12/23013785/16cea521-42ce-4665-9a68-1629b0751edc_image_for_silan_23013785_1920x1080.jpg"},
            new Araba{ Id = 3, Marka = "Hyundai",  Model = "i30", Renk = "Gri", ResimUrl = "https://arbstorage.mncdn.com/ilanfotograflari/2023/08/09/23215738/d195fb87-fe92-46dc-a3da-d239c3b10ac8_image_for_silan_23215738_580x435.jpg"},
            new Araba{ Id = 4, Marka = "Ford", Model = "Fiesta", Renk = "Beyaz", ResimUrl = "https://arbstorage.mncdn.com/ilanfotograflari/2023/05/20/22656909/2caade13-b80f-4df1-9305-92679951293c_image_for_silan_22656909_580x435.jpg"},
            new Araba{ Id = 5, Marka = "Volkswagen",  Model = "Golf", Renk = "Mavi", ResimUrl = "https://arbstorage.mncdn.com/ilanfotograflari/2023/07/03/22942737/1ec773c6-4c1a-4b88-b431-80f448d3b01b_image_for_silan_22942737_580x435.jpg"}
        };

        private static List<int> silinenIdListesi = new List<int>();
        private static int idCounter = arabalar.Count + 1; // Başlangıç ID'si

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ArabaSorgula(int id)
        {
            Araba araba = arabalar.FirstOrDefault(a => a.Id == id);

            if (araba != null)
            {
                return Json(araba);
            }
            else
            {
                return Json(new { errorMessage = "Araba bulunamadı." });
            }
        }

        [HttpPost]
        public IActionResult ArabaEkle([FromBody] Araba araba)
        {
            int yeniId;

            if (silinenIdListesi.Count > 0)
            {
                yeniId = silinenIdListesi.Min();
                silinenIdListesi.Remove(yeniId);
            }
            else
            {
                yeniId = idCounter++;
            }

            araba.Id = yeniId;
            arabalar.Add(araba);

            return Json(araba);
        }

        [HttpPost]
        public IActionResult ArabaSil(int id)
        {
            Araba araba = arabalar.FirstOrDefault(a => a.Id == id);

            if (araba != null)
            {
                arabalar.Remove(araba);
                silinenIdListesi.Add(id);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, errorMessage = "Araba bulunamadı." });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}