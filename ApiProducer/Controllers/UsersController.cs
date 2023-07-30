using ApiProducer.Models.Dtos;
using ApiProducer.Services.Interfaces;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProducer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IProducerService producerService;
    private const string topic = "USER_REGISTRATION";
    public UsersController(IProducerService producerService)
    {
        this.producerService = producerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRegistrationDto userDto)
    {
        // Your user registration logic here...
        // Assuming the user creation is successful, proceed to publish the event

        // Example user creation event
        string userCreationEvent = $"New user created: {userDto.Email}";

        // Publish the event to Kafka
        await producerService.SetTopic(topic);
        await producerService.Send(userCreationEvent);

        return Ok("User created successfully!");
    }
}
