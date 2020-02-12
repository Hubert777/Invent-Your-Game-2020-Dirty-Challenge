using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UiSnapping {

    public static void DrawText(string text)
    {
        Text UItext = GameObject.Find("UiSnap").GetComponent<Text>();
        Animator anim = GameObject.Find("UiSnap").GetComponent<Animator>();

        UItext.text = text;
        anim.Play("UiTextAnim");
    }

    public static void DrawTextForever(string text)
    {
        Text UItext = GameObject.Find("UiSnap").GetComponent<Text>();
        Animator anim = GameObject.Find("UiSnap").GetComponent<Animator>();

        UItext.text = text;
        anim.Play("UiForever");
    }
}
