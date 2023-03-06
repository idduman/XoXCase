using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace XoXCase
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private List<LevelBehaviour> _levels = new();
        [SerializeField] private string _receiptPath;

        private LevelBehaviour _previousLevel;
        private LevelBehaviour _currentLevel;
        private int _playerLevel;

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

        public void PrintReceipt(string receipt)
        {
            StreamWriter writer = new StreamWriter(_receiptPath, false);
            writer.Write(receipt);
            writer.Close();
        }
    }
}
