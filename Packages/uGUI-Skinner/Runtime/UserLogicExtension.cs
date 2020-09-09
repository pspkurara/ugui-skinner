using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="UserLogicExtension">が<see cref="UserLogic"/>からデータを受け取るためのインターフェイス
	/// </summary>
	internal interface IUserLogicExtension
	{

		/// <summary>
		/// 変数IDを元にフィールド配列インデックス取得する
		/// </summary>
		/// <param name="variableId">変数ID</param>
		/// <param name="valueIndex">変数番号</param>
		/// <returns>変数IDが見つかった場合は真</returns>
		bool TryGetValueIndex(int variableId, out int valueIndex);

	}

	/// <summary>
	/// <see cref="UserLogic"/>を記述する際に便利になる関数群
	/// 同期処理前提
	/// </summary>
	public static class UserLogicExtension
	{

		#region 内部メンバ

		/// <summary>
		/// 同期処理前提で動作する
		/// </summary>
		private static IUserLogicExtension m_ActiveUserLogic { get; set; }

		/// <summary>
		/// 指定したロジックをアクティブにする
		/// クラス内の関数が使える場所に来る直前に必ず呼び出す
		/// </summary>
		/// <param name="userLogic">アクティブにするユーザーロジック</param>
		internal static void SetActiveUserLogic(IUserLogicExtension userLogic)
		{
			m_ActiveUserLogic = userLogic;
		}

		/// <summary>
		/// ロジックを非アクティブにする
		/// クラス内の関数が使える場所が終了した直後に必ず呼び出す
		/// </summary>
		internal static void ReleaseActiveUserLogic()
		{
			m_ActiveUserLogic = null;
		}

		#endregion

		#region 取得系関数

		/// <summary>
		/// <see cref="SkinPartsPropertry.objectReferenceValues">を型変換して取得する
		/// </summary>
		/// <typeparam name="T">変換する型</typeparam>
		/// <param name="variable">ユーザー変数</param>
		public static T GetObjectReference<T>(this SkinPartsPropertry property, UserLogicVariable variable) where T : Object
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.objectReferenceValues[valueIndex] as T;
			}
			return null;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.boolValues">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static bool GetBool(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.boolValues[valueIndex];
			}
			return default;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.intValues">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static int GetInt(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.intValues[valueIndex];
			}
			return default;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.floatValues">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static float GetFloat(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.floatValues[valueIndex];
			}
			return default;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.colorValues">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static Color GetColor(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.colorValues[valueIndex];
			}
			return default;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.vector4Values">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static Vector4 GetVector4(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.vector4Values[valueIndex];
			}
			return default;
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.stringValues">を取得する
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static string GetString(this SkinPartsPropertry property, UserLogicVariable variable)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				return property.stringValues[valueIndex];
			}
			return default;
		}

		#endregion

		#region セット系関数

		/// <summary>
		/// <see cref="SkinPartsPropertry.objectReferenceValues">に値をセットする
		/// </summary>
		/// <typeparam name="T">変換する型</typeparam>
		/// <param name="variable">ユーザー変数</param>
		public static void SetObjectReference(this SkinPartsPropertry property, UserLogicVariable variable, Object value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.objectReferenceValues[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.boolValues">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetBool(this SkinPartsPropertry property, UserLogicVariable variable, bool value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.boolValues[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.intValues">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetInt(this SkinPartsPropertry property, UserLogicVariable variable, int value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.intValues[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.floatValues">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetFloat(this SkinPartsPropertry property, UserLogicVariable variable, float value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.floatValues[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.colorValues">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetColor(this SkinPartsPropertry property, UserLogicVariable variable, Color value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.colorValues[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.vector4Values">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetVector4(this SkinPartsPropertry property, UserLogicVariable variable, Vector4 value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.vector4Values[valueIndex] = value;
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.stringValues">に値をセットする
		/// </summary>
		/// <param name="variable">ユーザー変数</param>
		public static void SetString(this SkinPartsPropertry property, UserLogicVariable variable, string value)
		{
			int valueIndex;
			if (m_ActiveUserLogic.TryGetValueIndex(variable.VariableId, out valueIndex))
			{
				property.stringValues[valueIndex] = value;
			}
		}

		#endregion

	}

}
