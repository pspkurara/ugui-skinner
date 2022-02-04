using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// プロパティパック
	/// </summary>
	[System.Serializable]
	public sealed class SkinPartsPropertry : SkinPartsPropertryWithoutObjectReference, ISkinPartsProperty, System.IEquatable<SkinPartsPropertry>
	{

		#region 変数

		/// <summary>
		/// <see cref="UnityEngine.Object"/>系を保存
		/// </summary>
		[SerializeField] private List<Object> m_ObjectReferenceValues = new List<Object>();

		#endregion

		#region プロパティ

		/// <summary>
		/// Unityオブジェクト
		/// </summary>
		public List<Object> objectReferenceValues { get { return m_ObjectReferenceValues; } }

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinPartsPropertry"/>を初期化して生成
		/// </summary>
		public SkinPartsPropertry() : base() { }

		/// <summary>
		/// <see cref="SkinPartsPropertry"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製元となるオブジェクト</param>
		public SkinPartsPropertry(SkinPartsPropertry @base) :base(@base)
		{
			m_ObjectReferenceValues.AddRange(@base.m_ObjectReferenceValues);
		}

		#endregion

		#region メソッド

		/// <summary>
		/// 中身を消去する
		/// </summary>
		public override void Clear()
		{
			base.Clear();
			m_ObjectReferenceValues.Clear();
		}

		/// <summary>
		/// 引数に指定したものと自身の中身が一致するか調べる
		/// </summary>
		/// <param name="other">調査対象</param>
		/// <returns>一致する</returns>
		public bool Equals(SkinPartsPropertry other)
		{
			if (!ArrayHelper.ArrayEquals(m_ObjectReferenceValues, other.m_ObjectReferenceValues, (a, b) => a == b)) return false;
			return base.Equals(other);
		}

		/// <summary>
		/// 文字列に変換する
		/// クラスの変数の内容を出力する
		/// </summary>
		/// <returns>自身の中身の文字列</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			SkinnerUtility.AppendSkinPartsPropertyElementString(builder, m_ObjectReferenceValues);
			SkinnerUtility.AppendIfStringNotEmpty(builder, base.ToString());
			return builder.ToString();
		}

		#endregion

	}

	/// <summary>
	/// プロパティパック
	/// オブジェクト参照は除く
	/// </summary>
	[System.Serializable]
	public class SkinPartsPropertryWithoutObjectReference : System.IEquatable<SkinPartsPropertryWithoutObjectReference>
	{

		#region 変数

		/// <summary>
		/// 少数を保存
		/// </summary>
		[SerializeField] private List<float> m_FloatValues = new List<float>();

		/// <summary>
		/// Vector4を保存
		/// </summary>
		[SerializeField] private List<Vector4> m_Vector4Values = new List<Vector4>();

		/// <summary>
		/// 文字列を保存
		/// </summary>
		[SerializeField] private List<string> m_StringValues = new List<string>();

		#endregion

		#region プロパティ

		/// <summary>
		/// 少数値
		/// </summary>
		public List<float> floatValues { get { return m_FloatValues; } }

		/// <summary>
		/// Vector4
		/// </summary>
		public List<Vector4> vector4Values { get { return m_Vector4Values; } }

		/// <summary>
		/// 文字列
		/// </summary>
		public List<string> stringValues { get { return m_StringValues; } }

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference"/>を初期化して生成
		/// </summary>
		public SkinPartsPropertryWithoutObjectReference() { }

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製元となるオブジェクト</param>
		public SkinPartsPropertryWithoutObjectReference(SkinPartsPropertryWithoutObjectReference @base)
		{
			m_FloatValues.AddRange(@base.m_FloatValues);
			m_Vector4Values.AddRange(@base.m_Vector4Values);
			m_StringValues.AddRange(@base.m_StringValues);
		}

		#endregion

		#region メソッド

		/// <summary>
		/// 中身を消去する
		/// </summary>
		public virtual void Clear()
		{
			m_FloatValues.Clear();
			m_Vector4Values.Clear();
			m_StringValues.Clear();
		}

		/// <summary>
		/// 引数に指定したものと自身の中身が一致するか調べる
		/// </summary>
		/// <param name="other">調査対象</param>
		/// <returns>一致する</returns>
		public bool Equals(SkinPartsPropertryWithoutObjectReference other)
		{
			if (!ArrayHelper.ArrayEquals(m_FloatValues, other.m_FloatValues, (a, b) => Mathf.Approximately(a, b))) return false;
			if (!ArrayHelper.ArrayEquals(m_Vector4Values, other.m_Vector4Values, (a, b) => a == b)) return false;
			if (!ArrayHelper.ArrayEquals(m_StringValues, other.m_StringValues, (a, b) => string.Equals(a, b))) return false;
			return true;
		}
		
		/// <summary>
		/// 文字列に変換する
		/// クラスの変数の内容を出力する
		/// </summary>
		/// <returns>自身の中身の文字列</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			SkinnerUtility.AppendSkinPartsPropertyElementString(builder, m_FloatValues);
			SkinnerUtility.AppendSkinPartsPropertyElementString(builder, m_Vector4Values);
			SkinnerUtility.AppendSkinPartsPropertyElementString(builder, m_StringValues);
			return builder.ToString();
		}

		#endregion

	}

}
