using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace XoXCase
{
    public class LevelBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private ShoppingCart _shoppingCart;
        
        private bool _started;
        private bool _finished;
        private Camera _camera;
        private ShopItem _selectedItem;
        private Vector3 _offset;
        
        private LayerMask _bgMask;
        private LayerMask _itemMask;
        private LayerMask _playerMask;

        private void Awake()
        {
            _camera = Camera.main;
            //_player = GetComponentInChildren<PlayerBehaviour>();
            _bgMask = LayerMask.GetMask("Background");
            _itemMask = LayerMask.GetMask("ShopItem");
            _playerMask = LayerMask.GetMask("Player");
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Update()
        {
            if (!_started || _finished)
                return;
            
        }

        private void Subscribe()
        {
            InputController.Pressed += OnPressed;
            InputController.Moved += OnMoved;
            InputController.Released += OnReleased;
            
            _player.ItemTaken += OnItemTaken;
        }

        private void Unsubscribe()
        {
            InputController.Pressed -= OnPressed;
            InputController.Moved -= OnMoved;
            InputController.Released -= OnReleased;
            
            _player.ItemTaken -= OnItemTaken;
        }
        
        //Function to call when loading the level
        public void Load()
        {
            _started = true;
            _finished = false;
            
            Subscribe();
        }

        //MouseDown event function
        private void OnPressed(Vector3 pos)
        {
            if (!_started || _finished || !_player)
                return;
            
            if (_player.CurrentItem)
                return;

            var ray = _camera.ScreenPointToRay(pos);
            if (!Physics.Raycast(ray, out var hit, _itemMask)
                || !hit.collider.TryGetComponent<ShopItem>(out var item))
                return;

            // select item and determine its offset from the mouse pos;
            _selectedItem = item;
            _offset = item.transform.position - hit.point;
        }
        
        //MouseMove event function
        private void OnMoved(Vector3 pos)
        {
            if (!_selectedItem)
                return;
            
            var ray = _camera.ScreenPointToRay(pos);
            if (!Physics.Raycast(ray, out var hit, _bgMask))
                return;

            
            // move item over the background plane;
            _selectedItem.transform.position = hit.point + _offset;
        }
        
        //MouseUp event function
        private void OnReleased(Vector3 pos)
        {
            if (!_selectedItem)
                return;

            //cast ray to determine drop
            var ray = _camera.ScreenPointToRay(pos);
            if (!Physics.Raycast(ray, out var hit, _playerMask)
                || !hit.collider.CompareTag("Player"))
            {
                _selectedItem.Return();
                _selectedItem = null;
                return;
            }
            
            //give the player the item if released over the player plane
            _selectedItem.Return();
            _player.TakeItem(_selectedItem);
            _selectedItem = null;
        }
        
        private void OnItemTaken(ShopItem item)
        {
            _shoppingCart.AddItem(item);
        }
    }
}
