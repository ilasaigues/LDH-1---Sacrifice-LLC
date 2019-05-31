using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundToggle : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        SetSpriteState(Director.GetManager<SoundManager>().SoundOn);
    }

    public void Toggle()
    {
        SetSpriteState(Director.GetManager<SoundManager>().ToggleSound());
    }

    void SetSpriteState(bool state)
    {
        button.image.sprite = state ? onSprite : offSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
