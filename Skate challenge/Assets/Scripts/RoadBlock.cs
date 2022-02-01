using UnityEngine;

/*
 * Движение уровня на игрока
 */
public class RoadBlock : MonoBehaviour
{
    private GameManager GM;
    private Vector3 moveVec;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        moveVec = new Vector3(-1, 0, 0);
    }
    void Update()
    {
        if(GM.CanPlay)
            transform.Translate(moveVec * Time.deltaTime * GM.MoveSpeed);
        
    }
}
