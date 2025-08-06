using Bogus;
using Microsoft.AspNetCore.Mvc;
using Millenium.REST.FakeData;

namespace Millenium.REST;

[ApiController]
[Route("fake")]
public class FakeController : ControllerBase
{
    private static int _callCount;

    [HttpGet]
    public IActionResult Get()
    {
        _callCount += 1;

        if (_callCount % 10 == 0)
            return BadRequest();

        return Ok(GenerateFakeDto());
    }

    private FakeDto GenerateFakeDto()
    {
        var faker = new Faker<FakeDto>()
            .RuleFor(dto => dto.Name, f => f.Name.FullName());
        return faker.Generate();
    }
}