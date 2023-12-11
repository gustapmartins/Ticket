using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Interface;

namespace Ticket.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class FeatureToggleController: ControllerBase
{
    private readonly IFeatureToggleService _featureToggleService;

    public FeatureToggleController(IFeatureToggleService featureToggleService)
    {
        _featureToggleService = featureToggleService;
    }
}
