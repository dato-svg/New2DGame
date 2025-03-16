using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseScripts.SceneGod
{
    public class GameStarter : MonoBehaviour
    {
            public void ChangeSce()
        {
            SceneManager.LoadScene(1);
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                ChangeSce();
            }
        }
    }
}
