using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(int sceneIndex);
    }
    
    public class SceneLoader: MonoBehaviour, ISceneLoader
    {
        public async UniTask LoadSceneAsync(int sceneIndex)
        {
            Debug.LogWarning("Load1");
            await SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            Debug.LogWarning("Load2");
        }
    }
}