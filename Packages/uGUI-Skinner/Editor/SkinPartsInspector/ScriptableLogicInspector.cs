// #define SKIP_LOGIC_CACHE
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Type = System.Type;
using Attribute = System.Attribute;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ScriptableLogic))]
	internal sealed class ScriptableLogicInspector : SkinPartsInspector
	{

		internal sealed class UserLogicVariableDisplayData
		{

			public GUIContent DisplayName;
			public SerializedPropertyType PropertyType;
			public UserLogicVariable VariableData;
			public int FieldIndex;
			public string[] PopupDisplayName;
			public Dictionary<Type, PropertyAttribute> FieldDefinedAttributes;
			public List<Type> PropertyTypeDefinedAttributes;
			public List<Attribute> PropertyTypeAttributes;
			public int[] PopupValue;

		}

		private sealed class UserLogicVariableRootData
		{
			public int ObjectReferenceArrayCount;
			public int FloatArrayCount;
			public int Vector4ArrayCount;
			public int StringArrayCount;

			public List<UserLogicVariableDisplayData> VariableDisplayDatas;
		}

		private static Dictionary<Type, UserLogicVariableRootData> m_CachedVariableDisplayDatas = new Dictionary<Type, UserLogicVariableRootData>();

		private UserLogic currentUserLogic;
		private UserLogicVariableRootData current = null;
		private SkinPartsPropertry validateProperty = new SkinPartsPropertry();

		public override bool allowCallDrawInspectorAfterApplySkin { get { return true; } }

		public override void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanObjectReferenceArrayWithFlexibleSize<Object>(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength);

			var userLogic = property.objectReferenceValues.GetArrayElementAtIndex(ScriptableLogic.LogicIndex).objectReferenceValue as UserLogic;
			bool isCorrect = CreateDisplayData(userLogic);
			if (isCorrect)
			{
				SkinnerEditorUtility.StripArray<Object>(property.objectReferenceValues, current.ObjectReferenceArrayCount + ScriptableLogic.RequiredObjectLength);
				int objectReferenceCount = 0;
				for (int i = 0; i < current.VariableDisplayDatas.Count; i++)
				{
					var v = current.VariableDisplayDatas[i];
					if (!SkinnerSystemType.IsObjectReferenceValue(v.VariableData.FieldType)) continue;
					SkinnerEditorUtility.CleanObject(property.objectReferenceValues, v.VariableData.FieldType, objectReferenceCount + ScriptableLogic.RequiredObjectLength);
					objectReferenceCount++;
				}
				SkinnerEditorUtility.CleanArray(property.floatValues, current.FloatArrayCount);
				SkinnerEditorUtility.CleanArray(property.vector4Values, current.Vector4ArrayCount);
				SkinnerEditorUtility.CleanArray(property.stringValues, current.StringArrayCount);
			}
			else
			{
				SkinnerEditorUtility.CleanArray<UserLogic>(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength);
				SkinnerEditorUtility.CleanArray(property.floatValues);
				SkinnerEditorUtility.CleanArray(property.vector4Values);
				SkinnerEditorUtility.CleanArray(property.stringValues);
			}
		}

		public override void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, ScriptableLogic.RequiredObjectLength, false);

			var logicProperty = property.objectReferenceValues.GetArrayElementAtIndex(ScriptableLogic.LogicIndex);
			var preSelectLogic = logicProperty.objectReferenceValue as UserLogic;
			SkinnerEditorGUILayout.ObjectField(SkinContent.Logic, logicProperty, typeof(UserLogic)); ;

			if (logicProperty.hasMultipleDifferentValues) return;

			var userLogic = logicProperty.objectReferenceValue as UserLogic;
			bool isCorrect = CreateDisplayData(userLogic);
			if (isCorrect)
			{
				SkinnerEditorUtility.ResetArray(property.objectReferenceValues, current.ObjectReferenceArrayCount + ScriptableLogic.RequiredObjectLength, false);
				SkinnerEditorUtility.ResetArray(property.floatValues, current.FloatArrayCount);
				SkinnerEditorUtility.ResetArray(property.vector4Values, current.Vector4ArrayCount);
				SkinnerEditorUtility.ResetArray(property.stringValues, current.StringArrayCount);

				logicProperty.objectReferenceValue = userLogic;

				if (preSelectLogic != userLogic)
				{
					InitializeFields(property);
				}

				for (int i = 0; i < current.VariableDisplayDatas.Count; i++)
				{
					var v = current.VariableDisplayDatas[i];
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
								if (v.FieldDefinedAttributes.ContainsKey(typeof(ColorUsageAttribute)))
								{
									var colorUsageData = v.FieldDefinedAttributes[typeof(ColorUsageAttribute)] as ColorUsageAttribute;
									SkinnerEditorGUILayout.ColorField(v.DisplayName, element, false, colorUsageData.showAlpha, colorUsageData.hdr);
								}
								else
								{
									SkinnerEditorGUILayout.ColorField(v.DisplayName, element);
								}
							}
							break;
						case SerializedPropertyType.Float:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								if (v.FieldDefinedAttributes.ContainsKey(typeof(RangeAttribute)))
								{
									var rangeData = v.FieldDefinedAttributes[typeof(RangeAttribute)] as RangeAttribute;
									SkinnerEditorGUILayout.Slider(v.DisplayName, element, rangeData.min, rangeData.max);
								}
								else
								{
									SkinnerEditorGUILayout.FloatField(v.DisplayName, element);
								}
							}
							break;
						case SerializedPropertyType.Integer:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								if (v.FieldDefinedAttributes.ContainsKey(typeof(RangeAttribute)))
								{
									var rangeData = v.FieldDefinedAttributes[typeof(RangeAttribute)] as RangeAttribute;
									SkinnerEditorGUILayout.IntSlider(v.DisplayName, element, rangeData.min.ToInt(), rangeData.max.ToInt());
								}
								else
								{
									SkinnerEditorGUILayout.IntField(v.DisplayName, element);
								}
							}
							break;
						case SerializedPropertyType.LayerMask:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.LayerMaskField(v.DisplayName, element);
							}
							break;
						case SerializedPropertyType.Enum:
							{
								var element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
								if (v.PropertyTypeDefinedAttributes.Contains(typeof(System.FlagsAttribute)))
								{
									SkinnerEditorGUILayout.MaskField(v.DisplayName, element, v.PopupDisplayName);
								}
								else
								{
									SkinnerEditorGUILayout.IntPopup(v.DisplayName, element, v.PopupDisplayName, v.PopupValue);
								}
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
						case SerializedPropertyType.Rect:
							{
								var element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
								SkinnerEditorGUILayout.RectField(v.DisplayName, element);
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
								if (v.FieldDefinedAttributes.ContainsKey(typeof(TextAreaAttribute)))
								{
									var textAreaData = v.FieldDefinedAttributes[typeof(TextAreaAttribute)] as TextAreaAttribute;
									EditorGUILayout.LabelField(v.DisplayName);
									int lineCount = element.stringValue.Count(c => c == '\n');
									lineCount = Mathf.Clamp(lineCount, textAreaData.minLines, textAreaData.maxLines);
									SkinnerEditorGUILayout.TextArea(element,
										GUILayout.Height(lineCount * EditorGUIUtility.singleLineHeight));
								}
								else
								{
									SkinnerEditorGUILayout.TextField(v.DisplayName, element);
								}
							}
							break;
					}
				}

				SkinnerEditorUtility.MapRuntimePropertyFromEditorProperty(validateProperty, property);
				validateProperty.objectReferenceValues.RemoveAt(ScriptableLogic.LogicIndex);
				UserLogicExtension.SetActiveUserLogic(userLogic);
				userLogic.ValidateProperty(validateProperty);
				UserLogicExtension.ReleaseActiveUserLogic();
				validateProperty.objectReferenceValues.Insert(ScriptableLogic.LogicIndex, userLogic);
				SkinnerEditorUtility.MapEditorPropertyFromRuntimeProperty(property, validateProperty);
			}
		}

		/// <summary>
		/// 表示データを元にフィールドを全て初期化する
		/// </summary>
		/// <param name="property">property</param>
		private void InitializeFields(EditorSkinPartsPropertry property)
		{
			for (int i = 0; i < current.VariableDisplayDatas.Count; i++)
			{
				var v = current.VariableDisplayDatas[i];

				SerializedProperty element = null;
				switch (v.PropertyType)
				{
					case SerializedPropertyType.ObjectReference:
						{
							element = property.objectReferenceValues.GetArrayElementAtIndex(v.FieldIndex + ScriptableLogic.RequiredObjectLength);
						}
						break;
					case SerializedPropertyType.Float:
					case SerializedPropertyType.Boolean:
					case SerializedPropertyType.Integer:
					case SerializedPropertyType.LayerMask:
					case SerializedPropertyType.Enum:
						{
							element = property.floatValues.GetArrayElementAtIndex(v.FieldIndex);
						}
						break;
					case SerializedPropertyType.Vector2:
					case SerializedPropertyType.Vector3:
					case SerializedPropertyType.Vector4:
					case SerializedPropertyType.Color:
					case SerializedPropertyType.Rect:
						{
							element = property.vector4Values.GetArrayElementAtIndex(v.FieldIndex);
						}
						break;
					case SerializedPropertyType.Character:
					case SerializedPropertyType.String:
						{
							element = property.stringValues.GetArrayElementAtIndex(v.FieldIndex);
						}
						break;
				}

				if (element != null)
				{
					SkinnerEditorUtility.FieldClean(element, v.VariableData.DefaultValue);
				}
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
				current = m_CachedVariableDisplayDatas[userLogicType];
				return true;
			}

			# endif

			var userLogicVariableDisplayDatas = new List<UserLogicVariableDisplayData>();
			var objectReferenceArrayCount = 0;
			var floatArrayCount = 0;
			var vector4ArrayCount = 0;
			var stringArrayCount = 0;
			var fieldAttributes = new List<Attribute>();
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
				else if (SkinnerSystemType.IsLayerMask(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.LayerMask;
					data.FieldIndex = floatArrayCount;
					floatArrayCount++;
				}
				else if (SkinnerSystemType.IsEnum(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Enum;
					SkinnerEditorUtility.GetPopupOptionsFromEnum(v.FieldType, out data.PopupDisplayName, out data.PopupValue);
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
				else if (SkinnerSystemType.IsRect(v.FieldType))
				{
					data.PropertyType = SerializedPropertyType.Rect;
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
					if (v.PropertyAttributes != null)
					{
						data.FieldDefinedAttributes = v.PropertyAttributes.ToDictionary(a => a.GetType(), a => a);
					}
					else
					{
						data.FieldDefinedAttributes = new Dictionary<Type, PropertyAttribute>();
					}
					data.PropertyTypeAttributes = v.FieldType.GetCustomAttributesWithBaseType().ToList();
					data.PropertyTypeDefinedAttributes = data.PropertyTypeAttributes.Select(a => a.GetType()).ToList();
					userLogicVariableDisplayDatas.Add(data);
				}
			}

			current = new UserLogicVariableRootData()
			{
				VariableDisplayDatas = userLogicVariableDisplayDatas,
				ObjectReferenceArrayCount = objectReferenceArrayCount,
				FloatArrayCount = floatArrayCount,
				Vector4ArrayCount = vector4ArrayCount,
				StringArrayCount = stringArrayCount,
			};

			#if !SKIP_LOGIC_CACHE

			m_CachedVariableDisplayDatas.Add(userLogicType, current);

			#endif

			return true;
		}

	}

}
