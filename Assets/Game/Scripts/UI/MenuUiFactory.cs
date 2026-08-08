using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace FiveNightsAtLidl.UI
{
    internal static class MenuUiFactory
    {
        private const string StyleResourcePath = "UI/MenuStyles";

        public static VisualElement PrepareRoot(UIDocument document, string rootName)
        {
            VisualElement root = document.rootVisualElement;
            root.Clear();
            root.name = rootName;
            root.focusable = true;

            StyleSheet styles = Resources.Load<StyleSheet>(StyleResourcePath);
            if (styles != null && !root.styleSheets.Contains(styles))
            {
                root.styleSheets.Add(styles);
            }
            else if (styles == null)
            {
                Debug.LogError($"Could not load Resources/{StyleResourcePath}.uss.");
            }

            AddBackdrop(root);
            return root;
        }

        public static Button CreateButton(string text, Action clicked, string name = null)
        {
            Button button = new Button(clicked)
            {
                text = text,
                name = name ?? text.ToLowerInvariant().Replace(' ', '-')
            };
            button.AddToClassList("menu-button");
            return button;
        }

        public static Label CreateLabel(string text, string className)
        {
            Label label = new Label(text);
            label.AddToClassList(className);
            return label;
        }

        private static void AddBackdrop(VisualElement root)
        {
            VisualElement backdrop = new VisualElement { name = "placeholder-backdrop" };
            backdrop.pickingMode = PickingMode.Ignore;
            backdrop.AddToClassList("placeholder-backdrop");

            VisualElement leftShade = new VisualElement { name = "left-shade" };
            leftShade.pickingMode = PickingMode.Ignore;
            leftShade.AddToClassList("left-shade");
            backdrop.Add(leftShade);

            VisualElement crtNoise = new VisualElement { name = "crt-noise" };
            crtNoise.pickingMode = PickingMode.Ignore;
            crtNoise.AddToClassList("crt-noise");
            backdrop.Add(crtNoise);

            for (int index = 0; index < 18; index++)
            {
                VisualElement line = new VisualElement();
                line.pickingMode = PickingMode.Ignore;
                line.AddToClassList("scan-line");
                line.style.top = new Length(2f + index * 5.7f, LengthUnit.Percent);
                backdrop.Add(line);
            }

            root.Add(backdrop);
        }
    }
}
