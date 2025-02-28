using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace VkNet.Abstractions.Category;

/// <summary>
/// Методы этой секции предоставляют дополнительную возможность управления состоянием заказов, которые были сделаны пользователями в приложениях.
/// </summary>
public interface IOrdersCategoryAsync
{
	/// <summary>
	/// Отменяет подписку.
	/// </summary>
	/// <param name = "userId">
	/// Идентификатор пользователя. положительное число, обязательный параметр
	/// </param>
	/// <param name = "subscriptionId">
	/// Идентификатор подписки. положительное число, обязательный параметр
	/// </param>
	/// <param name = "pendingCancel">
	/// 1 — отключить подписку по истечении текущего оплаченного периода;
	/// 0 — отключить подписку сразу. флаг, может принимать значения 1 или 0, по умолчанию 0
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает 1. При отмене подписки на адрес обратного вызова будет отправлено платёжное уведомление с типом subscription_status_change.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.cancelSubscription
	/// </remarks>
	Task<bool> CancelSubscriptionAsync(ulong userId,
										ulong subscriptionId,
										bool? pendingCancel = null,
										CancellationToken token = default);

	/// <summary>
	/// Изменяет состояние заказа.
	/// </summary>
	/// <param name = "orderId">
	/// Идентификатор заказа. положительное число, обязательный параметр
	/// </param>
	/// <param name = "action">
	/// Действие, которое нужно произвести с заказом.
	/// Возможные действия:
	/// cancel — отменить неподтверждённый заказ.
	/// charge — подтвердить неподтверждённый заказ. Применяется только если не удалось обработать уведомление order_change_state.
	/// refund — отменить подтверждённый заказ.
	/// строка, обязательный параметр
	/// </param>
	/// <param name = "appOrderId">
	/// Внутренний идентификатор заказа в приложении. положительное число
	/// </param>
	/// <param name = "testMode">
	/// Если этот параметр равен 1, изменяется состояние заказа тестового режима. По умолчанию 0. флаг, может принимать значения 1 или 0
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает новый статус заказа.
	/// Статусы заказа
	/// chargeable — неподтверждённый заказ. В это состояние заказы попадают в случае, если магазин не обрабатывает уведомления.
	/// declined — отменённый заказ на этапе получения информации о товаре, например, была получена ошибка 20, "Товара не существует". В это состояние заказ может попасть из состояния chargeable.
	/// cancelled — отменённый заказ. В это состояние заказ может попасть из состояния chargeable.
	/// charged — оплаченный заказ. В это состояние заказ может попасть из состояния chargeable, либо сразу после оплаты, если приложение обрабатывает уведомления.
	/// refunded — отменённый после оплаты заказ, голоса возвращены пользователю.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.changeState
	/// </remarks>
	Task<OrderState> ChangeStateAsync(ulong orderId,
									OrderStateAction action,
									ulong? appOrderId = null,
									bool? testMode = null,
									CancellationToken token = default);

	/// <summary>
	/// Возвращает список заказов.
	/// </summary>
	/// <param name = "offset">
	/// Смещение относительно самого нового найденного заказа для выборки определенного подмножества. По умолчанию 0. положительное число, по умолчанию 0
	/// </param>
	/// <param name = "count">
	/// Количество возвращаемых заказов. положительное число, максимальное значение 1000, по умолчанию 100
	/// </param>
	/// <param name = "testMode">
	/// Если этот параметр равен 1, возвращается список заказов тестового режима. По умолчанию 0. флаг, может принимать значения 1 или 0
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает массив найденных заказов, отсортированный по дате в обратном порядке (самый новый в начале).
	/// Статусы заказа
	/// chargeable — неподтверждённый заказ. В это состояние заказы попадают в случае, если магазин не обрабатывает уведомления.
	/// declined — отменённый заказ на этапе получения информации о товаре, например, была получена ошибка 20, "Товара не существует". В это состояние заказ может попасть из состояния chargeable.
	/// cancelled — отменённый заказ. В это состояние заказ может попасть из состояния chargeable.
	/// charged — оплаченный заказ. В это состояние заказ может попасть из состояния chargeable, либо сразу после оплаты, если приложение обрабатывает уведомления.
	/// refunded — отменённый после оплаты заказ, голоса возвращены пользователю.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.get
	/// </remarks>
	Task<IEnumerable<Order>> GetAsync(ulong? offset = null,
									ulong? count = null,
									bool? testMode = null,
									CancellationToken token = default);

	/// <summary>
	/// Возвращает стоимость голосов в валюте пользователя.
	/// </summary>
	/// <param name = "userId">
	/// Идентификатор пользователя положительное число, обязательный параметр
	/// </param>
	/// <param name = "votes">
	/// Список голосов. Например: 1,7,77 список слов, разделенных через запятую, обязательный параметр, количество элементов должно составлять не более 100
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает валюту пользователя и массив результатов для каждого значения из votes.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.getAmount
	/// </remarks>
	Task<IEnumerable<VotesAmount>> GetAmountAsync(ulong userId,
												IEnumerable<string> votes,
												CancellationToken token = default);

