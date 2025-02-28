using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Model;

namespace VkNet.Abstractions.Category;

/// <summary>
/// Secure В этой секции представлены административные методы,
/// предназначенные для вызова от имени приложения с использованием стороннего
/// сервера.
/// Для использования этих методов необходимо применять специальную схему
/// авторизации.
/// Помимо стандартных параметров, указанных в описании методов,
/// к запросу необходимо добавлять параметр client_secret, содержащий значение из
/// поля «Защищенный ключ» в настройках приложения.
/// Обратите внимание, тестовый режим при работе с secure-методами не
/// поддерживается!
/// </summary>
public interface ISecureCategoryAsync
{
	/// <summary>
	/// Добавляет информацию о достижениях пользователя в приложении.
	/// </summary>
	/// <param name="userId">
	/// Идентификатор пользователя, для которого нужно записать данные.
	/// положительное число, по умолчанию идентификатор текущего пользователя,
	/// обязательный параметр
	/// </param>
	/// <param name="activityId">
	/// Идентификатор достижения. Доступные значения:
	/// 1 — достигнут новый уровень, работает аналогично secure.setUserLevel;
	/// 2 — заработано новое число очков;
	/// положительное число, отличное от 1 и 2 — выполнена миссия с идентификатором
	/// activity_id.
	/// положительное число, обязательный параметр
	/// </param>
	/// <param name="value">
	/// Номер уровня или заработанное количество очков (соответственно, для
	/// activity_id=1 и activity_id=2).
	/// Параметр игнорируется при значении activity_id, отличном от 1 и 2.
	/// положительное число, максимальное значение 10000
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.addAppEvent
	/// </remarks>
	Task<bool> AddAppEventAsync(ulong userId,
								ulong activityId,
								ulong? value = null,
								CancellationToken token = default);

	/// <summary>
	/// Позволяет проверять валидность пользователя в IFrame, Flash  и
	/// Standalone-приложениях с помощью передаваемого в приложения параметра
	/// access_token.
	/// </summary>
	/// <param name="token">
	/// Клиентский access_token строка
	/// </param>
	/// <param name="ip">
	/// Ip адрес пользователя. Обратите внимание, что пользователь может обращаться
	/// через ipv6, в этом случае обязательно передавать ipv6 адрес пользователя.
	/// Если параметр не передан – ip адрес проверен не будет. строка
	/// </param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>
	/// В случае успеха будет возвращен объект, содержащий следующие поля:
	/// success = 1
	/// user_id = идентификатор пользователя
	/// date = unixtime дата, когда access_token был сгенерирован
	/// expire = unixtime дата, когда access_token станет не валиден
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.checkToken
	/// </remarks>
	Task<CheckTokenResult> CheckTokenAsync(string token,
											string ip = null,
											CancellationToken cancellationToken = default);

	/// <summary>
	/// Возвращает платежный баланс (счет) приложения в сотых долях голоса.
	/// </summary>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает количество голосов (в сотых долях), которые есть на счете
	/// приложения.
	/// Например, если метод возвращает 5000, это означает, что на балансе приложения
	/// 50 голосов.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.getAppBalance
	/// </remarks>
	Task<ulong> GetAppBalanceAsync(CancellationToken token = default);

	/// <summary>
	/// Выводит список SMS-уведомлений, отосланных приложением с помощью метода
	/// secure.sendSMSNotification.
	/// </summary>
	/// <param name="userId">
	/// Фильтр по id пользователя, которому высылалось уведомление. положительное число
	/// </param>
	/// <param name="dateFrom">
	/// Фильтр по дате начала. Задается в виде UNIX-time. положительное число
	/// </param>
	/// <param name="dateTo">
	/// Фильтр по дате окончания. Задается в виде UNIX-time. положительное число
	/// </param>
	/// <param name="limit">
	/// Количество возвращаемых записей. По умолчанию 1000. положительное число, по
	/// умолчанию 1000, максимальное значение 1000
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает список SMS-уведомлений, отосланных приложением, отсортированных по
	/// убыванию даты и отфильтрованных с помощью параметров uid, date_from, date_to,
	/// limit.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.getSMSHistory
	/// </remarks>
	Task<ReadOnlyCollection<SmsHistoryItem>> GetSmsHistoryAsync(ulong? userId = null,
																DateTime? dateFrom = null,
																DateTime? dateTo = null,
																ulong? limit = null,
																CancellationToken token = default);

	/// <summary>
	/// Выводит историю транзакций по переводу голосов между пользователями и
	/// приложением.
	/// </summary>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает список транзакций, отсортированных по убыванию даты, и
	/// отфильтрованных с помощью параметров type, uid_from, uid_to, date_from,
	/// date_to, limit.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.getTransactionsHistory
	/// </remarks>
	Task<ReadOnlyCollection<Transaction>> GetTransactionsHistoryAsync(CancellationToken token = default);

	/// <summary>
	/// Возвращает ранее выставленный игровой уровень одного или нескольких
	/// пользователей в приложении.
	/// </summary>
	/// <param name="userIds">
	/// Идентификаторы пользователей, информацию об уровнях которых требуется получить.
	/// список целых чисел, разделенных запятыми, обязательный параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает значения игровых уровней пользователей в приложении.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.getUserLevel
	/// </remarks>
	Task<ReadOnlyCollection<SecureLevel>> GetUserLevelAsync(IEnumerable<long> userIds,
															CancellationToken token = default);

