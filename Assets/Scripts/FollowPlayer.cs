using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //https://discussions.unity.com/t/detect-if-player-is-in-range/99984
    public GameObject target;
    public int maxRange;
    public int minRange;
    private Vector3 targetTran;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        targetTran = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, target.transform.position) < maxRange)
           && (Vector3.Distance(transform.position, target.transform.position) > minRange))
        {
            transform.LookAt(targetTran);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
