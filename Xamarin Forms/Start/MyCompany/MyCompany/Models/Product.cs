using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCompany.Models
{
	public class Product
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("price")]
		public string Price { get; set; }
		[JsonProperty("options")]
		public IEnumerable<Option> Options { get; set; }
		[JsonProperty("image")]
		public string Image { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("soldout")]
		public bool? SoldOut { get; set; }
	}

	public class Option
	{
		[JsonProperty("size")]
		public string Size { get; set; }
		[JsonProperty("color")]
		public string Color { get; set; }
		[JsonProperty("oneOfAKind")]
		public bool? OneOfAKind { get; set; }
	}
}
