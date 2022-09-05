using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using nameTag;

namespace CameraController
{
    public class Camerafollow : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
        [SerializeField] private GameObject Popup;
        public float Timelerp;
        public float Currentsize;
        public float Endsize;
        private Camera camera;
        public float EndTimelerp;
        private float duration;
        public bool IsStartFollow;
        private float defaulttimelerp;
        public Vector3 Defaultposi;
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
        private GameObject popup
        {
            set => Popup = value;
            get => Popup;
        }
        void Start()
        {
            Defaultposi = this.transform.localPosition;
            camera = GetComponent<Camera>();
            defaulttimelerp = Timelerp;
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
                GetPopup( out Transform child);
                Target=GameObject.FindGameObjectWithTag(NameTag.Player);
                var newposi = this.transform.position;
                duration = timelerp / endtimelrp; 
                camera.orthographicSize= Mathf.Lerp(Currentsize, Endsize, duration);
                newposi.x = Mathf.Lerp(newposi.x, target.transform.position.x, duration);
                newposi.y = Mathf.Lerp(newposi.y, target.transform.position.y+2, duration);
                newposi.z = transform.position.z;
                timelerp += Time.deltaTime;
                transform.position = newposi;
                child.transform.position = target.transform.position+new Vector3(0,2,0);
                child.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                if (timelerp> endtimelrp)
                {
                    timelerp = endtimelrp;
                }
            }
        }
        void GetPopup(out Transform child)
        {
            child = popup.transform.GetChild(0);
        }
        public void CameraDefault()
        {
            IsStartFollow = false;
            timelerp = defaulttimelerp;
            camera.orthographicSize = Currentsize;
            this.transform.localPosition = Defaultposi;
        }

    }

}