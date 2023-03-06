using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XoXCase
{
    public class BookItem : ShopItem
    {
        [SerializeField] private string _title;
        [SerializeField] private string _author;
        [SerializeField] private DateTime _releaseDate = new(2022, 11, 1);
        [SerializeField] private string _blurb;

        public string Title => _title;
        public string Author => _author;
        public DateTime ReleaseDate => _releaseDate;
        public string Blurb => _blurb;
    }

}
