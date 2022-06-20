using UnityEngine;

namespace Scr
{
    public class SpeedPowerUp : MonoBehaviour
    {
        [SerializeField] private float buffDuration;

        [SerializeField] private float velBuff;
    
        private float _remainingTime;

        private bool _isPicked;
        
        private Collider2D collider2D;
        
        private GameObject player;
        void Start()
        {
            _remainingTime = buffDuration;
            _isPicked =  false;
            collider2D = GetComponent<BoxCollider2D>();
            player = GameObject.Find("Bomberman");
        }

        // Update is called once per frame
        void Update()
        {
            if(_isPicked)
                _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0)
            {
                var vel = player.GetComponent<Bomberman>().GetSpeed();
                player.GetComponent<Bomberman>().SetSpeed(vel-velBuff);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                _isPicked = true;
                var vel = player.GetComponent<Bomberman>().GetSpeed();
                player.GetComponent<Bomberman>().SetSpeed(vel+velBuff);
                this.transform.position = new Vector3(300, 300, 300);
            }
        }
    }
}
