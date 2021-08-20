using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Chain_of_Responsibility
{
    public class EnemyManagerModifire 
    {
        protected EnemyManagerModifire Next;
        protected EnemyManager _enemy;

        public EnemyManagerModifire(EnemyManager enemy)
        {
            _enemy = enemy;
        }

        public void Add(EnemyManagerModifire cm)
        {
            if (Next != null)
            {
                Next.Add(cm);
            }
            else
            {
                Next = cm;
            }
        }

        public virtual void Handle() => Next?.Handle();
    }

}



