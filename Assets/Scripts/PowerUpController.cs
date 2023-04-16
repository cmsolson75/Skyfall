using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private AudioSource audioSource;


    private float _damageWait;
    private float _rapidFireWait;
    private bool _inCoroutinePowerUp;
    private bool _inCoroutineDamaged;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1f; // Default Pitch
        _damageWait = 10f;
        _rapidFireWait = 10f;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.Instance.PlayerPowerUps == PowerUps.RappidFire && !_inCoroutinePowerUp)
        {
            audioSource.pitch = 1.5f; // 1.5 pitch
            StartCoroutine(PowerUpTimer());
            _inCoroutinePowerUp = true;

        }
        else if (GameController.Instance.PlayerPowerUps == PowerUps.SlowMo && !_inCoroutinePowerUp)
        {
            audioSource.pitch = 0.5f; // Half time pitch
            StartCoroutine(PowerUpTimer());
            _inCoroutinePowerUp = true;
        }
        else if (GameController.Instance.TowerState == PlayerTowerState.Damaged && !_inCoroutineDamaged)
        {
            StartCoroutine(MissileTowerTimer());
            _inCoroutineDamaged = true;
        }
    }
    public IEnumerator PowerUpTimer()
    {
        // Timer for powerups
        yield return new WaitForSeconds(_rapidFireWait);
        audioSource.pitch = 1f;
        GameController.Instance.PlayerPowerUps = PowerUps.Default;

        _inCoroutinePowerUp = false;

    }
    public IEnumerator MissileTowerTimer()
    {
        
        // Timer for missile tower
        yield return new WaitForSeconds(_damageWait);
        GameController.Instance.TowerState = PlayerTowerState.Undamaged;
        _inCoroutineDamaged = false;

    }
}
