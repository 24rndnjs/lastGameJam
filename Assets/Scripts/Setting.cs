using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject settingsPanel;
   public bool hanakonana = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            space();
        }
    }
    void space()
    {
        if (hanakonana == false)
        {
            settingsPanel.SetActive(true);
            hanakonana = true;
        }

        else
        {
            settingsPanel.SetActive(false);
            hanakonana = false;
        }
        
    }
}
