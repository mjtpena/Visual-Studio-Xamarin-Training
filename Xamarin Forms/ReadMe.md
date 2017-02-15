# Introduction to [Xamarin.Forms](https://www.xamarin.com/forms) 

## Prerequisites
1. Open the Solution file from the [Start Folder](https://github.com/mjtpena/Xamarin-Training/tree/master/Xamarin%20Forms/Start/MyCompany)
2. Build the Project and Restore Nuget Packages.
3. Try running the application in either iOS, UWP or Android. You can do this by choosing the project platform (Android, iOS, UWP) and click Start/Run Green Button. You should see the following:

<Insert Image here>

## What is included in Start Project
* Scaffold of MVVM Pattern. For simplicity purposes, I did not introduce any MVVM Framework in this sample, hence you will see some code-behind codes of XAML files.
* Tabbed View from App.xaml.cs with 2 Childrens: ProductPage and About Page

## Quick overview of MVVM Files included
### Model
* [Product.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/Models/Product.cs) from Models folder. This is your class that defines a Product object. You will notice that it includes a **JsonProperty** attribute. This is needed so that on runtime, when we call the JsonConvert Serialization in [ProductViewModel.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/ViewModels/ProductViewModel.cs#L47) it knows that the json properties matches to the corresponding C# properties. Example: "name" of json property becomes "Name" of C# property.
 
### ViewModel
* [ViewModelBase.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/ViewModels/ViewModelBase.cs) from ViewModels folder. Since in the future we will define multiple ViewModels, this will serve as a scaffold code so that we don't need to implement a lot if ***INotifyPropertyChanged*** definitions for our bindings. It also includes common ViewModel members such as Title property, IsBusy property for Preloaders, and RefreshCommand to refresh the page. 
 
* [ProductViewModel.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/ViewModels/ProductViewModel.cs) from ViewModels folder. This is your class that defines the Actions and Bindable items from your Product model to your Product Views. The Products property is an ObservableCollection of Product class. An ObservableCollection is the best type of collection for our use case since it is Dynamic (Add, Remove, etc) and has the ability to update the UI through BindingContexts. The GetProducts() method is an asynchronous call to our "Web Service" and gets the Json File using HttpClient. The Json file is then deserialized into a list of Products. The product list is then added to the Products (ObservableCollection). In the constructor, we initialize our objects such as the Title.

### View
* [ProductPage.xaml](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/Views/ProductPage.xaml) from Views folder. So basically this is your Presentation layer for your Products. 

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
	     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	     x:Class="MyCompany.Views.ProductPage" 
	     xmlns:vm="clr-namespace:MyCompany.ViewModels;assembly=MyCompany" 
	     Title="{Binding Title}" 
	     x:Name="ProductPage">
```

If you are new XAML, just think that every <Control /> is a C# Object and the control type is the C# Class. You will notice that after ContentPage is ***xmlns="http://xamarin.com/schemas/2014/forms"***, it is basically the using statement for Xamarin.Forms; hence the ***xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"*** is the Microsoft XAML implementation. Yes, at this point, these are 2 different XAML implementations, so some of your controls from WPF/UWP/Win8.1/etc will not work. Then next is ***xmlns:vm="clr-namespace:MyCompany.ViewModels;assembly=MyCompany"*** which basically means that we are defining the ViewModels namespace in our XAML page as **"vm"**. The Title="{Binding Title}" is a property where we bind the Page Title from the ViewModel, this is the Text you see on top of the page. 

``` 
<ContentPage.ToolbarItems>
	<ToolbarItem Text="Sync" Clicked="OnSyncClicked" />
</ContentPage.ToolbarItems>
```
This is just a simple declaration of adding a ToolbarItem with Sync. This includes a Clicked Event "OnSyncClicked". This will invoke the GetProducts() method in code behind.

```
<ContentPage.BindingContext>
	<vm:ProductViewModel />
</ContentPage.BindingContext>
```
This is defining that the BindingContext of the Page is of type ProductViewModel. 

```
<ListView ItemsSource="{Binding Products}" VerticalOptions="FillAndExpand" HasUnevenRows="true" RefreshCommand="{Binding RefreshCommand}" IsPullToRefreshEnabled="true" IsRefreshing="{Binding IsBusy, Mode=OneWay}" CachingStrategy="RecycleElement" x:Name="ProductsListView">
```
You then declare the ***"Content"*** property of the Page, then with an AbsoluteLayout and StackLayout. Then comes the ListView, where we will define the markup of our List of Products. It starts with defining the ItemSource="{Binding Products}" where we declare that the source of the list comes from the Products of the ViewModel. The RefreshCommand and IsRefreshing are binded to the Base members from the ViewModel. It is also interesting that we can declare a ListView with IsPullRefreshEnabled property to true hence will invoke the RefreshCommand.

``` 
<StackLayout.Padding>
	<OnPlatform x:TypeArguments="Thickness" iOS="10" Android="10" WinPhone="0, 0, 0, 10" />
</StackLayout.Padding>
```
The code <OnPlatform /> allows us to customize the XAML UI controls according to the Platform Type. This is a way to handle platform specific UIs.

```
<Image x:Name="HeroImage" Aspect="AspectFill" Source="{Binding Image}" HeightRequest="200" />
<Label Text="{Binding Name}" LineBreakMode="NoWrap" Style="{DynamicResource ListItemTextStyle}" FontSize="16" />
<Label Text="{Binding Price}" LineBreakMode="NoWrap" Style="{DynamicResource ListItemDetailTextStyle}" FontSize="13" />
```
This is the part of the DataTemplate that will allow us to bind the Product properties to our UI: Image, Name, Price. The binding was possible because of the ViewModel. Xamarin.Forms comes with some premade Style templates such as ListItemTextStyle and ListItemDetailTextStyle.

```
<StackLayout IsVisible="{Binding IsBusy}" Padding="32" AbsoluteLayout.LayoutFlags="PositionProportional" AbsoluteLayout.LayoutBounds="0.5,0.5,-1,-1">
	<ActivityIndicator IsRunning="{Binding IsBusy}" />
</StackLayout>
```
This is where we define our preloader which is of ActivityIndicator control type. The visibility is binded from the IsRunning XAML property with IsBusy ViewModel property.

* [ProductPage.xaml.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/Views/ProductPage.xaml.cs) as the code-behind of ProductPage.xaml. This is a partial class associated with the XAML file.

```
public ProductViewModel ViewModel { get { return (BindingContext as ProductViewModel); } }
```
This is a way to use the same ViewModel from the BindingContext of the XAML file which in this case is a ProductViewModel. This is declared so that, we can use its members within the code-behind. Most MVVM Frameworks removes this dependency and just focus on having a ViewModel and a View with no code-behind codes.

```
void OnSyncClicked(object sender, EventArgs e)
{
	ViewModel?.GetProductsCommand?.Execute(null);
}
```
This is an Event handler responding when the Sync button was clicked or touched. This Executes the GetProductsCommand from the ViewModel. The **"?"** are just null checkers as a new feature of C# 6.

```
protected override void OnAppearing()
{
	base.OnAppearing();
	if (ViewModel.Products.Count == 0)
		ViewModel.GetProductsCommand.Execute(null);
}
```
This basically allows us to execute the GetProductsCommand() during first load of the page.

### [App.xaml](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/MyCompany/App.xaml) and App.xaml.cs

```
<ResourceDictionary>
	<Color x:Key="Primary">#1A6053</Color>
	<Color x:Key="Accent">#96d1ff</Color>
	<Color x:Key="LightTextColor">#999999</Color>
</ResourceDictionary>
```
Inside the Application Resources, we implemented predefined Color templates. If you are familiar with CSS, basically it is similar to defining a value for an element, but this one includes naming the ID in a form of Key.

``` 
var mainPage = new TabbedPage();
var productPage = new NavigationPage(new ProductPage()) { Title = "Products"};
var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

mainPage.Children.Add(productPage);
mainPage.Children.Add(aboutPage);

MainPage = mainPage;
```
When you open the code behind (App.xaml.cs), you will see that the class inherits from the Application class of Xamarin.Forms. This what enables us to create a Cross-Platform app that can be launched from different platforms. As part of Application, you can define your **MainPage** property which serves as the Initial Page. In our case, we created a ***TabbedPage*** that can contain multiple NavigationPages. We then instantiated Product and About pages as NavigationPages and add them to MainPage as childrens.

If you navigate to Droid Project's [MainActivity.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/Droid/MainActivity.cs#L25) and iOS' Project's [AppDelegate.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Start/MyCompany/iOS/AppDelegate.cs#L17), you can see in the LoadApplication() method that you are loading the App class from the Portable Class Library.

**All good now. You're now ready to go from here. Take note that the implementation for AboutPage and AboutViewModel is straightforward and similar to product.**

## So what are you going to work on now?
Since you already know the idea on how things work for Xamarin.Forms with MVVM. You will now then perform the following:
* Try out creating your own Page and ViewModel with Employee Model
* Implement a Detail page from the Product Page
* Implement a Platform Dependent feature using Dependency Service

### Create the Model
```
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
```
First is just create the model for the employee. Don't forget to install Newtonsoft.Json and reference it in a using statement. A kind reminder for me to tell that, whenever you install a new package from nuget, you need to make sure that you install it to all project file - and not just to the portable class library project.

### Create the ViewModel
```
public class EmployeeViewModel : ViewModelBase

	public ObservableCollection<Employee> Employees { get; set; }

	public ICommand GetEmployeesCommand { get; set; }

	public EmployeeViewModel()
	{
		Employees = new ObservableCollection<Employee>();
		Title = "Speakers";
		GetEmployeesCommand = RefreshCommand = new Command(
		async () => await GetSpeakers(),
			() => !IsBusy);
	}

	async Task GetSpeakers()
	{
		if (IsBusy)
		return;

		Exception error = null;
		try
		{
			IsBusy = true;

			using (var client = new HttpClient())
			{
				var json = await client.GetStringAsync("https://demo3864976.mockable.io/employee");

				var items = JsonConvert.DeserializeObject<List<Employee>>(json);

				Employees.Clear();
				foreach (var item in items)
					Employees.Add(item);
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("Error: " + ex);
			error = ex;
		}
		finally
		{
			IsBusy = false;
		}

		if (error != null)
			await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
	}
}
```
Create an ObservableCollection of type Employee as Employees property. Declare the GetEmployeesCommand as ICommand type. You use the ICommand because it it bindable to a XAML Event. In the constructor, you instantiate the members. You will notice that the GetEmployeesCommand is instantiated in a **fancy** way such that it is coupled to the RefreshCommand that calls the GetSpeakers() method whenever the IsBusy property is False, meaning the Service call hasn't finished yet. In the future, you can improve the error handling and displaying of "friendlier" error messages.

### Create the View
```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	x:Class="MyCompany.Views.EmployeePage"
	     xmlns:vm="clr-namespace:MyCompany.ViewModels;assembly=MyCompany"
             Title="{Binding Title}"
             x:Name="EmployeesPage">
  <ContentPage.ToolbarItems>
    <ToolbarItem Text="Sync" Command="{Binding GetEmployeesCommand}" />
  </ContentPage.ToolbarItems>
  <ContentPage.BindingContext>
    <vm:EmployeeViewModel/>
  </ContentPage.BindingContext>
  <ContentPage.Content>
    <AbsoluteLayout HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
      <StackLayout
        AbsoluteLayout.LayoutFlags="All"
        AbsoluteLayout.LayoutBounds="0,0,1,1">
        <ListView ItemsSource="{Binding Employees}"
                  VerticalOptions="FillAndExpand"
                  HasUnevenRows="true"
                  RefreshCommand="{Binding RefreshCommand}"
                  IsPullToRefreshEnabled="true"
                  IsRefreshing="{Binding IsBusy, Mode=OneWay}"
                  CachingStrategy="RecycleElement"
                  x:Name="EmployeesListView">
          <ListView.ItemTemplate>
            <DataTemplate>
              <ImageCell Text="{Binding Name}"
                         Detail="{Binding Title}"
                         ImageSource="{Binding Avatar}"/>
            </DataTemplate>
          </ListView.ItemTemplate>

        </ListView>
      </StackLayout>
      <StackLayout IsVisible="{Binding IsBusy}"
                   Padding="32"
                   AbsoluteLayout.LayoutFlags="PositionProportional"
                   AbsoluteLayout.LayoutBounds="0.5,0.5,-1,-1">
        <ActivityIndicator IsRunning="{Binding IsBusy}"/>
      </StackLayout>
    </AbsoluteLayout>
  </ContentPage.Content>
</ContentPage>
```
Very similar to the one we did with ProductPage, you need to implement the following:
* Right Namespaces
* ToolbarItem
* ViewModel as the type of BindingContext
* Layouts inside the Content
* ListView with ItemsSource binded to Employees of ViewModel
* Other ListView properties
* ItemTemplate & DataTemplate
* Bindings of ViewModel properties to an ImageCell
* Lastly, an ActivityIndicator (Preloader) that is binded to the IsBusy VM property.

### Add the View to MainPage
```
var employeePage = new NavigationPage(new EmployeePage()) { Title = "Employees" };
mainPage.Children.Add(employeePage);
```
In the App.xaml.cs, just make sure to declare a employeePage object with type of NavigationPage. After that, just add the employeePage object as part of the children of MainPage.

### Congratulations, you should now be able to see the following:

<Insert Image Here>


Let's now try and add a Detail Page for our Products.

### Create the DetailPageView.xaml.cs
```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	x:Class="MyCompany.Views.ProductDetailPage"
	xmlns:vm="clr-namespace:MyCompany.ViewModels;assembly=MyCompany"
	xmlns:local="clr-namespace:MyCompany;assembly=MyCompany">

  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition Height="Auto"/>
      <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <StackLayout BackgroundColor="{StaticResource Primary}" VerticalOptions="FillAndExpand" HorizontalOptions="Fill">
      <ContentView Padding="10,40,10,40" VerticalOptions="FillAndExpand">
        <StackLayout Orientation="Vertical" HorizontalOptions="StartAndExpand" VerticalOptions="Center">
          <Label Text="{Binding ProductName}" TextColor="White" FontAttributes="Bold" FontSize="22"/>
          <Label FontSize="16" Text="{Binding ProductPrice}" TextColor="White"/>  
		  <Label FontSize="16" Text="{Binding ProductColor}" TextColor="White"/>  
		  <Label FontSize="16" Text="{Binding ProductSizes}" TextColor="White"/>  
        </StackLayout>
      </ContentView>
    </StackLayout>
    <ScrollView Grid.Row="1">
      <StackLayout Orientation="Vertical" Padding="16,40,16,40" Spacing="10">
     	<Image x:Name="HeroImage" Aspect="AspectFill" Source="{Binding ProductImage}" HeightRequest="200" />	
	<Label FontSize="22">
          <Label.FormattedText>
            <FormattedString>
              <FormattedString.Spans>
                <Span Text="Description" FontAttributes="Bold" FontSize="16" ForegroundColor="{StaticResource LightTextColor}"/>
              </FormattedString.Spans>
            </FormattedString>
          </Label.FormattedText>
        </Label>
        <Label Text="{Binding ProductDescription}" />
      </StackLayout>
    </ScrollView>
  </Grid>
</ContentPage>
```
It is very similar to the ProductPage.xaml except that, this one have more binded properties.

### Create the ProductDetailViewModel.cs
```
public class ProductDetailViewModel : ViewModelBase
{
	Product selectedProduct;

	public Product SelectedProduct
	{
		get { return selectedProduct; }
		set { SetProperty(ref selectedProduct, value); }
	}
	
	public string ProductName { get { return SelectedProduct?.Name; } }

	public string ProductDescription { get { return SelectedProduct?.Description; } }

	public string ProductPrice { get { return SelectedProduct?.Price; } }

	public string ProductImage { get { return SelectedProduct?.Image; } }

	public bool? ProductIsSoldOut { get { return SelectedProduct?.SoldOut; } }

	public string ProductSizes { get { return $"Available Sizes: {SelectedProduct?.Options?.FirstOrDefault()?.Size} "; } }
	public string ProductColor { get { return $"Available Colors: {SelectedProduct?.Options?.FirstOrDefault()?.Color} "; } }

	public bool? ProductIsUnique { get { return SelectedProduct?.Options?.FirstOrDefault()?.OneOfAKind; } }

	public ProductDetailViewModel(Product selectedProduct = null)
	{
		SelectedProduct = selectedProduct;
	}
}
```
So there are quite some things to discuss here.
* Create a Full Property (field + Property) for SelectedProduct. This is basically the product chosen from the listView of products. 
* Declare the bindable properties. You can see that there are some nested calls applied here. Good thing with C#6, we can always check if the Property is not null, so we don't throw a NullReference Exception. 
* In the constructor, initialize the SelectedProduct coming from the argument/parameter of the method once the Object is initialized.

### In the Products ListView of the [ProductPage.xaml](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Finish/MyCompany/MyCompany/Views/ProductPage.xaml) add an OnSelected event.
```
ItemSelected="OnItemSelected"
```
Set the value of ItemSelected event as OnItemSelected event handler method.

### Declare OnItemSelected in [ProductPage.xaml.cs](https://github.com/mjtpena/Xamarin-Training/blob/master/Xamarin%20Forms/Finish/MyCompany/MyCompany/Views/ProductPage.xaml.cs)
```
async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	{
		var item = args.SelectedItem as Product;
		if (item == null)
			return;

		await Navigation.PushAsync(new ProductDetailPage() { BindingContext = new ProductDetailViewModel(item) });

		ProductsListView.SelectedItem = null;
	}
```
Basically in this code snippet you declare the Event Argument's selected item as Product type. You then Navigate to a new ProductDetailPage where the BindingContext is of type ProductDetailViewModel where you pass the selected item as an argument. Manually, you remove the slected item.

### Congratulations you are now done! You should now see the following below:

<Insert Image Here>

