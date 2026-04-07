using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
    

public class Bomb : MonoBehaviour
{
    public GameObject Explosion;
    public float timer = 5f;

    public void Start()
    {
       
        Invoke(nameof(AutoDestroy), timer);
    }

   
    void Update()
    {
        
    }

    public void AutoDestroy()
    {
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }
       

        Destroy(gameObject);
    }
   
}
                