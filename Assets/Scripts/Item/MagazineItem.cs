using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XoXCase
{
    public class MagazineItem : ShopItem
    {
        [SerializeField] private string _title;
        [SerializeField] private int _issue;
        [SerializeField] private string _publisher;

        public string Title => _title;
        public int Issue => _issue;
        public string Publisher =>  _publisher;
    }

}
