using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoBox : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    private int xIndex;
    private float minX;
    private float maxX;
    private float maxY;
    private float xPadding = 0.5f;
    [SerializeField] private int _speed = 5;
    [SerializeField] AudioClip slowMoSound;

    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        float[] xSpawn = new float[] { minX - xPadding, maxX + xPadding };
        xIndex = Random.Range(0, 2); //Selects from xSpawn
        transform.position = new Vector3(xSpawn[xIndex], Random.Range(0, maxY), 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Box Direction
        if (xIndex == 0)
        {
            transform.position += new Vector3(_speed, 0, 0) * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, -90) * Time.deltaTime);

            if (transform.position.x >= maxX + xPadding)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += new Vector3(-_speed, 0, 0) * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);

            if (transform.position.x <= minX - xPadding)
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMissiles" || collision.tag == "Explosions")
        {
            GameController.Instance.PlayerPowerUps = PowerUps.SlowMo;
            AudioMannager.Instance.PlaySound(slowMoSound, 1f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
