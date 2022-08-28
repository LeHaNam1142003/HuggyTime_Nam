using System.Collections;
using System.Collections.Generic;
using Component;
using UnityEngine;
using DG.Tweening;
namespace Stick
{
    public class Setstick : MonoBehaviour,Istick
    {
        [SerializeField] private GameObject Stick;
        private float destinationx;
        private float time;
        [SerializeField, Range(1f, 10f)] private float StickMoveSpeed = 2.0f;
        void Start()
        {
            destinationx = -50.0f;
        }

        // Update is called once per frame
        public void MovveStick( )
        {
            time = Mathf.Abs(Mathf.Abs(destinationx) / (Mathf.Abs(StickMoveSpeed)));
            Stick.transform.DOLocalMoveX(destinationx, time);
        }
    }
}