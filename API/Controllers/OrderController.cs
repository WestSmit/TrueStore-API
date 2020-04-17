using BLL.Services.Interfaces;
using BLL.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq;
using BLL.Models;
using System;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderModel model)
        {
            var createOrder = _orderService.CreateOrder(model);
            return Ok(createOrder);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public IActionResult GetUserOrders()
        {
            StringValues stringValues;
            HttpContext.Request.Headers.TryGetValue("userId", out stringValues);
            var userId = stringValues.First().ToString();
            return Ok(_orderService.GetUserOrders(userId));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{orderId}")]
        public IActionResult GetUserOrder(int orderId)
        {
            StringValues stringValues;
            HttpContext.Request.Headers.TryGetValue("userId", out stringValues);
            var userId = stringValues.First().ToString();
            return Ok(_orderService.GetUserOrder(orderId, userId));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult DeleteUserOrder([FromBody]int orderId)
        {
            StringValues stringValues;
            HttpContext.Request.Headers.TryGetValue("userId", out stringValues);
            var userId = stringValues.First().ToString();
            _orderService.DeleteUserOrder(orderId, userId);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult OrderPayment([FromBody] int orderId)
        {
            StringValues stringValues;
            HttpContext.Request.Headers.TryGetValue("userId", out stringValues);
            var userId = stringValues.First().ToString();
            BaseModel model = new BaseModel();
            try
            {
                _orderService.OrderPayment(orderId, userId);
                model.Successed = true;
            }
            catch(Exception e)
            {
                model.Successed = false;
                model.Message = e.Message;
            }
            
            return Ok(model);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult ConfirmDelivery([FromBody] int orderId)
        {
            StringValues stringValues;
            HttpContext.Request.Headers.TryGetValue("userId", out stringValues);
            var userId = stringValues.First().ToString();
            _orderService.ConfirmDelibery(orderId, userId);
            return Ok();
        }
    }
}
