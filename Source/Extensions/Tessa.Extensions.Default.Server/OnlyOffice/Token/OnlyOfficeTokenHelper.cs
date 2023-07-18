#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.OnlyOffice.Token
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с распределённым хранилищем контента.
    /// </summary>
    public static class OnlyOfficeTokenHelper
    {
        #region Constants

        /// <summary>
        /// Имя используемой схемы проверки подлинности.
        /// </summary>
        public const string AuthenticationScheme = "Bearer";

        /// <summary>
        /// Длина ключа, используемая в алгоритме RSA.
        /// </summary>
        public const int RsaKeySize = 2048;

        /// <summary>
        /// Алгоритм, используемый  при создании подписи JSON Web Token для выполнения операции с контентом версии файла.
        /// </summary>
        public const string FileTokenSignatureAlgorithm = SecurityAlgorithms.HmacSha256;

        /// <summary>
        /// Формат полей JWT токена содержащих данные типа <see cref="Guid"/>.
        /// </summary>
        public const string GuidFormat = "N";

        /// <summary>
        /// Формат полей JWT токена содержащих данные типа <see cref="Enum"/>.
        /// </summary>
        public const string EnumFormat = "d";

        /// <summary>
        /// Имя пользователя, для которого выдается токен
        /// </summary>
        public const string UserName = "onlyoffice";

        #endregion

        #region Public Methods

        #region TryGet Methods

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения, значение которого требуется получить.</param>
        /// <param name="value">Значение утверждения, если утверждение найдено, иначе значение по умолчанию для типа.</param>
        /// <returns>Значение <see langword="true"/>, если значение удалось успешно получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGet(this IEnumerable<Claim> claims, string name, out string? value)
        {
            Check.ArgumentNotNull(claims, nameof(claims));

            var claim = claims.FirstOrDefault(p => p.Type == name);

            if (claim is not null)
            {
                value = claim.Value;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения значение которого требуется получить.</param>
        /// <param name="value">Значение утверждения, если утверждение найдено, иначе значение по умолчанию для типа.</param>
        /// <returns>Значение <see langword="true"/>, если значение удалось успешно получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGet(this IEnumerable<Claim> claims, string name, out Guid value)
        {
            // Значения параметров будут проверены в TryGetClaim.

            if (claims.TryGet(name, out string? valueStr) && valueStr is not null)
            {
                value = Guid.ParseExact(valueStr, GuidFormat);
                return true;
            }

            value = Guid.Empty;
            return false;
        }

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения значение которого требуется получить.</param>
        /// <param name="value">Значение утверждения, если утверждение найдено, иначе значение по умолчанию для типа.</param>
        /// <returns>Значение <see langword="true"/>, если значение удалось успешно получить, иначе - <see langword="false"/>.</returns>
        public static bool TryGet(this IEnumerable<Claim> claims, string name, out int value)
        {
            // Значения параметров будут проверены в TryGetClaim.

            if (claims.TryGet(name, out string? valueStr) && valueStr is not null)
            {
                value = int.Parse(valueStr);
                return true;
            }

            value = default;
            return false;
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения значение которого требуется получить.</param>
        /// <returns>Значение утверждения.</returns>
        /// <exception cref="KeyNotFoundException">Утверждение <paramref name="name"/> не найдено.</exception>
        public static string? GetString(this IEnumerable<Claim> claims, string name) =>
            claims.TryGet(name, out string? value)
            ? value
            : throw GetExceptionClaimNotFound(name);

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения значение которого требуется получить.</param>
        /// <returns>Значение утверждения.</returns>
        /// <exception cref="KeyNotFoundException">Утверждение <paramref name="name"/> не найдено.</exception>
        public static Guid GetGuid(this IEnumerable<Claim> claims, string name) =>
            claims.TryGet(name, out string? valueStr) && valueStr is not null
            ? Guid.ParseExact(valueStr, GuidFormat)
            : throw GetExceptionClaimNotFound(name);

        /// <summary>
        /// Возвращает значение утверждения имеющее заданное имя.
        /// </summary>
        /// <param name="claims">Перечисление утверждений.</param>
        /// <param name="name">Имя утверждения значение которого требуется получить.</param>
        /// <returns>Значение утверждения.</returns>
        /// <exception cref="KeyNotFoundException">Утверждение <paramref name="name"/> не найдено.</exception>
        public static int GetInt32(this IEnumerable<Claim> claims, string name) =>
            claims.TryGet(name, out int value)
            ? value
            : throw GetExceptionClaimNotFound(name);

        #endregion

        /// <summary>
        /// Читает ключ в формате PEM содержащийся в указанном файле.
        /// </summary>
        /// <param name="path">Полный путь к файлу с ключом.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Ключ прочитанный из указанного файла.</returns>
        public static Task<string> ReadPemKeyAsync(
            string path,
            CancellationToken cancellationToken = default) =>
            File.ReadAllTextAsync(path, cancellationToken);

        /// <summary>
        /// Записывает ключ в формате PEM в указанный файл.
        /// </summary>
        /// <param name="path">Полный путь к файлу в который должен быть записан ключ.</param>
        /// <param name="key">Ключ в формате PEM.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task WritePemKeyAsync(
            string path,
            string key,
            CancellationToken cancellationToken = default) =>
            File.WriteAllTextAsync(path, key, cancellationToken);

        #endregion

        #region Private Methods

        private static Exception GetExceptionClaimNotFound(string claim) =>
            new KeyNotFoundException($"Claim \"{claim}\" is not found.");

        #endregion
    }
}
