using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpBomb : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D circleCollider;
    private Bomberman player;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Bomberman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            player.activaPowerUpBomb();
            Destroy(gameObject);
        }
    }
}
