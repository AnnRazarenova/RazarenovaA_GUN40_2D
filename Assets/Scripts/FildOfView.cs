using System.Collections;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FildOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField, Range(0,360)] private float _angle;

    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private bool _canSeePlayer;

    public bool CanSeePlayer => _canSeePlayer;

    public float Radius => _radius;

    public float Angle => _angle;

    public Player Player => _player;

    private void Start()
    {
        StartCoroutine(ForRuntime());
    }

    private IEnumerator ForRuntime()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FildOfViewCheck();
        }
    }

    private void FildOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 diractionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, diractionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, diractionToTarget, distanceToTarget, _obstacleMask))
                {
                    _canSeePlayer = true;
                }
                else
                {
                    _canSeePlayer = false;
                }
            }
            else
            {
                _canSeePlayer = false;
            }
        }
        else if (_canSeePlayer) 
        {
            _canSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Radius);

        Vector3 viewAngleLeft = DitactionFromAngle(transform.eulerAngles.y, -Angle / 2);
        Vector3 viewAngleRight = DitactionFromAngle(transform.eulerAngles.y, Angle / 2);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + viewAngleLeft * Radius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleRight * Radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Player.transform.position);
        }
    }

    private Vector3 DitactionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
