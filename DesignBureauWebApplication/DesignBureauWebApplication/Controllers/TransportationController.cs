using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Repository;

namespace DesignBureauWebApplication.Controllers
{
    public class TransportationController : Controller
    {
        private readonly ITransportationRepository _transportationRepository;
        private readonly ITransportationHistoryRepository _transportationHistoryRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IInventoryTransportationRepository _inventoryTransportationRepository;
        private readonly IOrderRepository _orderRepository;

        public TransportationController(ITransportationRepository transportationRepository,
            ITransportationHistoryRepository transportationHistoryRepository,
            ILocationRepository locationRepository,
            IInventoryTransportationRepository inventoryTransportationRepository,
            IOrderRepository orderRepository)
        {
            _transportationRepository = transportationRepository;
            _transportationHistoryRepository = transportationHistoryRepository;
            _locationRepository = locationRepository;
            _inventoryTransportationRepository = inventoryTransportationRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var transportationList = await _transportationRepository.GetAll();
            var transportationVM = new List<DetailsTransportationViewModel>();

            foreach (var transportation in transportationList)
            {
                var order = new Order { };
                if (transportation.OriginId == null)
                {
                    var orderId = await _transportationRepository.GetOrderByTransportation(transportation.TransportationId);
                    order = await _orderRepository.GetByIdPlusDataAsync(orderId);
                }
                transportationVM.Add(new DetailsTransportationViewModel
                {
                    TransportationId = transportation.TransportationId,
                    PlanDepartureDateTime = transportation.PlanDepartureDateTime,
                    PlanArrivalDateTime = transportation.PlanArrivalDateTime,
                    OriginId = transportation.OriginId,
                    Origin = transportation.Origin,
                    DestinationId = transportation.DestinationId,
                    Destination = transportation.Destination,
                    LastTransportationStatus = await _transportationHistoryRepository.GetLastTransportationStatus(transportation.TransportationId),
                    Order = order
                });
            }

            return View(transportationVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            Transportation transportation = await _transportationRepository.GetByIdAsync(id);
            if (transportation == null)
            {
                return View("Error");
            }

            var order = new Order { };
            var lastTransportationStatus = await _transportationHistoryRepository.GetLastTransportationStatus(id);
            if (transportation.OriginId == null)
            {
                var orderId = await _transportationRepository.GetOrderByTransportation(transportation.TransportationId);
                order = await _orderRepository.GetByIdPlusDataAsync(orderId);
            }

            var transportationVM = new DetailsTransportationViewModel
            {
                TransportationId = transportation.TransportationId,
                PlanDepartureDateTime = transportation.PlanDepartureDateTime,
                PlanArrivalDateTime = transportation.PlanArrivalDateTime,
                OriginId = transportation.OriginId,
                Origin = transportation.Origin,
                DestinationId = transportation.DestinationId,
                Destination = transportation.Destination,
                LastTransportationStatus = lastTransportationStatus,
                TransportationHistories = transportation.TransportationHistories,
                InventoryTransportations = transportation.InventoryTransportations,
                Order = order
            };

            return View(transportationVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber
            }).ToList();

            var transportationVM = new TransportationViewModel
            {
                OriginList = new SelectList(locationList, "Value", "Text"),
                DestinationList = new SelectList(locationList, "Value", "Text")
            };

            return View(transportationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransportationViewModel transportationVM)
        {
            if (ModelState.IsValid)
            {
                var transportation = new Transportation
                {
                    PlanDepartureDateTime = transportationVM.PlanDepartureDateTime,
                    PlanArrivalDateTime = transportationVM.PlanArrivalDateTime,
                    OriginId = transportationVM.OriginId,
                    DestinationId = transportationVM.DestinationId
                };
                _transportationRepository.Add(transportation);

                var transportationHistory = new TransportationHistory
                {
                    TransportationId = transportation.TransportationId,
                    TransportationStatus = TransportationStatus.Created,
                    CreatedAt = DateTime.Now
                };
                _transportationHistoryRepository.Add(transportationHistory);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }

            return View(transportationVM);
        }
    }
}
