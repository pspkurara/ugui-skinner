using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// プロパティパック
	/// </summary>
	[System.Serializable]
	public sealed class SkinPartsPropertry : SkinPartsPropertryWithoutObjectReference
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

		#endregion

	}

	/// <summary>
	/// プロパティパック
	/// オブジェクト参照は除く
	/// </summary>
	[System.Serializable]
	public class SkinPartsPropertryWithoutObjectReference
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

		#endregion

	}

}
