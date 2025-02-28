using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace VkNet.Abstractions;

/// <summary>
/// Методы для работы со стеной пользователя.
/// </summary>
public interface IWallCategoryAsync
{
	/// <summary>
	/// Возвращает список записей со стены пользователя или сообщества.
	/// </summary>
	/// <param name="params"> Входные параметры. </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// В случае успеха возвращается запрошенный список записей со стены.
	/// </returns>
	/// <exception cref="System.ArgumentException">
	/// OwnerID must be negative in case
	/// filter equal to Suggests;ownerId
	/// </exception>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.get
	/// </remarks>
	Task<WallGetObject> GetAsync(WallGetParams @params,
								bool skipAuthorization = false,
								CancellationToken token = default);

	/// <summary>
	/// Возвращает список комментариев к записи на стене.
	/// </summary>
	/// <param name="params"> Входные параметры выборки. </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает список объектов комментариев.
	/// Если был задан параметр need_likes=1, у объектов комментариев возвращается
	/// дополнительное поле likes:
	/// count — число пользователей, которым понравился комментарий;
	/// user_likes — наличие отметки «Мне нравится» от текущего пользователя
	/// (1 — есть, 0 — нет, CancellationToken token = default);
	/// can_like — информация о том, может ли текущий пользователь поставить отметку
	/// «Мне нравится»
	/// (1 — может, 0 — не может).
	/// Если был передан параметр start_comment_id, будет также возвращено поле
	/// real_offset – итоговое смещение данного
	/// подмножества комментариев (оно может быть отрицательным, если был указан
	/// отрицательный offset).
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.getComments
	/// </remarks>
	Task<WallGetCommentsResult> GetCommentsAsync(WallGetCommentsParams @params,
												bool skipAuthorization = false,
												CancellationToken token = default);

	/// <summary>
	/// Возвращает список записей со стен пользователей или сообществ по их
	/// идентификаторам.
	/// </summary>
	/// <param name="posts">
	/// Перечисленные через запятую идентификаторы, которые представляют собой идущие
	/// через знак подчеркивания id
	/// владельцев стен и id самих записей на стене.
	/// Пример значения posts:
	/// 93388_21539,93388_20904,-1_340364 список строк, разделенных через запятую,
	/// обязательный параметр (Список строк,
	/// разделенных через запятую, обязательный параметр).
	/// </param>
	/// <param name="extended">
	/// 1 - возвращает объекты пользователей и групп, необходимые для отображения
	/// записей. флаг, может
	/// принимать значения 1 или 0 (Флаг, может принимать значения 1 или 0).
	/// </param>
	/// <param name="copyHistoryDepth">
	/// Определяет размер массива copy_history, возвращаемого в ответе, если запись
	/// является репостом записи с другой
	/// стены.
	/// Например, copy_history_depth=1 — copy_history будет содержать один элемент с
	/// информацией о записи, прямым репостом
	/// которой является текущая.
	/// copy_history_depth=2 — copy_history будет содержать два элемента, добавляется
	/// информация о записи, репостом которой
	/// является первый элемент, и так далее (при условии, что иерархия репостов
	/// требуемой глубины для текущей записи
	/// существует). целое число, по умолчанию 2 (Целое число, по умолчанию 2).
	/// </param>
	/// <param name="fields">
	/// Список дополнительных полей для профилей и  групп, которые необходимо вернуть.
	/// См. описание полей объекта user и
	/// описание полей объекта group.
	/// Обратите внимание, этот параметр учитывается только при extended=1. список
	/// строк, разделенных через запятую (Список
	/// строк, разделенных через запятую).
	/// </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает список объектов записей со стены.
	/// Если был задан параметр extended=1, ответ содержит три отдельных списка:
	/// items — содержит объекты записей со стены;
	/// profiles — содержит объекты пользователей с дополнительными полями sex, photo,
	/// photo_medium_rec и online;
	/// groups — содержит объекты сообществ.
	/// Если запись является репостом записи с другой стены, в ответе дополнительно
	/// возвращается массив copy_history
	/// записей со стены, репостом которых является текущая.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.getById
	/// </remarks>
	Task<WallGetObject> GetByIdAsync(IEnumerable<string> posts,
									bool extended,
									long? copyHistoryDepth = null,
									ProfileFields fields = null,
									bool skipAuthorization = false,
									CancellationToken token = default);

