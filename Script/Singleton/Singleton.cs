namespace Pattern
{
    using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected bool DontDestroy;
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = (T) FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
                return instance;
            }
        }
        public virtual void Awake()
        {
            if (DontDestroy && CheckInstance()) DontDestroyOnLoad(gameObject);
        }

        protected bool CheckInstance()
        {
            if (this == Instance) return true;

            Destroy(this);
            return false;
        }
    }
}