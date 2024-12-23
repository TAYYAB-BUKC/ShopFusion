﻿using System.ComponentModel.DataAnnotations;

namespace ShopFusion.Models.DTOs
{
	public class SignInRequestDTO
	{
		[Required(ErrorMessage = "Email is required.")]
		[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}