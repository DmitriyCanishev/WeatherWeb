using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public static class SceneManagerUtils
    {
        public static Task ToTask(this AsyncOperation asyncOperation)
        {
            var completionSource = new TaskCompletionSource<object>();
        
            void OnCompleted(AsyncOperation asyncOperation)
            {
                asyncOperation.completed -= OnCompleted;
                completionSource.SetResult(null);
            }
            asyncOperation.completed += OnCompleted;

            return completionSource.Task;
        }
    
        public static bool IsSceneOpened(string scene)
        {
            for (var i = SceneManager.sceneCount - 1; i >= 0; --i)
            {
                var openedScene = SceneManager.GetSceneAt(i);
                if (openedScene.name == scene)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
