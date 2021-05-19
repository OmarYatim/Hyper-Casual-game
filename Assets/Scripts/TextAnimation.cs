using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{

    Text text;
    float speed = 1;
    float alpha;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = Color.white;
        alpha = text.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= speed * Time.deltaTime;
        text.color = new Color(1, 1, 1, alpha);
        if (text.color.a <= 0)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = Color.white;
        alpha = text.color.a;
    }

    private void OnDisable()
    {
        text.color = Color.white;
        alpha = text.color.a;
    }
}
