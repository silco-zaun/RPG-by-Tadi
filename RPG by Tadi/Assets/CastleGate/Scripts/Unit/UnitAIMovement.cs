using UnityEngine;

public class UnitAIMovement : MonoBehaviour
{
    private UnitAINavMesh nav;
    private UnitAnimation unitAnim;
    private BalloonAnimation balloonAnim;
    private GameObject balloon;

    private Vector3 moveVec;

    // Start is called before the first frame update

    private void Awake()
    {
        nav = GetComponent<UnitAINavMesh>();
        unitAnim = GetComponentInChildren<UnitAnimation>();
        balloonAnim = GetComponentInChildren<BalloonAnimation>();

        balloon = balloonAnim.gameObject;
        balloon.SetActive(false);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        unitAnim.PlayMoveAnim(moveVec);
    }

    public void Move(Vector3 target)
    {
        nav.SetDestination(target);
    }

    public bool IsArrive()
    {
        return nav.IsArrive();
    }

    public void MissTarget(Vector3 target)
    {
        balloonAnim.QuestionFinish();
    }

    public void Talk()
    {
        balloonAnim.Talk();
    }

    public void TalkFinish()
    {
        balloonAnim.TalkFinish();
    }

    public void ArriveStartingPos()
    {
        ActivateBalloon(false);
    }

    public void PlayMoveAnim(Vector3 target)
    {
        moveVec = (target - transform.position).normalized;
    }

    public void PlayLookAnim(Vector3 target)
    {
        Vector3 look = (target - transform.position).normalized;
        unitAnim.PlayLookAnim(look);
    }

    public void PlayStopAnim()
    {
        moveVec = Vector3.zero;
    }

    public void ActivateBalloon(bool activate)
    {
        balloon.SetActive(activate);
    }
}
