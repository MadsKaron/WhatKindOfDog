using System;
using Newtonsoft.Json;

namespace Tabs
{
	public class DogInformationModel
	{
		[JsonProperty(PropertyName = "id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "Name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
	}
}
