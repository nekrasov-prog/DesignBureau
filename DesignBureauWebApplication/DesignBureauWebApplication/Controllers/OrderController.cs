using Microsoft.AspNetCore.Mvc;
using DesignBureauWebApplication.Data;
using DesignBureauWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using DesignBureauWebApplication.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesignBureauWebApplication.Repository;
using DesignBureauWebApplication.ViewModels;
using System.ComponentModel.DataAnnotations;
using DesignBureauWebApplication.Data.Enum;

namespace DesignBureauWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IInventoryTransportationRepository _inventoryTransportationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ITransportationRepository _transportationRepository;
        private readonly ITransportationHistoryRepository _transportationHistoryRepository;

        public OrderController(IOrderRepository orderRepository, IOrderHistoryRepository orderHistoryRepository,
            ISupplierRepository supplierRepository, IInventoryRepository inventoryRepository,
            IMaterialRepository materialRepository, ILocationRepository locationRepository,
            IInventoryTransportationRepository inventoryTransportationRepository,
            ITransportationRepository transportationRepository, ITransportationHistoryRepository transportationHistoryRepository)
        {
            _orderRepository = orderRepository;
            _orderHistoryRepository = orderHistoryRepository;
            _supplierRepository = supplierRepository;
            _inventoryRepository = inventoryRepository;
            _materialRepository = materialRepository;
            _inventoryTransportationRepository = inventoryTransportationRepository;
            _locationRepository = locationRepository;
            _transportationRepository = transportationRepository;
            _transportationHistoryRepository = transportationHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orderList = await _orderRepository.GetAllPlusData();
            var ordersViewModel = new List<DetailsOrderViewModel>();
            foreach (var order in orderList)
            {
                ordersViewModel.Add(new DetailsOrderViewModel
                {
                    OrderId = order.OrderId,
                    ContractNumber = order.ContractNumber,
                    SupplierId = order.SupplierId,
                    Supplier = order.Supplier,
                    LastOrderStatus = await _orderHistoryRepository.GetLastOrderStatus(order.OrderId),
                    OrderHistories = order.OrderHistories.OrderByDescending(oh => oh.CreatedAt).ToList()
                });
            }

            return View(ordersViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Order order = await _orderRepository.GetByIdPlusDataAsync(id);
            if (order == null)
            {
                return View("Error");
            }

            var lastOrderStatus = await _orderHistoryRepository.GetLastOrderStatus(id);

            var orderVM = new DetailsOrderViewModel
            {
                OrderId = order.OrderId,
                ContractNumber = order.ContractNumber,
                SupplierId = order.SupplierId,
                Supplier = order.Supplier,
                OrderHistories = order.OrderHistories,
                OrderItems = order.OrderItems,
                LastOrderStatus = lastOrderStatus
            };

            return View(orderVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var suppliers = await _orderRepository.GetAllSuppliers();

            var orderVM = new OrderViewModel
            {
                SupplierList = new SelectList(suppliers, "SupplierId", "SName")
            };
            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    ContractNumber = orderVM.ContractNumber,
                    SupplierId = orderVM.SupplierId
                };
                _orderRepository.Add(order);

                var orderHistory = new OrderHistory
                {
                    OrderId = order.OrderId,
                    OrderStatus = OrderStatus.Created,
                    CreatedAt = DateTime.Now
                };
                _orderHistoryRepository.Add(orderHistory);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }
            orderVM.SupplierList = new SelectList(await _orderRepository.GetAllSuppliers(), "SupplierId", "SName");
            return View(orderVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return View("Error");
            }
            var orderVM = new OrderViewModel
            {
                OrderId = order.OrderId,
                ContractNumber = order.ContractNumber,
                SupplierId = order.SupplierId,
                //SName = order.Supplier.SName,
                SupplierList = new SelectList(await _orderRepository.GetAllSuppliers(), "SupplierId", "SName")
            };
            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Не удалось внести изменения");
                return View("Edit", orderVM);
            }
            var order = new Order
            {
                OrderId = id,
                ContractNumber = orderVM.ContractNumber,
                SupplierId = orderVM.SupplierId
            };

            _orderRepository.Update(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateTransportationForOrder(int orderId)
        {
            var locations = await _locationRepository.GetAll();
            var locationList = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.City + ", " + l.Street + ", " + l.HouseNumber
            }).ToList();

            var order = await _orderRepository.GetByIdPlusDataAsync(orderId);
            var orderItems = order.OrderItems;
            var inventories = orderItems
                .Select(i => i.Inventory)
                .Where(i => !i.InventoryTransportations.Any())
                .ToList();

            var inventorySelection = new Dictionary<int, bool>();
            var quantities = new Dictionary<int, int>();

            foreach (var inventory in inventories)
            {
                inventorySelection[inventory.InventoryId] = false;
                if (inventory.EquipmentId != null)
                {
                    quantities[inventory.InventoryId] = 1;
                }
                else
                {
                    quantities[inventory.InventoryId] = inventory.Material.Quantity;
                }
            }

            var transportationForOrderVM = new TransportationForOrderViewModel
            {
                DestinationList = new SelectList(locationList, "Value", "Text"),
                Inventories = inventories,
                InventorySelection = inventorySelection,
                Quantities = quantities,
                OrderId = orderId
            };

            return View(transportationForOrderVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransportationForOrder(TransportationForOrderViewModel transportationForOrderVM)
        {
            if (ModelState.IsValid)
            {
                var transportation = new Transportation
                {
                    PlanArrivalDateTime = transportationForOrderVM.PlanArrivalDateTime,
                    DestinationId = transportationForOrderVM.DestinationId
                };
                _transportationRepository.Add(transportation);

                var transportationHistory = new TransportationHistory
                {
                    TransportationId = transportation.TransportationId,
                    TransportationStatus = TransportationStatus.Created,
                    CreatedAt = DateTime.Now
                };
                _transportationHistoryRepository.Add(transportationHistory);

                foreach (var inventorySelection in transportationForOrderVM.InventorySelection)
                {
                    if (inventorySelection.Value)
                    {
                        var inventoryId = inventorySelection.Key;
                        var quantity = transportationForOrderVM.Quantities[inventoryId];
                        var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);
                        if (inventory.MaterialId != null)
                        {
                            inventory.Material.Quantity -= quantity;
                            _materialRepository.Update(inventory.Material);
                        }

                        var inventoryTransportation = new InventoryTransportation
                        {
                            InventoryId = inventoryId,
                            TransportationId = transportation.TransportationId,
                            Quantity = quantity
                        };
                        _inventoryTransportationRepository.Add(inventoryTransportation);
                    }
                }

                return RedirectToAction("Details", "Order", new { id = transportationForOrderVM.OrderId });
            }
            else
            {
                ModelState.AddModelError("", "Проверьте правильность введенных данных");
            }

            return View(transportationForOrderVM);
        }
    }
}
