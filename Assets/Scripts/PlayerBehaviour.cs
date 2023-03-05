using System;
using DG.Tweening;
using UnityEngine;

namespace XoXCase
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public event Action<ShopItem> ItemTaken;
        
        [SerializeField] private Transform _takeItemPoint;
        [SerializeField] private Transform _handPoint;
        [SerializeField] private Transform _releaseItemPoint;
        private Sequence _itemSequence;
        private ShopItem _currentItem;
        private Vector3 _currentPos;

        public bool CurrentItem => _currentItem;
        public bool Moving { get; private set; }

        void Start()
        {
            _currentPos = transform.position;
        }

        //This method is called whenever an item is dragged onto the player
        public void TakeItem(ShopItem selectedItem)
        {
            if (_currentItem)
                return;
            
            //DoTween sequence setup to pick up the item
            Moving = true;
            _currentItem = selectedItem;
            _itemSequence = DOTween.Sequence();
            _itemSequence.Append(transform.DOMove(_takeItemPoint.position, 1f)
                .OnComplete(() => _currentItem.transform.SetParent(_handPoint)));
            _itemSequence.Append(_currentItem.transform.DOMove(_handPoint.position, 0.5f));
            _itemSequence.Append(transform.DOMove(_releaseItemPoint.position, 1.5f));
            _itemSequence.OnComplete(OnTakeItemComplete);
            _itemSequence.Play();

        }

        //TakeItem sequence complete callback
        private void OnTakeItemComplete()
        {
            _currentItem.transform.SetParent(transform.parent);
            transform.DOMove(_currentPos, 1f)
                .OnComplete(() => Moving = false);
            ItemTaken?.Invoke(_currentItem);
            _currentItem = null;
        }
    }
}