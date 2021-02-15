using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryFullText : MonoBehaviour
{
    public float FADE_OUT_MAX_TIME = 0.5f;
    public float fadeOutTimer;

    public float speed = 0.2f;

    public TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeOutTimer = FADE_OUT_MAX_TIME;
    }

    // Update is called once per frame
    void Update()
    {

        fadeOutTimer -= Time.deltaTime;
        if(fadeOutTimer > 0)
        {
            transform.position += new Vector3(0,1,0) * speed * Time.deltaTime;
            Color textColor = textMesh.color;
            textColor.a = fadeOutTimer / FADE_OUT_MAX_TIME;
            textMesh.color = textColor;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
