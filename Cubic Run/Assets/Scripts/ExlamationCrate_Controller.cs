using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExlamationCrate_Controller : MonoBehaviour
{
    public ParticleSystem explosioPefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosioPefab, transform.position, Quaternion.identity);
    }
}
