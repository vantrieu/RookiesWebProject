using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Services.Interfaces;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailrepository _orderDetailRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailrepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(List<int> productIds)
        {
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            return Ok();
        }
    }
}
