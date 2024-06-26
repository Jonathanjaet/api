using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        // Pruebas sin uso
        private static readonly List<Payment> Payments = new()
        {
            new Payment { Id = 1, Amount = 100, Currency = "USD", Status = "Pending" },
            new Payment { Id = 2, Amount = 200, Currency = "EUR", Status = "Approved" },
            new Payment { Id = 3, Amount = 300, Currency = "GBP", Status = "Declined" },
        };

        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> Get(int id)
        {
            var payment = Payments.FirstOrDefault(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpPost]
        public ActionResult<Payment> Post([FromBody] Payment payment)
        {
            // Validate the payment data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Process the payment
            payment.Id = Payments.Count + 1;
            payment.Status = "Pending";
            Payments.Add(payment);

            return CreatedAtAction(nameof(Get), new { id = payment.Id }, payment);
        } 
        
        //Implementacion
        private readonly DatabaseManager _databaseManager;

        /*public PaymentController(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }*/

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                List<Payment> products = await _databaseManager.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
