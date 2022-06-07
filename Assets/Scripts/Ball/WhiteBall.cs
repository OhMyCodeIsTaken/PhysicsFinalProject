using System;
using System.Collections;
using UnityEngine;

public class WhiteBall : Ball
{
    [SerializeField] private HitIndicator _indicator; // The UI indicator for telling the player where they are aiming and what is the power of the "strike"
    [SerializeField] private int _maxHitPower;
    [SerializeField] private float _currentHitPower = 0;

    [SerializeField] private float _maxHoldTime; // The maximum amount of time the mouse can be held. After reaching the max time, the elapsed time will start to drop.
    [SerializeField] private float _elapsedTime = 0; // The time the mouse is being held. Is used to determine how powerfull "striking" the whiteball wil be
    /* This bool exists to prevent the behavior of OnMouseUP() from being called (in Unity, if OnMouseDown() is called,
     * OnMouseUP() is called EVEN IF the mouse exited the object's bounds. Because in my game, OnMouseExit() Should completely
     * stop OnMouseUP() from occuring, this bool is checked inside OnMouseUp() to prevent unwanted behavior from executing. 
     */
    [SerializeField] private bool _currentlyInBallBounds;
    [SerializeField] private Camera _mainCamera; // Is used for input (clicking on WhiteBall)

    [SerializeField] private CustomRigidBody _rigidBody;
    [SerializeField] private CustomCircleCollider2D _collider;
    
    [SerializeField] private PlayBallsRespawnPoints _respawnPoints; // Is used to respawn the WhiteBall when it goes out of the table.

    private bool _isPoweringUp = true;
    private Vector3 _mousePosition;
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
        // Charging the shot while the mouse is held. This coroutine will stop (using the StopCharging() function)
        // when the mousebutton is released (OnMouseUp()) OR when the mouse exits the bounds of WhiteBall (OnMouseExit())
        

        while (true)
        {
            yield return null;

            // Raycast from mainCamera to mousePosition
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 

            if(hit.collider != null)
            {
                _mousePosition = hit.point;

                
                _direction = transform.position - _mousePosition;

                _indicator.transform.right = _direction; // Rotate the slider according the to the mousePosition in relation to the center of WhiteBall
            }
           
            // Add/subtract the _elapsedTime based on whether the shot is powering up or down
            if (_isPoweringUp)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= MaxHoldTime)
                {
                    // If the mouse is held for longer then MaxHoldTime, start powering down the shot insted of Powering Up
                    _isPoweringUp = false;
                }
            }
            else
            {
                _elapsedTime -= Time.deltaTime;

                if (_elapsedTime <= 0)
                {
                    // If _elapsedTime has reached 0 or lower, start Powering Up the shot again
                    _isPoweringUp = true;
                }
            }

            // Fill the slider according the _elapsedTime. Since the MaxValue of _indicator is set to MaxHoldTime, the ratio of the _indicator.Fill
            // will be the same as the ration of _elapsedTime/MaxHoldTime.
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
        // When a WhiteBall is "deposited", add it's (negative) score to the player's score and respawn it
        GameManager.Instance.Score += Score;
        _respawnPoints.RespawnBall(_collider);
    }
}
