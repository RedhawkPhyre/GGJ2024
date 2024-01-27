using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearSense : MonoBehaviour
{
    public struct Noise {
        public Vector3 position;
        public float alert;
    }

    private float _sensitivity = 1.0f;
    public float sensitivity {
        get { return _sensitivity; }
        set { _sensitivity = Mathf.Clamp01(value); }
    }

    public List<HearSense.Noise> noise_history = new List<HearSense.Noise>();

    public void Alert(Vector3 position)
    {
        float distance = Vector3.Distance(position, gameObject.transform.position);
        float alert = Mathf.Clamp01(1.0f / (_sensitivity * _sensitivity * distance * distance));
        noise_history.Add(new HearSense.Noise { alert=alert, position=position });
    }

    // Start is called before the first frame update
    void Start()
    {
        SenseManager.GetSenseManager().RegisterSense(this);
    }
}
