using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Image : MonoBehaviour
{
    public Sprite newSprite;
    public Laserblock l;
    void Start()
    {

    }
    void Update()
    {
        if (l.change == true)
        {
            GetComponent<SpriteRenderer>().sprite = newSprite;
            l.change = false;
        }
    }
}
