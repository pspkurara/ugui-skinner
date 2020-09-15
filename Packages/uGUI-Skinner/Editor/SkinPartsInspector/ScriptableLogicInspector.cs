// #define SKIP_LOGIC_CACHE
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Type = System.Type;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ScriptableLogic))]
	internal sealed class ScriptableLogicInspector : ISkinPartsInspector
	{

		internal sealed class UserLogicVariableDisplayData
		{

			public GUIContent DisplayName;
			public SerializedPropertyType PropertyType;
			public UserLogicVariable VariableData;
			public int FieldIndex;
			public GUIContent[] PopupDisplayName;
			public int[] PopupValue;

		}

		private sealed class UserLogicVariableRootData
		{
			public int objectReferenceArrayCount;
			public int floatArrayCount;
			public int vector4ArrayCount;
			public int stringArrayCount;

			public List<UserLogicVariableDisplayData> VariableDisplayDatas;
		}

		private static Dictionary<Type, UserLogicVariableRootData> m_CachedVariableDisplayDatas = new Dictionary<Type, UserLogicVariableRootData>();

		private List<UserLogicVariableDisplayData> userLogicVariableDisplayDatas = new List<UserLogicVariableDisplayData>();
		private UserLogic currentUserLogic;
		private int objectReferenceArrayCount = 0;
		private int floatArrayCount = 0;
		private int vector4ArrayCount = 0;
		private int stringArrayCount = 0;
		private SkinPartsPropertry validateProperty = new SkinPartsPropertry();

		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanObjectReferenceArrayWithFlexibleSize<Object>(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength);

			var userLogic = property.objectReferenceValues.GetArrayElementAtIndex(ScriptableLogic.LogicIndex).objectReferenceValue as UserLogic;
			bool isCorrect = CreateDisplayData(userLogic);
			if (isCorrect)
			{
				SkinnerEditorUtility.StripArray<Object>(property.objectReferenceValues, objectReferenceArrayCount + ScriptableLogic.RequiredObjectLength);
				int objectReferenceCount = 0;
				for (int i = 0; i < userLogicVariableDisplayDatas.Count; i++)
				{
					var v = userLogicVariableDisplayDatas[i];
					if (!SkinnerSystemType.IsObjectReferenceValue(v.VariableData.FieldType)) continue;
					SkinnerEditorUtility.CleanObject(property.objectReferenceValues, v.VariableData.FieldType, objectReferenceCount + ScriptableLogic.RequiredObjectLength);
					objectReferenceCount++;
				}
				SkinnerEditorUtility.CleanArray(property.floatValues, floatArrayCount);
				SkinnerEditorUtility.CleanArray(property.vector4Values, vector4ArrayCount);
				SkinnerEditorUtility.CleanArray(property.stringValues, stringArrayCount);
			}
			else
			{
				SkinnerEditorUtility.CleanArray<UserLogic>(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength);
				SkinnerEditorUtility.CleanArray(property.floatValues);
				SkinnerEditorUtility.CleanArray(property.vector4Values);
				SkinnerEditorUtility.CleanArray(property.stringValues);
			}
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength, false);

			var logicProperty = property.objectReferenceValues.GetArrayElementAtIndex(ScriptableLogic.LogicIndex);
			var preSelectLogic = logicProperty.objectReferenceValue as UserLogic;
			SkinnerEditorGUILayout.ObjectField(SkinContent.Logic, logicProperty, typeof(UserLogic));
			// このタイミングで参照対象が変わるとエラーが起こる
			if (preSelectLogic != logicProperty.objectReferenceValue)
			{
				// クリーンアップしてインスペクターは次に描画させる
				CleanupFields(property);
				return;
			}

			if (logicProperty.hasMultipleDifferentValues) return;

			var userLogic = property.objectReferenceValues.GetArrayElementAtIndex(ScriptableLogic.LogicIndex).objectReferenceValue as UserLogic;
			bool isCorrect = CreateDisplayData(userLogic);
			if (isCorrect)
			{
				SkinnerEditorUtility.ResetArray(property.objectReferenceValues, objectReferenceArrayCount + ScriptableLogic.RequiredObjectLength, false);
				SkinnerEditorUtility.ResetArray(property.floatValues, floatArrayCount);
				SkinnerEditorUtility.ResetArray(property.vector4Values, vector4ArrayCount);
				SkinnerEditorUtility.ResetArray(property.stringValues, stringArrayCount);
				for (int i = 0; i < userLogicVariableDisplayDatas.Count; i++)
				{
					var v = userLogicVariableDisplayDatas[i];
					switch (v.PropertyType)
					{
						case SerializedPropertyType.ObjectReference:
							{
								var element = property.objectReferenceValues.GetArrayElementAtIndex(v.FieldIndex + ScriptableLogic.RequiredObjectLength);
								SkinnerEditorGUILayout.ObjectField(v.DisplayName, element, v.VariableData.FieldType);
							}
							break;
						case SerializedPropertyType.Boolean:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.Toggle(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Color:
							{
								var element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.ColorField(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Float:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.FloatField(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Integer:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.IntField(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Enum:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.IntPopup(v.DisplayName, element, v.PopupDisplayName, v.PopupValue);
							}
							break;
						case SerializedPropertyType.Vector2:
							{
								var element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.Vector2Field(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Vector3:
							{
								var element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.Vector3Field(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Vector4:
							{
								var element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.Vector4Field(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Character:
							{
								var element = property.stringValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.CharField(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.String:
							{
								var element = property.stringValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.TextField(v.DisplayName, element);
							}
							break;
					}
				}

				SkinnerEditorUtility.MapRuntimePropertyFromEditorProperty(validateProperty, property);
				var logic = validateProperty.objectReferenceValues[ScriptableLogic.LogicIndex] as UserLogic;
				validateProperty.objectReferenceValues.Remove(logic);
				UserLogicExtension.SetActiveUserLogic(logic);
				userLogic.ValidateProperty(validateProperty);
				UserLogicExtension.ReleaseActiveUserLogic();
				validateProperty.objectReferenceValues.Insert(ScriptableLogic.LogicIndex, logic);
				SkinnerEditorUtility.MapRuntimePropertyFromEditorProperty(property, validateProperty);
			}
		}

		/// <summary>
		/// インスペクター用表示データを生成して初期化する
		/// </summary>
		/// <param name="userLogic">userLogic</param>
		/// <returns></returns>
		private bool CreateDisplayData(UserLogic userLogic)
		{
			if (!userLogic) return false;

			#if SKIP_LOGIC_CACHE
			
			currentUserLogic = userLogic;

			#else

			// ここでキャッシュしてクリエイトを抑制
			if (currentUserLogic == userLogic)
			{
				return true;
			}
			currentUserLogic = userLogic;

			// すでに存在したらキャッシュから取ってくる
			var userLogicType = userLogic.GetType();
			if (m_CachedVariableDisplayDatas.ContainsKey(userLogicType))
			{
				var data = m_CachedVariableDisplayDatas[userLogicType];
				userLogicVariableDisplayDatas = data.VariableDisplayDatas;
				objectReferenceArrayCount = data.objectReferenceArrayCount;
				floatArrayCount = data.floatArrayCount;
				vector4ArrayCount = data.vector4ArrayCount;
				stringArrayCount = data.stringArrayCount;
				return true;
			}

			# endif

			userLogicVariableDisplayDatas = new List<UserLogicVariableDisplayData>();
			objectReferenceArrayCount = 0;
			floatArrayCount = 0;
			vector4ArrayCount = 0;
			stringArrayCount = 0;
			foreach (var v in userLogic.variables)
			{
				bool isUnCorrect = false;
				var data = new UserLogicVariableDisplayData();
				if (v.FieldType == null)
				{
					// 型すら指定されてない場合は何も表示させるべきではない
					isUnCorrect = true;
				}
				else if (SkinnerSystemType.IsObject(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.ObjectReference;
					data.FieldIndex = objectReferenceArrayCount;
					objectReferenceArrayCount++;
				}
				else if (SkinnerSystemType.IsBoolean(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Boolean;
					data.FieldIndex = floatArrayCount;
					floatArrayCount++;
				}
				else if (SkinnerSystemType.IsColor(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Color;
					data.FieldIndex = vector4ArrayCount;
					vector4ArrayCount++;
				}
				else if (SkinnerSystemType.IsFloat(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Float;
					data.FieldIndex = floatArrayCount;
					floatArrayCount++;
				}
				else if (SkinnerSystemType.IsInteger(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Integer;
					data.FieldIndex = floatArrayCount;
					floatArrayCount++;
				}
				else if (SkinnerSystemType.IsEnum(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Enum;
					data.PopupDisplayName = v.FieldType.GetEnumNames().Select(n => new GUIContent(n)).ToArray();
					data.PopupValue = v.FieldType.GetEnumValues().Cast<int>().ToArray();
					data.FieldIndex = floatArrayCount;
					floatArrayCount++;
				}
				else if (SkinnerSystemType.IsVector2(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Vector2;
					data.FieldIndex = vector4ArrayCount;
					vector4ArrayCount++;
				}
				else if (SkinnerSystemType.IsVector3(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Vector3;
					data.FieldIndex = vector4ArrayCount;
					vector4ArrayCount++;
				}
				else if (SkinnerSystemType.IsVector4(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Vector4;
					data.FieldIndex = vector4ArrayCount;
					vector4ArrayCount++;
				}
				else if (SkinnerSystemType.IsChar(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Character;
					data.FieldIndex = stringArrayCount;
					stringArrayCount++;
				}
				else if (SkinnerSystemType.IsString(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.String;
					data.FieldIndex = stringArrayCount;
					stringArrayCount++;
				}
				else
				{
					isUnCorrect = true;
				}
				if (!isUnCorrect)
				{
					// 名前が未設定の場合は型の名前を出しておく
					var displayName = v.FieldDisplayName == null ? SkinnerEditorUtility.GetEditorName(v.FieldType.Name) : v.FieldDisplayName;
					data.DisplayName = new GUIContent(displayName);
					data.VariableData = v;
					userLogicVariableDisplayDatas.Add(data);
				}
			}

			#if !SKIP_LOGIC_CACHE

			m_CachedVariableDisplayDatas.Add(userLogicType, new UserLogicVariableRootData()
			{
				VariableDisplayDatas = userLogicVariableDisplayDatas,
				objectReferenceArrayCount = objectReferenceArrayCount,
				floatArrayCount = floatArrayCount,
				vector4ArrayCount = vector4ArrayCount,
				stringArrayCount = stringArrayCount,
			});

			#endif

			return true;
		}

	}

}