	/// <summary>
	/// Возвращает информацию об отдельном заказе.
	/// </summary>
	/// <param name = "orderIds">
	/// Идентификаторы заказов (при запросе информации о нескольких заказах). список положительных чисел, разделенных запятыми
	/// </param>
	/// <param name = "testMode">
	/// Если этот параметр равен 1, возвращаются заказы тестового режима. По умолчанию 0. флаг, может принимать значения 1 или 0
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращается массив найденных заказов, отсортированный по дате в обратном порядке (самый новый в начале).
	/// Статусы заказа
	/// chargeable — неподтверждённый заказ. В это состояние заказы попадают в случае, если магазин не обрабатывает уведомления.
	/// declined — отменённый заказ на этапе получения информации о товаре, например, была получена ошибка 20, "Товара не существует". В это состояние заказ может попасть из состояния chargeable.
	/// cancelled — отменённый заказ. В это состояние заказ может попасть из состояния chargeable.
	/// charged — оплаченный заказ. В это состояние заказ может попасть из состояния chargeable, либо сразу после оплаты, если приложение обрабатывает уведомления.
	/// refunded — отменённый после оплаты заказ, голоса возвращены пользователю.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.getById
	/// </remarks>
	Task<IEnumerable<Order>> GetByIdAsync(IEnumerable<ulong> orderIds = null,
										bool? testMode = null,
										CancellationToken token = default);

	/// <summary>
	/// Получает информацию о подписке по её идентификатору.
	/// </summary>
	/// <param name = "userId">
	/// Идентификатор пользователя. положительное число, обязательный параметр
	/// </param>
	/// <param name = "subscriptionId">
	/// Идентификатор подписки. положительное число, обязательный параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает объект, описывающий подписку. Содержит следующие поля:
	/// id (integer) — идентификатор подписки.
	/// item_id (string) — идентификатор товара в приложении.
	/// status (string) — статус подписки. Возможные значения:
	/// chargeable — неподтвержденная подписка;
	/// active — подписка активна;
	/// cancelled — подписка отменена.
	/// price (integer) — стоимость подписки.
	/// period (integer) — период подписки.
	/// create_time (integer) — дата создания в Unixtime.
	/// update_time (integer) — дата обновления в Unixtime.
	/// period_start_time (integer) — дата начала периода в Unixtime.
	/// next_bill_time (integer) — дата следующего платежа в Unixtime (если status = active).
	/// trial_expire_time (integer) — дата истечения триал-периода (если есть).
	/// pending_cancel (boolean, [true]) — true, если подписка ожидает отмены.
	/// cancel_reason (string) — причина отмены (если есть). Возможные значения:
	/// user_decision — по инициативе пользователя;
	/// app_decision — по инициативе приложения;
	/// payment_fail — из-за проблемы с платежом;
	/// unknown — причина неизвестна.
	/// test_mode (boolean, [true]) — true, если используется тестовый режим.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.getUserSubscriptionById
	/// </remarks>
	Task<SubscriptionItem> GetUserSubscriptionByIdAsync(ulong userId,
														ulong subscriptionId,
														CancellationToken token = default);

	/// <summary>
	/// Получает список активных подписок пользователя.
	/// </summary>
	/// <param name = "userId">
	/// Идентификатор пользователя, подписки которого необходимо получить. положительное число, обязательный параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает объект, содержащий число результатов в поле count и массив объектов, описывающих подписку, в поле items. Каждый объект массива items содержит следующие поля:
	/// id (integer) — идентификатор подписки.
	/// item_id (string) — идентификатор товара в приложении.
	/// status (string) — статус подписки. Возможные значения:
	/// active — подписка активна.
	/// price (integer) — стоимость подписки.
	/// period (integer) — период подписки.
	/// create_time (integer) — дата создания в Unixtime.
	/// update_time (integer) — дата обновления в Unixtime.
	/// period_start_time (integer) — дата начала периода в Unixtime.
	/// next_bill_time (integer) — дата следующего платежа в Unixtime (если status = active).
	/// trial_expire_time (integer) — дата истечения триал-периода (если есть).
	/// pending_cancel (boolean, [true]) — true, если подписка ожидает отмены.
	/// cancel_reason (string) — причина отмены (если есть). Возможные значения:
	/// user_decision — по инициативе пользователя;
	/// app_decision — по инициативе приложения;
	/// payment_fail — из-за проблемы с платежом;
	/// unknown — причина неизвестна.
	/// test_mode (boolean, [true]) — true, если используется тестовый режим.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.getUserSubscriptions
	/// </remarks>
	Task<IEnumerable<SubscriptionItem>> GetUserSubscriptionsAsync(ulong userId,
																CancellationToken token = default);

	/// <summary>
	/// Обновляет цену подписки для пользователя.
	/// </summary>
	/// <param name = "userId">
	/// Идентификатор пользователя. положительное число, обязательный параметр
	/// </param>
	/// <param name = "subscriptionId">
	/// Идентификатор подписки. Подписка должна быть активна. положительное число, обязательный параметр
	/// </param>
	/// <param name = "price">
	/// Новая стоимость подписки (должна быть ниже, чем текущая). положительное число, обязательный параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает 1.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/orders.updateSubscription
	/// </remarks>
	Task<bool> UpdateSubscriptionAsync(ulong userId,
										ulong subscriptionId,
										ulong price,
										CancellationToken token = default);
}