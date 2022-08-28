using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
namespace CameraController
{
    public class Camerafollow : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        public float Timelerp;
        public float Currentsize;
        public float Endsize;
        private Camera camerawin;
        public float EndTimelerp;
        private float duration;
        public bool IsStartFollow;
        private float endtimelrp
        {
            set => EndTimelerp = value;
            get => EndTimelerp;
        }
        private float timelerp
        {
            set => Timelerp = value;
            get => Timelerp;
        }
        private GameObject target
        {
            set => Target = value;
            get => Target;
        }
        void Start()
        {
            camerawin = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
        StartFollow();
        }
        void StartFollow()
        {
            if (IsStartFollow)
            {
                var newposi = this.transform.position;
                duration = timelerp / endtimelrp; camerawin.orthographicSize= Mathf.Lerp(Currentsize, Endsize, duration);
                newposi.x = Mathf.Lerp(newposi.x, target.transform.position.x, duration);
                newposi.y = Mathf.Lerp(newposi.y, target.transform.position.y+4, duration);
                newposi.z = transform.position.z;
                timelerp += Time.deltaTime;
                transform.position = newposi;
                if (timelerp> endtimelrp)
                {
                    timelerp = endtimelrp;
                }
            }
        }
    }

}