	/// <summary>
	/// Возвращает список записей со стен пользователей или сообществ по их
	/// идентификаторам.
	/// </summary>
	/// <param name="posts">
	/// Перечисленные через запятую идентификаторы, которые представляют собой идущие
	/// через знак подчеркивания id
	/// владельцев стен и id самих записей на стене.
	/// Пример значения posts:
	/// 93388_21539,93388_20904,-1_340364 список строк, разделенных через запятую,
	/// обязательный параметр (Список строк,
	/// разделенных через запятую, обязательный параметр).
	/// </param>
	/// <param name="copyHistoryDepth">
	/// Определяет размер массива copy_history, возвращаемого в ответе, если запись
	/// является репостом записи с другой
	/// стены.
	/// Например, copy_history_depth=1 — copy_history будет содержать один элемент с
	/// информацией о записи, прямым репостом
	/// которой является текущая.
	/// copy_history_depth=2 — copy_history будет содержать два элемента, добавляется
	/// информация о записи, репостом которой
	/// является первый элемент, и так далее (при условии, что иерархия репостов
	/// требуемой глубины для текущей записи
	/// существует). целое число, по умолчанию 2 (Целое число, по умолчанию 2).
	/// </param>
	/// <param name="fields">
	/// Список дополнительных полей для профилей и  групп, которые необходимо вернуть.
	/// См. описание полей объекта user и
	/// описание полей объекта group.
	/// Обратите внимание, этот параметр учитывается только при extended=1. список
	/// строк, разделенных через запятую (Список
	/// строк, разделенных через запятую).
	/// </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает список объектов записей со стены.
	/// Если был задан параметр extended=1, ответ содержит три отдельных списка:
	/// items — содержит объекты записей со стены;
	/// profiles — содержит объекты пользователей с дополнительными полями sex, photo,
	/// photo_medium_rec и online;
	/// groups — содержит объекты сообществ.
	/// Если запись является репостом записи с другой стены, в ответе дополнительно
	/// возвращается массив copy_history
	/// записей со стены, репостом которых является текущая.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.getById
	/// </remarks>
	Task<ReadOnlyCollection<Post>> GetByIdAsync(IEnumerable<string> posts,
												long? copyHistoryDepth = null,
												ProfileFields fields = null,
												bool skipAuthorization = false,
												CancellationToken token = default);

	/// <summary>
	/// Публикует новую запись на своей или чужой стене.
	/// </summary>
	/// <param name="params"> Входные параметры выборки. </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает идентификатор созданной записи (post_id).
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.post
	/// </remarks>
	Task<long> PostAsync(WallPostParams @params,
						CancellationToken token = default);

	/// <summary>
	/// Копирует объект на стену пользователя или сообщества.
	/// </summary>
	/// <param name="object">
	/// Строковый идентификатор объекта, который необходимо разместить на стене,
	/// например, wall66748_3675
	/// или wall-1_340364. строка, обязательный параметр (Строка, обязательный
	/// параметр).
	/// </param>
	/// <param name="message">
	/// Сопроводительный текст, который будет добавлен к записи с объектом. строка
	/// (Строка).
	/// </param>
	/// <param name="groupId">
	/// Идентификатор сообщества, на стене которого будет размещена запись с объектом.
	/// Если не указан,
	/// запись будет размещена на стене текущего пользователя. положительное число
	/// (Положительное число).
	/// </param>
	/// <param name="markAsAds"> Строка (Строка). </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает объект со следующими полями:
	/// success
	/// post_id — идентификатор созданной записи;
	/// reposts_count — количество репостов объекта с учетом осуществленного;
	/// likes_count — число отметок «Мне нравится» у объекта.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.repost
	/// </remarks>
	Task<RepostResult> RepostAsync(string @object,
									string message,
									long? groupId,
									bool markAsAds,
									CancellationToken token = default);

