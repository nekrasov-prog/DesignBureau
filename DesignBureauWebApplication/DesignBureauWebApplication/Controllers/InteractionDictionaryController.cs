//using Microsoft.AspNetCore.Mvc;
//using DesignBureauWebApplication.Data;
//using DesignBureauWebApplication.Models;
//using DesignBureauWebApplication.Interfaces;

//namespace DesignBureauWebApplication.Controllers
//{
//    public class InteractionDictionaryController : Controller
//    {
//        private readonly IInteractionDictionaryRepository _interactionDictionaryRepository;

//        public InteractionDictionaryController(IInteractionDictionaryRepository interactionDictionaryRepository)
//        {
//            _interactionDictionaryRepository = interactionDictionaryRepository;
//        }

//        public async Task<IActionResult> Index()
//        {
//            IEnumerable<InteractionDictionary> interactionDictionaries = await _interactionDictionaryRepository.GetAll();
//            return View(interactionDictionaries);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            InteractionDictionary interactionDictionary = await _interactionDictionaryRepository.GetByIdAsync(id);
//            return View(interactionDictionary);
//        }
//    }
//}
