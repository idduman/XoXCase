using System;
using System.Collections;
using System.Collections.Generic;
using XoXCase;
using UnityEngine;

namespace XoXCase
{
    public class UIController : SingletonBehaviour<UIController>
    {
        [SerializeField] private RectTransform _checkoutButton;

        private void Awake()
        {
            _checkoutButton.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _checkoutButton.gameObject.SetActive(true);
        }

        public void Checkout()
        {
            GameManager.Instance.Checkout();
        }
    }

}
