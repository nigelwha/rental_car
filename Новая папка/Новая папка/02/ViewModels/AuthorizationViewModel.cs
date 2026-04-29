using _02.Interfaces;
using _02.Models;
using Buro.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using _02.Data;

namespace _02.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private string _login = string.Empty;
        private readonly ApplicationDbContext _context;

        public static User? CurrentUser { get; private set; }

        public AuthorizationViewModel()
        {
            Title = "Авторизация";
            _context = new ApplicationDbContext();
        }

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        public IGettingPassword? GettingPassword { get; set; }
        private string Password => GettingPassword?.GetPassword()?.Trim() ?? string.Empty;

        public bool LogIn()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                    return false;

                string hash = HashPassword(Password);
                var user = _context.Users
                    .Include(u => u.Authorization)   // если после Scaffold свойство называется иначе (например, Authorizations), поправь
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Authorization != null &&
                                         u.Authorization.Login.Trim() == Login.Trim() &&
                                         u.Authorization.PasswordHash == hash);

                if (user != null)
                {
                    CurrentUser = user;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    "Ошибка подключения к БД:\n" + ex.Message,
                    "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }

        public static void LogOut() => CurrentUser = null;

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}