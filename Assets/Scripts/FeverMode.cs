using UnityEngine;

public class FeverMode : MonoBehaviour
{
    public float speedMultiplier = 3;
    public float feverDuration = 5;

    private CrystalCollected _crystalCollected;
    private MoveSnakeForward _moveSnakeForward;
    private SegmentMovement _segmentMovement;

    private void Start()
    {
        _crystalCollected = GetComponent<CrystalCollected>();
        _moveSnakeForward = GetComponent<MoveSnakeForward>();
        _segmentMovement = GetComponent<SegmentMovement>();
    }

    public void FeverOn()
    {
        _crystalCollected.isFever = true;
        _moveSnakeForward.snakeSpeed *= speedMultiplier;
        _segmentMovement.interpolationRatio *= speedMultiplier;
        //EAT EVERYTHING!!!
        Invoke(nameof(FeverOff), feverDuration);
    }

    void FeverOff()
    {
        _moveSnakeForward.snakeSpeed /= speedMultiplier;
        _segmentMovement.interpolationRatio /= speedMultiplier;
        //disable "EAT EVERYTHING!!!"
        _crystalCollected.isFever = false;
        _crystalCollected.crystalCount = 0;
        _crystalCollected.crystalCountUI.text = _crystalCollected.crystalCount.ToString();
        Debug.Log("fever off");
    }
}