	/// <summary>
	/// Редактирует запись на стене.
	/// </summary>
	/// <param name="params"> Входные параметры выборки. </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает post_id — идентификатор отредактированного поста.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.edit
	/// </remarks>
	Task<long> EditAsync(WallEditParams @params,
						CancellationToken token = default);

	/// <summary>
	/// Удаляет запись со стены.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1)  целое число, по
	/// умолчанию идентификатор текущего
	/// пользователя (Целое число, по умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число
	/// (Положительное число).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.delete
	/// </remarks>
	Task<bool> DeleteAsync(long? ownerId = null,
							long? postId = null,
							CancellationToken token = default);

	/// <summary>
	/// Восстанавливает удаленную запись на стене пользователя или сообщества.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находилась
	/// удаленная запись.
	/// Обратите внимание, идентификатор сообщества в параметре owner_id необходимо
	/// указывать со знаком "-" — например,
	/// owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)
	/// целое число, по умолчанию идентификатор
	/// текущего пользователя (Целое число, по умолчанию идентификатор текущего
	/// пользователя).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число
	/// (Положительное число).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.restore
	/// </remarks>
	Task<bool> RestoreAsync(long? ownerId = null,
							long? postId = null,
							CancellationToken token = default);

	/// <summary>
	/// Добавляет комментарий к записи на стене.
	/// </summary>
	/// <param name="params"> Входные параметры выборки. </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает идентификатор добавленного комментария
	/// (comment_id).
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.createComment
	/// </remarks>
	Task<long> CreateCommentAsync(WallCreateCommentParams @params,
								CancellationToken token = default);

	/// <summary>
	/// Удаляет комментарий текущего пользователя к записи на своей или чужой стене.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя, на чьей стене находится комментарий к записи.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1)  целое число, по
	/// умолчанию идентификатор текущего
	/// пользователя (Целое число, по умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="commentId">
	/// Идентификатор комментария. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.deleteComment
	/// </remarks>
	Task<bool> DeleteCommentAsync(long? ownerId,
								long commentId,
								CancellationToken token = default);

	/// <summary>
	/// Восстанавливает комментарий текущего пользователя к записи на своей или чужой
	/// стене.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится
	/// комментарий к записи.
	/// Обратите внимание, идентификатор сообщества в параметре owner_id необходимо
	/// указывать со знаком "-" — например,
	/// owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)
	/// целое число, по умолчанию идентификатор
	/// текущего пользователя (Целое число, по умолчанию идентификатор текущего
	/// пользователя).
	/// </param>
	/// <param name="commentId">
	/// Идентификатор комментария на стене. целое число, обязательный параметр (Целое
	/// число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.restoreComment
	/// </remarks>
	Task<bool> RestoreCommentAsync(long commentId,
									long? ownerId,
									CancellationToken token = default);

	/// <summary>
	/// Метод, позволяющий осуществлять поиск по стенам пользователей.
	/// </summary>
	/// <param name="params"> Входные параметры выборки. </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает список объектов записей на стене.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.search
	/// </remarks>
	Task<WallGetObject> SearchAsync(WallSearchParams @params,
									bool skipAuthorization = false,
									CancellationToken token = default);

