﻿namespace YAGO.FantasyWorld.Domain.Users
{
    /// <summary>
    /// Запрос регистрации
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователя
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}
