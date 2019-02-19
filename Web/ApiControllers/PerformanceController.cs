using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using Web.ViewModels;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    /// <summary>
    /// API for managing operations with performances
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : Controller
    {
        private readonly IPerformanceService _performanceService;
        private readonly IMapper _mapper;

        public PerformanceController(IPerformanceService performanceService, IMapper mapper)
        {
            _performanceService = performanceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceViewModel>>> Get()
        {
            var performances = await _performanceService.GetAllAsync();
            var mappedPerformances = _mapper.Map<IEnumerable<Performance>, IEnumerable<PerformanceViewModel>>(performances);

            return mappedPerformances.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceViewModel>> GetById(int id)
        {
            var performance = await _performanceService.GetByIdAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            var mappedPerformance = _mapper.Map<Performance, PerformanceViewModel>(performance);

            return mappedPerformance;
        }

        [HttpGet("{id}/speeches")]
        public async Task<ActionResult<IEnumerable<SpeechViewModel>>> GetSpeeches(int id)
        {
            var speeches = await _performanceService.GetSpeechesAsync(id);

            if (speeches == null)
            {
                return NotFound();
            }

            var mappedSpeeches = _mapper.Map<IEnumerable<Speech>, IEnumerable<SpeechViewModel>>(speeches);

            return mappedSpeeches.ToList();
        }

        //[HttpPost]
        //public async Task<ActionResult<Performance>> Create(PerformanceViewModel performance)
        //{

        //}
    }
}
