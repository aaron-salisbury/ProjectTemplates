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
        public ToolsViewModel ViewModel { get; } = new ToolsViewModel();

        public ToolsPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        #region Navigation
        // https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            BuildMenuItems();

            ContentFrame.Navigated += On_Navigated;

            if (NavView.MenuItems.Count > 0)
            {
                winui.NavigationViewItem startingViewItem = (winui.NavigationViewItem)NavView.MenuItems[0];
                NavView.SelectedItem = startingViewItem;
                NavView_Navigate(startingViewItem.Tag.ToString(), new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
            }
        }

        private void NavView_ItemInvoked(winui.NavigationView sender, winui.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            (string Content, Type Page) item = GetPageData(null, navItemTag);

            ContentFrame.Navigate(item.Page, null, transitionInfo);
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.SourcePageType != null)
            {
                (string Content, Type Page) item = GetPageData(e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<winui.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Content));

                // Tab pages can set their own headers or you can uniformly set them all using this.
                //NavView.Header = ((winui.NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        private void BuildMenuItems()
        {
            foreach ((string Content, Type Page) navItem in ViewModel.ToolPages)
            {
                NavView.MenuItems.Add(new winui.NavigationViewItem
                {
                    Content = navItem.Content,
                    Tag = navItem.Content
                });
            }
        }

        private (string Content, Type Page) GetPageData(Type sourcePageType = null, string navItemTag = null)
        {
            if (sourcePageType != null)
            {
                return ViewModel.ToolPages.FirstOrDefault(p => p.Page == sourcePageType);
            }
            else if (!string.IsNullOrEmpty(navItemTag))
            {
                return ViewModel.ToolPages.FirstOrDefault(p => p.Content.Equals(navItemTag));
            }

            return (null, null);
        }
        #endregion
    }
}
