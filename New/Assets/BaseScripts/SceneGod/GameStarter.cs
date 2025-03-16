using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseScripts.SceneGod
{
    public class GameStarter : MonoBehaviour
    {
        public bool CanChangeScene;
        public void Nakonecto()
        {
            if (CanChangeScene)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
