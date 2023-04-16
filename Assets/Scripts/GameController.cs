using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] AudioClip slowMoPowerup;
    public int score = 0;

    public int cityCounter = 0;

    private int missileDestroyedPoints = 25;
    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI TimerGUI;
    [SerializeField] private TextMeshProUGUI PauseGUI;
 
    public State GameState;
    public PlayerTowerState TowerState;
    public PowerUps PlayerPowerUps;

    private float _damageTimer;

    private float _timer;
    public float Timer
    {
        get => _timer;
        set
        {
            _timer = value;

            int minutes = Mathf.FloorToInt(Timer / 60.0f);
            int seconds = Mathf.FloorToInt(Timer % 60.0f);
            TimerGUI.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void Awake()
    {
        _damageTimer = 10f;
        cityCounter = GameObject.FindGameObjectsWithTag("PlayerObjects").Length;

        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;

        GameState = State.Play;
        TowerState = PlayerTowerState.Undamaged;
        PlayerPowerUps = PowerUps.Default;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        Timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        Time.timeScale = GameState == State.Pause ? 0 : 1;

        if (GameState !=State.Pause) // Protect Pause
        {
            Time.timeScale = PlayerPowerUps == PowerUps.SlowMo ? 0.5f : 1;
        }

        if (cityCounter == 0)
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
        //Change to else if
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GameState = GameState == State.Play ? State.Pause : State.Play;
            ToggleGUIVisability(PauseGUI);
        }
        // Tester
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    AudioMannager.Instance.PlaySound(slowMoPowerup, 1f);
        //    PlayerPowerUps = PowerUps.SlowMo;
        //}


        else if (TowerState == PlayerTowerState.Damaged)
        {
            StartCoroutine(MissileTowerTimer());
        }
    }

    public void UpdateScore()
    {
        myScoreText.text = "Score: " + score;
    }

    public void AddMissileDestroyedScore()
    {
        score += missileDestroyedPoints;
        UpdateScore();
    }
    public void cityUpdate()
    {
        cityCounter -= 1;
    }
    public IEnumerator MissileTowerTimer()
    {
        //Debug.Log("CO Start");
        yield return new WaitForSeconds(_damageTimer);
        TowerState = PlayerTowerState.Undamaged;

    }
    public void ToggleGUIVisability(TextMeshProUGUI GUI)
    {
        if (GUI.enabled == true)
        {
            GUI.enabled = false;

        }
        else
        {
            GUI.enabled = true;
        }
    }
}
