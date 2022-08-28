using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Component;
using nameTag;
using UnityEngine;
using Player;
namespace Stick
{
    public  class StickMovement : MonoBehaviour
    {
        private PlayerController playercontroller;
        private Istick istick;
        private void Awake()
        {
            istick = GetComponentInParent<Istick>();
        }
        private void Start()
        {
            playercontroller = GameObject.Find(NameTag.Player).GetComponent<PlayerController>();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(NameTag.StickTouch))
            {
                istick.MovveStick();
            }
        }
    }
}
