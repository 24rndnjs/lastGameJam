using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Image : MonoBehaviour
{
    public Sprite newSprite;
    void Start()
    {

    }
    void Update()
    {
            GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}