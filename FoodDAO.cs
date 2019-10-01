using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodWeb
{
    public class FoodDAO
    {
        public List<Food> GetAllFoods()
        {
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                return foodEntites.Foods.ToList();
            }
        }

        public Food GetFoodById(int id)
        {
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                return foodEntites.Foods.FirstOrDefault(f => f.ID == id);
            }
        }

        public void AddFood(Food food)
        {
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                foodEntites.Foods.Add(food);
                foodEntites.SaveChanges();
            }

        }

        public Food UpdateFood(int id, Food food)
        {
            Food foodForUpdate = new Food();
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                foodForUpdate = foodEntites.Foods.FirstOrDefault(f => f.ID == id);
                foodForUpdate.Name = food.Name;
                foodForUpdate.Calories = food.Calories;
                foodForUpdate.Ingridients = food.Ingridients;
                foodForUpdate.Grade = food.Grade;
                foodEntites.SaveChanges();
            }
            return foodForUpdate;
        }

        public Food DeleteFood(int id)
        {
            Food Delete;
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                Delete = foodEntites.Foods.FirstOrDefault(f => f.ID == id);
                foodEntites.Foods.Remove(Delete);
                foodEntites.SaveChanges();
            }
            return Delete;
        }

        public List<Food> GetAllFoodsByName(string name)
        {
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                return foodEntites.Foods.Where(f => f.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }
        }

        public List<Food> GetAllFoodsByMinCalories(int Calories)
        {
            using (FoodDBEntities foodEntites = new FoodDBEntities())
            {
                return foodEntites.Foods.Where(f => f.Calories > Calories).ToList();
            }
        }

        public List<Food> GetByFilter(string name, int maxcal, int mincal, string ingridients, int grade)
        {
            using (FoodDBEntities foodDBEntities = new FoodDBEntities())
            {
                if (name == null && mincal.Equals(null) && maxcal.Equals(null) && ingridients == null && !grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Grade == grade).ToList();

                if (name == null && mincal.Equals(null) && maxcal.Equals(null) && !(ingridients.Equals(null)) && grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Ingridients.ToUpper().Contains(ingridients.ToUpper())).ToList();

                if (name == null && mincal > 0 && maxcal.Equals(null) && ingridients == null && grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Calories >= mincal).ToList();

                if (name == null && mincal.Equals(null) && maxcal > 0 && ingridients == null && grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Calories <= maxcal).ToList();

                if (!(name.Equals(null)) && mincal.Equals(null) && maxcal.Equals(null) && ingridients == null && grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Name.ToUpper().Contains(name.ToUpper())).ToList();

                if (name == null && mincal > 0 && maxcal > 0 && ingridients == null && grade.Equals(null))
                    return foodDBEntities.Foods.Where(m => m.Calories >= mincal && m.Calories <= maxcal).ToList();

                return foodDBEntities.Foods.Where(m => m.Name.ToUpper().Contains(name.ToUpper()) || (!(ingridients.Equals(null)) && m.Ingridients.ToUpper().Contains(ingridients.ToUpper())) || (!grade.Equals(null)) && m.Grade == grade || (mincal > 0 && maxcal.Equals(null) && m.Calories >= mincal) || (mincal.Equals(null) && maxcal > 0 && m.Calories <= maxcal) || (mincal > 0 && maxcal > 0 && m.Calories >= mincal && m.Calories <= maxcal)).ToList();

            }

        }
    }
}
