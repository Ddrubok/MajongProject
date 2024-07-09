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
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
        ��,
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
