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

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1Select");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Stage2Select");
    }
    public void Stage3()
    {
        SceneManager.LoadScene("Stage3Select");
    }

}
