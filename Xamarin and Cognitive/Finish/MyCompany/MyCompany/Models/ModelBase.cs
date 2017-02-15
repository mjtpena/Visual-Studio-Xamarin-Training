using System;
using Microsoft.WindowsAzure.MobileServices;
using MyCompany.Common;
using Newtonsoft.Json;

namespace MyCompany
{
	public class ModelBase : ObservableObject
	{
		public ModelBase()
		{
		}

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }


		[Version]
		public string Version { get; set; }
	}
}
