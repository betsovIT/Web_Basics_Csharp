﻿using Andreys.Data;
using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Andreys.Services
{
    public class UsersService : IUsersService
    {
        private readonly AndreysDbContext db;

        public UsersService(AndreysDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            return this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == this.Hash(password))?.Id;
        }

        public bool IsEmailTaken(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameTaken(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        public void Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = this.Hash(password),
                Email = email
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
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
