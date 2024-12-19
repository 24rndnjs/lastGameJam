using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject settingsPanel;

    public GameObject lico;
 
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
            lico.SetActive(false);
         
            hanakonana = true;

        }

        else
        {
            settingsPanel.SetActive(false);
            lico.SetActive(true);
        
            hanakonana = false;
        }
        
    }
}
