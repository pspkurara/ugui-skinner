using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Type = System.Type;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// ScriptableObjectで挙動を変更できるスキンパーツのロジック本体
	/// 継承して挙動を記述する
	/// アセット化して保存していないと設定ができない
	/// </summary>
	/// <seealso cref="ScriptableLogic"/>
	public abstract class UserLogic : ScriptableObject, IUserLogicExtension
	{

		/// <summary>
		/// ユーザー変数の設定データ
		/// </summary>
		private List<UserLogicVariable> m_Variables = null;

		/// <summary>
		/// ユーザー変数のIDとインデックスマップ
		/// </summary>
		private Dictionary<int, int> m_VariableIdMap = null;

		/// <summary>
		/// ユーザー変数の設定データ
		/// </summary>
		public List<UserLogicVariable> variables {
			get {
				if (m_Variables == null)
				{
					m_Variables = new List<UserLogicVariable>();
					InsertUserLogicVariables(variables);
				}
				return m_Variables;
			}
		}

		/// <summary>
		/// ユーザー変数の設定データを生成する
		/// 変数追加の際は引数にインスタンスを追加する
		/// </summary>
		/// <param name="variables">空配列</param>
		protected abstract void InsertUserLogicVariables(List<UserLogicVariable> variables);

		/// <summary>
		/// 値をオブジェクトに反映させる
		/// <seealso cref="ISkinLogic.SetValues(SkinPartsPropertry)"/>から呼び出される
		/// </summary>
		/// <param name="property">プロパティ (ユーザーロジックの参照は持たない)</param>
		public abstract void SetValues(SkinPartsPropertry property);

		/// <summary>
		/// 変数の値の制限を行う
		/// 必要に応じてオーバーライドして使う
		/// </summary>
		/// <param name="property">property</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public virtual void ValidateProperty(SkinPartsPropertry property) { }

		#region 内部的に呼び出す

		/// <summary>
		/// 変数IDを元にフィールド配列インデックス取得する
		/// </summary>
		/// <param name="variableId">変数ID</param>
		/// <param name="valueIndex">変数番号</param>
		/// <returns>変数IDが見つかった場合は真</returns>
		bool IUserLogicExtension.TryGetValueIndex(int variableId, out int valueIndex)
		{
			if (m_VariableIdMap == null)
			{
				m_VariableIdMap = SkinnerUtility.CreateVariableIdToIndexDictionary(variables);
			}
			if (m_VariableIdMap.ContainsKey(variableId))
			{
				valueIndex = m_VariableIdMap[variableId];
				return true;
			}
			// 見つからないときはとりあえず0を返しておく
			valueIndex = 0;
			return false;
		}

		#endregion

	}

	/// <summary>
	/// スキンパーツのインスペクターに表示するユーザー変数
	/// </summary>
	public sealed class UserLogicVariable
	{

		/// <summary>
		/// 変数ID
		/// </summary>
		internal int VariableId { get { return RuntimeHelpers.GetHashCode(this); } }

		/// <summary>
		/// 変数の型
		/// </summary>
		public Type FieldType;

		/// <summary>
		/// 表示名
		/// </summary>
		public string FieldDisplayName;

	}

}
