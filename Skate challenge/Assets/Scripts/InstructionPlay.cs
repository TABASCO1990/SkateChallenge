using UnityEngine;

/*
 * Инструкции к игре - только на старте первого уровня 
 */
public class InstructionPlay : MonoBehaviour
{
    private GameManager GM;
    private PlayerMovement PM;
    public RoadBlock RB;
    private Vector3 stopVecUp;
    private Vector3 stopVecDown;
    private bool goUp;
    private bool goDown;

    private void Start()
    {
        stopVecUp = new Vector3(-10.82f, 0, 0);
        stopVecDown = new Vector3(-23.23f, 0, 0);
        GM = FindObjectOfType<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (GM.LevelCount == 1)
        {
            InstructionUp();
            InstructionDown();
        }
    }

    // Вызывем окно с первой инструкцие по достижению определенного растояния
    private void InstructionUp()
    {
        if (!goUp)
        {
            if (RB.transform.position.x <= stopVecUp.x)
            {
                GM.InstructionScreen1.SetActive(true);
                Time.timeScale = 0f;
                GM.zoomButtonW.updateMode = AnimatorUpdateMode.UnscaledTime;
                if (Input.GetAxisRaw("Vertical") > 0){
                    GM.InstructionScreen1.SetActive(false);
                    goUp = true;
                    Time.timeScale = 1f;
                }
            }
        }
    }
    // Вызывем окно со второй инструкцией по достижению определенного растояния
    private void InstructionDown()
    {
        if (!goDown)
        {
            if (RB.transform.position.x <= stopVecDown.x)
            {
                GM.InstructionScreen2.SetActive(true);
                PM.wannaJump = false;
                Time.timeScale = 0f;
                GM.zoomButtonS.updateMode = AnimatorUpdateMode.UnscaledTime;
                if (Input.GetKeyDown(KeyCode.S)){
                    GM.InstructionScreen2.SetActive(false);
                    goDown = true;
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
