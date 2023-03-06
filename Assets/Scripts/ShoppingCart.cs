using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace XoXCase
{
    public class ShoppingCart : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ingredientsText;
        [SerializeField] private TMP_Text _totalCostText;
        private List<ShopItem> _items = new();

        public string IngredientsText => _ingredientsText.text;
        public string TotalCostText => _totalCostText.text;

        private float _totalCost;
        
        // Method for adding items to cart
        public void AddItem(ShopItem item)
        {
            item.transform.SetParent(transform);
            item.transform.DOLocalJump(_items.Count * 0.1f * Vector3.right,
                1f, 1, 0.5f);
            _items.Add(item);
            UpdateTexts();
        }

        // Method for updating UI texts for the cart ingredients
        private void UpdateTexts()
        {
            _ingredientsText.text = "Ingredients: \n";
            foreach (var item in _items)
            {
                _ingredientsText.text += $"{item.Name}  $ {item.Price:#.00}\n";
                _totalCost += item.Price;
            }
            _totalCostText.text = $"Cost: $ {_totalCost:#.00}";
        }

        //Method for emptying the shopping cart and sending items back
        public void Clear()
        {
            foreach (var item in _items)
            {
                item.Return();
            }
            _items.Clear();
            UpdateTexts();
        }
    }
}

