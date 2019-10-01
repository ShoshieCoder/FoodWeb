using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWeb.Controllers
{
    public class FoodController : ApiController
    {
            
        public FoodDAO f = new FoodDAO();
        public HttpResponseMessage Get()
        {
            List<Food> foods = f.GetAllFoods();
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foods);
            return msg;
        }

        public HttpResponseMessage Get(int id)
        {
            Food food = f.GetFoodById(id);
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        public HttpResponseMessage Post([FromBody]Food value)
        {
            f.AddFood(value);
            return Request.CreateResponse(HttpStatusCode.Created, value);
        }

        public HttpResponseMessage Put(int id, [FromBody]Food value)
        {
            Food food = f.UpdateFood(id, value);
            return Request.CreateResponse(HttpStatusCode.Accepted, food);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Food food = f.DeleteFood(id);
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        [Route("api/foods/byname/{name}")]
        [HttpGet]
        public HttpResponseMessage GetByName(string name)
        {
            List<Food> food = f.GetFoodsByName(name);
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        [Route("api/foods/bymincal/{mincalories}")]
        [HttpGet]
        public HttpResponseMessage GetByMinCalories(int mincalories)
        {
            List<Food> foods = f.GetFoodsByMinCalories(mincalories);
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        [Route("api/foods/search")]
        [HttpGet]
        public HttpResponseMessage GetBySearch(string name, int maxcal, int mincal, string ingridients, int grade)
        {
            List<Food> foods = f.GetBySearch(name, mincal, maxcal, ingridients, grade);
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }
    }
}

