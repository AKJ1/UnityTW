using System;
using Assets.UI;
using UnityEngine;
using System.Collections;

public class Button : IClickable {
    public override event OnClick clicked;
    public Rect position { get; set; }
    public Texture2D buttonTexture { get; set; }
    public GUIContent content { get; set; }
    public bool active { get; set; }
    
    public Button(Rect positon, Texture2D texture, GUIContent content, OnClick onclick )
    {
        this.position = positon;
        this.buttonTexture = texture;
        this.content = content;
        this.clicked = onclick;
        this.active = false;
    }

}
