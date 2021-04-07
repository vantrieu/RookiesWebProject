using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Services.Interfaces;
using Web.ShareModels;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailrepository _orderDetailRepository;
        private readonly IRateRepository _rateRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailrepository orderDetailRepository, IRateRepository rateRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _rateRepository = rateRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(List<int> productIds)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.Claims.ToList().ElementAt(5).Value;
            await _orderRepository.CreateAsync(productIds, userId);
            return StatusCode(201);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyOrder()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst("sub").Value;
            var results = await _orderRepository.GetMyOrder(userId);
            return Ok(results);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteOrderItem(int orderId, int productId)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst("sub").Value;
            var result = await _orderDetailRepository.DeleteAsync(orderId, productId);
            if (result == null)
                return NotFound();
            else
            {
                if(!await _orderDetailRepository.OrderDetailExistsAsync(orderId))
                {
                    await _orderRepository.DeleteMyOrder(orderId);
                    await _rateRepository.DeleteRatingAsync(productId, userId);
                }
                return Ok();
            }
        }
    }
}
