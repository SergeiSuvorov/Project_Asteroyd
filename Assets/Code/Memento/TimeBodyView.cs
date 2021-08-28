using System.Collections.Generic;
using UnityEngine;


namespace Memento
{
    public sealed class TimeBodyView: MonoBehaviour 
    {
        private float _recordTime = 5f;
        private List<PointInTime> _pointsInTime = new List<PointInTime>();
        private Rigidbody _rb;
        private bool _isRewinding;
        private GameObject _gameObject;
        public Rigidbody Rigidbody { get { return _rb; } }
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
       
    }
}
