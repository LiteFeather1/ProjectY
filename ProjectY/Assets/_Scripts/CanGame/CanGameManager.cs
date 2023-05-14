using System.Collections.Generic;
using ScriptableObjectEvents;
using System.Collections;
using UnityEngine;
using SkeeBall;

public class CanGameManager : MonoBehaviour
{
    [Header("Cans prefeab")]
    [SerializeField] private Can _heavyCanPrefab;
    [SerializeField] private Can _lightCanPrefab;
    [SerializeField] private Material[] _canMats;
    private List<Material> _canMaterials;
    [SerializeField] private MeshRenderer _canMeshRenderer;
    [SerializeField] float xPadding;

    [Header("Cans")]
    [SerializeField, Min(MIN_ROWS)] int rows;
    private const int MIN_ROWS = 3;
    [SerializeField] private int _maxRows = 10;
    [SerializeField] private AnimationCurve _cansCurve;
    private int _currentCansNeededToKnock;
    private int _currentCansKnocked;
    private List<Can> _currentCans;

    [Header("Balls")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private AnimationCurve _ballCurve;
    [SerializeField] private int _startBalls;
    [SerializeField] private VrBall _ballPrefab;
    private int _ballCount;
    private const int BALL_LIMIT = 9;
    private readonly List<VrBall> _balls = new();

    [Header("End Game")]
    [SerializeField] private VoidEvent _endGame;
    private bool _gameStarted;
    private bool _respawningCans = false;

    private float XSpacing => _canMeshRenderer.bounds.size.z / 10 * .3f + xPadding;

    private readonly YieldInstruction _waitBetweenBalls = new WaitForSeconds(1.4f);
    private readonly YieldInstruction _waitBetweenPhase = new WaitForSeconds(1.5f);

    //Event
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        _ballCount = 0;
        rows = MIN_ROWS;
        _gameStarted = true;
        InitMaterialList();
        StartCoroutine(SpawnCans());
    }

    private void InitMaterialList()
    {
        _canMaterials = new List<Material>();
        foreach (var material in _canMats)
            _canMaterials.Add(material);
    }

    private IEnumerator SpawnCans()
    {
        StartCoroutine(SpawnBalls());

        int canQuantity = rows;
        float height = transform.position.y + 0.01f;

        if (_canMaterials.Count == 0)
            InitMaterialList();

        Material mat = _canMaterials[Random.Range(0, _canMaterials.Count)];
        _canMaterials.Remove(mat);
        _currentCans = new List<Can>();

        float totalCount = 0;

        for (int x = 0; x < rows; x++)
        {
            float xPos = transform.position.z + XSpacing / 2 - (XSpacing * canQuantity / 2);

            for (int i = 0; i < canQuantity; i++)
            {
                Vector3 canPosition = new(transform.position.x, height, xPos);

                Can newCan;
                if (x == 0)
                {
                    newCan = Instantiate(_lightCanPrefab, canPosition, Quaternion.identity);
                }
                else
                {
                    newCan = Instantiate(_heavyCanPrefab, canPosition, Quaternion.identity);
                }

                newCan.SetMaterial(mat);
                _currentCans.Add(newCan);
                xPos += XSpacing;
                totalCount++;
            }

            canQuantity--;
            height += _canMeshRenderer.bounds.size.y;
        }

        _currentCansKnocked = 0;
        _currentCansNeededToKnock = (int)(_cansCurve.Evaluate(rows) * totalCount);

        _respawningCans = false;
        yield return null;
    }

    private IEnumerator SpawnBalls()
    {
        int ballsToSpawn = (int)_ballCurve.Evaluate(rows);

        for (int i = 0; i < ballsToSpawn; i++)
        {
            if (_ballCount >= BALL_LIMIT)
                break;

            _balls.Add(Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity));
            _ballCount++;
            yield return _waitBetweenBalls;
        }
    }

    public void CanKnocked()
    {
        _currentCansKnocked++;

        if (_currentCansKnocked >= _currentCansNeededToKnock && !_respawningCans)
        {
            _respawningCans = true;
            print("Knocked");

            rows++;

            if (rows > _maxRows)
                rows = _maxRows;

            StartCoroutine(Delay_Spawn());
        }
    }

    private IEnumerator Delay_Spawn()
    {
        yield return _waitBetweenPhase;

        foreach (var can in _currentCans)
            Destroy(can.gameObject);

        yield return SpawnCans();
    }

    private void EndGame()
    {
        if (_ballCount <= 0)
        {
            print("Can Game Ended");
            _endGame.Raise();

            foreach (var can in _currentCans)
                Destroy(can.gameObject);

            foreach (var ball in _balls)
                Destroy(ball.gameObject);

            _balls.Clear();
            _gameStarted = false;
            _currentCans = new();
        }
    }

    //Event
    public void BallDisapperead()
    {
        if (!_gameStarted)
            return;

        _ballCount--;
        EndGame();
    }
}
