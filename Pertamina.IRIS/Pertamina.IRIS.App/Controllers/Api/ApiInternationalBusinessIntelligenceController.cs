using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Services.Interfaces;
using System;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiInternationalBusinessIntelligenceController : BaseController
    {
        private readonly IInternationalBusinessIntellegenceService _service;

        public ApiInternationalBusinessIntelligenceController(IIdamanService idamanService, IInternationalBusinessIntellegenceService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}
