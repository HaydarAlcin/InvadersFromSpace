using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    Camera cam;
    public float width;
    private float speed = 3f;

    bool isShooting;
    float coolDown = 0.5f;

    public ShipStats shipStats;
    private Vector2 offScrennPos = new Vector2(0, -20);
    private Vector2 startPos = new Vector2(0, -6);

    [SerializeField] private ObjectPool objectPool = null;

    private void Awake()
    {
        cam = Camera.main;
        width = (1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f) / 2) - 0.25f;
    }

    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLifes = shipStats.maxLifes;
    }

   
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A)&& transform.position.x>-width)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x < width)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.Space)&& !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = new Vector2(transform.position.x, transform.position.y + .5f);
        //Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y + .5f), Quaternion.identity);
        yield return new WaitForSeconds(coolDown);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("woqbfoýweg");
            collision.gameObject.SetActive(false);
            TakeDamage();
        }    

    }


    private IEnumerator Respawn()
    {
        transform.position = offScrennPos;
        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;
    }

    public void TakeDamage()
    {
        shipStats.currentHealth--;

        if (shipStats.currentHealth<=0)
        {
            shipStats.currentLifes--;

            if (shipStats.currentLifes<=0)
            {
                Debug.Log("Game Over");
            }

            else
            {
                Debug.Log("Respawn");
            }
        }
    }

    
}
