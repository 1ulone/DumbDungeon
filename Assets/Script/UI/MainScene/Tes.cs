using UnityEngine;

public class Tes : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthSystem.Instance.UpdateHealth(50);
            ExpSystem.Instance.UpdateEXP(50);
            ExpSystem.Instance.UpdateLevel(20);
        }
    }
}
