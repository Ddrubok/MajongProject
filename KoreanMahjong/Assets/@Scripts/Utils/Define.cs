using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
	public enum EScene
	{
		TitleScene,
		GameScene,
        Unknown,
    }

    public enum KoreanAlphabet
    {
        ぁ,
        あ,
        い,
        ぇ,
        え,
        ぉ,
        け,
        げ,
        こ,
        さ,
        ざ,
        し,
        じ,
        す,
        ず,
        せ,
        ぜ,
        そ,
        ぞ,
        た,
        だ,
        ち,
        ぢ,
        っ,
        つ,
        づ,
        て,
        で,
        と,
        ど,
        な,
        に,
        ぬ,
        ね,
        の,
        は,
        ば,
        ぱ,
        ひ,
        び,
        last,
    }
    public enum EUIEvent
	{
		Click,
		PointerDown,
		PointerUp,
		Drag,
	}

	public enum ESound
	{
		Bgm,
		Effect,
		Max,
	}
    public enum EProviderType
    {
        None= 0,
        Guest =1,
        Google=2,
        Facebook=3,

    }
}
