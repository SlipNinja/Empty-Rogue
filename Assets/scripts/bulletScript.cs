using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float lifeTime=10;
    public float damage;
    [Space]
    public int bouncetimes=1;
    public bool ExplodedsOnImpact;
    public GameObject explosion;
    public float explosionRadius;
    public float explosionForce;
    public float explosionDamage;
    public LayerMask enemyLayer;
    Rigidbody2D rb;
    bool hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotates if hasnt hit anything
        if (!hit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bouncetimes<=1||collision.gameObject.tag=="enemy") {
            hit = true;
            rb.velocity = Vector2.zero;
            
            if (ExplodedsOnImpact)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,explosionRadius, enemyLayer);
                
                foreach(Collider2D obj in objects)
                {
                    Vector2 direction = obj.transform.position - transform.position;
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
                    obj.GetComponent<EnemyAI>().takeDamage(gameObject);
                    print("take damage");
                }
            }
            Destroy(gameObject);
        }
        else { bouncetimes--; }
    }
}