	/// <summary>
	/// Выдает пользователю стикер и открывает игровое достижение.
	/// </summary>
	/// <param name="userIds">
	/// Список id пользователей которым нужно открыть достижение список положительных
	/// чисел, разделенных запятыми, обязательный параметр
	/// </param>
	/// <param name="achievementId">
	/// Id игрового достижения на платформе игр положительное число, обязательный
	/// параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает список результатов выполнения в виде списка объектов:
	/// {
	/// "user_id": int,
	/// "status": string
	/// }
	/// status может принимать значения:
	/// OK - операция успешна
	/// ERROR_ACHIEVEMENT_ALREADY_OPENED - стикер уже выдан пользователю
	/// ERROR_UNKNOWN_ERROR - непредвиденная ошибка
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.giveEventSticker
	/// </remarks>
	Task<ReadOnlyCollection<EventSticker>> GiveEventStickerAsync(IEnumerable<ulong> userIds,
																ulong achievementId,
																CancellationToken token = default);

	/// <summary>
	/// Отправляет уведомление пользователю.
	/// </summary>
	/// <param name="message">
	/// Текст уведомления, который следует передавать в кодировке UTF-8 (максимум 254
	/// символа). строка, обязательный параметр
	/// </param>
	/// <param name="userIds">
	/// Перечисленные через запятую идентификаторы пользователей, которым отправляется
	/// уведомление (максимум 100 штук). список положительных чисел, разделенных
	/// запятыми
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает перечисленные через запятую ID пользователей, которым было успешно
	/// отправлено уведомление.
	/// Обратите внимание, нельзя отправлять пользователю более 1 уведомления в час (3
	/// в сутки). Кроме того, нельзя отправить одному пользователю два уведомления с
	/// одинаковым текстом подряд.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.sendNotification
	/// </remarks>
	Task<ReadOnlyCollection<ulong>> SendNotificationAsync(string message,
														IEnumerable<ulong> userIds = null,
														CancellationToken token = default);

	/// <summary>
	/// Отправляет SMS-уведомление на мобильный телефон пользователя.
	/// </summary>
	/// <param name="userId">
	/// Id пользователя, которому отправляется SMS-уведомление. Пользователь должен
	/// разрешить приложению отсылать ему уведомления (getUserSettings, +1).
	/// положительное число, обязательный параметр
	/// </param>
	/// <param name="message">
	/// Текст SMS, который следует передавать в кодировке UTF-8. Допускаются только
	/// латинские буквы и цифры. Максимальный размер - 160 символов. строка,
	/// обязательный параметр
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает 1 в случае успешной отсылки SMS.
	/// Если номер пользователя еще не известен системе, то метод вернет ошибку 146
	/// (The mobile number of the user is unknown). Для решения этой проблемы метод
	/// users.get возвращает поле has_mobile, которое позволяет определить, известен ли
	/// номер пользователя.
	/// Если номер пользователя неизвестен, но Вы хотели бы иметь возможность высылать
	/// ему SMS-уведомления, необходимо предложить ему ввести номер мобильного
	/// телефона, не отвлекая от приложения.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.sendSMSNotification
	/// </remarks>
	Task<bool> SendSmsNotificationAsync(ulong userId,
										string message,
										CancellationToken token = default);

	/// <summary>
	/// Устанавливает счетчик, который выводится пользователю жирным шрифтом в левом
	/// меню.
	/// </summary>
	/// <param name="counters">
	/// Позволяет устанавливать счетчики нескольким пользователям за один запрос.
	/// Значение следует указывать в следующем формате:
	/// user_id1:counter1[:increment],user_id2:counter2[:increment], пример:
	/// 66748:6:1,6492:2. В случае, если указан этот параметр, параметры counter,
	/// user_id и increment не учитываются. Можно передать не более 200 значений за
	/// один запрос. список слов, разделенных через запятую
	/// </param>
	/// <param name="userId">
	/// Идентификатор пользователя. положительное число
	/// </param>
	/// <param name="counter">
	/// Значение счетчика. целое число
	/// </param>
	/// <param name="increment">
	/// Определяет, нужно ли заменить значение счетчика или прибавить новое значение к
	/// уже имеющемуся. 1 — прибавить counter к старому значению, 0 — заменить счетчик
	/// (по умолчанию). флаг, может принимать значения 1 или 0
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Возвращает 1 в случае успешной установки счетчика.
	/// Если пользователь не установил приложение в левое меню, метод вернет ошибку 148
	/// (Access to the menu of the user denied). Избежать этой ошибки можно с помощью
	/// метода account.getAppPermissions.
	/// Вы также можете обращаться к этому методу при стандартном взаимодействии с
	/// клиентской стороны, указывая setCounter вместо secure.setCounter в названии
	/// метода. В этом случае параметр uid передавать не нужно, счетчик установится для
	/// текущего пользователя.
	/// Метод setCounter при стандартном, а не защищенном взаимодействии можно
	/// использовать для того, чтобы, например, сбрасывать счетчик при заходе
	/// пользователя в приложение.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/secure.setCounter
	/// </remarks>
	Task<bool> SetCounterAsync(IEnumerable<string> counters,
								ulong? userId = null,
								long? counter = null,
								bool? increment = null,
								CancellationToken token = default);
}