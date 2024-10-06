using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanel : MonoBehaviour
{
    public bool AllOn;

    [SerializeField]
    private List<GameObject> controlledObjectsOn;
    [SerializeField]
    private List<GameObject> controlledObjectsOff;

    [SerializeField]
    private List<GameObject> onObjects;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AllOn = true;
        foreach (var obj in onObjects)
        {
            if (!obj.activeInHierarchy)
            {
                AllOn = false;
                break;
            }
        }
        foreach (var obj in controlledObjectsOn)
        {
            obj.SetActive(AllOn);
        }
        foreach (var obj in controlledObjectsOff)
        {
            obj.SetActive(!AllOn);
        }
    }
}
