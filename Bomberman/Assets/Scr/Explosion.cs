using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update

    private float countdown = 1f;
    private AudioManager audioManager;
    

    private Collider2D circleCollider;
    [SerializeField]
    private Bomberman player;
    
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Bomberman>();
        audioManager.seleccionAudio(0,.5f);
        
    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;
        if (countdown <= 0){
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy"){
            if (other.gameObject.tag == "Player"){
                audioManager.seleccionAudio(5,1);
				player.Animator.SetTrigger("IsDeathing");
                GameManager.Instance.GameOver();
            }
            else{
                audioManager.seleccionAudio(1,1);
                player.enemyDestroyed(other.gameObject);
				// other.gameObject.Animator.SerTrigger("IsDeathing");
            }
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }
}
