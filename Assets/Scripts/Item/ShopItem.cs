using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace XoXCase
{
    ///<summary>
    /// Base class for shop items
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class ShopItem : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private float _price;

        public float Price => _price;
        public string Name => _name;
        private Vector3 _startingPos;
        
        public bool Returning { get; private set; }

        private void Awake()
        {
            _startingPos = transform.position;
        }

        //Method for the return of the item to the original position on MouseUp
        public void Return()
        {
            if (Returning)
                return;
            
            Returning = true;
            transform.DOMove(_startingPos, 0.2f)
                .OnComplete(() => Returning = false);
        }
    }

}