	/// <summary>
	/// Позволяет получать список репостов заданной записи.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Если параметр не
	/// задан, то он считается равным идентификатору текущего пользователя. Обратите
	/// внимание, идентификатор сообщества в
	/// параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1
	/// соответствует идентификатору
	/// сообщества ВКонтакте API (club1)  целое число, по умолчанию идентификатор
	/// текущего пользователя (Целое число, по
	/// умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число
	/// (Положительное число).
	/// </param>
	/// <param name="offset">
	/// Смещение, необходимое для выборки определенного подмножества записей.
	/// положительное число
	/// (Положительное число).
	/// </param>
	/// <param name="count">
	/// Количество записей, которое необходимо получить. положительное число, по
	/// умолчанию 20, максимальное
	/// значение 1000 (Положительное число, по умолчанию 20, максимальное значение
	/// 1000).
	/// </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает объект, содержащий поля:
	/// items — содержит массив записей-репостов;
	/// profiles — содержит объекты пользователей с дополнительными полями sex, online,
	/// photo, photo_medium_rec,
	/// screen_name;
	/// groups — содержит информацию о сообществах.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.getReposts
	/// </remarks>
	Task<WallGetObject> GetRepostsAsync(long? ownerId,
										long? postId,
										long? offset,
										long? count,
										bool skipAuthorization = false,
										CancellationToken token = default);

