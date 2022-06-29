using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpBomb : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D circleCollider;
    private Bomberman player;
    private AudioManager audioManager;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Bomberman>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            audioManager.seleccionAudio(2,1);
            player.activaPowerUpBomb();
            Destroy(gameObject);
        }
    }
}
