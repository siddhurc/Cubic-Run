using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupActions : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }
}