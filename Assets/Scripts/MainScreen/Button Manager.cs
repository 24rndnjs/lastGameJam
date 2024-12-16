using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
    
    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

}
