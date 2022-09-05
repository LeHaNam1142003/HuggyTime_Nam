using System.Collections;
using System.Collections.Generic;
using Component;
using Pattern;
using UnityEngine;

namespace Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private int Target;
        private int getcurrenttarget;
        public int CurrentTarget
        {
            set => Target = value;
            get => Target;
        }
        void Start()
        {
        
        }
        // Update is called once per frame
        void Update()
        {
        }
        public void UpDateTarget()
        {
            CurrentTarget--;
            if (CurrentTarget<=0)
            {
                GameManager.GameManager.Instance.GameState = GameState.Winning;
            }
        }
    }
}
