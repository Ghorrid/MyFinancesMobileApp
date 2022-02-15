using MyFinancesMobileApp.Models;
using MyFinancesMobileApp.Models.Dtos;
using MyFinancesMobileApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyFinancesMobileApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
       
        public ObservableCollection<OperationDto> Operations { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<OperationDto> ItemTapped { get; }

        public Command DeleteItemCommand { get; }

        public ItemsViewModel()
        {
            Title = "Operacje";
            Operations = new ObservableCollection<OperationDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<OperationDto>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DeleteItemCommand = new Command <OperationDto>(async (x) => await OnDeleteItem(x));

        }

        private  async Task OnDeleteItem(OperationDto operation)
        {
            if (operation == null)            
                return;

            var dialog = await Shell.Current.DisplayAlert("Usuwanie", $"Czy chcesz usunąc operację: {operation.Name} ?",
                "Tak", "Nie");
            if (!dialog)
                return;

            var response = await OperationService.DeleteItemAsync(operation.Id);
            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            await ExecuteLoadItemsCommand();

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var response = await OperationService.GetAsync();

                if (!response.IsSuccess)
                    await ShowErrorAlert(response);

                Operations.Clear();

                foreach (var operation in response.Data)
                {
                    Operations.Add(operation);
                }  
       
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Wystapił błąd",
                     ex.Message
                     , "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            
        }

 

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(OperationDto operation)
        {
            if (operation == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={operation.Id}");
        }
    }
}