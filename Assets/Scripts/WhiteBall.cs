using System;
using System.Collections;
using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    [SerializeField] private HitIndicator _indicator;
    [SerializeField] private int _maxHitPower;
    [SerializeField] private int _currentHitPower = 0;
    [SerializeField] private float _proccessedHitPower;

    //[SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private CustomRigidBody _rigidBody;
    [SerializeField] private Camera _mainCamera;

    private bool _isPoweringUp = true;
    private Vector3 _mousePos;
    private Vector2 _direction;
    private Vector2 _movementVector;

    [SerializeField] private AnimationCurve _curve;

    public int MaxHitPower { get => _maxHitPower; }

    private void OnMouseDown()
    {
        StopBallMotion();
        _indicator.ToggleSliderState(true);
        _indicator.transform.position = _mainCamera.WorldToScreenPoint(transform.position);
        StartCoroutine(ChargeShot());
    }

    private void OnMouseExit()
    {
        StopCharging();
        ResetSettings();
    }

    private void OnMouseUp()
    {
        StopCharging();

        // Normalizing so that clicking closer/farther from ball center wont affect strike power
        _direction.Normalize();

        _proccessedHitPower = _curve.Evaluate(_currentHitPower);

        _movementVector = _direction * _currentHitPower;

        Debug.Log("yes" + _movementVector);
        _rigidBody.AddForce(_movementVector);
        //_rigidBody.AddForce(_movementVector);
        //_rigidBody.AddForceAtPosition(_movementVector, _mousePos);

        ResetSettings();
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
                _currentHitPower += 1;

                if (_currentHitPower >= MaxHitPower)
                {
                    _isPoweringUp = false;
                }
            }
            else
            {
                _currentHitPower -= 1;

                if (_currentHitPower <= 0)
                {
                    _isPoweringUp = true;
                }
            }

            _indicator.FillSlider(_currentHitPower);
        }


    }

    private void StopCharging()
    {
        StopCoroutine(ChargeShot());
    }

    private void ResetSettings()
    {
        _indicator.ToggleSliderState(false);
        StopAllCoroutines();
        _currentHitPower = 0;
        _isPoweringUp = true;

    }

    public void StopBallMotion()
    {
        _rigidBody.Velocity = Vector3.zero;
    }


}
