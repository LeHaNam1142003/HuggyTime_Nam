using System;
using System.Collections;
using System.Collections.Generic;
using Component;
using UnityEngine;
using Player;
using nameTag;
namespace Enemy
{
    public class Secretary_Enemy : MonoBehaviour
    {
        public EnemyController EnemyController;
        private Rigidbody2D enemyrb;
        public State SecretaryState;
        void Start()
        {
            enemyrb = GetComponent<Rigidbody2D>();
            EnemyController = GetComponent<EnemyController>();
        }
        void Update()
        {
         UpdateEnemy();
         Afraid();
         Die();
        }
        void Die()
        {
            if (EnemyController.Isdead!=false&& EnemyController.Isafraid!=true)
            {
                SecretaryState = State.Die;
            }
        }
        public void Afraid()
        {
             if (EnemyController.Isafraid!=false&& EnemyController.Isdead!=true)
             {
                 SecretaryState = State.Afraid;
             }
        }
        void UpdateEnemy()
        {
            if (EnemyController.Issee!=false)
            {
                GameObject findplayer=GameObject.FindGameObjectWithTag(NameTag.Player);
                Flip(findplayer.transform.position.x);
            }
        }
        private void Flip( float player)
        {
            var setflip = this.transform.eulerAngles.y ==180 ?true :false;
            if (setflip!=false)
            {
                if (this.transform.position.x>player)
                {
                    return;
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x , this.transform.eulerAngles.y-180, this.transform.eulerAngles.z);

                }
            }
            else if (setflip!=true)
            {
                if (this.transform.position.x<player)
                {
                    return;
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x , this.transform.eulerAngles.y+180, this.transform.eulerAngles.z);
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(NameTag.Ground))
            {
                enemyrb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }
 
}