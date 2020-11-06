using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	public static class EditorConst
	{

		public static readonly GUIStyle NormalFoldoutStyle = CreateNormalFoldoutStyle();
		public static readonly GUIStyle HighLightFoldoutStyle = EditorStyles.foldoutPreDrop;
		public static readonly Color AddButtonColor = Color.cyan;
		public static readonly Color RemoveButtonColor = new Color(1, 0.7f, 0.78f);
		public static readonly Color CleanupButtonColor = Color.yellow;

		public static readonly GUIContent LeftSkinSelectArrow = new GUIContent("<<");
		public static readonly GUIContent RightSkinSelectArrow = new GUIContent(">>");

		public static readonly GUIContent CurrentSelectStyleTitle = new GUIContent("Current Select Style");

		public const string CurrentSkinHasMultipleDifferentValue = "-";

		public const string SkinFoldTitle = "Skin {0}";
		public const string SkinFoldTitleHasStyleKey = "Skin {0} [{1}]";

		public static readonly GUIContent AddSkinButtonTitle = new GUIContent("Add New Skin Style");
		public static readonly GUIContent AddPartsButtonTitle = new GUIContent("Add New Skin Parts");
		public static readonly GUIContent RemoveSkinButtonTitle = new GUIContent("Remove Skin");
		public static readonly GUIContent RemovePartsButtonTitle = new GUIContent("Remove Parts");

		public static readonly GUIContent CleanupButtonTitle = new GUIContent("Cleanup");

		public const string AddFieldButtonTitle = "Add {0}";
		public static readonly GUIContent RemoveFieldButtonTitle = new GUIContent("Remove");

		public static readonly GUIContent SkinnerPartsTypeFieldTitle = new GUIContent("Type");

		public static readonly GUIContent SkinnerStyleKeyFieldTitle = new GUIContent("Style Key");

		public const string FieldNumberTitle = "No.{0}";

		public const string MissingSkinPartsTypeMessage = "Skin Parts Type \" {0} \" is incorrect.\nPlease delete this Skin or correct the value.";
		public const MessageType MissingSkinPartsTypeMessageType = MessageType.Warning;

		public const string MissingSkinPartsInspectorTypeMessage = "Inspector for \" {0} \" not found.\nPlease check if it is registered correctly";
		public const MessageType MissingSkinPartsInspectorTypeMessageType = MessageType.Warning;

		public static readonly GUIContent UserLogicSampleTitle = new GUIContent("Skin Parts Inspector Sample");

		public static readonly GUILayoutOption SkinSelectArrowMaxWidth = GUILayout.MaxWidth(50);
		public static readonly GUILayoutOption SkinAddOrRemoveButtonMaxWidth = GUILayout.MaxWidth(150);

		public static readonly GUILayoutOption ComponentIndexFieldMaxWidth = GUILayout.MaxWidth(50);

		public static readonly GUILayoutOption[] LineBoxStyle = new GUILayoutOption[] { GUILayout.Height(1), GUILayout.ExpandWidth(true) };

		public const int SkinStyleChildIndent = 1;
		public const int SkinPartsChildIndent = 2;

		private static GUIStyle CreateNormalFoldoutStyle()
		{
			var guiStyle = new GUIStyle(EditorStyles.foldout);
			guiStyle.margin = EditorStyles.foldoutPreDrop.margin;
			return guiStyle;
		}

	}

}
