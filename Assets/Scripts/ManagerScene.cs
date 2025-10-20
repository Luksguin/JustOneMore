using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void ChangeScene(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
