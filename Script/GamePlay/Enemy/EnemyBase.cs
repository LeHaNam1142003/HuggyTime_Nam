using System;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase: MonoBehaviour
    {
        public bool Isdead;
        public bool Isafraid;
        public bool Issee;
        private void Start()
        {
           BoolStart();
        }
        private void BoolStart()
        {
            Isdead = false;
            Isafraid = false;
            Issee = false;
        }
        public void EnemyAfraid()
        {
            Isafraid = true;
            Isdead = false;
        }
        public void EnemyDie()
        {
            Isdead = true;
            Isafraid = false;
        }
        public void See() => Issee = true;
    }
}