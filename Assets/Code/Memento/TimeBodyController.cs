using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    public class TimeBodyController 
    {
        private float _recordTime = 5f;
        private List<PointInTime> _pointsInTime = new List<PointInTime>();
        private Rigidbody _rb;
        private bool _isRewinding;
        private GameObject _gameObject;
       
        public TimeBodyController (TimeBodyView timeBodyView)
        {
            _rb = timeBodyView.Rigidbody;
            _gameObject = timeBodyView.gameObject;
        }

        public void CheckRewind()
        {
            if (_isRewinding)
            {
                Rewind();
            }
            else
            {
                Record();
            }
        }

        private void Rewind()
        {
            if (_pointsInTime.Count > 1)
            {
                PointInTime pointInTime = _pointsInTime[0];
                _gameObject.transform.position = pointInTime.Position;
                _gameObject.transform.rotation = pointInTime.Rotation;
                _pointsInTime.RemoveAt(0);
            }
            else
            {
                PointInTime pointInTime = _pointsInTime[0];
                _gameObject.transform.position = pointInTime.Position;
                _gameObject.transform.rotation = pointInTime.Rotation;
                StopRewind();
            }
        }

        private void Record()
        {
            if (_pointsInTime.Count > Mathf.Round(_recordTime / Time.fixedDeltaTime))
            {
                _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
            }

            _pointsInTime.Insert(0, new PointInTime(_gameObject.transform.position, _gameObject.transform.rotation, _rb.velocity, _rb.angularVelocity));
        }

        public void StartRewind()
        {
            if (_isRewinding)
                return;

            _isRewinding = true;
            _rb.isKinematic = true;
        }

        public void StopRewind()
        {
            if (!_isRewinding)
                return;

            _isRewinding = false;
            _rb.isKinematic = false;
            _rb.velocity = _pointsInTime[0].Velocity;
            _rb.angularVelocity = _pointsInTime[0].AngularVelocity;
        }
    }
}

