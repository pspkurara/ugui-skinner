using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[CanEditMultipleObjects]
	[CustomEditor(typeof(UserLogic),true)]
	internal sealed class UserLogicInspector : Editor
	{

		#region クラス

		/// <summary>
		/// ユーザーロジックのサンプルを描画するためのダミーデータ
		/// </summary>
		private sealed class DummyDataObject : ScriptableObject
		{

			/// <summary>
			/// サンプルユーザーロジック用のダミープロパティ
			/// </summary>
			public SkinPartsPropertry dummyProperty = new SkinPartsPropertry();

		}

		#endregion

		#region プロパティや変数

		private EditorSkinPartsPropertry dummyEditorSkinPartsProperty = new EditorSkinPartsPropertry();

		private SerializedProperty dummyProperty { get; set; }

		private DummyDataObject dummyData = null;

		#endregion

		#region メソッド

		private void OnEnable()
		{
			// サンプル用にダミーオブジェクトを作っておく
			if (!dummyData)
			{
				dummyData = CreateInstance<DummyDataObject>();
				dummyData.hideFlags = HideFlags.HideAndDontSave;
			}
			ReloadDummyData(target);
		}

		private void OnDisable()
		{
			// 終わったら破棄
			if (dummyData)
			{
				DestroyImmediate(dummyData);
			}
			dummyProperty.serializedObject.Dispose();
			dummyProperty = null;
		}

		private void ReloadDummyData(Object target)
		{
			if (dummyData.dummyProperty.objectReferenceValues.Count > ScriptableLogic.RequiredObjectLength &&
				dummyData.dummyProperty.objectReferenceValues[ScriptableLogic.LogicIndex] == target)
			{
				return;
			}
			if (dummyProperty != null)
			{
				dummyProperty.serializedObject.Dispose();
			}
			dummyData.dummyProperty.Clear();
			dummyData.dummyProperty.objectReferenceValues.Add(target);
			EditorUtility.SetDirty(dummyData);
			SerializedObject self = new SerializedObject(dummyData);
			dummyProperty = self.FindProperty("dummyProperty");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			SkinnerEditorUtility.DrawLine();
			EditorGUILayout.LabelField(EditorConst.UserLogicSampleTitle);
			SkinnerEditorUtility.DrawLine();

			UserLogic drawTarget = serializedObject.targetObject as UserLogic;

			ReloadDummyData(drawTarget);

			var scriptableLogic = SkinPartsInspectorAccess.GetSkinInspector(typeof(ScriptableLogic));

			// ダミーデータをマッピングする
			dummyProperty.serializedObject.Update();
			dummyEditorSkinPartsProperty.MapProperties(dummyProperty);

			GUI.enabled = false;

			// ダミーデータを描画
			UserLogicExtension.SetActiveUserLogic(drawTarget);
			scriptableLogic.CleanupFields(dummyEditorSkinPartsProperty);
			scriptableLogic.DrawInspector(dummyEditorSkinPartsProperty);
			UserLogicExtension.ReleaseActiveUserLogic();

			GUI.enabled = true;

			// ダミーデータを更新
			dummyProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo();

			SkinnerEditorUtility.DrawLine();
		}

		#endregion

	}

}

