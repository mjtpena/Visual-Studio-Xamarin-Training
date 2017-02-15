using System;
using Newtonsoft.Json;

namespace MyCompany.Models
{
	public class Employee
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("twitter")]
		public string Twitter { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("avatar")]
		public string Avatar { get; set; }
	}
}
