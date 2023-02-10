using System;
using Newtonsoft.Json;
using VkNet.Infrastructure;
using VkNet.Utils;

namespace VkNet.Model.GroupUpdate;

/// <summary>
/// Добавление участника или заявки на вступление в сообщество
/// </summary>
[Serializable]
public class DonutNew : IGroupUpdate
{
	/// <summary>
	/// Идентификатор пользователя
	/// </summary>
	[JsonProperty("user_id")]
	public long? UserId { get; set; }

	/// <summary>
	/// Цена в рублях
	/// </summary>
	[JsonProperty("amount")]
	public decimal? Amount { get; set; }

	/// <summary>
	/// Цена без комиссии (в рублях)
	/// </summary>
	[JsonProperty("amount_without_fee")]
	public decimal? AmountWithoutFee { get; set; }

	/// <summary>
	/// Разобрать из json.
	/// </summary>
	/// <param name="response"> Ответ сервера. </param>
	public static DonutNew FromJson(VkResponse response)
	{
		var groupJoin = JsonConvert.DeserializeObject<DonutNew>(response.ToString(), JsonConfigure.JsonSerializerSettings);
		groupJoin.UserId = response["user_id"];
		groupJoin.Amount = response["amount"];
		groupJoin.AmountWithoutFee = response["amount_without_fee"];

		return groupJoin;
	}

	/// <summary>
	/// Преобразование класса <see cref="DonutNew" /> в <see cref="VkParameters" />
	/// </summary>
	/// <param name="response"> Ответ сервера. </param>
	/// <returns> Результат преобразования в <see cref="DonutNew" /> </returns>
	public static implicit operator DonutNew(VkResponse response)
	{
		if (response == null)
		{
			return null;
		}

		return response.HasToken()
			? FromJson(response)
			: null;
	}
}