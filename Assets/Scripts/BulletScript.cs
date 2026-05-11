using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private int damage;
    private Rigidbody2D bulletBody;
    private SpriteRenderer bulletSrpiteRenderer;
    private float timeAlive;
    private float maxTimeALive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= maxTimeALive)
        {
            Destroy(gameObject);
        }
    }

    public void setBullet(Vector2 speed, Sprite sprite, int damage, float liveTime, float angle)
    {
        bulletBody = gameObject.GetComponent<Rigidbody2D>();
        bulletSrpiteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timeAlive = 0.0f;
        maxTimeALive = 10.0f;
        bulletSrpiteRenderer.sprite = sprite;
        bulletBody.linearVelocity = speed;
        this.damage = damage;
        maxTimeALive = liveTime;
        bulletBody.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyPlushy")
        {
            collision.gameObject.GetComponent<PlushyScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
