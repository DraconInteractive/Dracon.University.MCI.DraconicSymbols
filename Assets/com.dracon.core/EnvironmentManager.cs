using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UltimateXR.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dracon.Core
{
    public class EnvironmentManager : MonoBehaviour
    {
        public static EnvironmentManager Instance { get; private set; }

        public List<Environment> environments = new List<Environment>();

        Coroutine loadRoutine;

        public static bool Loading;

        [System.Serializable]
        public class Environment
        {
            public string name;
            public Transform playerSpawn;
        }

        public string activeEnvironment;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadEnvironment(string scene)
        {
            if (loadRoutine != null)
            {
                Debug.LogError("Still loading a different scene");
                return;
            }

            if (environments.FirstOrDefault(x => x.name == scene) == null)
            {
                if (scene == "uselastenv")
                {
                    if (activeEnvironment == "")
                    {
                        scene = "Env_Base";
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Debug.LogError("Invalid scene load: environment not found");
                    return;
                }
            }

            if (scene == activeEnvironment)
            {
                Debug.LogError("Environment already active");
                return;
            }

            loadRoutine = StartCoroutine(LoadEnvironment_Routine(scene));
        }

        IEnumerator LoadEnvironment_Routine(string scene)
        {
            Loading = true;
            GlobalFade.Instance.SetTarget(1);
            yield return new WaitForSeconds(GlobalFade.Duration);

            if (activeEnvironment != "")
            {
                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(activeEnvironment);
                while (!asyncUnload.isDone)
                {
                    yield return null;
                }
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Bootstrap"));
            activeEnvironment = scene;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
            Loading = false;
            yield return null;

            var env = environments.FirstOrDefault(x => x.name == activeEnvironment);
            if (env != null)
            {
                UxrManager.Instance.TeleportLocalAvatar(env.playerSpawn.position, env.playerSpawn.rotation);
            }
            GlobalFade.Instance.SetTarget(0);
            yield return new WaitForSeconds(GlobalFade.Duration);
            loadRoutine = null;
            yield break;
        }


    }
}

