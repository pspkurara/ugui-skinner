using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 細かいツール
	/// </summary>
	internal static class SkinnerUtility
	{

		/// <summary>
		/// <see cref="object.ToString"/>した際に接頭子として使うインデント
		/// </summary>
		public const string ToStringIndent = "  ";

		/// <summary>
		/// <see cref="object.ToString"/>した際にフィールド型名の接尾子として使うセパレーター
		/// </summary>
		public const string ToStringSeparater = ":";

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
			int floatCount = 0;
			int vector4Count = 0;
			int stringCount = 0;
			foreach (var v in variables)
			{
				if (SkinnerSystemType.IsObjectReferenceValue(v.FieldType))
				{
					dic.Add(v.VariableId, objectReferenceCount);
					objectReferenceCount++;
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

		/// <summary>
		/// <see cref="SkinPartsPropertry">を<see cref="object.ToString"/>した際の各要素の文字列を<see cref="StringBuilder"/>に追加する
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="builder">追加対象の<see cref="StringBuilder"></param>
		/// <param name="property">プロパティの要素</param>
		internal static void AppendSkinPartsPropertyElementString<T>(StringBuilder builder, List<T> property)
		{
			if (property != null && property.Count > 0)
			{
				if (builder.Length > 0) builder.AppendLine();
				builder.Append(typeof(T).Name);
				builder.Append(ToStringSeparater);
				for (int i = 0; i < property.Count; i++)
				{
					builder.AppendLine();
					var v = property[i];
					builder.Append(ToStringIndent);
					builder.Append(v);
				}
			}
		}

		/// <summary>
		/// 引数が空じゃなかったら文字列を<see cref="StringBuilder"/>に追加する
		/// </summary>
		internal static void AppendIfStringNotEmpty(StringBuilder builder, string value)
		{
			if (value.Length > 0)
			{
				if (builder.Length > 0) builder.AppendLine();
				builder.Append(value);
			}
		}

		/// <summary>
		/// 引数の文字列にインデントを追加して返す
		/// </summary>
		/// <param name="value">インデントを追加したい文字列</param>
		/// <returns>インデント付き文字列</returns>
		internal static string ToIndentedString(string value, int indentCount = 1)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var v in value.Split('\n'))
			{
				if (v.Length > 0)
				{
					builder.AppendLine();
				}
				else
				{
					for (int i = 0; i < indentCount; i++)
					{
						builder.Append(ToStringIndent);
					}
					builder.AppendLine(v);
				}
			}
			// 最後の行を削除
			builder.Remove(builder.Length - 1, 1);
			return builder.ToString();
		}

	}

}
