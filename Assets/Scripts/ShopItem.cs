using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace XoXCase
{
    [RequireComponent(typeof(Collider))]
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private float _price;

        public float Price => _price;
        private Vector3 _startingPos;
        
        public bool Returning { get; private set; }

        private void Awake()
        {
            _startingPos = transform.position;
        }

        public void Return()
        {
            if (Returning)
                return;
            
            Returning = true;
            transform.DOMove(_startingPos, 0.5f)
                .OnComplete(() => Returning = false);
        }
    }

}
