using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XoXCase
{
    public enum FoodType
    {
        FreshProduce,
        Dairy,
        Meat,
        Bakery,
        Beverages,
        Snacks,
        Sweets
    }
    public class FoodItem : ShopItem
    {
        [SerializeField] private FoodType _foodType;
        [SerializeField] private float _weight;
        [SerializeField] private DateTime _bestBefore = new(2023, 8, 11);

        public FoodType FoodType => _foodType;
        public float Weight => _weight;
        public DateTime BestBefore => _bestBefore;
    }

}
