using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CameraController;
using Component;
using Level;
using Pattern;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SocialPlatforms;
using Debug = UnityEngine.Debug;

namespace GameManager
{
    public class GameManager : Singleton<GameManager>
    {
        public bool isfollow;

        [SerializeField, Range(1, 10)] float Timetocamerafollow;
        [SerializeField] private Camerafollow CameraFollow;
        [SerializeField] private GameObject CurrentLevel;
        [SerializeField] private GameUI GameUI;
        public GameState GameState;
        private GameState previousgamestate;
        private GameObject currentlevel { get; set; }
        public float Gettimetocamerafollow
        {
            set => Timetocamerafollow = value;
            get => Timetocamerafollow;
        }
        // Start is called before the first frame update
       void Start()
       {
          LoadLevel(LevelData.Currentlevel);
       }
       public void LoadLevel(int index)
       {
           GameState = GameState.Loading;
           Addressables.LoadAssetAsync<GameObject>(LevelData.Levelname+index).Completed += ShowLevel;
       }
       public void ReLoadLevel(int index)
       {
           GameState = GameState.Reloading;
           Addressables.LoadAssetAsync<GameObject>(LevelData.Levelname+index).Completed += ShowLevel;
       }
       void ShowLevel(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
       {
           currentlevel = obj.Result;
           if (CurrentLevel.transform.childCount==0)
           {
               Instantiate(currentlevel, CurrentLevel.transform);
           }
           else
           {
               Destroy(CurrentLevel.transform.GetChild(0).gameObject);
               Instantiate(currentlevel, CurrentLevel.transform);
           }
           GameState = GameState.Playing;
           GameUI.SetLevelText(LevelData.Currentlevel);
       }
     // Update is called once per frame
        void Update()
        {
            if (previousgamestate!=GameState)
            {
                 UpdateStateGame();
            }
            previousgamestate = GameState;
        }
        void UpdateStateGame()
         {
             switch (GameState)
             {
                 case GameState.Winning :
                     GameWin();
                     return;
                 case GameState.Losing:
                     GameLost();
                     return;
                 
             }
         }
        void GameWin()
         {
             GamePopupManager.Instance.SetGameWin();
             CameraFollow.IsStartFollow = true;
         }

        void GameLost()
        {
            GamePopupManager.Instance.SetGameLost();
            CameraFollow.IsStartFollow = true;
        }
        public void GameDefaultState()
        {
            CameraFollow.CameraDefault();
        }
    }
}
