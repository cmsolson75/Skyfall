using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    private Vector2 target;
    private float playerMissileSpeed = 8f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] AudioClip playerExplosion;


    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, playerMissileSpeed * Time.deltaTime);
        if (transform.position == (Vector3)target)
        {
            AudioMannager.Instance.PlaySound(playerExplosion, 0.5f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
