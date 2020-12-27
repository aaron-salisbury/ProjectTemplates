using System;
using System.Linq;
using Win10App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using winui = Microsoft.UI.Xaml.Controls;

namespace Win10App.Views
{
    public sealed partial class ToolsPage : Page
    {
        private ToolsViewModel ViewModel
        {
            get => ViewModelLocator.Current.ToolsViewModel;
        }

        public ToolsPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        // https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview

        private void SampleToolsNavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach ((string Content, Type Page) navItem in ViewModel.ToolPages)
            {
                SampleToolsNavView.MenuItems.Add(new winui.NavigationViewItem
                {
                    Content = navItem.Content,
                    Tag = navItem.Content
                });
            }

            ContentFrame.Navigated += On_Navigated;

            winui.NavigationViewItem startingViewItem = (winui.NavigationViewItem)SampleToolsNavView.MenuItems[0];
            SampleToolsNavView.SelectedItem = startingViewItem;
            SampleToolsNavView_Navigate(startingViewItem.Tag.ToString(), new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }

        private void SampleToolsNavView_ItemInvoked(winui.NavigationView sender, winui.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                SampleToolsNavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void SampleToolsNavView_Navigate(string navItemTag, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            var item = ViewModelLocator.Current.ToolsViewModel.ToolPages.FirstOrDefault(p => p.Content.Equals(navItemTag));

            ContentFrame.Navigate(item.Page, null, transitionInfo);
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.SourcePageType != null)
            {
                var item = ViewModel.ToolPages.FirstOrDefault(p => p.Page == e.SourcePageType);

                SampleToolsNavView.SelectedItem = SampleToolsNavView.MenuItems
                    .OfType<winui.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Content));

                // Tab pages can set their own headers or you can uniformly set them all using this.
                //SampleToolsNavView.Header = ((winui.NavigationViewItem)SampleToolsNavView.SelectedItem)?.Content?.ToString();
            }
        }
    }
}
