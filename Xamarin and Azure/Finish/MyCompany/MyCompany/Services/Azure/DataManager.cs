using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

using MyCompany.Models;

namespace MyCompany
{
	public class DataManager
	{
		static DataManager defaultInstance = new DataManager();
		MobileServiceClient client;

		private DataManager()
		{
			Initialize();
		}

		public static DataManager DefaultManager
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public MobileServiceClient CurrentClient
		{
			get { return client; }
		}

		private void Initialize()
		{
			this.client = new MobileServiceClient(
			   Constants.ApplicationURL);

			//Define your tables here

			var store = new MobileServiceSQLiteStore("localstore.db");
			store.DefineTable<Product>();
			store.DefineTable<Employee>();

			//Initializes the SyncContext using the default IMobileServiceSyncHandler.
			this.client.SyncContext.InitializeAsync(store);
		}

		public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : ModelBase
		{
			try
			{
				await this.SyncAsync<T>();

				return await this.client.GetSyncTable<T>().ToEnumerableAsync();
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task SyncAsync<T>() where T : ModelBase
		{
			ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
			var identifier = typeof(T).Name;
			try
			{
				await this.client.SyncContext.PushAsync();

				await client.GetSyncTable<T>().PullAsync($"all{identifier}", this.client.GetSyncTable<T>().CreateQuery());
			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult != null)
				{
					syncErrors = exc.PushResult.Errors;
				}
			}

			// Simple error/conflict handling. A real application would handle the various errors like network conditions,
			// server conflicts and others via the IMobileServiceSyncHandler.
			if (syncErrors != null)
			{
				foreach (var error in syncErrors)
				{
					if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
					{
						//Update failed, reverting to server's copy.
						await error.CancelAndUpdateItemAsync(error.Result);
					}
					else
					{
						// Discard local change.
						await error.CancelAndDiscardItemAsync();
					}

					Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
				}
			}
		}

		public async Task SaveItemAsync<T>(T item) where T : ModelBase
		{
			if (item.Id == null)
			{
				await this.client.GetSyncTable<T>().InsertAsync(item);
			}
			else
			{
				await this.client.GetSyncTable<T>().UpdateAsync(item);
			}
		}

		public async Task DeleteItemAsync<T>(T item) where T : ModelBase
		{
			if (item.Id == null)
			{
				Debug.WriteLine(@"Item does not have an Id!");
			}
			else
			{
				await this.client.GetSyncTable<T>().DeleteAsync(item);
			}
		}
	}
}
