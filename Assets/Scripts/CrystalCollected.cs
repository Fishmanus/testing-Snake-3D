using UnityEngine;
using UnityEngine.UI;

public class CrystalCollected : MonoBehaviour
{
    public Text crystalCountUI;
    public float inARowTimer;
    public int inARowCounter; //Crystals needed to fever
    public bool isFever;
    public int crystalCount;
    
    private float _timeElapsed;
    private int _inARowGot;

    private void Start()
    {
        crystalCountUI.text = crystalCount.ToString();
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            Destroy(other.gameObject);
            crystalCount++;
            crystalCountUI.text = crystalCount.ToString();

            if (_timeElapsed < inARowTimer)
            {
                _inARowGot++;
                if (_inARowGot >= inARowCounter && !isFever)
                {
                    Debug.Log("FEVER!");
                    GetComponent<FeverMode>().FeverOn();
                }
            }
            else _inARowGot = 0;
            
            _timeElapsed = 0;
        }
    }
}
