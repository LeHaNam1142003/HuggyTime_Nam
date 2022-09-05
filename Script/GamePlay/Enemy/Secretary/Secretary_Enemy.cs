using System;
using System.Collections;
using System.Collections.Generic;
using Component;
using Level;
using UnityEngine;
using Player;
using nameTag;
namespace Enemy
{
    public class Secretary_Enemy : EnemyBase
    {
      
        private Rigidbody2D enemyrb;
        public State SecretaryState;
        private BoxCollider2D boxcollider2D;
        void Start()
        {
            enemyrb = GetComponent<Rigidbody2D>();
            boxcollider2D = GetComponent<BoxCollider2D>();
        }
        void Update()
        {
         UpdateEnemy();
         Afraid();
         Die();
        }
        void Die()
        {
            if (Isdead!=false&& Isafraid!=true)
            {
                SecretaryState = State.Die;
                boxcollider2D.enabled = false;
            }
        }
        public void Afraid()
        {
             if (Isafraid!=false&& Isdead!=true)
             {
                 SecretaryState = State.Afraid;
             }
        }
        void UpdateEnemy()
        {
            if (Issee!=false)
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