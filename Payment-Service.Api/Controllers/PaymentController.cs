using Microsoft.AspNetCore.Mvc;
using Payment_Service.Application.Services.Contracts;
using Payment_Service.Core.Entities;

namespace Payment_Service.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Payment payment)
        {
            if (payment is null)
                return BadRequest();

            await _paymentService.Add(payment);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetCustomerById(int id)
        {
            var payment = await _paymentService.Get(id);

            if (payment is null)
                return NotFound();

            return Ok(payment);

        }
    }
}
