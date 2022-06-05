using System;
using System.Collections;
using UnityEngine;

public class WhiteBall : Ball
{
    [SerializeField] private HitIndicator _indicator;
    [SerializeField] private int _maxHitPower;
    [SerializeField] private float _currentHitPower = 0;

    [SerializeField] private float _maxHoldTime;
    [SerializeField] private float _elapsedTime = 0;
    [SerializeField] private bool _currentlyInBallBounds;

    [SerializeField] private CustomRigidBody _rigidBody;
    [SerializeField] private CustomCircleCollider2D _collider;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PlayBallsRespawnPoints _respawnPoints;

    private bool _isPoweringUp = true;
    private Vector3 _mousePos;
    private Vector2 _direction;
    private Vector2 _movementVector;

    public float MaxHoldTime { get => _maxHoldTime; }

    private void Start()
    {
        _collider = GetComponent<CustomCircleCollider2D>();
    }

    private void OnMouseDown()
    {
        if(!GameManager.Instance.LockPlayerInput)
        {
            StopBallMotion();
            _indicator.ToggleSliderState(true);
            _indicator.transform.position = _mainCamera.WorldToScreenPoint(transform.position);
            _currentlyInBallBounds = true;
            StartCoroutine(ChargeShot());
        }        
    }

    private void OnMouseExit()
    {
        if (!GameManager.Instance.LockPlayerInput)
        {
            StopCharging();
            ResetSettings();
        }
    }

    private void OnMouseUp()
    {
        if (!GameManager.Instance.LockPlayerInput && _currentlyInBallBounds)
        {
            StopCharging();

            // Normalizing so that clicking closer/farther from ball center wont affect strike power
            _direction.Normalize();

            _currentHitPower = _elapsedTime * _maxHitPower;

            _movementVector = _direction * _currentHitPower;

            _rigidBody.AddForce(_movementVector);

            GameManager.Instance.ExpendShot();
            ResetSettings();   
        }
    }

    private IEnumerator ChargeShot()
    {
        
        while (true)
        {
            yield return null;
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                _mousePos = hit.point;

                _direction = transform.position - _mousePos;

                _indicator.transform.right = _direction;
            }
           

            if (_isPoweringUp)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= MaxHoldTime)
                {
                    _isPoweringUp = false;
                }
            }
            else
            {
                _elapsedTime -= Time.deltaTime;

                if (_elapsedTime <= 0)
                {
                    _isPoweringUp = true;
                }
            }

            _indicator.FillSlider(_elapsedTime);
        }


    }

    private void StopCharging()
    {
        StopCoroutine(ChargeShot());
    }

    private void ResetSettings()
    {
        _currentlyInBallBounds = false;
        _indicator.ToggleSliderState(false);
        StopAllCoroutines();
        _currentHitPower = 0;
        _elapsedTime = 0;
        _isPoweringUp = true;

    }

    public void StopBallMotion()
    {
        _rigidBody.Velocity = Vector3.zero;
    }

    public override void PocketBall()
    {
        GameManager.Instance.Score += Score;
        _respawnPoints.RespawnBall(_collider);
    }
}
