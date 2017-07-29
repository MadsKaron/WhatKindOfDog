using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices;

namespace Tabs
{
	public class AzureManager
	{
		private static AzureManager instance;
		private MobileServiceClient client;
		private IMobileServiceTable<DogInformationModel> dogInformationTable; 

		private AzureManager()
		{
			
			this.client = new MobileServiceClient("http://myfurryfriends.azurewebsites.net/");
			this.dogInformationTable = this.client.GetTable<DogInformationModel>(); 
		}

		public MobileServiceClient AzureClient
		{
			get { return client; }
		}
		public static AzureManager AzureManagerInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new AzureManager();
				}
				return instance;
			}
		}

		public async Task<List<DogInformationModel>> GetDogInformation()
		{
			return await this.dogInformationTable.ToListAsync();
		}



	}
}
