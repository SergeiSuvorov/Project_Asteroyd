using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    
    public class MementoGameController : MonoBehaviour
    {
       private InputMemento _input;
       private List<TimeBodyController> _timeBodys = new List<TimeBodyController>();

        private void Start()
        {
            _input = new InputMemento(this);
            var timeBodyViews = FindObjectsOfType<TimeBodyView>();

            for (int i = 0; i < timeBodyViews.Length; i++)
            {
                var timeBody = new TimeBodyController(timeBodyViews[i]);
                _timeBodys.Add(timeBody);
            }
                
        }

        private void Update()
        {
            _input.GetInput();
        }

        private void FixedUpdate()
        {
            for(int i=0; i< _timeBodys.Count;i++)
                _timeBodys[i].CheckRewind();
        }

        public void StartRewind()
        {
            for (int i = 0; i < _timeBodys.Count; i++)
                _timeBodys[i].StartRewind();
        }

        public void StopRewind()
        {
            for (int i = 0; i < _timeBodys.Count; i++)
                _timeBodys[i].StopRewind();
        }
    }
}
