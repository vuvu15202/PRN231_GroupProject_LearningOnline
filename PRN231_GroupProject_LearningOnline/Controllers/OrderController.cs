using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        DonationWebApp_v2Context _context = new DonationWebApp_v2Context();

        [Authorize(RoleEnum.Admin)]
        [HttpGet()]
        public IActionResult GetAllOrders()
        {
            var order = _context.Orders.ToList();
            return Ok(order);
        }

        //[Authorize(RoleEnum.Admin)]
        //[HttpGet("order/{id:string}")]
        //public IActionResult GetOrderById(string id)
        //{
        //    var order = _context.Orders.SingleOrDefault(o => o.OrderId.Equals(id));
        //    return Ok(order);
        //}


        [Authorize(RoleEnum.Admin)]
        [HttpGet("orderofproject/{id:int}")]
        public IActionResult GetAllOrdersOfAProject(int id)
        {
            var orders = _context.Orders.Where(o => o.ProjectId == id).ToList();
            return Ok(orders);
        }


    }

    
}
