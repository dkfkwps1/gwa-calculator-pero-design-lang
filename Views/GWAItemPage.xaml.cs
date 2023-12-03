using BASILIOMidExamGWAApp.Data;
using BASILIOMidExamGWAApp.Models;

namespace BASILIOMidExamGWAApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GWAItemPage : ContentPage
{
	public GWAItemPage()
	{
		InitializeComponent();
	}

	async void OnSaveClicked(object sender, EventArgs e)
	{
		var gwaItem = (GWAItem)BindingContext;
		GWAItemDatabase database = await GWAItemDatabase.Instance;
		await database.SaveItemAsync(gwaItem);
		await Navigation.PopAsync();
	}

	async void OnDeleteClicked(object sender, EventArgs e)
	{
		var gwaItem = (GWAItem)BindingContext;
		GWAItemDatabase database = await GWAItemDatabase.Instance;
		await database.DeleteItemAsync(gwaItem);
		await Navigation.PopAsync();
	}

	async void OnCancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}