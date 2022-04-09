using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        private static T _instance;

        private static readonly object Lock = new object();

        [Header("Singleton Properties")]
        [SerializeField]
        private bool _persistent = true;

        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                    return null;
                }
                lock (Lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }
                    var instances = FindObjectsOfType<T>();
                    if (instances.Length > 0)
                    {
                        if (instances.Length > 1)
                        {
                            Debug.LogError($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene {SceneManager.GetActiveScene().name}, but {instances.Length} were found. The first instance found will be used, and all others will be destroyed.");
                            for (var i = 1; i < instances.Length; i++)
                            {
                                Destroy(instances[i]);
                            }
                        }

                        return _instance = instances[0];
                    }
                    else
                    {
                        Debug.LogError($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                        return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
                    }

                }
            }
        }

        private void Awake()
        {
            if (_persistent && Application.isPlaying)
                DontDestroyOnLoad(gameObject);
        }
    }

    public abstract class Singleton : MonoBehaviour
    {
        public static bool Quitting { get; private set; }

        private void OnApplicationQuit()
        {
            Quitting = true;
        }

    }