	/// <summary>
	/// Закрепляет запись на стене (запись будет отображаться выше остальных).
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1)  целое число, по
	/// умолчанию идентификатор текущего
	/// пользователя (Целое число, по умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.pin
	/// </remarks>
	Task<bool> PinAsync(long postId,
						long? ownerId = null,
						CancellationToken token = default);

	/// <summary>
	/// Отменяет закрепление записи на стене.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1)  целое число, по
	/// умолчанию идентификатор текущего
	/// пользователя (Целое число, по умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.unpin
	/// </remarks>
	Task<bool> UnpinAsync(long postId,
						long? ownerId = null,
						CancellationToken token = default);

	/// <summary>
	/// Редактирует комментарий на стене пользователя или сообщества.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор владельца стены. целое число, по умолчанию идентификатор текущего
	/// пользователя
	/// (Целое число, по умолчанию идентификатор текущего пользователя).
	/// </param>
	/// <param name="commentId">
	/// Идентификатор комментария, который необходимо отредактировать. положительное
	/// число,
	/// обязательный параметр (Положительное число, обязательный параметр).
	/// </param>
	/// <param name="message"> Новый текст комментария. строка (Строка). </param>
	/// <param name="attachments">
	/// Новые вложения к комментарию. список строк, разделенных через запятую (Список
	/// строк,
	/// разделенных через запятую).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.editComment
	/// </remarks>
	Task<bool> EditCommentAsync(long commentId,
								string message,
								long? ownerId = null,
								IEnumerable<MediaAttachment> attachments = null,
								CancellationToken token = default);

	/// <summary>
	/// Позволяет пожаловаться на запись.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, которому принадлежит запись. целое
	/// число, обязательный
	/// параметр (Целое число, обязательный параметр).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи. положительное число, обязательный параметр (Положительное
	/// число,
	/// обязательный параметр).
	/// </param>
	/// <param name="reason">
	/// Причина жалобы:
	/// 0 — спам;
	/// 1 — детская порнография;
	/// 2 — экстремизм;
	/// 3 — насилие;
	/// 4 — пропаганда наркотиков;
	/// 5 — материал для взрослых;
	/// 6 — оскорбление.
	/// положительное число (Положительное число).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.reportPost
	/// </remarks>
	Task<bool> ReportPostAsync(long ownerId,
								long postId,
								ReportReason? reason = null,
								CancellationToken token = default);

	/// <summary>
	/// Позволяет пожаловаться на комментарий к записи.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, которому принадлежит комментарий.
	/// целое число,
	/// обязательный параметр (Целое число, обязательный параметр).
	/// </param>
	/// <param name="commentId">
	/// Идентификатор комментария. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="reason">
	/// Причина жалобы:
	/// 0 — спам;
	/// 1 — детская порнография;
	/// 2 — экстремизм;
	/// 3 — насилие;
	/// 4 — пропаганда наркотиков;
	/// 5 — материал для взрослых;
	/// 6 — оскорбление.
	/// положительное число (Положительное число).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.reportComment
	/// </remarks>
	Task<bool> ReportCommentAsync(long ownerId,
								long commentId,
								ReportReason? reason,
								CancellationToken token = default);

	/// <summary>
	/// Позволяет отредактировать скрытую запись.
	/// </summary>
	/// <param name="params"> Параметры запроса </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.editAdsStealth
	/// </remarks>
	Task<bool> EditAdsStealthAsync(EditAdsStealthParams @params,
									CancellationToken token = default);

	/// <summary>
	/// Позволяет создать скрытую запись,
	/// которая не попадает на стену сообщества и в дальнейшем может быть использована
	/// для создания рекламного объявления типа "Запись в сообществе".
	/// </summary>
	/// <param name="params"> Параметры запроса </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// Идентификатор созданной записи
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.postAdsStealth
	/// </remarks>
	Task<long> PostAdsStealthAsync(PostAdsStealthParams @params,
									CancellationToken token = default);

	/// <summary>
	/// Включает комментирование записи
	/// Работает только с конкретными записями, комментирование которых было
	/// выключено с помощью wall.closeComments
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.openComments
	/// </remarks>
	Task<bool> OpenCommentsAsync(long ownerId,
								long postId,
								CancellationToken token = default);

	/// <summary>
	/// Выключает комментирование записи
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор пользователя или сообщества, на стене которого находится запись.
	/// Обратите внимание,
	/// идентификатор сообщества в параметре owner_id необходимо указывать со знаком
	/// "-" — например, owner_id=-1
	/// соответствует идентификатору сообщества ВКонтакте API (club1).
	/// </param>
	/// <param name="postId">
	/// Идентификатор записи на стене. положительное число, обязательный параметр
	/// (Положительное число,
	/// обязательный параметр).
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.closeComments
	/// </remarks>
	Task<bool> CloseCommentsAsync(long ownerId,
								long postId,
								CancellationToken token = default);

	/// <summary>
	/// Проверяет ссылку для указания источника.
	/// </summary>
	/// <param name="link">
	/// Ссылка на источник. Поддерживаются внешние и внутренние ссылки.
	/// </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте https://vk.com/dev/wall.checkCopyrightLink
	/// </remarks>
	Task<bool> CheckCopyrightLinkAsync(string link,
										CancellationToken token = default);

	/// <summary>
	/// Получает информацию о комментарии на стене.
	/// </summary>
	/// <param name="ownerId">
	/// Идентификатор владельца стены (для сообществ — со знаком «минус»).
	/// </param>
	/// <param name="commentId">
	/// Идентификатор комментария.
	/// </param>
	/// <param name="extended">
	/// <c>true</c> — в ответе будут возвращены дополнительные поля profiles и groups,
	/// содержащие информацию о пользователях и сообществах. По умолчанию: <c>false</c>.
	/// </param>
	/// <param name="fields">
	/// Список дополнительных полей для профилей и сообществ, которые необходимо вернуть.
	/// Обратите внимание, этот параметр учитывается только при extended = <c>true</c>.
	/// </param>
	/// <param name="skipAuthorization"> Если <c> true </c>, то пропустить авторизацию </param>
	/// <param name="token">Токен отмены</param>
	/// <returns>
	/// После успешного выполнения возвращает <c> true </c>.
	/// </returns>
	/// <remarks>
	/// Страница документации ВКонтакте http://vk.com/dev/wall.getComment
	/// </remarks>
	Task<WallGetCommentResult> GetCommentAsync(int ownerId,
												int commentId,
												bool? extended = null,
												string fields = null,
												bool skipAuthorization = false,
												CancellationToken token = default);
}