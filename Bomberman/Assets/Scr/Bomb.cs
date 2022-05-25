using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float countdown = 3f;


    [SerializeField]
    private MapDestroyer map;

    // Update is called once per frame

    private void Start() {
        map = FindObjectOfType<MapDestroyer>();
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0){
            Debug.Log("Exploción");
            map.Explosion(transform.position);
            Destroy(gameObject);

        }
    }

    
}
