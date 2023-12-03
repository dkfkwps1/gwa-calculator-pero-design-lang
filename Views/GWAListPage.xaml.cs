using BASILIOMidExamGWAApp.Data;
using BASILIOMidExamGWAApp.Models;

namespace BASILIOMidExamGWAApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GWAListPage : ContentPage
{
	public GWAListPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		GWAItemDatabase database = await GWAItemDatabase.Instance;
		listView.ItemsSource = await database.GetItemsAsync();
	}

	async void OnItemAdded(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new GWAItemPage
		{
			BindingContext = new GWAItem()
		});
	}

	async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			await Navigation.PushAsync(new GWAItemPage
			{
				BindingContext = e.SelectedItem as GWAItem
			});
		}
	}

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        var gwaItem = (GWAItem)BindingContext;
        GWAItemDatabase database = await GWAItemDatabase.Instance;
        await database.DeleteItemAsync(gwaItem);
        await Navigation.PopAsync();
    }
    //async void OnDeleteClicked(object sender, EventArgs e)
    //{
    //	var menuItem = sender as MenuItem;
    //	var gwaItem = menuItem?.BindingContext as GWAItem;

    //	if (gwaItem != null)
    //	{
    //		GWAItemDatabase database = await GWAItemDatabase.Instance;
    //		await database.DeleteItemAsync(gwaItem);
    //		listView.ItemsSource = await database.GetItemsAsync();
    //	}
    //}
    public double CalculateGWA()
	{
		double totalWeightedScore = 0;
		double totalUnits = 0;

		foreach (var item in listView.ItemsSource as IEnumerable<GWAItem>)
		{
			totalWeightedScore += item.Grade * item.Units;
			totalUnits += item.Units;
		}

		if (totalUnits == 0)
		{
			return 0; // Avoid division by zero
		}

		return totalWeightedScore / totalUnits;
	}

	public void UpdateGwaLabel()
	{
		double calculatedGWA = CalculateGWA();
		GWALabel.Text = $"GWA: {calculatedGWA:F2}";
	}
}