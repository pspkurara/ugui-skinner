using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 見た目の個別データ
	/// </summary>
	[System.Serializable]
	public sealed class SkinParts : System.IEquatable<SkinParts>
	{

		#region 変数

		/// <summary>
		/// 見た目を反映させる仕組みの種類
		/// </summary>
		[SerializeField] private int m_Type;

		/// <summary>
		/// 見た目を反映されるときに参照される値
		/// </summary>
		[SerializeField] private SkinPartsPropertry m_Property;

		private ISkinLogic applySkinFunction;

		#endregion

		#region プロパティ

		/// <summary>
		/// 見た目を反映させる仕組みの種類
		/// </summary>
		public int type { get { return m_Type; } internal set { m_Type = value; } }

		/// <summary>
		/// 見た目を反映されるときに参照される値
		/// </summary>
		public SkinPartsPropertry property { get { return m_Property; }}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinParts"/>を初期化して生成
		/// </summary>
		public SkinParts()
		{
			m_Type = (int)default(SkinPartsType);
			m_Property = new SkinPartsPropertry();
			applySkinFunction = null;
		}

		/// <summary>
		/// <see cref="SkinParts"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製対象となるデータ</param>
		public SkinParts(SkinParts @base)
		{
			m_Type = @base.m_Type;
			m_Property = new SkinPartsPropertry(@base.m_Property);
			applySkinFunction = null;
		}

		#endregion

		/// <summary>
		/// 自身の複製を作る
		/// </summary>
		/// <returns>複製</returns>
		public SkinParts Clone()
		{
			return new SkinParts(this);
		}

		/// <summary>
		/// 見た目を反映する
		/// </summary>
		public void Apply()
		{
			//初回はタイプによって状態を変える
			if (applySkinFunction == null)
			{
				applySkinFunction = SkinPartsAccess.CreateSkinLogicInstance(type);
			}
			//値を適応する
			applySkinFunction.SetValues(new SkinLogicProperty(this));
		}

		/// <summary>
		/// 見た目を反映する
		/// </summary>
		internal void Apply(SkinStyle parentStyle, Stack<ISkinStyleParent> applyTrace)
		{
			//初回はタイプによって状態を変える
			if (applySkinFunction == null)
			{
				applySkinFunction = SkinPartsAccess.CreateSkinLogicInstance(type);
			}
			//値を適応する
			applySkinFunction.SetValues(new SkinLogicProperty(this, parentStyle, applyTrace));
		}

		/// <summary>
		/// インスペクター変更時コールバック
		/// </summary>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void OnValidate()
		{
			//空にすることでタイプが変わっても正常に動作するようにする
			applySkinFunction = null;
		}

		/// <summary>
		/// 引数に指定したものと自身の中身が一致するか調べる
		/// </summary>
		/// <param name="other">調査対象</param>
		/// <returns>一致する</returns>
		public bool Equals(SkinParts other)
		{
			if (m_Type != other.m_Type) return false;
			return m_Property.Equals(other);
		}

		/// <summary>
		/// 文字列に変換する
		/// クラスの変数の内容を出力する
		/// </summary>
		/// <returns>自身の中身の文字列</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("Type: ");
			var rootType = SkinPartsAccess.GetSkinPartsRootType(m_Type);
			if (rootType != null)
			{
				builder.Append(rootType.Name);
			}
			else
			{
				builder.Append(m_Type);
			}
			builder.AppendLine();

			var propertyString = m_Property.ToString();
			if (propertyString.Length > 0)
			{
				builder.AppendLine("Property:");
				builder.Append(SkinnerUtility.ToIndentedString(propertyString));
			}

			return builder.ToString();
		}

	}

}
