using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float countdown = 3f;
    [SerializeField]
    private Bomberman player;
    private MapDestroyer map;
    private Collider2D circleCollider;

    
    // Update is called once per frame
    private void Start() {
        map = FindObjectOfType<MapDestroyer>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Bomberman>();
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0){
            map.Explosion(transform.position, player.getPowerBomb());
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            circleCollider.isTrigger=false;
        }
    }

    
}
