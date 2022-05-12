using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : MonoBehaviour
{

    [SerializeField] private int _maxHitPower;
    [SerializeField] private int _currentHitPower = 0;

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Camera _mainCamera;

    private bool _isPoweringUp = true;
    private Vector3 _mousePos;
    private Vector2 _direction;
    private Vector2 _movementVector;

    private void OnMouseDown()
    {
        StopBallMotion();
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
        RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            _mousePos = hit.point;
            //_mousePos.y = transform.position.y;

            _direction = transform.position - _mousePos;

            // Normalizing so that clicking closer/farther from ball center wont affect strike power
            _direction.Normalize();

            _movementVector = _direction * _currentHitPower;

            _rigidBody.AddForce(_movementVector);
            //_rigidBody.AddForceAtPosition(_movementVector, _mousePos);
        }

        ResetSettings();
    }

    private IEnumerator ChargeShot()
    {
        while (true)
        {
            yield return null;
            if (_isPoweringUp)
            {
                _currentHitPower += 5;

                if (_currentHitPower >= _maxHitPower)
                {
                    _isPoweringUp = false;
                }
            }
            else
            {
                _currentHitPower -= 5;

                if (_currentHitPower <= 0)
                {
                    _isPoweringUp = true;
                }
            }
        }


    }

    private void StopCharging()
    {
        StopCoroutine(ChargeShot());
    }

    private void ResetSettings()
    {
        StopAllCoroutines();
        _currentHitPower = 0;
        _isPoweringUp = true;

    }

    public void StopBallMotion()
    {
        _rigidBody.velocity = Vector3.zero;
    }


}
