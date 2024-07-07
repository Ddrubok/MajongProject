using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public static class Util
{
	public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
	{
		T component = go.GetComponent<T>();
		if (component == null)
			component = go.AddComponent<T>();

		return component;
	}

	public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
	{
		Transform transform = FindChild<Transform>(go, name, recursive);
		if (transform == null)
			return null;

		return transform.gameObject;
	}

	public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
	{
		if (go == null)
			return null;

		if (recursive == false)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				Transform transform = go.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || transform.name == name)
				{
					T component = transform.GetComponent<T>();
					if (component != null)
						return component;
				}
			}
		}
		else
		{
			foreach (T component in go.GetComponentsInChildren<T>())
			{
				if (string.IsNullOrEmpty(name) || component.name == name)
					return component;
			}
		}

		return null;
	}

	public static T ParseEnum<T>(string value)
	{
		return (T)Enum.Parse(typeof(T), value, true);
	}

	public static IPAddress GetIpv4Address(string hostAddress)
	{
		IPAddress[] ipAddr = Dns.GetHostAddresses(hostAddress);

		if(ipAddr.Length == 0)
		{
			Debug.LogError("AuthServer DNS Failed");
			return null;
		}

		foreach(IPAddress ip in ipAddr)
		{
			if(ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return ip;
			}
		}

        Debug.LogError("AuthServer IPv4 Failed");
        return null;
    }
}