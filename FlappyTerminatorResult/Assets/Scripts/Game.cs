using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Window _startScreen;
    [SerializeField] private Window _endScreen;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private void OnEnable()
    {
        _startScreen.ButtonClicked += OnPlayButtonClick;
        _endScreen.ButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.ButtonClicked -= OnPlayButtonClick;
        _endScreen.ButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
        _enemySpawner.SetCanSpawn(false);
        _bulletSpawner.SetCanSpawn(false);
        _enemySpawner.Reset();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _endScreen.Open();
        _enemySpawner.SetCanSpawn(false);
        _bulletSpawner.SetCanSpawn(false);
        _enemySpawner.Reset();
    }

    private void OnRestartButtonClick()
    {
        _endScreen.Close();
        StartGame();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _player.Reset();
        _enemySpawner.SetCanSpawn(true);
        _bulletSpawner.SetCanSpawn(true);
    }
}
