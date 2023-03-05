using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace XoXCase
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private List<LevelBehaviour> _levels = new();

        private LevelBehaviour _previousLevel;
        private LevelBehaviour _currentLevel;
        private int _playerLevel;

        private int _totalCost;
        
        private void Start()
        {
            Load();
        }

        private void Load()
        {
            if (_levels.Count < 1)
            {
                Debug.LogError("No levels are present in GameManager");
                return;
            }

            _totalCost = 0;
            UIController.Instance.Initialize();
            StartCoroutine(LoadRoutine());
        }

        private IEnumerator LoadRoutine()
        {
            if (_currentLevel)
            {
                Destroy(_currentLevel);
                _currentLevel = null;
            }

            _currentLevel = Instantiate(_levels[_playerLevel % _levels.Count]);
            yield return new WaitForEndOfFrame();
            _currentLevel.Load();
        }

        public void Checkout()
        {
            throw new NotImplementedException();
        }
    }
}
