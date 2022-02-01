using UnityEngine;

/*
 * Скрипт не используется (переделал и теперь мир движется на игрока)
 */
public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    private Vector3 startDeistance, moveVec;

    private void Start()
    {
        startDeistance = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        moveVec = Target.position + startDeistance;
        moveVec.z = 0;
        moveVec.y = startDeistance.y;
        transform.position = moveVec;
    }
}
