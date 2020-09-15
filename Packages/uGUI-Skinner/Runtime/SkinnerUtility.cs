using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 細かいツール
	/// </summary>
	internal static class SkinnerUtility
	{

		/// <summary>
		/// オブジェクトを強制的にリロードする
		/// </summary>
		/// <param name="component">リロードしたいオブジェクトのコンポーネント</param>
		public static void ReloadGameObject(Component component)
		{
			if (!component) return;
			bool activeSelf = component.gameObject.activeSelf;
			if (!activeSelf) return;
			component.gameObject.SetActive(false);
			component.gameObject.SetActive(activeSelf);
		}

		/// <summary>
		/// リストを指定数に合わせて正規化する
		/// </summary>
		/// <typeparam name="T">リストの種類</typeparam>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		/// <param name="defaultValue">新規追加の際の初期要素値</param>
		public static void ResetList<T>(List<T> resetList, int count, T defaultValue = default)
		{
			int listCount = resetList.Count;
			for (int i = listCount; i < count; i++)
			{
				resetList.Add(defaultValue);
			}
			resetList.RemoveRange(count, resetList.Count);
		}

		/// <summary>
		/// ユーザー変数設定データを元に<see cref="SkinPartsPropertry"/>の各値のインデックスを返す
		/// </summary>
		/// <param name="variables">ユーザー変数</param>
		/// <returns>変数IDをキーとしたインデックスのマップ</returns>
		internal static Dictionary<int, int> CreateVariableIdToIndexDictionary(IEnumerable<UserLogicVariable> variables)
		{
			var dic = new Dictionary<int, int>();
			int objectReferenceCount = 0;
			int intCount = 0;
			int floatCount = 0;
			int boolCount = 0;
			int colorCount = 0;
			int vector4Count = 0;
			int stringCount = 0;
			foreach (var v in variables)
			{
				if (SkinnerSystemType.IsObjectReferenceValue(v.FieldType))
				{
					dic.Add(v.VariableId, objectReferenceCount);
					objectReferenceCount++;
				}
				else if (SkinnerSystemType.IsColorValue(v.FieldType))
				{
					dic.Add(v.VariableId, colorCount);
					colorCount++;
				}
				else if (SkinnerSystemType.IsBoolValue(v.FieldType))
				{
					dic.Add(v.VariableId, boolCount);
					boolCount++;
				}
				else if (SkinnerSystemType.IsIntValue(v.FieldType))
				{
					dic.Add(v.VariableId, intCount);
					intCount++;
				}
				else if (SkinnerSystemType.IsFloatValue(v.FieldType))
				{
					dic.Add(v.VariableId, floatCount);
					floatCount++;
				}
				else if (SkinnerSystemType.IsVector4Value(v.FieldType))
				{
					dic.Add(v.VariableId, vector4Count);
					vector4Count++;
				}
				else if (SkinnerSystemType.IsStringValue(v.FieldType))
				{
					dic.Add(v.VariableId, stringCount);
					stringCount++;
				}
			}
			return dic;
		}

	}

}
