using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace GameFrame
{
    public class GameObjectMenu
    {
        static TextEditor TextEditor = new TextEditor();
        [MenuItem("GameObject/复制物体/复制路径(预制体)", false, 0)]
        public static void CopyPath()
        {
            GameObject go = Selection.activeGameObject;
            if (go == null)
                return;
            string path = GetGOPath(go,true);
            CopyToClipboard(path);
        }
        [MenuItem("GameObject/复制物体/复制路径(忽略顶级)", false, 0)]
        public static void CopyFullPathIgnoreFirst()
        {
            GameObject go = Selection.activeGameObject;
            if (go == null)
                return;
            string path = GetGOPath(go, false);
            int firstIndex = path.IndexOf("/");
            if (firstIndex >= 0)
            {
                path = path.Substring(firstIndex + 1);
            }
            CopyToClipboard(path);
        }
        [MenuItem("GameObject/复制物体/复制路径(完整)", false, 0)]
        public static void CopyFullPath()
        {
            GameObject go = Selection.activeGameObject;
            if (go == null)
                return;
            string path = GetGOPath(go, false);
            CopyToClipboard(path);
        }

        static string GetGOPath(GameObject go, bool stopPrefabTop = false)
        {
            string path = go.gameObject.name;
            return GetParentName(go.transform.parent, path, stopPrefabTop);
        }
        static string GetParentName(Transform parent, string name, bool stopPrefabTop)
        {
            if (parent == null)
                return name;
#if UNITY_2018_1_OR_NEWER
            if (!PrefabUtility.IsPartOfAnyPrefab(parent))
            {
                stopPrefabTop = false;
            }
#endif
            if (parent.parent != null)
            {
                if (stopPrefabTop)
                {
#if UNITY_2018_1_OR_NEWER
                    if (!PrefabUtility.IsPartOfAnyPrefab(parent.parent))
                    {
                        return name;
                    }
#endif
                }
                name = StringTools.Contact(parent.name, "/", name);
                return GetParentName(parent.parent, name, stopPrefabTop);
            }
            else
            {
                name = StringTools.Contact(parent.name, "/", name);
            }
            return name;
        }
        static void CopyToClipboard(string str)
        {
            TextEditor.text = str;
            TextEditor.OnFocus();
            TextEditor.Copy();
            Debug.Log(str + " 已经复制到剪切板了！");
        }

    }

    public class StringTools
    {
        public static StringBuilder Builder = new StringBuilder();

        public static void Clear()
        {
            Builder.Remove(0, Builder.Length);
        }

        public static string Contact(string a, string b)
        {
            Builder.Remove(0, Builder.Length);
            Builder.Append(a);
            Builder.Append(b);
            return Builder.ToString();
        }

        public static string Contact(string a, string b, string c)
        {
            Builder.Remove(0, Builder.Length);
            Builder.Append(a);
            Builder.Append(b);
            Builder.Append(c);
            return Builder.ToString();
        }
        public static string Contact(string a, string b, string c, string d)
        {
            Builder.Remove(0, Builder.Length);
            Builder.Append(a);
            Builder.Append(b);
            Builder.Append(c);
            Builder.Append(d);
            return Builder.ToString();
        }
        public static string Contact(string a, string b, string c, string d, string e)
        {
            Builder.Remove(0, Builder.Length);
            Builder.Append(a);
            Builder.Append(b);
            Builder.Append(c);
            Builder.Append(d);
            Builder.Append(e);
            return Builder.ToString();
        }

        public static string Contact(string a, string b, string c, string d, string e, string f)
        {
            Builder.Remove(0, Builder.Length);
            Builder.Append(a);
            Builder.Append(b);
            Builder.Append(c);
            Builder.Append(d);
            Builder.Append(e);
            Builder.Append(f);
            return Builder.ToString();
        }


        public static string SecondToHMS(float seconds)
        {
            float h = Mathf.FloorToInt(seconds / 3600f);
            float m = Mathf.FloorToInt(seconds / 60f - h * 60f);
            float s = Mathf.FloorToInt(seconds - m * 60f - h * 3600f);
            return Contact(h.ToString("00"), ":", m.ToString("00"), ":", s.ToString("00"));
        }

        /// <summary>
        /// 设置文本颜色
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetColorString(string text, Color color)
        {
            string colorText = ColorUtility.ToHtmlStringRGBA(color);
            colorText = Contact("<color=#", colorText, ">", text, "</color>");
            return colorText;
        }
    }
}
