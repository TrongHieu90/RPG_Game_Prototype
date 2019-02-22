using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
    Vector3 clickPoint;

    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    //private void FixedUpdate()
    //{
    //    ProcessMouseMovement();
    //}

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        //print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());

    //        clickPoint = cameraRaycaster.hit.point;

    //        switch (cameraRaycaster.layerHit)
    //        {
    //            case Layer.Walkable:                   
    //                currentDestination = ShortenDestination(clickPoint, walkMoveStopRadius);

    //                break;
    //            case Layer.Enemy:
    //                currentDestination = ShortenDestination(clickPoint, attackMoveStopRadius);
    //                break;
    //            default:
    //                print("shouldnt be here");
    //                return;

    //        }

    //    }
    //    WalkToDestination();
        
    //}

    private Vector3 ShortenDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;

        if (playerToClickPoint.magnitude >= 0)
        {
            m_Character.Move(playerToClickPoint, false, false);

        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

