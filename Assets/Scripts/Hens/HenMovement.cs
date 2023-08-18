using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenMovement : MonoBehaviour
{
    [SerializeField] private Collider _henPlaceCollider;

   
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isMoving = false;

    private AnimationsHenController _animator;
    private HenStats _henStats;
    private HenStatus _henStatus;
    private Transform _hen;

    private void Start()
    {
        _animator = GetComponentInChildren<AnimationsHenController>();
        _henStatus = GetComponent<HenStatus>();
        _henStats = GetComponentInChildren<HenStats>();
        _hen = transform.GetChild(0);

        StartCoroutine(StartMovingAfterDelay());
    }
  
    private void Update()
    {
        if (isMoving)
        {
            Move();
            RotateTowardsTarget();
        }
    }

    private void Move()
    {
        
        Vector3 newPosition = Vector3.MoveTowards(_hen.position, targetPosition, 0.5f * Time.fixedDeltaTime);
        _hen.position = newPosition;

        
        if (Vector3.Distance(_hen.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            PlayRandomIdleAnimation();
            float idleTime = Random.Range(_henStats.IdleTimeMin, _henStats.IdleTimeMax);
            Invoke(nameof(ResumeMoving), idleTime);
        }
        else
        {
            if(!(_henStatus.GetStatus() == Status.Moving))
                _animator.UpdateSpeed(0.5f);
        }
            
    }
    private void RotateTowardsTarget()
    {
        Vector3 directionToTarget = targetPosition - _hen.position;
        directionToTarget.y = 0; // Ignore vertical difference for rotation

        if (directionToTarget != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
            _hen.rotation = Quaternion.Slerp(_hen.rotation, targetRotation, Time.deltaTime * _henStats.TurnSmoothTime);
        }
    }
    private IEnumerator StartMovingAfterDelay()
    {
        PlayRandomIdleAnimation();
        yield return new WaitForSeconds(Random.Range(_henStats.IdleTimeMin, _henStats.IdleTimeMax));

        isMoving = true;
        SetRandomTargetPosition();
    }

    private void PlayRandomIdleAnimation()
    {
        _animator.UpdateSpeed(0);
    }

    private void ResumeMoving()
    {
        SetRandomTargetPosition();
        isMoving = true;
    }

    private void SetRandomTargetPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(_henPlaceCollider.bounds.min.x, _henPlaceCollider.bounds.max.x),
            transform.position.y,
            Random.Range(_henPlaceCollider.bounds.min.z, _henPlaceCollider.bounds.max.z)
        );

        // Check if the distance is greater than the minimum distance
        if (Vector3.Distance(_hen.position, randomPosition) > _henStats.NpcDistanceWalkingThreshHold)
        {
            targetPosition = randomPosition;
            isMoving = true;
        }
        else
        {
            SetRandomTargetPosition(); // Try again if the selected point is too close
        }
        
    }
}