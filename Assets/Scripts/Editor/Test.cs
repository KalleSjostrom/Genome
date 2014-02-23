using UnityEngine;
using UnityEditor;
using System.Collections;

public class GraphEditorWindow : EditorWindow
{
	Rect windowRect = new Rect (100 + 100, 100, 100, 100);
	Rect windowRect2 = new Rect (100, 100, 100, 100);
	
	
	[MenuItem ("Window/Graph Editor Window")]
	static void Init () {
		EditorWindow.GetWindow (typeof (GraphEditorWindow));
	}
	
	private void OnGUI()
	{
		Handles.BeginGUI();
		Handles.DrawBezier(windowRect.center, windowRect2.center, new Vector2(windowRect.xMax + 50f,windowRect.center.y), new Vector2(windowRect2.xMin - 50f,windowRect2.center.y),Color.red,null,5f);
		Handles.EndGUI();
		
		BeginWindows();
		windowRect = GUI.Window(0, windowRect, WindowFunction, "Box1");
		windowRect2 = GUI.Window(1, windowRect2, WindowFunction, "Box2");

		EndWindows();
		
	}
	void WindowFunction (int windowID) {
		if (GUI.Button(new Rect(20, 50, 50, 50), "Hello")) {
			Debug.Log("HELLO");
		}
		GUI.Toggle(new Rect(70, 50, 50, 50), false, "Doh");
		EditorGUILayout.Popup(1, new string[]{ "1", "2" });

		EditorGUIUtility.AddCursorRect(new Rect(90, 90, 10, 10), MouseCursor.ResizeUpLeft);

		GUI.DragWindow();
	}
}
