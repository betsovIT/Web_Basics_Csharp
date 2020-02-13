﻿using SULS.App.Data;
using SULS.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SULS.App.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService()
        {
            this.db = new ApplicationDbContext();
        }

        public bool EmailExists(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            string userId = db.Users
                .FirstOrDefault(u => u.Username == username && u.Password == this.Hash(password))?.Id;

            return userId;
        }

        public void Register(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.Hash(password)                
            };

            this.db.Users.Add(user);
            db.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Any(u => u.Username == username);
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
