using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using DAL.Contracts;
using Domines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;

namespace Ui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShippmentType _IGenericRepository;
        private readonly ICairrer _cairrer;
        private readonly ICity _city;
        private readonly ICountry _country;
        private readonly IPaymentMethod _aymentMethod;
        private readonly ISetting _setting;
        private readonly IShippmentStatus _shippmentStatus;
        private readonly IShippment _shippmnt;
       
    

        public HomeController(ILogger<HomeController> logger,IShippmentType generic, ICairrer cairrer
, ICity city , ICountry country , IPaymentMethod payment , ISetting setting , IShippmentStatus shippmentStatus
            ,IShippment shippment)
        {
            _logger = logger;
            _IGenericRepository = generic;
            _cairrer = cairrer;
            _city = city;
            _country = country;
            _aymentMethod = payment;
            _setting = setting;
            _shippmnt= shippment;
            
        }


        void ShipmientTest()
        {
            var testShipmentDto = new ShippmentDTOs
            {
                Width = 20.0,
                Height = 10.0,
                Weight = 5.0,
                Length = 30.0,
                PackageValue = 250.00m,
                ShippingDate = DateTime.UtcNow,
                DelivryDate = DateTime.UtcNow.AddDays(3),
                ShippingTypeId = Guid.Parse("459afb92-3374-4b02-ac67-086590009462"),
                PaymentMethodId = Guid.Parse("43a97c02-646b-4393-ad18-02c129399527"),
                SenderId = Guid.Empty,     // ÂÌ „ ≈‰‘«ƒÂ  ·ﬁ«∆Ì« œ«Œ· «·„ÌÀÊœ
                ReceiverId = Guid.Empty,   // ÂÌ „ ≈‰‘«ƒÂ  ·ﬁ«∆Ì« œ«Œ· «·„ÌÀÊœ

                UserSender = new UserSenderDTOs
                {
                    UserId = Guid.Empty,
                    SenderName = "”«„Õ",
                    Email = "ahmed.sender@example.com",
                    Phone = "01012345678",
                    PostalCode = "12345",
                    Contact = "Ahmed Contact",
                    OtherAddress = "⁄‰Ê«‰ ¬Œ— ··„—”·",
                    CityId = Guid.Parse("b2401f26-8342-418a-b4d5-1624886d7e5c"),
                    Address = "‘«—⁄ «· Õ—Ì— - «·ÃÌ“…",
                    IsDefalte = true
                },

                UserReceiver = new UserReceiverDtos
                {
                    UserId = Guid.Empty,
                    ReceiverName = "”«„Õ ",
                    Email = "mona.receiver@example.com",
                    Phone = "01198765432",
                    PostalCode = "54321",
                    Contact = "Mona Contact",
                    OtherAddress = "⁄‰Ê«‰ ¬Œ— ··„” ·„",
                    CityId = Guid.Parse("b2401f26-8342-418a-b4d5-1624886d7e5c"),
                    Address = "‘«—⁄ «·‰’— - «·ﬁ«Â—…"
                }

            };





            _shippmnt.Create(testShipmentDto);


        }


        public IActionResult Index()
        {

            ShipmientTest();
            var data= _shippmnt.GetAll();
            
            return View();

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
