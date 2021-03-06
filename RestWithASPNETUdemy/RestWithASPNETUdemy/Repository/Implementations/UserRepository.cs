﻿using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository.Implementations
{
	public class UserRepository : IUserRepository
	{
		private readonly MysqlContext _context;

		public UserRepository(MysqlContext context)
		{
			_context = context;
		}

		public User ValidateCredentials(UserVO user)
		{
			var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

			return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);
		}

		public User ValidateCredentials(string userName)
		{
			return _context.Users.SingleOrDefault(u => u.UserName == userName);
		}

		public bool RevokeToken(string userName)
		{
			var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
			if (user == null) return false;
			user.RefreshToken = null;
			_context.SaveChanges();
			return true;
		}

		private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
		{
			Byte[] inputBytes = Encoding.UTF8.GetBytes(input); 
			Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

			return BitConverter.ToString(hashedBytes);
		}

		public User RefreshUserInfo(User user)
		{
			if(!_context.Users.Any(u => u.Id == user.Id)) return null;

			var result = _context.Users.SingleOrDefault(i => i.Id.Equals(user.Id));
			if (result != null)
			{
				try
				{
					_context.Entry(result).CurrentValues.SetValues(user);
					_context.SaveChanges();
					return user;
				}
				catch (Exception)
				{

					throw;
				}
			}
			return result;
		}

	}
}
