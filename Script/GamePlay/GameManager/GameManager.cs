using System.Collections;
using System.Collections.Generic;
using CameraController;
using Pattern;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GameManager
{
    public class GameManager : Singleton<GameManager>
    {
        public bool isfollow;

        [SerializeField, Range(1, 10)] float Timetocamerafollow;
        [SerializeField] private Camerafollow CameraFollow;
        public float Gettimetocamerafollow
        {
            set => Timetocamerafollow = value;
            get => Timetocamerafollow;
        }
       // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
         public  void StartFollow()
         { 
             CameraFollow.IsStartFollow = true;
         }
    }
}
