using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ViewManager : MonoBehaviour
{
	private static ViewManager s_instance;

	[SerializeField] private View _startingView;

	[SerializeField] private View[] _views;

	private View _currentView;

	private readonly Stack<View> _history = new Stack<View>();

	private void Awake()
	{
		if(s_instance == null)
		s_instance = this;
	} 
	public static View GetCurrentView()
	{
		return s_instance._currentView;
	}
	private void Start()
	{
		for (int i = 0; i < _views.Length; i++)
		{
			_views[i].Initialize();
			// _views[i].Hide();
		}

		if (_startingView != null)
		{
			// _startingView.Initialize();
			Show(_startingView, true, true);
		}
	}

	public static T GetView<T>() where T : View
	{
		for (int i = 0; i < s_instance._views.Length; i++)
		{
			if (s_instance._views[i] is T tView)
			{
				return tView;
			}
		}

		return null;
	}

	public static void Show<T>(bool remember = true, bool hide = true) where T : View
	{
		for (int i = 0; i < s_instance._views.Length; i++)
		{
			if (s_instance._views[i] is T)
			{
				if (s_instance._currentView != null)
				{
					if (remember)
					{
						s_instance._history.Push(s_instance._currentView);
					}

					if(hide)
					{
						s_instance._currentView.Hide();
					}
				}

				// s_instance._views[i].Initialize();
				s_instance._views[i].Show();

				s_instance._currentView = s_instance._views[i];
			}
		}
	}

	public static void Show(View view, bool remember = true, bool hide = true)
	{
		if (s_instance._currentView != null)
		{
			if (remember)
			{
				s_instance._history.Push(s_instance._currentView);
			}

			if(hide)
			{
				s_instance._currentView.Hide();
			}
		}
		
		view.Show();

		s_instance._currentView = view;
	}

	public static void ShowLast(bool hide)
	{
		if (s_instance._history.Count != 0)
		{
			Show(s_instance._history.Pop(), false, hide);
		}
	}
}
