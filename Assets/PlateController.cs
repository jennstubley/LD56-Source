using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public bool IsOn;

    [SerializeField]
    private GameObject offVisual;
    [SerializeField]
    private GameObject onVisual;
    [SerializeField]
    private List<GameObject> controlledObjectsOn;
    [SerializeField]
    private List<GameObject> controlledObjectsOff;

    private HashSet<Rigidbody2D> objectsUnder = new HashSet<Rigidbody2D>();


    // Start is called before the first frame update
    void Start()
    {
        IsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        offVisual.SetActive(!IsOn);
        onVisual.SetActive(IsOn);
        foreach (var obj in controlledObjectsOn)
        {
            if (obj != null)
            {
                obj.SetActive(IsOn);
            }
        }
        foreach (var obj in controlledObjectsOff)
        {
            if (obj != null)
            {
                obj.SetActive(!IsOn);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<Rigidbody2D>() != null)
        {
            objectsUnder.Add(collision.GetComponent<Rigidbody2D>());
            IsOn = true;
            AudioController.Instance.PlayPlate();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.simulated)
        {
            objectsUnder.Remove(rb);
            if (objectsUnder.Count == 0)
            {
                IsOn = false;
                AudioController.Instance.PlayPlate();
            }
        }
    }
}
