# [Xamarin.Forms](https://www.xamarin.com/forms) and [Azure](https://azure.microsoft.com)

## Prerequisites
1. Open the Solution file from the [Start Folder](https://github.com/mjtpena/Xamarin-Training/tree/master/Xamarin%20and%20Azure/Start/MyCompany)
2. Build the Project and Restore Nuget Packages.
3. Try running the application in either iOS, UWP or Android. You can do this by choosing the project platform (Android, iOS, UWP) and click Start/Run Green Button. You should see the following:

<Insert Image here>

## What is included in Start Project
* The same Solution folder from the Finish folder of Xamarin Forms folder / module.

## Create your Mobile App service from Azure
If you don't have an Azure subscription yet, register here for [Free](https://azure.microsoft.com/en-us/free/)

### Create a new Mobile App Service
On the search bar, look for **Mobile App Quickstart**
See the following for Visual Explanation on how to create an Azure Mobile App service.
Just need to make sure to supply the App Name and choose your Service Plan properly.
![](https://raw.githubusercontent.com/mjtpena/Xamarin-Training/master/Xamarin%20and%20Azure/Createa%20Azure%20Mobile%20App.gif)

### Upload the Employee.csv
In the Easy Tables tab, click on Import CSV, and import the Employee.csv from the [Mocks Folder](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Mocks/employee.csv)

![](https://raw.githubusercontent.com/mjtpena/Xamarin-Training/master/Xamarin%20and%20Azure/UploadCSV.png)

### Application URL

Change the Value of ApplicationURL from [Constants.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Start/MyCompany/MyCompany/Constants.cs). Enter your app service site url.

### Azure Folder

You will notice that we now have abstracted the services into interfaces. We also now have an Azure folder where we have an implementation for [EmployeeAzureService.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Start/MyCompany/MyCompany/Services/Azure/EmployeeAzureService.cs)

### See the services in Action

Change the following implementations from Mock Service to Azure Service:

* EmployeeService in GetEmployees() method from [EmployeeViewModel.cs class](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Start/MyCompany/MyCompany/ViewModels/EmployeeViewModel.cs#L38)
* EmployeeService in DeleteEmployee() method from [EmployeeViewModel.cs class](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Start/MyCompany/MyCompany/ViewModels/EmployeeViewModel.cs#L70)
* EmployeeService in SaveEmployee() method from [AddEmployeeViewModel.cs class](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20and%20Azure/Start/MyCompany/MyCompany/ViewModels/AddEmployeeViewModel.cs#L19)

### Run the App, and play along with Employee Page.


